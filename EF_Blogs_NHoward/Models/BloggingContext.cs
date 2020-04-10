using System;
using System.Data.Entity;
using System.Linq;

namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext") { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();
        }
        public void AddPost(Post post)
        {
            this.Posts.Add(post);
            this.SaveChanges();
        }
        public void ListBlogs()
        {
            var query = Blogs.OrderBy(b => b.Name);

            Console.WriteLine("All blogs in the database:");
            Console.WriteLine("{0,-10}{1,-20}", "Blog Id", "Blog Name");
            Console.WriteLine("-------------------------------------");
            foreach (var item in query)
            {
                Console.WriteLine("{0,-10}{1,-20}", item.BlogId, item.Name);
                Console.WriteLine();

            }
        }
        public void MakeNewPost()
        {
            string blogID,title, postData;
            Console.WriteLine("Which blog would you like to post to?");
            ListBlogs();
            blogID = Console.ReadLine();
            Post post = new Post();
            Console.WriteLine("Title of Post:");
            title = Console.ReadLine();
            Console.WriteLine("Post Content:");
            postData = Console.ReadLine();
            post.Title = title;
            post.Content = postData;
            post.BlogId = Int32.Parse(blogID);
            AddPost(post);
        }
    }
}
