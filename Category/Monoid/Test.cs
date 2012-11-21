namespace Sandbox
{
    using System;
    
    public static class Test
    {
        /// <summary>
        /// Comprueba los axiomas del Monoide para tres valores particulares. 
        /// </summary>
        public static void Check<S>(Monoid<S> id, Monoid<S> a, Monoid<S> b, Monoid<S> c)
        {
            Console.WriteLine("{0}", id.GetType().Name);
            Console.WriteLine("---------------------------------");
            
            /// Closure
            /// For all a, b in S, the result of the operation a • b is also in S.
            /// 
            /// Garantizada por la firma de la función Dot:
            /// Dot: Monoid a → Monoid a
            /// Monoid<T> Dot(Monoid<T> other)
            /// 
            Console.WriteLine("Check Closure");
            Console.WriteLine("  -> Is Closure");
            Console.WriteLine();

            /// Associativity
            /// For all a, b and c in S, the equation (a • b) • c = a • (b • c) holds.
            CheckAssociativity(a, b, c);

            /// Empty element
            /// There exists an element e in S, such that for all elements a in S, 
            /// the equation e • a = a • e = a holds.
            CheckIdentity(id, a);

            Console.WriteLine();
        }

        static void CheckAssociativity<T>(Monoid<T> a, Monoid<T> b, Monoid<T> c)
        {
            Console.WriteLine("Check Associativity");

            // El operador (*) equivale a la regla de combinación

            /// Associativity
            /// For all a, b and c in S, the equation (a • b) • c = a • (b • c) holds.
            var x = (a * b) * c;

            var y = a * (b * c);

            Console.WriteLine("  (a · b) · c: {0}", x);

            Console.WriteLine("  a · (b · c): {0}", y);

            Console.WriteLine("  (a · b) · c = a · (b · c): {0}", x == y);
            if (x == y)
                Console.WriteLine("  -> Is Associative");
            else
                Console.WriteLine("  -> Is Not Associative");

            Console.WriteLine();
        }

        public static void CheckIdentity<T>(Monoid<T> id, Monoid<T> a)
        {
            Console.WriteLine("Check Identity");

            // El operador (*) equivale a la regla de combinación

            /// Empty element
            /// There exists an element e in S, such that for all elements a in S, 
            /// the equation e • a = a • e = a holds.
            var x = id * a;

            var y = a * id;

            Console.WriteLine("  id · a: {0}", x);

            Console.WriteLine("  a · id: {0}", y);

            Console.WriteLine("  id · a = a · id = a: {0}", (x == y) && (y == a));
            if ((x == y) && (y == a))
                Console.WriteLine("  -> Is Identity");
            else
                Console.WriteLine("  -> Is Not Identity");
           
            Console.WriteLine();
        }
    
    }
}
