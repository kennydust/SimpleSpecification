using System;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleSpecification
{
	public abstract class Specification<T>
	{
		public static readonly Specification<T> All = new IdentitySpecification<T>(); // null object pattern
		public abstract Expression<Func<T, bool>> ToExpression();

		public bool IsSatisfiedBy(T entity)
		{
			var predicate = ToExpression().Compile();
			return predicate(entity);
		}

		// Composite specifications
		public Specification<T> And(Specification<T> specification)
		{
			// check for All specifications
			if (this == All)
				return specification;

			if (specification == All)
				return this;

			return new AndSpecification<T>(this, specification);
		}

		public Specification<T> Or(Specification<T> specification)
		{
			// check for All specifications
			if (this == All || specification == All)
				return All;

			return new OrSpecification<T>(this, specification);
		}

		public Specification<T> Not(Specification<T> specification)
		{
			return new NotSpecification<T>(specification);
		}
	}
}