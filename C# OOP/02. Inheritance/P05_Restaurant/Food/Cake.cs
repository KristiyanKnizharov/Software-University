namespace Restaurant.Food
{
    public class Cake : Dessert
    {
        private const double GRAMS = 250;
        private const double CALORIES = 1000;
        private const decimal CAKE_PRICE = 5;
        public Cake(string name, decimal price,
            double grams)
            : base(name, 0, 0, 0)
        {

        }
        public override double Grams { get => GRAMS; }

        public override double Calories { get => CALORIES; }

        public override decimal Price { get => CAKE_PRICE; }
    }
}
