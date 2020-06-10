
namespace _03_ShoppingSpree
{
    using _03_ShoppingSpree.Models;
    using System.Collections.Generic;
    using System;
    using _03_ShoppingSpree.Common;

    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> bag;

        private Person()
        {
            this.bag = new List<Product>();
        }
        public Person(string name, decimal money)
            : this()
        {
            this.Name = name;
            this.Money = money;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(GlobalConstants.InvalidNameExceptionMessage);
                }
                this.name = value;
            }
        }

        public decimal Money
        {
            get
            {
                return this.money;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(GlobalConstants.InvalidMoneyException);
                }
                this.money = value;
            }
        }

        public IReadOnlyCollection<Product> Bag
            => this.bag.AsReadOnly();

        public void BuyProduct(Product product)
        {
            if (this.Money < product.Cost)
            {
                throw new InvalidOperationException
                    (string.Format(GlobalConstants
                    .InsMoneyExceptionMessage
                    , this.Name, product.Name));
            }

            this.Money -= product.Cost;
            this.bag.Add(product);
        }
            public override string ToString()
        {
            string prodOutput = this.Bag.Count > 0 ?
                string.Join(", ", this.Bag) : "Nothing bought";
            return $"{this.Name} - {prodOutput}"; 
        }
    }
}
