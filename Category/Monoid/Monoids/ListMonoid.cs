namespace Sandbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Un monoide formado por las listas, la adición y la lista vacía.
    /// </summary>
    public class ListConcatMonoid : Monoid<IList<string>>
    {
        public ListConcatMonoid(IList<string> value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<IList<string>> Empty = 
            new ListConcatMonoid(new List<string>());

        /// <summary>
        /// Indica si el monoide es conmutativo
        /// </summary>
        /// <remarks>
        /// Un monoide es conmutativo si se cumple la siguiente ecuación:
        ///  a • b = b • a
        /// </remarks>
        public static readonly bool IsCommutative = false;

        /// <summary>
        /// La operación de combinación (•) del monoide
        /// </summary>
        /// <param name="a">El monoide a</param>
        /// <param name="b">El monoide b</param>
        /// <returns>El resultado de a • b</returns>
        protected override Monoid<IList<string>> Combine(IList<string> a, IList<string> b)
        {
            var result = new List<string>(a);
            result.AddRange(b);
            return new ListConcatMonoid(result);
        }

        //
        // Infraestructura
        //

        protected override bool ValueEquals(IList<string> a, IList<string> b)
        {
            return Enumerable.SequenceEqual(a, b);
        }

        protected override string ValueToString(IList<string> value)
        {
            return String.Join(String.Empty, value);
        }
    }

    /// <summary>
    /// Un monoide formado por las listas, la adición y la lista vacía.
    /// </summary>
    public class OrderedListConcatMonoid : Monoid<IList<string>>
    {
        public OrderedListConcatMonoid(IList<string> value) :
            this(value.ToList()) { }

        OrderedListConcatMonoid(List<string> value) :
            base(value)
        {
            value.Sort();
        }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<IList<string>> Empty =
            new OrderedListConcatMonoid(new List<string>());

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
        protected override Monoid<IList<string>> Combine(IList<string> a, IList<string> b)
        {
            var result = new List<string>(a);
            result.AddRange(b);
            result.Sort();
            return new OrderedListConcatMonoid(result);
        }

        //
        // Infraestructura
        //

        protected override bool ValueEquals(IList<string> a, IList<string> b)
        {
            return Enumerable.SequenceEqual(a, b);
        }

        protected override string ValueToString(IList<string> value)
        {
            return String.Join(String.Empty, value);
        }
    }

}
