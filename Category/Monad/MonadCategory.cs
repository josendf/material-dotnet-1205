using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Monad (category theory)
    /// http://en.wikipedia.org/wiki/Monad_(category_theory)
    /// In category theory, a monad or triple is an (endo-)functor, together with two associated natural 
    /// transformations. 
    /// 
    /// If F and G are a pair of adjoint functors, with F left adjoint to G, then the 
    /// composition F ◦ G  is a monad.
    /// 
    /// Therefore, a monad is an endofunctor.
    /// 
    /// If F and G are inverse functors the corresponding monad is the identity functor.
    /// 
    /// If C is a category, a monad on C consists of:
    /// 
    ///   • a functor T: C → C
    ///   
    ///   • together with two natural transformations:
    ///     
    ///     • η: 1C  → T  where 1C denotes the identity functor on C
    /// 
    ///     • μ: T²  → T  where T² is the functor T ◦ T from C to C
    /// 
    /// These are required to fulfill the following axioms:
    /// 
    ///   • μ ◦ Tμ =  μ ◦ μT  
    ///     as natural transformations T³ → T
    ///     This axiom is akin to the associativity in monoids. 
    /// 
    ///   • μ ◦ Tη =  μ ◦ ηT = 1T  where 1T denotes the identity transformation from T to T
    ///     as natural transformations T → T
    ///     This second axiom is akin to the existence of an identity element.
    /// 
    ///  A monad on C can alternatively be defined as a monoid in the category EndC  whose objects are 
    ///  the endofunctors of C and whose morphisms are the natural transformations between them, with the 
    ///  monoidal structure induced by the composition of endofunctors.
    ///  
    /// We can rewrite these conditions using following commutative diagrams:
    /// 
    /// 
    ///                    T(μ x)
    ///      T(T(T(X))) -------------> T(T(X))
    ///        |                         |
    ///        |                         |
    /// μ T(x) |                         | μ x
    ///        |                         | 
    ///        v                         v
    ///        T(T(X))  --------------> T(X)
    ///                      μ x
    /// 
    /// 
    ///                    ηT(X)
    ///      T(X) -------------------> T(T(X))
    ///        |  \                      |
    ///        |       \                 |
    /// T(ηx)  |            \            | μ x
    ///        |                 \       | 
    ///        v                     \   v
    ///        T(T(X))  --------------> T(X)
    ///                     μ x
    /// 
    /// Notation
    /// η eta
    /// μ my
    /// 
    /// </remarks>
    
    public static class MonadCategory
    {

        public static void Run()
        {
            Sample1();
        }

        // A monad is an endofunctor together with two special families of morphisms, both going vertically, 
        // one up and one down.
        //
        // The one going up is called Unit(η) and the one going down is called Join (μ).

        static void Sample1()
        {
            /// Un elemento T(X)
            List<int> a = new List<int>(){ 2, 3 };

            // El elemento T(x) mapeado a T(T(X))
            List<List<int>> a_ = MUnit(a);

            // El elemento T(T(X)) mapeado a T(X)
            List<int> a__ = MJoin(a_);
        }


        // Unit can be though of as immersing values from a lower level into the higher level 
        // in the most natural way possible.
        //
        // return :: (Monad m) => a -> m a
        //
        static List<A> MUnit<A>(A a)
        {
            return new List<A>() { a };
        }

        // To explain Join, imagine the functor acting twice.
        // For instance, from a given type T the list functor will first 
        // construct the type [T] (list of T), and then [[T]] (list of list of T).
        // Join removes one layer of “listiness” by joining the sub-lists.
        //
        // join :: (Monad m) => m (m a) -> m a
        //
        static List<A> MJoin<A>(List<List<A>> a)
        {
            List<A> res = new List<A>();
            foreach (List<A> x in a)
            {
                foreach (A y in x)
                {
                    res.Add(y);
                }
            }
            return res;
        }

        //
        // (>>=) :: (Monad m) => m a -> (a -> m b) -> m b
        //
        // join :: Monad m => m (m a) -> m a
        //
        // Equivalence between bind and join
        //
        // join x = x >>= id
        //
        // x >>= f = join (fmap f x)
        //

        static List<B> MBind<A,B>(List<List<A>> a, Func<List<A>, List<B>> selector)
        {
            var res = new List<B>();
            foreach (var x in a)
            {
                var sel = selector(x);
                foreach (var y in sel)
                {
                    res.Add(y);
                }
            }
            return res;
        }

        //
        // IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source,
        //                                                      Func<TSource, IEnumerable<TResult>> selector)
        //
        static List<B> SelectMany<A,B>(List<List<A>> a, Func<List<A>,List<B>> selector)
        {
            var res = new List<B>();
            foreach (var x in a)
            {
                var sel = selector(x);
                foreach (var y in sel)
                {
                    res.Add(y);
                }
            }
            return res;
        }

    }
}
