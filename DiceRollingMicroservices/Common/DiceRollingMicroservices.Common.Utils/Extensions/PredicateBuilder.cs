namespace DiceRollingMicroservices.Common.Utils.Extensions
{
    using System.Linq.Expressions;

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() => x => true;

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> firstExpr,
            Expression<Func<T, bool>> secondExpr)
        {
            var parameter = Expression.Parameter(typeof(T));

            var left = new ParameterReplacer(parameter).Visit(firstExpr.Body);
            var right = new ParameterReplacer(parameter).Visit(secondExpr.Body);

            var combined = Expression.AndAlso(left!, right!);
            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;
            public ParameterReplacer(ParameterExpression parameter) => _parameter = parameter;

            protected override Expression VisitParameter(ParameterExpression node) => _parameter;
        }
    }
}
