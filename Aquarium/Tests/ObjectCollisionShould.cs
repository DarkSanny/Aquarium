using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Aquarium.Tests
{
	[TestFixture]
	public class ObjectCollisionShould
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
			_neon1 = new BlueNeon(_aquarium, new Point(10, 10), 0);
			_neon2 = new BlueNeon(_aquarium, new Point(11, 11), 0);
			_neon3 = new BlueNeon(_aquarium, new Point(12, 12), 0);
			_defaultSize = new Size(100, 100);
			A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
			_objects = new List<GameObject> { _neon1, _neon2, _neon3 };
			A.CallTo(() => _aquarium.GetObjects()).Returns(_objects);
			A.CallTo(() => _aquarium.GetFishes()).Returns(_objects.OfType<Fish>());
			var typeNeon1 = _neon1.GetType();
			typeNeon1.GetProperty("IsLeader")?.SetValue(_neon1, true);
		}


		[Test]
		public void ShouldCollise_WhenLeaderTryColliseNotLeader()
		{
			_neon1.IsShouldCollise(_neon2.GetCollisionType()).Should().BeTrue();
		}

		[Test]
		public void ShouldNotCollise_WhenNotLeaderTryColliseLeader()
		{
			_neon2.IsShouldCollise(_neon1.GetCollisionType()).Should().BeFalse();
		}

		[Test]
		public void ShouldNotCollise_WhenNotLeaderTryColliseNotLeader()
		{
			_neon3.IsShouldCollise(_neon2.GetCollisionType()).Should().BeFalse();
		}
	}
}