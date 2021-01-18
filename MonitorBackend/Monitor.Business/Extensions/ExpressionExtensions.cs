using System;
using System.Linq;
using System.Linq.Expressions;

namespace Monitor.Business.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> OR<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> nextExp)
        {
            var invokedExpr = Expression.Invoke(nextExp, exp.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(exp.Body, invokedExpr), exp.Parameters);
        }

        public static Expression<Func<T, bool>> AND<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> nextExp)
        {
            var invokedExpr = Expression.Invoke(nextExp, exp.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(exp.Body, invokedExpr), exp.Parameters);
        }
    }
}
