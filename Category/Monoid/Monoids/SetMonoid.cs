namespace Sandbox
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Un monoide formado por el contenedor Set, la unión y el conjunto vacío.
    /// </summary>
    public class SetUnionMonoid : Monoid<ISet<string>>
    {
        public SetUnionMonoid(ISet<string> value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<ISet<string>> Empty = 
            new SetUnionMonoid(new HashSet<string>());

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
        protected override Monoid<ISet<string>> Combine(ISet<string> a, ISet<string> b)
        {
            var result = new HashSet<string>(a);
            result.UnionWith(b);
            return new SetUnionMonoid(result);
        }

        //
        // Infraestructura
        //

        protected override bool ValueEquals(ISet<string> a, ISet<string> b)
        {
            return a.SetEquals(b);
        }

        protected override string ValueToString(ISet<string> value)
        {
            return String.Join(String.Empty,value);
        }
    }
}
