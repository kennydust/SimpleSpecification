#region License
// Copyright (c) 2017 Kenny Lai
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

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