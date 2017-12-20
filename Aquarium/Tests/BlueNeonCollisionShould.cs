using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Fishes;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Aquarium.Tests
{
	[TestFixture]
	public class BlueNeonCollisionShould
	{
		private IAquarium _aquarium;
		private Size _defaultSize;
		private BlueNeon _neon1;
		private Point _defaultPostition;

		[SetUp]
		public void SetUp()
		{
			_aquarium = A.Fake<IAquarium>();
			_defaultPostition = new Point(10, 10);
			_neon1 = new BlueNeon(_aquarium, _defaultPostition, 0, new Size(20, 10));
			_defaultSize = new Size(100, 100);
		}

		[Test]
		public void ShouldHaveCorrectType()
		{
			_neon1.GetCollisionType().Should().Be(ObjectType.BlueNeon);
		}

		[Test]
		public void NotCollise_WithNeon()
		{
			var neon = new BlueNeon(_aquarium, _defaultPostition, 0, _defaultSize);
			_neon1.IsShouldCollise(neon).Should().BeFalse();
		}

		[Test]
		public void NotCollise_WithPiranha()
		{
			var piranha = new Piranha(_aquarium, _defaultPostition, 0, _defaultSize);
			_neon1.IsShouldCollise(piranha).Should().BeFalse();
		}
		[Test]
		public void NotCollise_WithCatfish()
		{
			var catfish = new Catfish(_aquarium, _defaultPostition, 0, _defaultSize);
			_neon1.IsShouldCollise(catfish).Should().BeFalse();
		}
		[Test]
		public void NotCollise_WithSwordFish()
		{
			var swordfish = new Swordfish(_aquarium, _defaultPostition, 0, _defaultSize);
			_neon1.IsShouldCollise(swordfish).Should().BeFalse();
		}

		[Test]
		public void DieWhenColision()
		{
			var anyFishWishCollise = new Piranha(_aquarium, _defaultPostition, 0, _defaultSize);
			var counter = 0;
			_neon1.ShouldDie += () => counter++;
			_neon1.Collision(anyFishWishCollise);
			counter.Should().Be(1);
		}

	}
}