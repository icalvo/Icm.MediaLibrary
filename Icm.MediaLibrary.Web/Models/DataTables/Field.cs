using System;
using System.Linq;
using System.Linq.Expressions;

namespace Icm.MediaLibrary.Web.Models
{
    public class Field<T, TKey> : IField<T>
    {
        private readonly Expression<Func<T, TKey>> expression;
        private readonly Func<TKey, string> getString;
        private readonly Expression<Func<T, string, bool>> searchExpression;
        private readonly Func<IQueryable<T>, string, IQueryable<T>> searchFunction;

        public Field(Expression<Func<T, TKey>> expression)
        {
            this.expression = expression;
            this.getString = key => key == null ? null : key.ToString();
        }

        public Field(Expression<Func<T, TKey>> expression, Expression<Func<T, string, bool>> searchExpression)
            : this(expression)
        {
            this.searchExpression = searchExpression;
        }
        public Field(Expression<Func<T, TKey>> expression, Func<IQueryable<T>, string, IQueryable<T>> searchFunction)
            : this(expression)
        {
            this.searchFunction = searchFunction;
        }

        public Field(Expression<Func<T, TKey>> expression, Func<TKey, string> getString)
        {
            this.expression = expression;
            this.getString = getString;
        }

        public Field(Expression<Func<T, TKey>> expression, Expression<Func<T, string, bool>> searchExpression, Func<TKey, string> getString)
            : this(expression)
        {
            this.searchExpression = searchExpression;
            this.getString = getString;
        }
        public Field(Expression<Func<T, TKey>> expression, Func<IQueryable<T>, string, IQueryable<T>> searchFunction, Func<TKey, string> getString)
            : this(expression)
        {
            this.searchFunction = searchFunction;
            this.getString = getString;
        }

        class ReplacementVisitor : ExpressionVisitor
        {
            private readonly Expression original, replacement;

            public ReplacementVisitor(Expression original, Expression replacement)
            {
                this.original = original;
                this.replacement = replacement;
            }

            public override Expression Visit(Expression node)
            {
                return node == original ? replacement : base.Visit(node);
            }
        }

        public IQueryable<T> Search(IQueryable<T> items, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return items;
            }

            if (this.searchFunction != null)
            {
                return this.searchFunction(items, searchString);
            }

            if (searchExpression == null)
            {
                return items;
            }

            return items.Where(Curry<Func<T, bool>>(searchExpression, 1, searchString));
        }

    private Expression<TLambda> Curry<TLambda>(
        LambdaExpression searchExpression, 
        int replacedParameterIndex, 
        object replacement)
    {
        var parameter = searchExpression.Parameters[replacedParameterIndex];
        var constant = Expression.Constant(replacement, parameter.Type);
        var visitor = new ReplacementVisitor(parameter, constant);
        var newBody = visitor.Visit(searchExpression.Body);
        var lambda = Expression.Lambda<TLambda>(newBody, searchExpression.Parameters.Except(new[] { parameter }));

        return lambda;
    }

        public IQueryable<T> Sort(IQueryable<T> items)
        {
            return items.OrderBy(this.expression);
        }

        public IQueryable<T> SortDescending(IQueryable<T> items)
        {
            return items.OrderByDescending(this.expression);
        }

        public string GetString(T item)
        {
            return this.getString(this.expression.Compile()(item));
        }
    }
}