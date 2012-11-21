namespace Sandbox
{
    using System;

    /// <summary>
    /// A Monoid is an algebraic structure with a single 
    /// associative binary operation and an identity element. 
    /// </summary>
    /// <remarks>
    /// Monoid
    /// http://en.wikipedia.org/wiki/Monoid
    /// 
    /// A monoid is a set, S, together with a binary operation “•” (pronounced "dot" or "times") 
    /// that satisfies the following three axioms:
    /// 
    /// Closure
    /// For all a, b in S, the result of the operation a • b is also in S.
    /// 
    /// Associativity
    /// For all a, b and c in S, the equation (a • b) • c = a • (b • c) holds.
    /// 
    /// Empty element
    /// There exists an element e in S, such that for all elements a in S, 
    /// the equation e • a = a • e = a holds.
    /// 
    /// In mathematical notation we can write these as
    /// 
    /// Closure: a • b ϵ S   Ʉ a,b ϵ S
    /// 
    /// Associativity: (a • b) • c = a • (b • c)  Ʉ a,b,c ϵ S 
    /// 
    /// Empty element: э ҽ ϵ S ˸ ҽ • a = a • ҽ = a  Ʉ a ϵ S
    /// 
    /// Notation
    /// ϵ  is element of
    /// Ʉ  for all
    /// э  there exists
    /// ˸  such that
    /// </remarks>
    public abstract class Monoid<S> : 
        IEquatable<Monoid<S>>
    {
        readonly S _value;

        public Monoid(S value)
        {
            if (Object.ReferenceEquals(value, null))
                throw new ArgumentNullException("value");
            
            _value = value;
        }

        /// <summary>
        /// La operación de combinación (•) del monoide
        /// </summary>
        /// <param name="b">El monoide b</param>
        /// <returns>El resultado de a • b, donde a es esta instancia.</returns>
        public Monoid<S> Dot(Monoid<S> b)
        {
            if (b == null)
                throw new ArgumentNullException("other");

            var res = Combine(this._value, b._value);

            return res;
        }

        /// <summary>
        /// La operación de combinación (•) del monoide
        /// </summary>
        /// <param name="a">El monoide a</param>
        /// <param name="b">El monoide b</param>
        /// <returns>El resultado de a • b</returns>
        public static Monoid<S> operator *(Monoid<S> a, Monoid<S> b)
        {
            if (object.ReferenceEquals(a, null))
                throw new ArgumentNullException("a");
            if (object.ReferenceEquals(b, null))
                throw new ArgumentNullException("b");
            return a.Dot(b);
        }


        //
        // Infrastructure
        //
        protected abstract Monoid<S> Combine(S a, S b);

        public override string ToString()
        {
            return ValueToString(this._value);
        }

        protected virtual string ValueToString(S value)
        {
            return value.ToString();
        }

        protected virtual bool ValueEquals(S a, S b)
        {
            return a.Equals(b);
        }

        public static bool operator ==(Monoid<S> a, Monoid<S> b)
        {
            if (object.ReferenceEquals(a, null))
                return object.ReferenceEquals(b, null);
            return a.Equals(b);
        }

        public static bool operator !=(Monoid<S> a, Monoid<S> b)
        {
            if (object.ReferenceEquals(a, null))
                return !object.ReferenceEquals(b, null);
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Monoid<S>))
                return false;
            return Equals((Monoid<S>)obj);
        }

        public bool Equals(Monoid<S> other)
        {
            if (object.ReferenceEquals(other, null))
                return false;
            return ValueEquals(this._value, other._value);
        }


    }
}
