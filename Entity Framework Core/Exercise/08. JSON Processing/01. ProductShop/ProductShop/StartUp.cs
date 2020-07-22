using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        private static string ResultDirectoryPath = @"../../../Datasets/Results";
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            //Task 01. Import Users
            //var dataJson = File.ReadAllText(@"..\..\..\Datasets\users.json");
            //Console.WriteLine(ImportUsers(context, dataJson));

            //Task 02. Import Products
            //var dataJson = File.ReadAllText(@"..\..\..\Datasets\products.json");
            //Console.WriteLine(ImportProducts(context, dataJson));

            //Task 03. Import Categories
            //var dataJson = File.ReadAllText(@"..\..\..\Datasets\categories.json");
            //Console.WriteLine(ImportCategories(context, dataJson)); ;

            //Task 04. Import CategoryProducts
            //var dataJson = File.ReadAllText(@"..\..\..\Datasets\categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(context, dataJson));

            //Task 05 
            //EnsureDirectoryExist(ResultDirectoryPath);
            //File.WriteAllText(ResultDirectoryPath + "/products-in-range.json", GetProductsInRange(context));

            //Task 06
            //EnsureDirectoryExist(ResultDirectoryPath);
            //File.WriteAllText(ResultDirectoryPath + "/users-sold-products.json", GetSoldProducts(context));

            //Task 07
            //EnsureDirectoryExist(ResultDirectoryPath);
            //File.WriteAllText(ResultDirectoryPath +
            //    "/categories-by-products.json",
            //    GetCategoriesByProductsCount(context));

            //Task 08
            //EnsureDirectoryExist(ResultDirectoryPath);
            //File.WriteAllText(ResultDirectoryPath +
            //    "/users-and-products.json",
            //    GetUsersWithProducts(context));
        }

        public static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");

        }

        //Desirialization
        public static string ImportUsers
            (ProductShopContext context,string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);
            
            context.Users.AddRange(users);

            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        public static string ImportProducts
            (ProductShopContext context, string InputJson)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(InputJson);

            context.AddRange(products);

            context.SaveChanges();

            return $"Successfully imported {products.Count()}";           
        }

        public static string ImportCategories
            (ProductShopContext context, string InputJson)
        {
            List<Category> categories = JsonConvert
                        .DeserializeObject<List<Category>>(InputJson);

            categories.RemoveAll(x => x.Name == null);

            context.AddRange(categories);

            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts
            (ProductShopContext context, string InputJson)
        {
            List<CategoryProduct> categoryProducts =
                JsonConvert.DeserializeObject<List<CategoryProduct>>(InputJson);

            context.CategoryProducts.AddRange(categoryProducts);

            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }


        //Serialization
        private static void EnsureDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                    .Products
                    .Where(p => (p.Price >= 500) && (p.Price <= 1000))
                    .OrderBy(p => p.Price)
                    .Select(info => new
                    {
                        name = info.Name,
                        price = info.Price,
                        seller = $"{info.Seller.FirstName} {info.Seller.LastName}"
                    })
                    .ToList();

            string json = JsonConvert.SerializeObject
                (products, Formatting.Indented);

            return json;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context
                    .Users
                    .Where(u => u.ProductsSold.Count >= 1)
                    .OrderBy(u => u.FirstName)
                    .Select(user => new
                    {
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        soldProducts = user.ProductsSold.Select(sp => new
                        {
                            name = sp.Name,
                            price = sp.Price,
                            buyerFirstName = sp.Buyer.FirstName,
                            buyerLastName = sp.Buyer.LastName
                        })
                    })
                    .ToList();

            string json = JsonConvert.SerializeObject
                (soldProducts, Formatting.Indented);

            return json;
        }

        public static string GetCategoriesByProductsCount
            (ProductShopContext context)
        {
            var categories = context
                    .Categories
                    .Select(c => new
                    {
                        category = c.Name,
                        productsCount = c.CategoryProducts.Count(),
                        averagePrice = c.CategoryProducts
                            .Average(cp => cp.Product.Price).ToString("f2"),
                        totalRevenue = c.CategoryProducts
                            .Sum(cp => cp.Product.Price).ToString("f2"),
                        
                    })
                    .OrderByDescending(c => c.productsCount)
                    .ToList();

            string json = JsonConvert.SerializeObject(categories,
                Formatting.Indented);

            return json;
        }

        public static string GetUsersWithProducts
            (ProductShopContext context)
        {
            var curentUsers = context.Users
                             .AsEnumerable()
                             .Where(p => p.ProductsSold.Any(b => b.Buyer != null))
                             .OrderByDescending(p => p.ProductsSold.Count(c => c.Buyer != null))
                             .Select(c => new
                             {
                                 lastName = c.LastName,
                                 age = c.Age,
                                 soldProducts = new
                                 {
                                     count = c.ProductsSold.Count(b => b.Buyer != null),
                                     products = c.ProductsSold
                                                 .Where(x => x.Buyer != null)
                                                 .Select(y => new
                                                 {
                                                     name = y.Name,
                                                     price = y.Price
                                                 })
                                                 .ToList()
                                 }
                             })
                             .ToList();

            var result = new
            {
                usersCount = curentUsers.Count,
                users = curentUsers
            };

            string json = JsonConvert.SerializeObject(result,
                Formatting.Indented);

            return json;
        }

        /*public static void NewDatabase(ProductShopContext context)
        {
            var usersJson = File.ReadAllText
                (@"..\..\..\Datasets\users.json");

            var productsJson = File.ReadAllText
                (@"..\..\..\Datasets\products.json");

            var categoriesJson = File.ReadAllText
                (@"..\..\..\Datasets\categories.json");

            var categoriesProductsJson = File.ReadAllText
                (@"..\..\..\Datasets\categories-products.json");

            ResetDatabase(context);

            ImportUsers(context, usersJson);
            ImportProducts(context, productsJson);
            ImportCategories(context, categoriesJson);
            string result = ImportCategoryProducts
                (context, categoriesProductsJson);

            Console.WriteLine(result);
        }
        */
    }
}
