using System.Linq.Expressions;

namespace LRMS.Infrastructure.Util;

internal class FilterExpressionBuilder<T> : IFilterExpressionBuilder<T>
{
    private Expression<Func<T, bool>> CompositeFilter { get; set; } = t => true;
    public void AddFilter(Expression<Func<T, bool>> filter)
    {
        CompositeFilter = ExpressionBuilder.AndAlso(CompositeFilter, filter);
    }

    public Expression<Func<T, bool>> BuildExpression()
    {
        return CompositeFilter;
    }
}
