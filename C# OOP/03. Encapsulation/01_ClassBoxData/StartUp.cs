namespace ClassBoxData
{
    using System;
    using System.Linq;
    using System.Reflection;

    class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                Type boxType = typeof(Box);
                FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                //Console.WriteLine(fields.Count());

                double length = double.Parse(Console.ReadLine());
                double width = double.Parse(Console.ReadLine());
                double height = double.Parse(Console.ReadLine());

                Box box = new Box(length, width, height);
                Console.WriteLine("Surface Area - {0:F2}", box.GetSurfaceArea());
                Console.WriteLine("Lateral Surface Area - {0:F2}", box.GetLateralSurfaceArea());
                Console.WriteLine("Volume - {0:F2}", box.GetVolume());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
