using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{

    public class VisitorBox : Box
    {
        public void VisitProducts(Action<Product> visitorAction)
        {
            foreach(var product in products)
            {
                switch (product)
                {
                    case null:
                        break;
                    case Product p:
                        visitorAction(p);
                        break;
                    case VisitorBox box:
                        box.VisitProducts(visitorAction);
                        break;
                }
            }
        }
    }
}
