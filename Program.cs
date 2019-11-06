//Present the user with 3 options: 
// 1.Display all blogs 
// 2.Add Blog 
// 3.Create Post 
// 4.Display Posts 
// Options 1 and 2 are self explanatory
// For option 3, prompt the user to select the Blog  
//   Once the Blog is selected, the Post details can be entered 
//   and Posts should be saved to the Posts table
// For option 4, give the user to display all posts 
// or posts from a specific blog
//
// All user errors must be handled

using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogsConsole;


namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //public static object db { get; private set; }
       
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                var MenuChoice = "x";
                while (MenuChoice.ToUpper() != "Q")
                {
                    Console.WriteLine("1) Display all blogs");
                    Console.WriteLine("2) Add Blog");
                    Console.WriteLine("3) Create Post");
                    Console.WriteLine("4) Display posts");
                    Console.WriteLine("***Press Q to quit***");
                    Console.WriteLine();
                    Console.Write("Please enter your choice (1, 2, 3, 4): ");
                    
                    MenuChoice = Console.ReadLine();

                    var db = new BloggingContext();

                    if (MenuChoice == "1")
                    {
                        // Display all Blogs from the database

                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine();
                        Console.WriteLine("Here's a list of All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }

                        Console.WriteLine();
                    }

                    else if (MenuChoice == "2")
                    {
                        // Create and save a new Blog
                        Console.WriteLine();
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                        Console.WriteLine();
                    }

                    else if (MenuChoice == "3")
                    {
                        // Create a blog post
                        Console.WriteLine();
                        Console.WriteLine("Enter the blog you want to create a post for: ");
                        var blogName = Console.ReadLine();

                        var blog = db.Blogs.FirstOrDefault(b => b.Name.ToUpper() == blogName.ToUpper());
                        Console.Write("Enter post title: ");
                        var postTitle = Console.ReadLine();

                        Console.Write("Enter post content: ");
                        var postContent = Console.ReadLine();

                        Post newPost = new Post { Title = postTitle, Content = postContent, Blog = blog };
                        db.AddPost(newPost);
                        Console.WriteLine();
                    }

                    else if (MenuChoice == "4")
                    {
                        //Display Posts in the database

                        //query is  list of blogs sorted by Name
                        var query = db.Blogs.OrderBy(b => b.BlogId);                      

                        Console.WriteLine();
                        Console.WriteLine("Select the blog's posts to display:");
                        Console.WriteLine("0) Posts from all blogs");

                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.BlogId}) Posts from {item.Name}");
                        }

                        var postMenu = Console.ReadLine();

                        if (postMenu == "0")
                        {
                            // blogQuery is a list of blogs sorted by BlogId. It also enables us to retrieve the posts
                            // for each blog (because we are using Eager Loading by using the "include" statement)
                            // this saves the results of the query in memory
                            var blogQuery = db.Blogs.Include("Posts").OrderBy(b => b.BlogId);

                            Console.WriteLine();
                            Console.WriteLine("Here's a list of All posts in the database:\n");
                            foreach (var blog in blogQuery)
                            {
                                Console.WriteLine($"Blog: {blog.Name}");
                                foreach (var post in blog.Posts)
                                {
                                    Console.WriteLine($"  Post Title: {post.Title} \n  Post Content: {post.Content}");
                                }
                                Console.WriteLine();
                            }

                            Console.WriteLine();
                        }

                        else
                        {
                            int postID = Convert.ToInt32(postMenu);
                            var postQuery = db.Posts.Where(p => p.BlogId == postID);
                            foreach (var post in postQuery)
                            {
                                Console.WriteLine($"Post Title: {post.Title} \nPost Content: {post.Content}");
                                Console.WriteLine();
                            }
                        }

                        Console.WriteLine();

                    }

                    else if (MenuChoice.ToUpper() == "Q")
                    {
                        logger.Info("Program Ended");
                        Environment.Exit(0);  //Exit the application
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid menu option");
                    }

                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }
    }
}
