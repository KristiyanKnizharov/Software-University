using System;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System.Collections.Generic;

using CarDealer.Data;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        private static string Path = @"..\..\..\Datasets\";
        public static void Main(string[] args)
        {
            var db = new CarDealerContext();
            //ResetDatabase(db);

            //Task 09. Import Suppliers
            //var inputJson = File.ReadAllText(Path + @"suppliers.json");
            //Console.WriteLine(ImportSuppliers(db, inputJson));

            // Task 10.Import Parts
            //var inputParts = File.ReadAllText(Path + @"parts.json");
            //Console.WriteLine(ImportParts(db, inputParts));

            // Task 11. Import Cars
            //var inputCars = File.ReadAllText(Path + @"cars.json");
            //Console.WriteLine(ImportCars(db, inputCars));

            // Task 12. Import Customers
            //var inputCustomers = File.ReadAllText(Path + @"customers.json");
            //Console.WriteLine(ImportCustomers(db, inputCustomers));

            // Task 13. Import Sales
            //var inputSales = File.ReadAllText(Path + @"sales.json");
            //Console.WriteLine(ImportSales(db, inputSales));

            // Task 14. Export Ordered Customers
            //Console.WriteLine(GetOrderedCustomers(db));

            // Task 15. Export Cars from Make Toyota
            //Console.WriteLine(GetCarsFromMakeToyota(db));

            // Task 16. Export Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(db));

            // Task 17. Export Cars with Their List of Parts
            //Console.WriteLine(GetCarsWithTheirListOfParts(db));

            // Task 18. Export Total Sales by Customer
            //Console.WriteLine(GetTotalSalesByCustomer(db));

            // Task 19. Export Sales with Applied Discount
            //Console.WriteLine(GetSalesWithAppliedDiscount(db));
        }

        public static void ResetDatabase
            (CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was seccesfully created");
        }

        public static string ImportSuppliers
            (CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliers);

            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        public static string ImportParts
            (CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            var count = 0;

            foreach (var p in parts.Where(p => p.SupplierId <= context.Suppliers.Count()))
            {
                context.Parts.Add(p);
                count++;
            }

            context.SaveChanges();

            return $"Successfully imported {count}.";
        }

        public static string ImportCars
            (CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<List<Car>>(inputJson);

            foreach (var car in cars)
            {
                var newCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                context.Cars.Add(newCar);

                foreach (var part in car.PartCars.Select(x => x.CarId).Distinct())
                {
                    var newPartCar = new PartCar
                    {
                        PartId = part,
                        Car = newCar
                    };

                    context.PartCars.Add(newPartCar);
                }
            }

            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        public static string ImportCustomers
            (CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.AddRange(customers);

            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        public static string ImportSales
            (CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);

            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        public static string GetOrderedCustomers
            (CarDealerContext context)
        {
            var customers = context.Customers
                            .AsEnumerable()
                            .OrderBy(y => y.BirthDate)
                            .ThenBy(d => d.IsYoungDriver)
                            .Select(c => new
                            {
                                c.Name,
                                BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                                c.IsYoungDriver
                            })
                            .ToList();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        public static string GetCarsFromMakeToyota
            (CarDealerContext context)
        {
            var carsFromToyota = context.Cars
                                 .Where(c => c.Make == "Toyota")
                                 .Select(c => new
                                 {
                                     c.Id,
                                     c.Make,
                                     c.Model,
                                     c.TravelledDistance
                                 })
                                 .OrderBy(m => m.Model)
                                 .ThenByDescending(d => d.TravelledDistance)
                                 .ToList();

            var json = JsonConvert.SerializeObject(carsFromToyota,
                Formatting.Indented);

            return json;
        }

        public static string GetLocalSuppliers
            (CarDealerContext context)
        {
            var suppliers = context.Suppliers
                            .Where(s => s.IsImporter == false)
                            .Select(p => new
                            {
                                p.Id,
                                p.Name,
                                PartsCount = p.Parts.Where(q => q.Quantity > 0).Count()
                            })
                            .ToList();

            var json = JsonConvert.SerializeObject(suppliers,
                Formatting.Indented);

            return json;
        }

        public static string GetCarsWithTheirListOfParts
            (CarDealerContext context)
        {
            var cars = context.Cars
                       .Select(c => new
                       {
                           car = new
                           {
                               c.Make,
                               c.Model,
                               c.TravelledDistance
                           },
                           parts = c.PartCars
                                   .Select(p => new
                                   {
                                       p.Part.Name,
                                       Price = p.Part
                                        .Price.ToString("F2")
                                   })
                       }) 
                       .ToList();

            var json = JsonConvert.SerializeObject(cars,
                Formatting.Indented);


            return json;
        }

        public static string GetTotalSalesByCustomer
            (CarDealerContext context)
        {
            var customers = context.Cars
                            .Where(c => c.Sales.Count() > 0)
                            .Select(c => new
                            {
                                fullName = c.Sales
                                    .Select(t => t.Customer.Name).First(),
                                boughtCars = c.Sales.Count(),
                                spentMoney = c.PartCars.Sum(y => y.Part.Price)
                            })
                            .OrderByDescending(m => m.spentMoney)
                            .ThenByDescending(t => t.boughtCars)
                            .ToList();

            var json = JsonConvert.SerializeObject(customers,
                Formatting.Indented);

            return json;
            
        }

        public static string GetSalesWithAppliedDiscount
            (CarDealerContext context)
        {
            var sales = context.Sales
                        .Select(s => new
                        {
                            car = new 
                            { 
                                s.Car.Make,
                                s.Car.Model,
                                s.Car.TravelledDistance 
                            },
                            customerName = s.Customer.Name,
                            Discount = s.Discount.ToString("F2"),
                            price = s.Car.PartCars.Sum(p => p.Part.Price)
                                .ToString("F2"),
                            priceWithDiscount = 
                            $"{s.Car.PartCars.Sum(x => x.Part.Price) * (1 - s.Discount / 100):F2}",
                        })
                        .Take(10)
                        .ToList();

            var json = JsonConvert.SerializeObject
                (sales, Formatting.Indented);

            return json;
        }
    }
}