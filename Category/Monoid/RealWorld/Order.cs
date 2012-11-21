using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Sandbox
{
    /// <summary>
    /// Representa un pedido que tiene un código único
    /// y una colección de líneas de detalle
    /// </summary>
    class Order
    {
        string _code;

        List<OrderLine> _lines;

        public Order(string code, string text)
        {
            _code = code;
            var ic = CultureInfo.InvariantCulture;
            var lines = text.Split('\n');
            _lines = new List<OrderLine>(lines.Length);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                Product prod = new Product(parts[0]);
                double q = Double.Parse(parts[1],ic);
                double a = Double.Parse(parts[2], ic);
                var values = new OrderValues(q, a);
                var ol = new OrderLine(prod, values);
                _lines.Add(ol);
            }
        }

        public Order(string code, IEnumerable<OrderLine> lines)
        {
            _code = code;
            _lines = lines.ToList();
        }

        public string Code { get { return _code; } }

        public ICollection<OrderLine> Lines { get { return _lines; } }

        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();
            buff.Append(_code);
            foreach (var line in _lines)
            {
                if (buff.Length > 0)
                    buff.Append(' ');
                buff
                    .Append(line.Product)
                    .Append(' ')
                    .Append(line.Values);
            }
            return buff.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Order))
                return false;
            Order other = (Order)obj;
            if (_lines.Count != other._lines.Count)
                return false;
            for (int i = 0; i < _lines.Count; ++i)
            {
                var a = _lines[i];
                var b = other._lines[i];
                if (!a.Equals(b))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static IEnumerable<Order> Samples(int count)
        {
            return Samples(count, 1);
        }

        public static IEnumerable<Order> Samples(int count, int start)
        {
            Random rnd = new Random();

            var seq = Enumerable
                .Range(0, count)
                .Select(n =>
                {
                    var code = string.Format("O{0}", n + start);
                    var r = rnd.Next(1, 6);
                    var lines = OrderLine.Samples(r);
                    return new Order(code,lines);
                });
            return seq;
        }
    }

}
