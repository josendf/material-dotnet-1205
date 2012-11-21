using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// Representa una agrupación de pedidos por producto
    /// </summary>
    class OrdersByProduct
    {
        Dictionary<Product, HashSet<Order>> _items;

        public IDictionary<Product, HashSet<Order>> Items { get { return _items; } }

        public OrdersByProduct(Dictionary<Product, HashSet<Order>> items)
        {
            _items = items;
        }

        public OrdersByProduct(IEnumerable<Order> orders)
        {
            var seq = orders
                .SelectMany(x => x.Lines.Select(y => new { Order = x, Line = y }))
                .GroupBy(x => x.Line.Product, x => x.Order)
                .ToDictionary(x => x.Key, x => new HashSet<Order>(x));
            _items = seq;
        }

        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();
            foreach (var pair in _items)
            {
                if (buff.Length > 0)
                    buff.Append(' ');
                buff.Append(pair.Key).Append(":[");
                int sep = 0;
                foreach (var ord in pair.Value)
                {
                    if (sep++ > 0)
                        buff.Append(' ');
                    buff.Append(ord.Code);
                }
                buff.Append(']');
            }
            return buff.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is OrdersByProduct))
                return false;
            OrdersByProduct other = (OrdersByProduct)obj;
            var otherItems = other.Items;
            foreach (var pair in this.Items)
            {
                HashSet<Order> otherOrders;
                if (!otherItems.TryGetValue(pair.Key, out otherOrders))
                    return false;
                if (!otherOrders.SetEquals(pair.Value))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static IEnumerable<OrdersByProduct> Samples(int count)
        {
            Random rnd = new Random();

            int accum = 1;

            var seq = Enumerable
                .Range(1, count)
                .Select(n =>
                {
                    var num = rnd.Next(1, 6);
                    var prev = accum;
                    accum += num;
                    var orders = Order.Samples(num, prev);
                    return new OrdersByProduct(orders);
                });
            return seq;
        }
    }
}
