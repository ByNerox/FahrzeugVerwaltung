using System;
using System.Linq;
using System.Collections.Generic;

namespace Aufgabe
{

    abstract public class Vehicle
    {
        public static int currentId = 0;
        public int Ident { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }

        public Vehicle(string Brand, string Model)
        {
            currentId++;
            this.Ident = currentId;
            this.Brand = Brand;
            this.Model = Model;
        }

    }

    public class PKW : Vehicle
    {
        public PKW(string Brand, string Model) : base(Brand, Model)
        {
            this.Type = "PKW";
        }
    }

    public class LKW : Vehicle
    {
        public double Capacity { get; set; }
        public LKW(string Brand, string Model, double Capacity) : base(Brand, Model)
        {

            this.Capacity = Capacity;
            this.Type = "LKW";
        }

    }


}