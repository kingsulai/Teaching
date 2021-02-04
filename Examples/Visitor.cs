using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{
    public interface Visitor
    {
        void VistProduct(Product p);
    }

    public class VisitorBox : Box
    {
        public void VisitProducts(Visitor v)
        {
            foreach(var p in products)
            {
                switch (p)
                {
                    case Product p1:
                        v.VistProduct(p1);
                        break;
                    case VisitorBox vb:
                        vb.VisitProducts(v);
                        break;
                }
            }
        }
    }
}
