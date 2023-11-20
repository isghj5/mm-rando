using System;
using System.Linq.Expressions;

namespace MMR.Common.Extensions
{
    public static class ExpressionExtensions
    {
        private class Finder : ExpressionVisitor
        {
            private readonly Type declaringType;
            private readonly string toFind;
            public bool IsFound { get; private set; }

            public Finder(Type declaringType, string toFind)
            {
                this.declaringType = declaringType;
                this.toFind = toFind;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                IsFound |= node.Member.MemberType == System.Reflection.MemberTypes.Property && node.Member.DeclaringType == declaringType && node.Member.Name == toFind;
                return base.VisitMember(node);
            }
        }

        public static bool VisitsMember(this Expression expression, Type declaringType, string member)
        {
            var finder = new Finder(declaringType, member);
            finder.Visit(expression);
            return finder.IsFound;
        }
    }
}
