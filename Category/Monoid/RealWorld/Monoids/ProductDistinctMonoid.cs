using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// Un monoide formado un conjunto de productos 
    /// no duplicados (Set), el conjunto vacío y la unión de conjuntos
    /// </summary>
    class ProductDistinctMonoid : Monoid<ProductSet>
    {
        public ProductDistinctMonoid(ProductSet value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<ProductSet> Empty =
            new ProductDistinctMonoid(new ProductSet(new Product[0]));

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
        protected override Monoid<ProductSet> Combine(ProductSet a, ProductSet b)
        {
            var set = new HashSet<Product>(a.Items);
            set.UnionWith(b.Items);
            var result = new ProductSet(set);
            return new ProductDistinctMonoid(result);
        }
    }
}
