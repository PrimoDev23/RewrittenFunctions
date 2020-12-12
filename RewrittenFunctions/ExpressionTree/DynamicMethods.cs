using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions.ExpressionTree
{
    /// <summary>
    /// Get specific methods in class without reflection
    /// </summary>
    /// <typeparam name="T">Containing class type</typeparam>
    public static class DynamicMethods<T>
    {
        /// <summary>
        /// Get the delegate for a method in a class type
        /// </summary>
        /// <param name="methodName">Methods name</param>
        /// <returns></returns>
        public static Delegate GetMethod(string methodName)
        {
            MethodInfo info = typeof(T).GetMethod(methodName);

            ParameterExpression class_param = Expression.Parameter(typeof(T), "class");

            var method_params = info.GetParameters().Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToList();

            MethodCallExpression call = null;

            if (info.IsStatic)
            {
                call = Expression.Call(null, info, method_params);
            }
            else
            {
                call = Expression.Call(class_param, info, method_params);
                method_params.Insert(0, class_param);
            }

            return Expression.Lambda(call, false, method_params).Compile();
        }
    }
}