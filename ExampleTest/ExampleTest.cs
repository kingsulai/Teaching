using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Teaching;
using Teaching.Examples;

namespace ExampleTest
{
    /// <summary>
    /// 
    /// </summary>
    public class ExampleTest
    {
        IEnumerable<IExample> examples;

        [SetUp]
        public void Setup()
        {
            var type = typeof(IExample);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => type.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null);

            Console.WriteLine(String.Join(",", types.Select(t => t.Name)));
            examples = types.Where(t => !t.IsInterface && !t.IsAbstract).Select(t => (IExample)Activator.CreateInstance(t));

        }

        [Test]
        public void FindExamplesTest()
        {
            if (examples.Count() > 0)
            {
                foreach (var e in examples)
                {
                    e.Run();
                }
                Assert.Pass();
            }
            else
                Assert.Fail("Could not find Any classes that implement IExample");
        }

        [Test]
        public void IProductTest()
        {
            Box b1 = new Box()
            {
                products = new List<IProduct>()
                {
                    new Product() { price = 1 },
                    new Product() { price = 2 },
                    new Product() { price = 3 },
                    new Product() { price = 4 },
                    new Product() { price = 5 },
                    new Product() { price = 6 },
                    new Product() { price = 7 },
                    new Product() { price = 8 },
                    new Product() { price = 9 },
                    new Product() { price = 10 }
                }
            };
            Box b2 = new Box()
            {
                products = new List<IProduct>()
                {
                    new Product() { price = 1 },
                    new Product() { price = 2},
                    new Product() { price = 3 },
                    new Product() { price = 4 },
                    new Product() { price = 5 },
                    new Product() { price = 6 },
                    new Product() { price = 7 },
                    new Product() { price = 8 },
                    new Product() { price = 9 },
                    new Product() { price = 10 },
                    b1
                }
            };

            Box b3 = new Box()
            {
                products = new List<IProduct>()
                {
                    new Product() { price = 1 },
                    new Product() { price = 2},
                    new Product() { price = 3 },
                    new Product() { price = 4 },
                    new Product() { price = 5 },
                    new Product() { price = 6 },
                    new Product() { price = 7 },
                    new Product() { price = 8 },
                    new Product() { price = 9 },
                    new Product() { price = 10 },
                }
            };
            Box b4 = new Box()
            {
                products = new List<IProduct>()
                {
                    new Product() { price = 1 },
                    new Product() { price = 2},
                    new Product() { price = 3 },
                    new Product() { price = 4 },
                    new Product() { price = 5 },
                    new Product() { price = 6 },
                    new Product() { price = 7 },
                    new Product() { price = 8 },
                    new Product() { price = 9 },
                    new Product() { price = 10 },
                }
            };

            Box topB = new Box
            {
                products = new List<IProduct>()
                {
                    b2,b3,b4
                }
            };

            Assert.AreEqual(220, topB.CalculatePrice(),"Preis stimmmt nicht!");
        }

        [Test]
        void VisitorTest()
        {
            // Boxen Befüllen
            VisitorBox b = new VisitorBox();

            // Visitor übergeben und einen Sinnvolle Test Prüfung schreiben
            throw new NotImplementedException();
        }
    }
}