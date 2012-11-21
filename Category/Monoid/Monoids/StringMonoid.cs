namespace Sandbox
{
    using System;

    /// <summary>
    /// Un monoide formado por las cadenas, la concatenación y la cadena vacía.
    /// </summary>
    public class StringConcatMonoid : Monoid<string>
    {
        public StringConcatMonoid(string value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<string> Empty = 
                new StringConcatMonoid(String.Empty);

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
        protected override Monoid<string> Combine(string a, string b)
        {
            var result = string.Concat(a, b);
            return new StringConcatMonoid(result);
        }
    }
}
