namespace Sandbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            //
            // Monoides básicos
            // 
            
            // Un monoide formado por:
            // los números enteros, la suma y el valor cero.
            TestIntSumMonoid();

            // Un monoide formado por:
            // los números enteros, la multiplicación y el valor 1.
            TestIntMultMonoid();

            // Un monoide formado por:
            // las cadenas, la concatenación y la cadena vacía.
            TestStringConcatMonoid();

            // Un monoide formado por:
            // las listas, la concatenación y la lista vacía.
            TestListConcatMonoid();

            // Un monoide formado por:
            // las listas ordenadas, la adición y la lista vacía.
            TestOrderedListConcatMonoid();

            // Un monoide formado por:
            // el contenedor Set, la unión y el conjunto vacío.
            TestSetUnionMonoid();


            //
            // No - monoides
            // 

            // Un no-monoide formado por los números enteros, la resta y el valor 0.
            // La resta no es asociativa.
            // (3 - 2) - 1 = 0
            // 3 - (2 - 1) = 2
            TestIntSubNotMonoid();

            // La suma de números enteros negativos.
            // Obtenemos una de las posibles respuestas de la resta. 
            // 
            // (3 - 2) - 1 = 0
            // 3 - (2 - 1) = 2
            //
            // (3 + (-2)) + (-1) = 0
            // 3 + ((-2)) + (-1)) = 0
            TestIntSumNegMonoid();

            // Un no-monoide formado por los valores double, 
            // la siguiente regla de combinación:
            //  • la división cuando el denominador es diferente de 0 
            //  • de lo contrario Double.NaN
            // y el valor 1 como identidad.
            //
            // La división no es asociativa.
            // (4 / 2) / 3 = 0.6666666666666666
            // 4 / (2 / 3) = 6
            TestDoubleDivNotMonoid();

            // La multiplicación de valores inversos.
            // Obtenemos una de las posibles respuestas de la división. 
            // 
            // (4 / 2) / 3 = 0.6666666666666666
            // 4 / (2 / 3) = 6
            // 
            // (4 * (1/2)) * (1/3) = 0.6666666666666666
            // 4 * ((1/2) * (1/ 3)) = 0.6666666666666666
            TestDoubleMultInvMonoid();

            // Cuando tratamos con monoides podemos paralelizar
            // la combinación gracias a la asociatividad
            TestIntSumParallel();

            //
            // Mundo real
            //
            
            // Operaciones de agregado (SUM)
            TestOrderSum();
            TestOrderSumParallel();

            // Unión de conjuntos (DISTINCT)
            TestProductDistinct();
            TestProductDistinctParallel();

            // Operaciones de agrupado (GROUP BY)
            TestOrdersByProduct();
            TestOrdersByProductParallel();
        }

        static void TestIntSumMonoid()
        {
            var id = IntSumMonoid.Empty;
            
            var a = new IntSumMonoid(1);
            var b = new IntSumMonoid(2);
            var c = new IntSumMonoid(3);

            Test.Check(id, a, b, c);
        }

        static void TestIntMultMonoid()
        {
            var id = IntProdMonoid.Empty;
            
            var a = new IntProdMonoid(1);
            var b = new IntProdMonoid(2);
            var c = new IntProdMonoid(3);

            Test.Check(id, a, b, c);
        }

        static void TestDoubleDivNotMonoid()
        {
            var id = DoubleDivNotMonoid.Empty;

            var a = new DoubleDivNotMonoid(30);
            var b = new DoubleDivNotMonoid(20);
            var c = new DoubleDivNotMonoid(10);

            Test.Check(id, a, b, c);
        }

        static void TestDoubleMultInvMonoid()
        {
            var id = DoubleMultMonoid.Empty;

            var a = new DoubleMultMonoid(30.0);
            var b = new DoubleMultMonoid(1.0 / 20.0);
            var c = new DoubleMultMonoid(1.0 / 10.0);

            Test.Check(id, a, b, c);
        }

        static void TestIntSubNotMonoid()
        {
            var id = IntSubNotMonoid.Empty;

            var a = new IntSubNotMonoid(3);
            var b = new IntSubNotMonoid(2);
            var c = new IntSubNotMonoid(1);

            Test.Check(id, a, b, c);
        }

        static void TestIntSumNegMonoid()
        {
            var id = IntSumMonoid.Empty;

            var a = new IntSumMonoid(3);
            var b = new IntSumMonoid(-2);
            var c = new IntSumMonoid(-1);

            Test.Check(id, a, b, c);
        }

        static void TestStringConcatMonoid()
        {
            var id = StringConcatMonoid.Empty;

            var a = new StringConcatMonoid("abc");
            var b = new StringConcatMonoid("de");
            var c = new StringConcatMonoid("f");

            Test.Check(id, a, b, c);
        }

        static void TestListConcatMonoid()
        {
            var id = ListConcatMonoid.Empty;

            var a = new ListConcatMonoid(new List<string>() { "a", "b", "c" });
            var b = new ListConcatMonoid(new List<string>() { "d", "e" });
            var c = new ListConcatMonoid(new List<string>() { "f" });

            Test.Check(id, a, b, c);
        }

        static void TestOrderedListConcatMonoid()
        {
            var id = OrderedListConcatMonoid.Empty;

            var a = new OrderedListConcatMonoid(new string[] { "b", "c", "a" });
            var b = new OrderedListConcatMonoid(new string[] { "d", "f", "e" });
            var c = new OrderedListConcatMonoid(new string[] { "g", "h" });

            Test.Check(id, a, b, c);
        }


        static void TestSetUnionMonoid()
        {
            var id = SetUnionMonoid.Empty;

            var a = new SetUnionMonoid(new HashSet<string>() { "a", "b", "c" });
            var b = new SetUnionMonoid(new HashSet<string>() { "d", "e", "a" });
            var c = new SetUnionMonoid(new HashSet<string>() { "f" });

            Test.Check(id, a, b, c);
        }

        static void TestIntSumParallel()
        {
            int count = 10000;

            // Los datos de entrada
            // a b c d e f .....
            // let data_ = [1..10000]
            var data = Enumerable
                .Range(1, count);

            // Asociamos los datos con la regla de combinación
            var input = data
                .Select(x => new IntSumMonoid(x) as Monoid<int>)
                .ToList();

            // Reducción secuencial
            // a (•) b (•) c (•) d ....
            //
            // a (+) b (+) c (+) d ....
            //
            // import Data.List
            // foldl' (+) 0 input
            var seqRes = input.Aggregate(IntSumMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("seqRes: {0}", seqRes);

            // Reducción en paralelo
            //
            // Podemos asociar en grupos arbitrarios y después combinar
            // los resultados parciales.
            //
            // Si además el monoide es conmutativo no importa el orden de
            // combinación de los resultados parciales.
            //
            // (a • b) • (c • d • e)....
            //  cpu1       cpu2
            //  srv1       srv2
            //
            // Ejemplo ilustrativo, pueden ser varios servidores.
            // Ver DryadLINQ
            // http://research.microsoft.com/en-us/collaboration/tools/dryad.aspx
            // Some sample programs written in DryadLINQ
            // http://research.microsoft.com/apps/pubs/default.aspx?id=66811
            //
            var parRes = input.AsParallel().Aggregate(IntSumMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("parRes: {0}", parRes);
            Console.WriteLine("seqRes == parRes: {0}", seqRes == parRes);
            Console.WriteLine();
        }

        static void TestOrderSum()
        {
            var id = OrderValuesSumMonoid.Empty;

            var a = new OrderValuesSumMonoid(new OrderValues(1, 2.5));
            var b = new OrderValuesSumMonoid(new OrderValues(2, 5));
            var c = new OrderValuesSumMonoid(new OrderValues(3, 7.5));

            Test.Check(id, a, b, c);
        }

        static void TestOrderSumParallel()
        {
            int count = 100;

            // Los datos de entrada
            var data = OrderValues
                .Samples(count);

            // Asociamos los datos con la regla de combinación
            var input = data
                .Select(x => new OrderValuesSumMonoid(x) as Monoid<OrderValues>)
                .ToList();

            // Reducción secuencial
            var seqRes = input.Aggregate(OrderValuesSumMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("seqRes: {0}", seqRes);

            // Reducción en paralelo
            // Ejemplo ilustrativo, pueden ser varios servidores.
            var parRes = input.AsParallel().Aggregate(OrderValuesSumMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("parRes: {0}", parRes);
            Console.WriteLine("seqRes == parRes: {0}", seqRes == parRes);
            Console.WriteLine();
        }

        static void TestProductDistinct()
        {
            var id = ProductDistinctMonoid.Empty;

            var a = new ProductDistinctMonoid(new ProductSet("A","B","C"));
            var b = new ProductDistinctMonoid(new ProductSet("B", "C"));
            var c = new ProductDistinctMonoid(new ProductSet("D", "E", "F"));

            Test.Check(id, a, b, c);
        }

        static void TestProductDistinctParallel()
        {
            int count = 100;

            // Los datos de entrada
            var data = ProductSet
                .Samples(count);

            // Asociamos los datos con la regla de combinación
            var input = data
                .Select(x => new ProductDistinctMonoid(x) as Monoid<ProductSet>)
                .ToArray();

            // Reducción secuencial
            var seqRes = input.Aggregate(ProductDistinctMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("seqRes: {0}", seqRes);

            // Reducción en paralelo
            // Ejemplo ilustrativo, pueden ser varios servidores.
            var parRes = input.AsParallel().Aggregate(ProductDistinctMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("parRes: {0}", parRes);
            Console.WriteLine("seqRes == parRes: {0}", seqRes == parRes);
            Console.WriteLine();
        }

        static void TestOrdersByProduct()
        {
            var id = OrdersByProductMonoid.Empty;

            var a = new OrdersByProductMonoid(new OrdersByProduct(new Order[]{ 
                new Order("O1","P1,1,1.5"), new Order("O2","P2,2,2.5") }));
            var b = new OrdersByProductMonoid(new OrdersByProduct(new Order[] {
                new Order("O3","P1,1,1.5"), new Order("O4","P1,1,1.5\nP2,2,2.5"), new Order("O5","P3,3,3.5")  }));
            var c = new OrdersByProductMonoid(new OrdersByProduct(new Order[] {
                new Order("O6","P1,1,1.5")  }));

            Test.Check(id, a, b, c);
        }

        static void TestOrdersByProductParallel()
        {
            int count = 100;

            // Los datos de entrada
            var data = OrdersByProduct
                .Samples(count);

            // Asociamos los datos con la regla de combinación
            var input = data
                .Select(x => new OrdersByProductMonoid(x) as Monoid<OrdersByProduct>)
                .ToArray();

            // Reducción secuencial
            var seqRes = input.Aggregate(OrdersByProductMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("seqRes: {0}", seqRes);

            // Reducción en paralelo
            // Ejemplo ilustrativo, pueden ser varios servidores.
            var parRes = input.AsParallel().Aggregate(OrdersByProductMonoid.Empty, (a, b) => a * b);

            Console.WriteLine("parRes: {0}", parRes);
            Console.WriteLine("seqRes == parRes: {0}", seqRes == parRes);
            Console.WriteLine();
        }
    }
}
