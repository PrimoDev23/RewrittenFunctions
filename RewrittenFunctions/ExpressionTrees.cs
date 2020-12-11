using FastExpressionCompiler.LightExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions
{
    public static class ExpressionTrees
    {
        public static class GetSet<T, T2>
        {
            public static Func<T, T2> GetGetterProperty(string propertyName)
            {
                ParameterExpression param = Expression.Parameter(typeof(T), "param");
                return Expression.Lambda<Func<T, T2>>(Expression.Property(param, propertyName), param).CompileFast();
            }

            public static Func<T, T2> GetGetterField(string fieldName)
            {
                ParameterExpression param = Expression.Parameter(typeof(T), "param");
                return Expression.Lambda<Func<T, T2>>(Expression.Field(param, fieldName), param).CompileFast();
            }

            public static Action<T, T2> GetSetterProperty(string propertyName)
            {
                ParameterExpression param = Expression.Parameter(typeof(T), "param");
                ParameterExpression newValue = Expression.Parameter(typeof(T2), "newValue");

                MemberExpression property = Expression.Property(param, typeof(T).GetProperty(propertyName));
                BinaryExpression assign = Expression.Assign(property, newValue);

                return Expression.Lambda<Action<T, T2>>(assign, param, newValue).CompileFast();
            }

            public static Action<T, T2> GetSetterField(string fieldName)
            {
                var info = typeof(T).GetField(fieldName);

                ParameterExpression param = Expression.Parameter(typeof(T), "param");
                ParameterExpression newValue = Expression.Parameter(typeof(T2), "newValue");

                MemberExpression field = Expression.Field(param, info);
                BinaryExpression assign = Expression.Assign(field, newValue);

                return Expression.Lambda<Action<T, T2>>(assign, param, newValue).CompileFast();
            }
        }
    }
}