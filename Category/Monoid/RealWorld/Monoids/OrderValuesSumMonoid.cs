using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// Un monoide formado por los valores de una línea
    /// de pedido, los valores cero y la suma
    /// </summary>
    class OrderValuesSumMonoid : Monoid<OrderValues>
    {
        public OrderValuesSumMonoid(OrderValues value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<OrderValues> Empty =
            new OrderValuesSumMonoid(new OrderValues(0, 0));

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
        protected override Monoid<OrderValues> Combine(OrderValues a, OrderValues b)
        {
            var qnt = a.Quantity + b.Quantity;
            var amnt = a.Amount + b.Amount;
            var result = new OrderValues(qnt, amnt);
            return new OrderValuesSumMonoid(result);
        }
    }
}
