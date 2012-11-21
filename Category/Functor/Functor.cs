using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// A functor is a map from one category to another: 
    /// it maps objects into objects and morphisms into morphisms. 
    /// </summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Functor
    /// In category theory a functor is a special type of mapping between categories.
    /// Functors can be thought of as homomorphisms between categories.
    /// 
    /// Let C and D be categories. A functor F from C to D is a mapping that:
    /// 
    /// • associates to each object x ϵ C an object F(X) ϵ D,
    /// 
    /// • associates to each morphism f: X → Y ϵ C a morphism F(f): F(Y) → F(X) ϵ D
    ///   such that the following two conditions hold:
    ///   
    ///   • F(idₓ) = idF₍ₓ₎ for every object x ϵ C 
    ///   
    ///   • F(g ◦ f) = F(f) ◦ F(f) for all morphisms f: X → Y and g: Y → Z
    ///   
    /// That is, functors must preserve identity morphisms and composition of morphisms.
    /// 
    /// 
    ///              F(f)
    /// D    F(A) -------------> F(B)
    ///       ^        ^         ^
    ///       |        |         |
    ///       |     Lifting      |
    ///       |        |         | 
    ///       |        f         |
    /// C     A  --------------> B
    /// 
    /// 
    /// Notation
    /// ϵ  is element of
    /// Ʉ  for all
    /// э  there exists
    /// ˸  such that
    /// </remarks>
    static class FunctorSamples
    {
        public static void Run()
        {

            TestLift();

            TestComposition();
        }

        static void TestLift()
        {
            // Un elemento de A
            int a = 2;

            // Un morfismo f: A → B
            Func<int, double> f = x => x * 2.5;

            // Un elemento de B
            // terminal del morfismo f(a)
            double b = f(a);


            // El elemento a mapeado a F(A)
            List<int> a_ = MapElement(a);

            // El morfismo f mapeado al morfismo F(f)
            Func<List<int>, List<double>> f_ = MapMorphism(f);

            // El elemento b mapeado a F(B)
            List<double> b_ = f_(a_);


            Console.WriteLine("a: {0}", a);
            Console.WriteLine("b: {0}", b);

            Console.WriteLine("Fa: {0}", String.Join(String.Empty, a_));
            Console.WriteLine("Fb: {0}", String.Join(String.Empty, b_));
        }

        static void TestComposition()
        {
            // Functors must preserve composition of morphisms.
            
            // Un elemento de A
            int a = 2;

            // Un morfismo f: A → B
            Func<int, double> f = x => x * 2.5;

            // Un elemento de B
            // terminal del morfismo f(a)
            double b = f(a);

            // Un morfismo g: B → C
            Func<double, string> g = x => x.ToString();

            // Un elemento de C
            // terminal del morfismo g(b)
            string c = g(b);

            string c1 = g(f(a));


            // El elemento a mapeado a F(A)
            List<int> a_ = MapElement(a);

            // El morfismo f mapeado al morfismo F(f)
            Func<List<int>, List<double>> f_ = MapMorphism(f);

            // El elemento b mapeado a  F(B)
            List<double> b_ = f_(a_);

            // El morfismo g mapeado al morfismo F(g)
            Func<List<double>, List<string>> g_ = MapMorphism(g);

            // El elemento c mapeado a F(C)
            List<string> c_ = g_(b_);

            List<string> c_1 = g_(f_(a_));


            Console.WriteLine("g(b): {0}", c);
            Console.WriteLine("g(f(a)): {0}", c1);

            Console.WriteLine("Fg(Fb): {0}", String.Join(String.Empty, c_));
            Console.WriteLine("Fg(Ff(Fb)): {0}", String.Join(String.Empty, c_1));

        }


        //
        // Dado un elemento de A devuelve el elemento F(A)
        //
        static List<A> MapElement<A>(A a)
        {
            return new List<A>(){ a };
        }

        //
        // Dado el morfismo:
        //
        //   f: A → B
        //
        // devuelve el morfismo:
        //
        //  F(f): F(A) → F(B)
        //
        static Func<List<A>,List<B>> MapMorphism<A,B>(Func<A,B> f)
        {
            Func<List<A>, List<B>> fLifted =
                a =>
                {
                    List<B> b = new List<B>();
                    foreach (A a_ in a)
                    {
                        var b_ = f(a_);
                        b.Add(b_);
                    }
                    return b;
                };

            return fLifted;
        }

    }
}
