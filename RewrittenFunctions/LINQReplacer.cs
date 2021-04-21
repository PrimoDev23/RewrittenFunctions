using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class LINQReplacer
    {
        public static int FirstOrDefault(this IEnumerable<int> list, Func<int, bool> func)
        {
            foreach(var item in list)
            {
                if (func(item))
                {
                    return item;
                }
            }
            return default;
        }
    }
}
