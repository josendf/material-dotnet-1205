using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    // Monad
    // http://en.wikipedia.org/wiki/Monad_(functional_programming)
    // In functional programming, a monad is a kind of abstract data type constructor used 
    // to represent computations (instead of data in the domain model).
    // Monads allow the programmer to chain actions together to build a pipeline,
    // in which each action is decorated with additional processing rules provided by the monad.
    // Programs written in functional style can make use of monads to structure procedures that
    // include sequenced operations,[1][2] or to define some arbitrary control flows (like handling concurrency, 
    // continuations, side effects such as input/output, or exceptions).
    //
    //
    //
    // The usual formulation of a monad for programming is known as a Kleisli triple, and has the 
    // following components:
    //
    // A type construction that defines, for every underlying type, how to obtain a 
    // corresponding monadic type.
    // In Haskell's notation, the name of the monad represents the type constructor.
    // If M is the name of the monad and t is a data type, then "M t" is the corresponding type in the monad.
    //
    // A MUnit function that maps a value in an underlying type to a value in the corresponding monadic type.
    // The result is the "simplest" value in the corresponding type that completely preserves the original
    // value (simplicity being understood appropriately to the monad). In Haskell, this function is called return
    // due to the way it is used in the do-notation described later.
    // The MUnit function has the polymorphic type t→M t.
    //
    // A binding operation of polymorphic type (M t)→(t→M u)→(M u), which Haskell represents by the 
    // infix operator >>=. Its first argument is a value in a monadic type, its second argument is a 
    // function that maps from the underlying type of the first argument to another monadic type, and its
    // result is in that other monadic type. The binding operation can be understood as having four stages:
    // The monad-related structure on the first argument is "pierced" to expose any number
    // of values in the underlying type t.
    // The given function is applied to all of those values to obtain values of type (M u).
    // The monad-related structure on those values is also pierced, exposing values of type u.
    // Finally, the monad-related structure is reassembled over all of the results, giving a single 
    // value of type (M u).
    //
    //
    // Axioms
    // For a monad to behave correctly, the definitions must obey a few axioms.
    // [8] (The ≡ symbol is not Haskell code, but indicates an equivalence between two Haskell expressions.)
    // "return" acts approximately as a neutral element of >>=.
    // (return x) >>= f ≡ f x
    // m >>= return ≡ m
    //
    // Binding two functions in succession is the same as binding one 
    // function that can be determined from them.
    // (m >>= f) >>= g ≡ m >>= ( \x -> (f x >>= g) )
    // In the last rule, the notation \x -> defines an anonymous function that maps 
    // any value x to the expression that follows.
    // In mathematical notation, the axioms are:
    // 
    // (return x) >>= f  == f x
    //
    // m >>= return == m
    //
    // (m >>= f) >>= g == m >>= (\x . (f x >>= g) 
    //
    // 
    // Monads for the Curious Programmer
    // http://bartoszmilewski.wordpress.com/2011/01/09/monads-for-the-curious-programmer-part-1/
    // 
    // A monad is an endofunctor together with two special families of morphisms, both going vertically, 
    // one up and one down . The one going up is called Unit and the one going down is called Join.
    // 
    // Unit takes a value from the poorer type, then picks one value from the 
    // richer type, and pronounces the two roughly equivalent. 
    // Such a rough equivalent of True from the Bool object is the 
    // one-element list [True] from the [Bool] object.
    // Similarly, MUnit would map False into [False].
    // It would also map integer 5 into [5] and so on.
       
    // Unit can be though of as immersing values from a lower level into the higher level 
    // in the most natural way possible.
    // By the way, in programming we call a family of functions defined for any type 
    // a polymorphic function.
    // 
    // To explain Join, imagine the functor acting twice.
    // For instance, from a given type T the list functor will first 
    // construct the type [T] (list of T), and then [[T]] (list of list of T).
    // Join removes one layer of “listiness” by joining the sub-lists.
    // Plainly speaking, it just concatenates the inner lists.
    // Given, for instance, [[a, b], [c], [d, e]], it produces [a, b, c, d, e].
    // It’s a many-to-one mapping from the richer type to the poorer type.
    // 
    //              F(f)
    // D    F(A) -------------> F(B)
    //       ^                  ^
    //       |                  |
    //       |                  |
    //       |                  | 
    //       |        f         |
    // C     A  --------------> B
    // 



}
