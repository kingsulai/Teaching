using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{
    public interface IProduct
    {
        float CalculatePrice();
    }

    /// <summary>
    /// Einfach den Preis zurückgeben
    /// </summary>
    public class Product : IProduct
    {
        public float price;
        public float CalculatePrice()
        {
            return price;
        }
    }


    /// <summary>
    /// Preis von allen Boxen zurückgeben
    /// </summary>
    public class Box : IProduct
    {
        public List<IProduct> products;
        public float CalculatePrice()
        {
            var result = 0.0f;
            foreach (var product in products)
                result += product.CalculatePrice();
            return result;
        }
    }
}
