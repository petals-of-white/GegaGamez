﻿using System.Linq.Expressions;

namespace GegaGamez.BLL;

internal static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> Combine<T>(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression, Func<Expression, Expression, BinaryExpression> combineOperator)
    {
        var leftParameter = leftExpression.Parameters [0];
        var rightParameter = rightExpression.Parameters [0];

        var visitor = new ReplaceParameterVisitor(rightParameter, leftParameter);

        var leftBody = leftExpression.Body;
        var rightBody = visitor.Visit(rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(combineOperator(leftBody, rightBody), leftParameter);
    }

    private class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _newParameter;
        private readonly ParameterExpression _oldParameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return ReferenceEquals(node, _oldParameter) ? _newParameter : base.VisitParameter(node);
        }

        public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }
    }

    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> leftExpression,
        Expression<Func<T, bool>> rightExpression)
    {
        return Combine(leftExpression, rightExpression, Expression.AndAlso);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> leftExpression,
        Expression<Func<T, bool>> rightExpression)
    {
        return Combine(leftExpression, rightExpression, Expression.Or);
    }
}
