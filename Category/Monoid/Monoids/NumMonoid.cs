    namespace Sandbox
{
    using System;

    /// <summary>
    /// Un monoide formado por los números enteros, la suma y el valor cero.
    /// </summary>
    public class IntSumMonoid : Monoid<int>
    {
        public IntSumMonoid(int value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<int> Empty = new IntSumMonoid(0);

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
        protected override Monoid<int> Combine(int a, int b)
        {
            var result = a + b;
            return new IntSumMonoid(result);
        }
    }

    /// <summary>
    /// Un monoide formado por los números enteros, la multiplicación y el valor 1.
    /// </summary>
    public class IntProdMonoid : Monoid<int>
    {
        public IntProdMonoid(int value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<int> Empty = new IntProdMonoid(1);

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
        protected override Monoid<int> Combine(int a, int b)
        {
            var result = a * b;
            return new IntProdMonoid(result);
        }
    }

    /// <summary>
    /// Un no-monoide formado por los números enteros, la resta y el valor 0. 
    /// </summary>
    public class IntSubNotMonoid : Monoid<int>
    {
        public IntSubNotMonoid(int value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<int> Empty = new IntSubNotMonoid(0);

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
        protected override Monoid<int> Combine(int a, int b)
        {
            var result = a - b;
            return new IntSubNotMonoid(result);
        }
    }

    /// <summary>
    /// Un monoide formado por los valores double, la multiplicación y el valor 1.
    /// </summary>
    public class DoubleMultMonoid : Monoid<double>
    {
        public DoubleMultMonoid(double value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<double> Empty = new DoubleMultMonoid(1);

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
        protected override Monoid<double> Combine(double a, double b)
        {
            var result = a * b;
            return new DoubleMultMonoid(result);
        }
    }

    /// <summary>
    /// Un no-monoide formado por los valores double, 
    /// la siguiente regla de combinación:
    ///  • la división cuando el denominador es diferente de 0 
    ///  • de lo contrario Double.NaN
    /// y el valor 1 como identidad.
    /// </summary>
    public class DoubleDivNotMonoid : Monoid<double>
    {
        public DoubleDivNotMonoid(double value) :
            base(value) { }

        /// <summary>
        /// El valor neutro del monoide
        /// </summary>
        public static readonly Monoid<double> Empty = new DoubleDivNotMonoid(1);

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
        protected override Monoid<double> Combine(double a, double b)
        {
            double result = (b == 0) ? Double.NaN : (a / b);
            return new DoubleDivNotMonoid(result);
        }
    }

}
