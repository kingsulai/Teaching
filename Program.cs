using System;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Teaching.Examples;

namespace Teaching
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            /* Commenting Trick
            var e = new Example1();
            /*/
            var e = new Example1();
            //*/

            e.Run();

        }
    }

    /// <summary>
    /// Common Interface to execute an Example
    /// </summary>
    public interface IExample
    {
        /// <summary>
        /// Run the Example
        /// </summary>
        void Run();
    }

}
