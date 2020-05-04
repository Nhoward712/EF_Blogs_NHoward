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
            var db = new BloggingContext();
            var blogs = db.Blogs;
            string title, postData;

            Console.WriteLine("Which blog would you like to post to?");
            ListBlogs();
            int choice = Int32.Parse(Console.ReadLine());
            var blog = blogs.FirstOrDefault(b => b.BlogId == choice);

            Post post = new Post();
            Console.WriteLine("Title of Post:");
            title = Console.ReadLine();
            Console.WriteLine("Post Content:");
            postData = Console.ReadLine();

            post.Title = title;
            post.Content = postData;
            post.BlogId = blog.BlogId;
            AddPost(post);
            Console.WriteLine("Here are the posts:");
            foreach(var item in db.Posts) 
            {
                Console.WriteLine("{0,-20}{1,-20}", item.Title, item.Content);
                Console.WriteLine();
            }
        }

        public void ListPosts()
        {
            var db = new BloggingContext();
            var blogs = db.Blogs;
            Console.WriteLine("Which blog would you like to list posts for?");
            ListBlogs();
            int choice = Int32.Parse(Console.ReadLine());
            var blog = blogs.FirstOrDefault(b => b.BlogId == choice);
            //lists posts
            foreach (var item in db.Posts)
            {
                Console.WriteLine("{0,-20}{1,-20}", item.Title, item.Content);
                Console.WriteLine();
            }

        }
        public void EditPosts()
        {
            var db = new BloggingContext();
            var blogs = db.Blogs;

            Console.WriteLine("Which post would you like to edit? <Title>");
            foreach (var item in db.Posts)
            {
                Console.WriteLine("0,-20}{1,-20}", item.Title, item.Content);
                Console.WriteLine();
            }
            var choice = Console.ReadLine();
            var post = db.Posts.FirstOrDefault(b => b.Title == choice);
            Console.WriteLine("New Post Title: ");
            post.Title = Console.ReadLine();
            Console.WriteLine("New Post Content: ");
            post.Content = Console.ReadLine();
            db.SaveChanges();
        }
        public void DeletePost()
        {
            var db = new BloggingContext();
            var blogs = db.Blogs;

            Console.WriteLine("Which post would you like to edit? <Title>");
            foreach (var item in db.Posts)
            {
                Console.WriteLine("0,-20}{1,-20}", item.Title, item.Content);
                Console.WriteLine();
            }
            var choice = Console.ReadLine();
            var post = db.Posts.FirstOrDefault(b => b.Title == choice);
            db.Posts.Remove(post);
            db.SaveChanges();
            Console.WriteLine("Your Post Has Been Deleted");
        }

    }
}
