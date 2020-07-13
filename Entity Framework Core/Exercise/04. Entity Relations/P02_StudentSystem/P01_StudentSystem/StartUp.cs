using System;
using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var databaseSS = new StudentSystemContext();
            //db.Database.EnsureCreated();
            databaseSS.Database.EnsureDeleted();
            Console.WriteLine("deleted");
        }
    }
}
