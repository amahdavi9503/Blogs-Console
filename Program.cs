//Present the user with 3 options: 
// 1.Display all blogs 
// 2.Add Blog 
// 3.Create Post 
// Options 1 and 2 are self explanatory
// If option 3 is selected, prompt the user to select the Blog they are posting to 
// Once the Blog is selected, the Post details can be entered 
// Posts should be saved to the Posts table 
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

        public static object db { get; private set; }
       
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                Console.WriteLine("1) Display all blogs");
                Console.WriteLine("2) Add Blog");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("4) EXIT");
                Console.Write("Please enter your choice (1, 2, 3, 4): ");
                var MenuChoice = Console.ReadLine();

                if (MenuChoice == "1")
                {
                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("Here's a list of All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                }

                 else if (MenuChoice == "2")
                {
                    // Create and save a new Blog
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();

                    var blog = new Blog { Name = name };

                    var db = new BloggingContext();
                    db.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);
                }

                else if (MenuChoice == "3")
                {
                    // Create a blog post
                    Console.WriteLine("Enter the blog you want to create a post for: ");
                    var blogName = Console.ReadLine();

                    var blog = db.Blogs.FirstOrDefault(b => b.Name == blogName);
                    Console.Write("Enter post title: ");
                    var postTitle = Console.ReadLine();

                    Console.Write("Enter post content: ");
                    var postContent = Console.ReadLine();

                    Post newPost = new Post { Title = postTitle, Content = postContent, Blog = blog };
                    db.AddPost(newPost);
                }

                else if (MenuChoice == "4")
                {
                    logger.Info("Program Ended");
                    Environment.Exit(0);  //Exit the application
                }

                else
                {
                    Console.WriteLine("Please enter a valid menu option");
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
