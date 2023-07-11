using DbFirstApproachEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DbFirstApproachEF
{
    internal class Program 
    {
        pubsContext context = new pubsContext();

        void PrintTitlesByPublishers()
        {
            Console.WriteLine("Enter the publisher Name :");
            String name=Console.ReadLine();
            var publishers =context.Publishers.Where(p => p.PubName ==name).Include(p => p.Titles);
            foreach (var publisher in publishers)
            {
                Console.WriteLine("_________________");
                Console.WriteLine(publisher.PubName);
                Console.WriteLine("_________________");
                foreach (var title in publisher.Titles)
                {
                    Console.WriteLine("\t" + title.Title1);
                }
            }
        }

        void PrintAuthorDetailsByAuID()
        {
            Console.WriteLine("Enter the Author ID :");
            String id = Console.ReadLine();
            var author = context.Authors.Where(a => a.AuId == id).Include(b => b.Titleauthors).ThenInclude(c=>c.Title);

            //(from p in context.Authors where p.AuId == id select p)
            foreach (var item in author)
            {
                Console.WriteLine("_________________");
                Console.WriteLine("Name : " + item.AuFname + " " + item.AuLname);
                Console.WriteLine("ID : " + item.AuId);
                Console.WriteLine("Phone : " + item.Phone);
                Console.WriteLine("Address : " + item.Address);
                Console.WriteLine(" City : " + item.City);
                Console.WriteLine("State : " + item.State);
                Console.WriteLine("Zip : " + item.Zip);
                Console.WriteLine("Contract : " + item.Contract);
                Console.WriteLine("_________________");
                Console.WriteLine("***Books***");
                foreach (var titleauthor in item.Titleauthors)
                {
                    Console.WriteLine(titleauthor.Title.Title1);
                }
            }
        }

        void PrintSaleDetailsAndSalePrice()
        {
            Console.WriteLine("Enter the Title Name :");
            String name = Console.ReadLine();
            int sum = 0;
            double price = 0;
            var sale = context.Titles.Where(t => t.Title1 == name).Include(b => b.Sales);
            foreach (var sales in sale)
            {
                foreach (var item in sales.Sales)
                {
                    Console.WriteLine("_________________");
                    Console.WriteLine("Store ID : " + item.StorId);
                    Console.WriteLine("Store Name: " + item.OrdNum);
                    Console.WriteLine("Ord_Date : " + item.OrdDate);
                    Console.WriteLine("Qty : " + item.Qty);
                    Console.WriteLine("Payterms : " + item.Payterms);
                    Console.WriteLine("Title ID : " + item.TitleId);
                    sum += item.Qty;
                }
                price = (double)sales.Price;            }
            Console.WriteLine();
            Console.WriteLine("Total sale price : " + Math.Round(price * sum));
        }

        //void GetPublishers()
        //{
        //    //var publishers = context.Publishers.Where(p => p.Country == "USA");
        //    var publishers = (from p in context.Publishers where p.Country == "USA" select p).ToList();
        //    foreach (var publisher in publishers)
        //    {
        //        Console.WriteLine(publisher.PubName);
        //    }
        //}

        //void CountPublishers()
        //{
        //    var publishersCount = context.Publishers.Where(p => p.Country == "USA").Count();
        //    Console.WriteLine(publishersCount);
        //}

        //void OrdereData()
        //{
        //    var publishersOrder = context.Publishers.OrderByDescending(p => p.Country);
        //    foreach (var publisher in publishersOrder)
        //    {
        //        Console.WriteLine(publisher.PubName+" "+publisher.Country);
        //    }

        //}

        //void GroupData()
        //{
        //    var publisherGroup = context.Publishers.ToList();
        //    var pubsGroup =publisherGroup.GroupBy(p => p.Country);
        //    foreach (var publisher in pubsGroup)
        //    {
        //        Console.WriteLine(publisher.Key);
        //        foreach (var item in publisher)
        //        {
        //            Console.WriteLine("\t" + item.PubName + " " + item.Country);
        //        }
        //    }
        //}
        //void JoinExample()
        //{
        //    List<Product> products = new List<Product>
        //    {
        //    new Product { Name = "Cola", CategoryId = 0 },
        //    new Product { Name = "Tea", CategoryId = 0 },
        //    new Product { Name = "Apple", CategoryId = 1 },
        //    new Product { Name = "Kiwi", CategoryId = 1 },
        //    new Product { Name = "Carrot", CategoryId = 2 },
        //    };

        //    List<Category> categories = new List<Category>
        //    {
        //    new Category { Id = 0, CategoryName = "Beverage" },
        //    new Category { Id = 1, CategoryName = "Fruit" },
        //    new Category { Id = 2, CategoryName = "Vegetable" }
        //    };
        //    //var query = from product in products
        //    //            join category in categories on product.CategoryId equals category.Id
        //    //            select new { ProductName = product.Name, category.CategoryName };
        //    var query = products.Join(categories,
        //        (p) => p.CategoryId,
        //        (c) => c.Id,
        //        (p, c) => new { ProductName = p.Name, CategoryName = c.CategoryName });

        //    foreach (var item in query)
        //    {
        //        Console.WriteLine($"{item.ProductName} - {item.CategoryName}");
        //    }
        //}
        //void UnderstandingInclude()
        //{
        //    var publishers = context.Publishers.Include(p => p.Titles);
        //    foreach (var publisher in publishers)
        //    {
        //        Console.WriteLine(publisher.PubName);
        //        foreach (var title in publisher.Titles)
        //        {
        //            Console.WriteLine("\t" + title.Title1);
        //        }
        //    }
        //}

        void CheckAuthor()
        {
            Console.WriteLine("Please enter author first name");
            string fname = Console.ReadLine();
            Console.WriteLine("Please enter author last name");
            string lname = Console.ReadLine();
            Author author = context.Authors.SingleOrDefault(a => a.AuFname == fname && a.AuLname == lname);
            if (author == null)
                Console.WriteLine("Invalid username or password");
            else
                Console.WriteLine("Welcome " + author.AuFname + " " + author.AuLname);
        }




        static void Main(string[] args)
        {
            Program program = new Program();
            program.CheckAuthor();

            //program.PrintTitlesByPublishers();
            //Console.WriteLine("---------------------------------------");
            //program.PrintAuthorDetailsByAuID();
            //Console.WriteLine("---------------------------------------");
            //program.PrintSaleDetailsAndSalePrice();
            //Console.WriteLine("---------------------------------------");

        }
    }
}