using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
		private BlueNeon _neon2;
		private BlueNeon _neon3;
		private IEnumerable<GameObject> _objects;

		[SetUp]
		public void SetUp()
		{
			_aquarium = A.Fake<IAquarium>();
			_neon1 = new BlueNeon(_aquarium, new Point(10, 10), 0, new Size(20, 10));
			_neon2 = new BlueNeon(_aquarium, new Point(11, 11), 0, new Size(20, 10));
			_neon3 = new BlueNeon(_aquarium, new Point(12, 12), 0, new Size(20, 10));
            _defaultSize = new Size(100, 100);
			A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
			_objects = new List<GameObject> { _neon1, _neon2, _neon3 };
			A.CallTo(() => _aquarium.GetObjects()).Returns(_objects);
			A.CallTo(() => _aquarium.GetFishes()).Returns(_objects.OfType<Fish>());
		}


		[Test]
		public void Collise_WhenLeaderTryColliseNotLeader()
		{
            _neon1.IsShouldCollise(_neon2).Should().BeTrue();
        }

		[Test]
		public void NotCollise_WhenNotLeaderTryColliseLeader()
		{
            _neon2.IsShouldCollise(_neon1).Should().BeFalse();
        }

		[Test]
		public void NotCollise_WhenNotLeaderTryColliseNotLeader()
		{
            _neon3.IsShouldCollise(_neon2).Should().BeFalse();
        }

		[Test]
		public void ColliseWithPiranha()
		{
            var piranha = A.Fake<Piranha>();
            _neon1.IsShouldCollise(piranha).Should().BeTrue();
        }

		[Test]
		[Ignore("Not implemented")]
		public void Die_WhenCollisionWithPiranha()
		{
			var piranha = A.Fake<Fish>();
			var counter = 0;
			_neon1.ShouldDie += () => counter++; 
			//_neon1.Collision(ObjectType.Piranha, piranha);
			counter.Should().Be(1);
		}
	}
}