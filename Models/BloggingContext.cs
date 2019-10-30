using System.Data.Entity;

namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        //Create a derived context to create a session with the database
        public BloggingContext() : base("name=BlogContext") { }

        //DbSet creates a relationshhip between our classes and the database:
        //  Blogs table is a list (a DbSet) of type Blog
        //  Posts table is a list (a DbSet) of type Post
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        //This method adds a blog to the list of blogs
        public void AddBlog(Blog blog)
        {
            // "this" referes to a property of this class. Once we create an instance of BloggingContext, 
            // "this" will refer to its own blogs property that's part of this class 
            // instead of a local variable called blogs. Use of "this" is optional, but it is good practice
            // to use, because it lets us know that the property belongs to this object
            this.Blogs.Add(blog);  //add a blog DbSet
            this.SaveChanges();  //updates the database based on DbSet
        }
    }
}

