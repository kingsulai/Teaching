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
}
