using System;

namespace SoftUniParking
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            Car car = new Car("Skoda", "Fabia", 65, "CC1856BG");
            Car car2 = new Car("Audi", "A3", 110, "EB8787MN");
            Car car3 = new Car("Dacia", "Fabia", 65, "CC1956BG");
            Car car4 = new Car("Opel", "Astra", 110, "EB9987MN");
            Car car5 = new Car("BMW", "Astra", 110, "EB8887MN");
            Car car6 = new Car("Mazda", "Astra", 110, "EB4487MN");

            Parking parking = new Parking(5);
            Console.WriteLine(parking.AddCar(car));
            Console.WriteLine(parking.AddCar(car2));
            Console.WriteLine(parking.AddCar(car3));
            Console.WriteLine(parking.AddCar(car4));
            Console.WriteLine(parking.AddCar(car5));
            Console.WriteLine(parking.AddCar(car6));

            Console.WriteLine(parking.GetCar("CC1856BG").ToString());
            Console.WriteLine(parking.GetCar("EB8787MN").ToString());
            Console.WriteLine(parking.GetCar("CC1956BG").ToString());
            Console.WriteLine(parking.GetCar("EB9987MN").ToString());
        }
    }
}
