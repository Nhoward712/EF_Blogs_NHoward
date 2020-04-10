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
            string keyboard;
            logger.Info("Program started");
            Console.WriteLine("Welcome to the Blogger 9000");

            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("(1) Create a Blog");
                Console.WriteLine("(2) List All Blogs");
                Console.WriteLine("(3) Add a Post");
                Console.WriteLine("(9) Exit ");
                keyboard = Console.ReadLine();
                var db = new BloggingContext();
                switch (keyboard)
                {
                    case "1":
                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();
                        var idNum = (db.Blogs.Count()) + 100; 
                        var blog = new Blog { BlogId = idNum,
                                              Name = name };

                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                        break;
                    case "2":
                        db.ListBlogs();
                        break;
                    case "3":
                        db.MakeNewPost();
                        break;
                }


            } while (keyboard != "9");

        }
    }
}
