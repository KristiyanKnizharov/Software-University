using System;
using System.Text;
namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int hp = int.Parse(Console.ReadLine());
            double fuel = double.Parse(Console.ReadLine());
            double kilometers = double.Parse(Console.ReadLine());

            Vehicle vehicle = new Vehicle(hp, fuel);
            vehicle.Drive(kilometers);
        }
    }
}
