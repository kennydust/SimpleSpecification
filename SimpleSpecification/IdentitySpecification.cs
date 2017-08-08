using System;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleSpecification
{
	// Null object design pattern
	internal sealed class IdentitySpecification<T> : Specification<T>
	{
		public override Expression<Func<T, bool>> ToExpression()
		{
			return x => true;
		}
	}
}