namespace OperativeService.Infrastructure.Utils.Extensions
{
    using System.Linq.Expressions;

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() => f => true;

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> firstExpr,
            Expression<Func<T, bool>> secondExpr)
        {
            var parameter = Expression.Parameter(typeof(T));
            var combined = Expression.AndAlso(
                Expression.Invoke(firstExpr, parameter),
                Expression.Invoke(secondExpr, parameter));

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }
    }
}
