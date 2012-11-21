using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// Un monoide formado un conjunto de conjuntos de pedidos 
    /// no duplicados (Set), el conjunto vacío y la unión de conjuntos
    /// </summary>
    class OrdersByProductMonoid : Monoid<OrdersByProduct>
    {
        public OrdersByProductMonoid(OrdersByProduct value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<OrdersByProduct> Empty =
            new OrdersByProductMonoid(new OrdersByProduct(new Order[0]));

        /// <summary>
        /// Indica si el monoide es conmutativo
        /// </summary>
        /// <remarks>
        /// Un monoide es conmutativo si se cumple la siguiente ecuación:
        ///  a • b = b • a
        /// </remarks>
        public static readonly bool IsCommutative = true;

        /// <summary>
        /// La operación de combinación (•) del monoide
        /// </summary>
        /// <param name="a">El monoide a</param>
        /// <param name="b">El monoide b</param>
        /// <returns>El resultado de a • b</returns>
        protected override Monoid<OrdersByProduct> Combine(OrdersByProduct a, OrdersByProduct b)
        {
            var set = new Dictionary<Product, HashSet<Order>>();

            Merge(set, a.Items);

            Merge(set, b.Items);

            var result = new OrdersByProduct(set);

            return new OrdersByProductMonoid(result);
        }

        static void Merge(Dictionary<Product, HashSet<Order>> set, IDictionary<Product, HashSet<Order>> items)
        {
            foreach (var pair in items)
            {
                HashSet<Order> orders;
                if (!set.TryGetValue(pair.Key, out orders))
                {
                    orders = new HashSet<Order>(pair.Value);
                    set.Add(pair.Key, orders);
                }
                else
                {
                    orders.UnionWith(pair.Value);
                }
            }
        }

    }
}
