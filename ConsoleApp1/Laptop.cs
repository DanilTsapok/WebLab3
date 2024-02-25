using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Laptop
    {
        private int id;
        private string brand;
        public int screenSize;
        protected double price;
        internal bool isNew;
        public string operatingSystem;
        public Laptop(int id, string brand, int screenSize, double price, bool isNew, string operatingSystem)
        {
            this.id = id;
            this.brand = brand;
            this.screenSize = screenSize;
            this.price = price;
            this.isNew = isNew;
            this.operatingSystem = operatingSystem;

        }
        public void getLaptopPrice()
        {
            Console.WriteLine($"Price is {price}");
        }
        protected void getScreenSize()
        {
            Console.WriteLine($"The screen size of the laptop is {screenSize}");
        }
        public void getBrand(Laptop laptop) => Console.WriteLine($"Laptop Brand:{laptop.brand}");
    }
}
