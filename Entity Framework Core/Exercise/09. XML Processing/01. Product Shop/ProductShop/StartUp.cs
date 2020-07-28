using System;
using System.IO;
using System.Linq;
using ProductShop.Data;

using ProductShop.Models;
using ProductShop.Dtos.Import;
using System.Collections.Generic;
using ProductShop.Dtos.Export;
using ProductShop.XMLHelper;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new ProductShopContext();
            //ResetDatabase(db);

            //Task 01. Import Users
            //var inputXml01 = File.ReadAllText(@"..\..\..\Datasets\users.xml");
            //Console.WriteLine(ImportUsers(db, inputXml01));

            //Task 02.Import Products
            //var inputXml02 = File.ReadAllText(@"..\..\..\Datasets\products.xml");
            //Console.WriteLine(ImportProducts(db, inputXml02));

            //Task 03.Import Categories
            //var inputXml03 = File.ReadAllText(@"..\..\..\Datasets\categories.xml");
            //Console.WriteLine(ImportCategories(db, inputXml03));

            //Task 04.Import Categories and Products
            //var inputXlm04 = File.ReadAllText("../../../Datasets/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(db, inputXlm04));

            //Task 05.Products In Range
            //var result05 = GetProductsInRange(db);
            //File.WriteAllText(@"..\..\..\Datasets\Results\productsInRange.xml", result05);

            //Task 06.Sold Products
            //var result06 = GetSoldProducts(db);
            //File.WriteAllText
            //    (@"..\..\..\Datasets\Results\soldProducts.xml", result06);

            //Task 07.Categories By Products Count
            //var result07 = GetCategoriesByProductsCount(db);
            //File.WriteAllText
             //   (@"..\..\..\Datasets\Results\categoriesByProductsCount.xml", result07);

            //Task 08.Users and Proucts
            var result08 = GetUsersWithProducts(db);
            File.WriteAllText
                  (@"..\..\..\Datasets\Results\users-and-products.xml", result08);
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

            var usersResult = XmlConverter.Deserializer<ImportUserDto>(inputXml, rootElement);

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

            var productsDtos = XmlConverter
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

            var categoryDtos = XmlConverter.Deserializer<ImportCategoryDto>(inputXml, rootAtribute);

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

            var categoriesProductsResult = XmlConverter
                .Deserializer<ImportCategoryProductDto>(inputXml, rootElement);

            var categoriesCount = context.Categories.Count();
            var productsCount = context.Products.Count();

            var categoriesProducts = categoriesProductsResult
                .Where(x => x.CategoryId <= categoriesCount && x.ProductId <= productsCount)
                .Select(x => new CategoryProduct 
                {
                    CategoryId = x.CategoryId,
                    ProductId = x.ProductId })
                .ToArray();
            context.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Length}";
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

            var result = XmlConverter.Serialize(products, "Products");

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

            var result = XmlConverter.Serialize
                    (usersWithProducts, "Users");

            return result;
        }

        //Task 07.
        public static string GetCategoriesByProductsCount
            (ProductShopContext context)
        {
            //Get all categories.For each category select its name,
            //    the number of products, the average price of those
            //    products and the total revenue(total price sum) of 
            //    those products(regardless if they have a buyer or not).
            //Order them by the number of products(descending) then by total revenue.

            var category = context
                           .Categories
                           .Select(c => new ExportCategoriesByProductDto
                           {
                               Name = c.Name,
                               Count = c.CategoryProducts.Count(),
                               AveragePrice = c.CategoryProducts.Average(x => x.Product.Price),
                               TotalRevenue = c.CategoryProducts.Sum(y => y.Product.Price)
                           })
                           .OrderByDescending(c => c.Count)
                           .ThenBy(t => t.TotalRevenue)
                           .ToList();

            var categoryByProducts = XmlConverter.Serialize(category, "Categories");

            return categoryByProducts;
        }

        public static string GetUsersWithProducts
            (ProductShopContext context)
        {
            //Select users who have at least 1 sold product.
            //    Order them by the number of sold products(from highest to lowest).
            //Select only their first and last name, age,
            //count of sold products and for each product 
            //    -name and price sorted by price(descending).
            //    Take top 10 records.

            var usersDto = context
                .Users
                .ToList()
                .Where(u => u.ProductsSold.Any())
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new ExportSoldProductCountDto
                    {
                        Count = u.ProductsSold.Count(),
                        Products = u.ProductsSold.Select(ps => new ExportProductsDto
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .OrderByDescending(x => x.Price)
                        .ToList()
                    }
                })
                .OrderByDescending(ps => ps.SoldProducts.Count)
                .Take(10)
                .ToList();

            var resultDto = new ExportUsersWithProductDto
            {
                Count = context.Users.Count(x => x.ProductsSold.Any()),
                Users = usersDto
            };

            var result = XmlConverter.Serialize(resultDto, "Users");

            return result;
        }
    }
}
