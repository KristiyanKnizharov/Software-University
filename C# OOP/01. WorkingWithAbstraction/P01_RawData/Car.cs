using System;
using System.Collections.Generic;
using System.Text;

namespace P01_RawData
{
    public class Car
    {
        private readonly Tire[] tires;

        public Car(string model,Engine engine
            , Cargo cargo
            , Tire[] tires)
        {
            this.Model = model;
            this.Engine = engine;
            this.Cargo = cargo;
            this.tires = tires;
        }
        public Engine Engine { get; }
        public Cargo Cargo  { get; }
        public string Model { get; set; }

        public IReadOnlyCollection<Tire> Tires
        {
            get
            {
                return this.tires;
            }
        }

    }
}
