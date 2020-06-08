namespace Restaurant.Food
{
    public class Fish : Food
    {
        private const double GRAMS = 22;
        public Fish(string name, decimal price)
            : base(name, price, 0)
        {
        }
        public override double Grams  { get => GRAMS; }

    }
}
