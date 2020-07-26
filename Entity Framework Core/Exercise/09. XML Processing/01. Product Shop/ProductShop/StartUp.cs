using System;
using System.IO;
using System.Linq;
using ProductShop.Data;

using ProductShop.Models;
using ProductShop.XMLHelper;
using ProductShop.Dtos.Import;
using System.Collections.Generic;
using ProductShop.Dtos.Export;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new ProductShopContext();
            //ResetDatabase(db);

            //Task 01. Import Users
            //var inputXml = File.ReadAllText(@"..\..\..\Datasets\users.xml");
            //Console.WriteLine(ImportUsers(db,inputXml));

            //Task 02. Import Products
            //var inputXml = File.ReadAllText(@"..\..\..\Datasets\products.xml");
            //Console.WriteLine(ImportProducts(db, inputXml));

            //Task 03. Import Categories
            //var inputXml = File.ReadAllText(@"..\..\..\Datasets\categories.xml");
            //Console.WriteLine(ImportCategories(db, inputXml));

            //Task 04. Import Categories and Products
            //var inputXml = File.ReadAllText(@"..\..\..\Datasets\categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(db, inputXml));

            //Task 05. Products In Range
            //var result = GetProductsInRange(db);
            //File.WriteAllText(@"..\..\..\Datasets\Results\productsInRange.xml", result);

            //Task 06. Sold Products
            //var result = GetSoldProducts(db);
            //File.WriteAllText
            //    (@"..\..\..\Datasets\Results\soldProducts.xml", result);

            //TODO
        }

        public static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");

        }

        // Task 01.
        public static string ImportUsers
            (ProductShopContext context, string inputXml)
        {
            const string rootElement = "Users";

            var usersResult = XMLConverter.Deserializer<ImportUserDto>(inputXml, rootElement);

            var users = usersResult
                .Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //Task 02.
        public static string ImportProducts
            (ProductShopContext context, string inputXml)
        {
            const string rootElement = "Products";

            var productsDtos = XMLConverter
                .Deserializer<ImportProductDto>(inputXml, rootElement);

            var products = productsDtos
                    .Select(pr => new Product 
                    {
                        Name = pr.Name,
                        Price = pr.Price,
                        SellerId = pr.SellerId,
                        BuyerId = pr.BuyerId
                    })
                    .ToList();

            context.Products.AddRange(products);
            context.SaveChanges();


            return $"Successfully imported {products.Count}";
        }

        //Task 03.
        public static string ImportCategories
            (ProductShopContext context, string inputXml)
        {
            const string rootAtribute = "Categories";

            var categoryDtos = XMLConverter.Deserializer<ImportCategoryDto>(inputXml, rootAtribute);

            var category = categoryDtos
                    .Where(c => c.Name != null)
                    .Select(pr => new Category
                    {
                        Name = pr.Name
                    })
                    .ToList();

            context.Categories.AddRange(category);
            context.SaveChanges();


            return $"Successfully imported {category.Count}";
        }

        //Task 04.
        public static string ImportCategoryProducts
            (ProductShopContext context, string inputXml)
        {
            const string rootElement = "CategoryProducts";

            var categoryProductDtos = XMLConverter
                .Deserializer<ImportCategoryProductDto>(inputXml, rootElement);

            var categories = categoryProductDtos
                .Where(x => context.Categories.Any(c => c.Id == x.CategoryId)
                            && context.Products.Any(p => p.Id == x.ProductId))
                .Select(c => new CategoryProduct
                {
                    CategoryId = c.CategoryId,
                    ProductId = c.ProductId
                })
                .ToList();

            context.CategoryProducts.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //Task 05.
        public static string GetProductsInRange
            (ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(pr => new ExportProductDto
                {
                    Name = pr.Name,
                    Price = pr.Price,
                    Buyer = pr.Buyer.FirstName + " " + pr.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();

            var result = XMLConverter.Serialize(products, "Products");

            return result;
        }

        //Task 06.
        public static string GetSoldProducts
            (ProductShopContext context)
        {
            var usersWithProducts = context
                .Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(us => new ExportSoldProductsDto
                {
                    FirstName = us.FirstName,
                    LastName = us.LastName,
                    SoldProducts = us.ProductsSold
                            .Select(ps => new UserProductDto
                    {
                        Name = ps.Name, 
                        Price = ps.Price
                    })
                    .ToArray()
                })
                .OrderBy(us => us.LastName)
                .ThenBy(us => us.FirstName)
                .Take(5)
                .ToArray();

            var result = XMLConverter.Serialize
                    (usersWithProducts, "Users");

            return result;
        }

        //Task 07.
        public static string GetCategoriesByProductsCount
            (ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(c => new ExportCategoriesByProductDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count()
                })
                .ToList();

            //TODO

            return null;
        }
    }
}