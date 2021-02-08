using System;
using System.IO;
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
                .AddLogging(c => c.AddConsole())
                .AddSingleton(typeof(IDIExample),typeof( DIExample))
                .BuildServiceProvider();
            /* Commenting Trick
            var e = new Example1();
            /*/
            IExample e = new Example1();
            //*/

            var diExample = serviceProvider.GetService<IDIExample>();
            diExample.LogSomething();
            e.Run();

            StreamWriter w = new StreamWriter("log.txt");
            w.WriteLine("TEST");
            w.Flush();
            w.Close();

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

    public abstract class DefaultExample : IExample
    {
        private readonly int x;

        public DefaultExample(int x, int y)
        {
            this.x = x;
        }


        public abstract void Run();
    }

    public class FileLogger : ILogger
    {
        public StreamWriter fstream;

        public FileLogger()
        {
            fstream = new StreamWriter("test.log");
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return fstream;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            fstream.WriteLine(formatter(state,exception)+"TEST");
            fstream.Flush();
        }
    }
    public class MyLoggerProvider : ILoggerProvider
    {
        private FileLogger fileLogger;

        public ILogger CreateLogger(string categoryName)
        {
            fileLogger = new FileLogger();
            return fileLogger;
        }

        public void Dispose()
        {
            fileLogger.fstream.Flush();
            fileLogger.fstream.Close();
        }
    }
}
