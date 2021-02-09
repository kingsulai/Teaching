using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{
    public class SingletonExample
    {
        private static SingletonExample instance;
        public static SingletonExample Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonExample();
                }
                return instance;
            }
        }
        private SingletonExample()
        {
                
        }

    }
}
