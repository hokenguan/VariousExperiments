using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTry
{
    class Program
    {
        static void Main(string[] args)
        {
            //Expression<Func<string, string>> exp = o => o;
            //var list = Expression.Parameter(typeof(IEnumerable<Outer>), "list");

            //MethodCallExpression orderByExp = Expression.Call(typeof(Enumerable), "OrderBy",
            //    new Type[] { typeof(String), exp.Body.Type }, list, exp);
            //var lambda = Expression.Lambda<Func<IEnumerable<String>, IEnumerable<String>>>(orderByExp, list);
            //var data = new String[] { "asdasdasd", "asdads", "123", "xcvxcvs", "ASDSD" };
            //var result = lambda.Compile()(data);

            List<Outer> items = new List<Outer> { new Outer { B = 1, InnerObj = new Inner { A = 2 } }, new Outer { B = 3, InnerObj = new Inner { A = 4 } }, new Outer { B = 3, InnerObj = new Inner { A = 6 } } };

            Expression ex_x = Expression.Parameter(typeof (Outer), "x");
            Expression ex_y = Expression.Parameter(typeof (Outer), "y");
            Expression ex_Bofx = Expression.PropertyOrField(ex_x, "B");
            
            //var method = typeof (Enumerable).GetMethods().First(x => x.Name == "OrderByDescending");
            Expression final = Expression.Call(typeof(Enumerable),"OrderBy", new []{typeof(Outer),typeof(int)}, ex_x, ex_Bofx );
            items.OrderByDescending<Outer,int>(x=>x.B);

            foreach (var aaa in items.AsQueryable().Provider.CreateQuery<Outer>(final))
            {
                Console.WriteLine(aaa.B + ";" + aaa.InnerObj.A);
            }
            Console.ReadKey();
        }
    }

    public class Inner
    {
        public int A;
    }

    public class Outer
    {
        public int B;
        public Inner InnerObj;
    }
}
