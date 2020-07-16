using System;
using P03_SalesDatabase.Data;
using System.Collections.Generic;
using P03_SalesDatabase.IOManagment;
using P03_SalesDatabase.Data.Seeding;
using P03_SalesDatabase.IOManagment.Contracts;
using P03_SalesDatabase.Data.Seeding.Contracts;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //SalesContext dbContext = new SalesContext();
            //Random random = new Random();
            //IWriter consoleWriter = new ConsoleWriter();

            //ICollection<ISeeder> seeders = new List<ISeeder>();
            //seeders.Add(new ProductSeeder(dbContext, random, consoleWriter));
            //seeders.Add(new StoreSeeder(dbContext, consoleWriter));

            //foreach (ISeeder seeder in seeders)
            //{
            //    seeder.Seed();
            //}
        }
    }
}
