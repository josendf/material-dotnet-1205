using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Sandbox
{
    /// <summary>
    /// Representa una línea de un pedido
    /// </summary>
    class OrderLine
    {
        public Product Product { get; private set; }

        public OrderValues Values { get; private set; }

        public OrderLine(Product product, OrderValues values)
        {
            this.Product = product;
            this.Values = values;
        }

        public override string ToString()
        {
            var ic = CultureInfo.InvariantCulture;
            return string.Format(ic, "{0} {1}", Product, Values);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is OrderLine))
                return false;
            OrderLine other = (OrderLine)obj;
            if (!Product.Equals(other.Product))
                return false;
            if (!Values.Equals(other.Values))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static IEnumerable<OrderLine> Samples(int count)
        {
            Random rnd = new Random();

            var seq = Enumerable
                .Range(1, count)
                .Select(n =>
                {
                    var r = rnd.Next(1, 6);
                    var prod = new Product(string.Format("P{0}", r));
                    var vals = new OrderValues(r, 1.5 * (double)r);
                    return new OrderLine(prod, vals);
                });
            return seq;
        }
    }

}
