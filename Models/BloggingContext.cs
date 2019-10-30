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
            this.Blogs.Add(blog);  //add a blog DbSet
            this.SaveChanges();  //updates the database based on DbSet
        }
    }
}

