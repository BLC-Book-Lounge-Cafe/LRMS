using System.Linq.Expressions;

namespace LRMS.Infrastructure.Util;

internal interface IFilterExpressionBuilder<T>
{
    void AddFilter(Expression<Func<T, bool>> filter);
    Expression<Func<T, bool>> BuildExpression();
}
