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

            
        }

        public static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");

        }

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




        public static void NewDatabase(ProductShopContext context)
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
    }
}