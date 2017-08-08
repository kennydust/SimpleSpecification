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