using System;
using System.Linq.Expressions;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class LambdaExtensao
    {
        public static Expression<Func<T, bool>> ETambem<T>(this Expression<Func<T, bool>> expressao1, Expression<Func<T, bool>> expressao2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var inspetorEsquerda = new ReplaceExpressionVisitor(expressao1.Parameters[0], parameter);
            var esquerda = inspetorEsquerda.Visit(expressao1.Body);

            var inspetorDireita = new ReplaceExpressionVisitor(expressao2.Parameters[0], parameter);
            var direita = inspetorDireita.Visit(expressao2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(esquerda, direita), parameter);
        }

        internal class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;
            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }
}