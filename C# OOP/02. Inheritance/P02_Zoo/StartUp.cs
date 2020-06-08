namespace Zoo
{
    using System;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string name = Console.ReadLine();
            Animal animal = new Animal(name);
        }
    }
}