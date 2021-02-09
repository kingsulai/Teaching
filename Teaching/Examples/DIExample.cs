using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{
    public interface IDIExample
    {
        void LogSomething();
    }

    public class DIExample : IDIExample
    {
        private ILogger logger;

        public DIExample(ILoggerFactory f)
        {
            logger = f.CreateLogger("Debug");
        }

        public void LogSomething()
        {
            logger.LogInformation("Test");
        }
    }
    public class DIExample2 : IDIExample
    {
        private ILogger logger;


        public DIExample2(ILoggerFactory f)
        {
            logger = f.CreateLogger("Debug");
        }

        public void LogSomething()
        {
            logger.LogInformation("Test2");
        }
    }

    public interface IDependant
    {

        public void CallDependentObj();

    }

    public class Dependant : IDependant
    {
        private IDIExample e;

        public Dependant(IDIExample e)
        {
            this.e = e;
        }

        public void CallDependentObj()
        {
            e.LogSomething();
        }
    }

}
