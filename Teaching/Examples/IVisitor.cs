using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{

    public class VisitorBox : Box
    {
        public void VisitProducts(Action<Product> v)
        {
            foreach(var p in products)
            {
                switch (p)
                {
                    case null:
                        break;
                    case Product p1:
                        v(p1);
                        break;
                    case VisitorBox vb:
                        vb.VisitProducts(v);
                        break;
                }
            }
        }
    }
}
