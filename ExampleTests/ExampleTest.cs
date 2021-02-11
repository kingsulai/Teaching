using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private VisitorBox topB;
        private VisitorBox empty;
        private ILogger logger;

        [SetUp]
        public void Setup()
        {

            var serviceProvider = new ServiceCollection()
                .AddLogging(c => c.AddConsole())
                .BuildServiceProvider();

            logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(nameof(ExampleTest));
            logger.LogInformation("TestSetup");

            var type = typeof(IExample);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => type.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null);

            Console.WriteLine(String.Join(",", types.Select(t => t.Name)));
            examples = types.Where(t => !t.IsInterface && !t.IsAbstract).Select(t => (IExample)Activator.CreateInstance(t));

            VisitorBox b1 = new VisitorBox()
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
            VisitorBox b2 = new VisitorBox()
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

            VisitorBox b3 = new VisitorBox()
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
            VisitorBox b4 = new VisitorBox()
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

            topB = new VisitorBox
            {
                products = new List<IProduct>()
                {
                    b2,b3,b4
                }
            };
            
            empty = new VisitorBox();
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
           

            Assert.AreEqual(220, topB.CalculatePrice(),"Preis stimmmt nicht!");


            try
            {
                empty.CalculatePrice();
                Assert.IsTrue(false, "Empty Box does not cost anything!");
            }
            catch (NullReferenceException e)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void VisitorTest()
        {

            float sum = 0;
            // Visitor übergeben und einen Sinnvolle Test Prüfung schreiben
            topB.VisitProducts(product => {
                sum+=product.CalculatePrice();
            });

            Assert.AreEqual(220, sum);
        }


        class TestObserverClass 
        {

            int observationIndex = 0;

            List<(int, int)> observations = new List<(int, int)>()
            {
                (0,1),
                (1,2),
                (2,3),
                (3,4),
                (4,1),
                (0,1),
                (3,1),
                (1,0)
            };
            public void TestObserver((int, int) t)
            {
                var observation = observations[observationIndex++];
                Assert.AreEqual(t.Item1, observation.Item1, "Observed list index is wrong");
                Assert.AreEqual(t.Item2, observation.Item2, "Observed value is wrong");
            }

        }

        [Test]
        public void ObserverTest()
        {

            ObservableList<int> a = new ObservableList<int>();
            var oc = new TestObserverClass();

            a.AddObserver(oc.TestObserver); 

            var b = new List<int>() { 1, 2, 3, 4, 1 };
            a = a + b;
            a.Remove(1);
            a.Remove(1);
            var e = a.Get(1);
            e.Value = 0;
            Assert.AreEqual(a.Get(1).Value, 0, "Even outside of the list the change needs to affect the element in the list");

            a.RemoveObserver(oc.TestObserver);
            a.Add(2);
            a.Add(3);
            a.Add(5);
            a.Add(6);

        }
    }
}