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

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {

                // Create and save a new Blog
                //Console.Write("Enter a name for a new Blog: ");
                //var name = Console.ReadLine();

                //var blog = new Blog { Name = name };

                var db = new BloggingContext();
                //db.AddBlog(blog);
                //logger.Info("Blog added - {name}", name);

                // Display all Blogs from the database
                var query = db.Blogs.OrderBy(b => b.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                // Create a blog post ****
                Console.WriteLine("Enter the blog you want to create a post for: ");
                var blogName = Console.ReadLine();

                var blog = db.Blogs.FirstOrDefault(b => b.Name == blogName);
                //Console.WriteLine(blog.BlogId);
                Console.Write("Enter post title: ");
                var postTitle = Console.ReadLine();

                Console.Write("Enter post content: ");
                var postContent = Console.ReadLine();

                Post newPost = new Post { Title = postTitle, Content = postContent, Blog = blog };
                db.AddPost(newPost);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }
    }
}
