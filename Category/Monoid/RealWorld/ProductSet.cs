using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// Representa un conjunto de productos
    /// </summary>
    class ProductSet
    {
        HashSet<Product> _items;

        public ICollection<Product> Items { get { return _items; } }

        public ProductSet(params string[] products)
        {
            var seq = products.Select(p => new Product(p));
            _items = new HashSet<Product>(seq);
        }

        public ProductSet(IEnumerable<Product> products)
        {
            _items = new HashSet<Product>(products);
        }

        public ProductSet(HashSet<Product> products)
        {
            _items = products;
        }

        public override string ToString()
        {
            return String.Join(String.Empty, _items);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is ProductSet))
                return false;
            ProductSet other = (ProductSet)obj;
            return _items.SetEquals(other._items);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static IEnumerable<ProductSet> Samples(int count)
        {
            Random rnd = new Random();

            var seq = Enumerable
                .Range(1, count)
                .Select(x => new ProductSet(Enumerable
                    .Range(1, rnd.Next(1, 6))
                    .Select(y => new Product(string.Format("P{0}", y)))));

            return seq;
        }
    }

}
