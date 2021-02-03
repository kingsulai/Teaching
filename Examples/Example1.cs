using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Teaching.Examples
{
    /// <summary>
    /// Value vs Reference
    /// Class using a Serializable Attribute
    /// </summary>
    [Serializable]
    public class Example1 : IExample
    {

        /// <summary>
        /// Read Write Property
        /// </summary>
        public int A { get; set; } = 1;

        /// <summary>
        /// Read only property
        /// </summary>
        public int B => 1;

        /// <summary>
        /// Also a read only property
        /// </summary>
        public int C { get; } = 0;

        delegate void Del();
        Del del;
        /// <summary>
        /// Shows the following:
        /// - Reference vs Value
        /// - Delegate and lambda
        /// </summary>
        public void Run()
        {
            A = 0;
            //B = 0;
            //C = 0;
            var v = 0;
            //Call 2 Methods one wich passes an integer by value and modifies it, the other passes it by reference and modifies it.After each call the integer is printed to the console
            Method(v);
            Console.WriteLine(v);
            Method(ref v);
            Console.WriteLine(v);

            // assign lambda expression to a delegate
            del = () => Console.WriteLine("Called from a delegate");
            del();

            // call a local function
            log("Local Function");
            void log(string s){ Console.WriteLine(s); }

            // tuples are great, normally youe would have to define a tmp variable
            int a = 0, b = 1;
            Console.WriteLine($"{a},{b}");
            (a, b) = (b, a);
            Console.WriteLine($"{a},{b}");

            // test multiple conditions in a switch statement
            switch (a, b)
            {
                case (0, 1):
                    Console.WriteLine("a must be 0 and b must be 1 ");
                    break;
                case (_, 1):
                    Console.WriteLine("a can be any value, but b must be 1 ");
                    break;
                case (1,_):
                    Console.WriteLine("a must be 1, but b can be any value");
                    break;
                //case (1, 0):
                //    Console.WriteLine("recognizes that this cas will never be hit");
                //    break;
                default:
                    Console.WriteLine("Something went wrong");
                    break;
            }

            // switch assign
            var x = a switch
            {
                0 => 0,
                1 => 2,
                _ => -1
            };

            // if assign
            x = a > 0 ? a : b;

            Console.WriteLine(JsonSerializer.Serialize(this));

        }


        void Method(ref int v)
        {
            v = 1;
        }

        void Method(int v)
        {
            v = 1;
        }
    }
}
