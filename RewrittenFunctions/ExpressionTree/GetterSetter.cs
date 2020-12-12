using FastExpressionCompiler.LightExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions.ExpressionTree
{
    /// <summary>
    /// Basic ExpressionTrees to get getter and setter methods of properies and fields
    /// This is mostly used to replace reflection
    /// </summary>
    /// <typeparam name="T">Containing class type</typeparam>
    /// <typeparam name="T2">return / set value type</typeparam>
    public static class GetterSetter<T, T2>
    {
        /// <summary>
        /// Get the getter method for a property
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>Getter method</returns>
        public static Func<T, T2> GetGetterProperty(string propertyName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "param");
            return Expression.Lambda<Func<T, T2>>(Expression.Property(param, propertyName), param).CompileFast();
        }

        /// <summary>
        /// Get the getter method for a field
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <returns>Getter method</returns>
        public static Func<T, T2> GetGetterField(string fieldName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "param");
            return Expression.Lambda<Func<T, T2>>(Expression.Field(param, fieldName), param).CompileFast();
        }

        /// <summary>
        /// Get the setter method for a property
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>Setter method</returns>
        public static Action<T, T2> GetSetterProperty(string propertyName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "param");
            ParameterExpression newValue = Expression.Parameter(typeof(T2), "newValue");

            MemberExpression property = Expression.Property(param, propertyName);
            BinaryExpression assign = Expression.Assign(property, newValue);

            return Expression.Lambda<Action<T, T2>>(assign, param, newValue).CompileFast();
        }

        /// <summary>
        /// Get the setter method for a field
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <returns>Setter method</returns>
        public static Action<T, T2> GetSetterField(string fieldName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "param");
            ParameterExpression newValue = Expression.Parameter(typeof(T2), "newValue");

            MemberExpression field = Expression.Field(param, fieldName);
            BinaryExpression assign = Expression.Assign(field, newValue);

            return Expression.Lambda<Action<T, T2>>(assign, param, newValue).CompileFast();
        }
    }
}