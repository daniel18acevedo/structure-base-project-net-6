using System.Linq.Expressions;

namespace ExtenisonsService;
public static class ExpressionTreeExtension
{
    public static Expression<Func<TEntity, bool>> AddLeafToRightWithAnd<TEntity>(this Expression<Func<TEntity, bool>> oldExpression, params Expression<Func<TEntity, bool>>[] newExpressions)
    {
        Expression<Func<TEntity, bool>> combined = oldExpression;

        foreach (var newExpression in newExpressions)
        {
            Expression expressionCombined = Expression.AndAlso(new SwapVisitor(combined.Parameters.First(), newExpression.Parameters.First()).Visit(combined.Body), newExpression.Body);

            combined = Expression.Lambda<Func<TEntity, bool>>(expressionCombined, newExpression.Parameters.First());
        }

        return combined;
    }
}

//https://stackoverflow.com/questions/10613514/how-can-i-combine-two-lambda-expressions-without-using-invoke-method
internal class SwapVisitor : ExpressionVisitor
{
    private readonly Expression from, to;
    public SwapVisitor(Expression from, Expression to)
    {
        this.from = from;
        this.to = to;
    }
    public override Expression Visit(Expression node)
    {
        return node == from ? to : base.Visit(node);
    }
}