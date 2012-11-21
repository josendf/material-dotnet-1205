using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox
{
    /// <summary>
    /// Identity Monad
    /// </summary>
    /// <typeparam name="T">El tipo del valor contenido</typeparam>
    public class Identity<T>
    {
        public T Value { get; private set; }

        /// <summary>
        /// Construye una instancia de Identity
        /// </summary>
        /// <param name="value">El valor contenido</param>
        public Identity(T value)
        {
            Value = value;
        }
    }

    public static class IdentityExt
    {
        public static Identity<T> MReturn<T>(this T value)
        {
            return new Identity<T>(value);
        }

        public static Identity<B> MBind<A, B>(this Identity<A> a, Func<A, Identity<B>> func)
        {
            return func(a.Value);
        }

        public static Identity<C> SelectMany<A, B, C>(this Identity<A> a, 
                                                        Func<A, Identity<B>> func, Func<A, B, C> select)
        {
            return select(a.Value, func(a.Value).Value).MReturn();
        }
    }

    public static class MonadSamples
    {
        public static void Run()
        {
            Sample1();

            Sample2();
        }

        static void Sample1()
        {
            var result =
                "Abc".MReturn().MBind(a =>
                7.MReturn().MBind(b =>
                DateTime.Now.MReturn().MBind(c =>
                (a + "  " + b + "  " + c).MReturn()
                )));

            Console.WriteLine(result.Value);
        }

        static void Sample2()
        {
            var result =
                from a in "Abc".MReturn()
                from b in 7.MReturn()
                from c in DateTime.Now.MReturn()
                select a + " " + b + " " + c;

            Console.WriteLine(result.Value);
        }
    }
}
