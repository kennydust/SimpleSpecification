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

using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace SimpleSpecification.Tests
{
	/// <summary>
	/// Contrived examples, will work on this.
	/// </summary>
	public class SimpleSpecificationTests
	{
		[Test]
		public void dog_specification_should_be_true()
		{
			var dog = new Dog();
			Assert.True(new BarkingDogSpecification().IsSatisfiedBy(dog));
		}

		[Test]
		public void dog_should_bark_and_not_meow()
		{
			var dog = new Dog();
			Assert.True(new BarkingDogSpecification()
				.Not(new MeowingDogSpecification())
				.IsSatisfiedBy(dog));
		}
	}

	internal sealed class BarkingDogSpecification : Specification<Dog>
	{
		public override Expression<Func<Dog, bool>> ToExpression()
		{
			return x => x.Canbark();
		}
	}

	internal sealed class MeowingDogSpecification : Specification<Dog>
	{
		public override Expression<Func<Dog, bool>> ToExpression()
		{
			return x => x.CanMeow();
		}
	}

	internal sealed class Dog
	{
		public bool Canbark()
		{
			return true;
		}

		public bool CanMeow()
		{
			return false;
		}
	}
}