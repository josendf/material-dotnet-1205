using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Sandbox
{
    /// <summary>
    /// Representa los valores numéricos 
    /// de una línea de un pedido
    /// </summary>
    class OrderValues
    {
        public double Quantity { get; private set; }

        public double Amount { get; private set; }

        public OrderValues(double quantity, double amount)
        {
            this.Quantity = quantity;
            this.Amount = amount;
        }

        public override string ToString()
        {
            var ic = CultureInfo.InvariantCulture;
            return string.Format(ic, "{0} ${1:F2}", Quantity, Amount);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is OrderValues))
                return false;
            OrderValues other = (OrderValues)obj;
            return Quantity == other.Quantity && Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return Quantity.GetHashCode() ^ Amount.GetHashCode();
        }

        public static IEnumerable<OrderValues> Samples(int count)
        {
            Random rnd = new Random();

            var seq = Enumerable
                .Range(1, count)
                .Select(n =>
                {
                    var q = (double)rnd.Next(1, 5);
                    return new OrderValues(q, q * 1.5);
                });
            return seq;
        }
    }

}
