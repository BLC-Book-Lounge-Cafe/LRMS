using System.Linq.Expressions;

namespace LRMS.Infrastructure.Util;

public static partial class ExpressionBuilder
{
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> func1, Expression<Func<T, bool>> func2)
    {
        return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(System.Linq.Expressions.Expression.AndAlso(func1.Body, GetInvocationExpression(func1, func2)), func1.Parameters);
    }

    private static InvocationExpression GetInvocationExpression<T>(Expression<Func<T, bool>> func1, Expression<Func<T, bool>> func2)
    {
        return System.Linq.Expressions.Expression.Invoke(func2, func1.Parameters.Cast<System.Linq.Expressions.Expression>());
    }
}
