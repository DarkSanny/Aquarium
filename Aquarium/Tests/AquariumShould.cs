using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;
using Aquarium.Aquariums;
using System.Collections.Generic;

namespace Aquarium.Tests
{
	[TestFixture]
	public class AquariumShould
	{
        private IAquarium _aquarium;
        private GameObject _object;
        private readonly Size _defaultSize = new Size(1000, 1000);
		private const int DefaultFishCount = 2;
        private const int DefaultObgectCount = 2;
        private IEnumerable<GameObject> _objects;
        private Fish _fish;

        [SetUp]
		public void SetUp ()
        {
            _object = A.Fake<GameObject>();
            _fish = A.Fake<Fish>();
            _aquarium = A.Fake<IAquarium>();         
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _objects = new List<GameObject> { _object,_fish,_fish };
            A.CallTo(() => _aquarium.GetObjects()).Returns(_objects);
            A.CallTo(() => _aquarium.GetFishes()).Returns(_objects.OfType<Fish>());
        }

        [Test]
        public void HaveSameSize()
        {
            _aquarium.GetSize().Should().Be(_defaultSize);
        }

        [Test]
		public void HaveSameCountFishes()
        {
            _aquarium.GetFishes().ToList().Count.Should().Be(DefaultFishCount);
        }

        [Test]
        [Ignore("Not implemented")]
        public void NotKillFishesBeforeUpdate_WhenTheyDied()
        {
            var fish = _aquarium.GetFishes().FirstOrDefault();
            var fish2 = A.Fake<Fish>();
	       // fish?.Collision(ObjectType.Piranha);
	        _aquarium.GetFishes().ToList().Count.Should().Be(DefaultFishCount);
        }

        [Test]
        [Ignore("Not implemented")]
		public void KillFishesAfterUpdate_WhenTheyDied()
        {
            var fish = _aquarium.GetFishes().FirstOrDefault();
            var fish2 = A.Fake<Fish>();
	        //fish?.Collision(ObjectType.Piranha, fish2);
	        _aquarium.Update();
            _aquarium.GetFishes().ToList().Count.Should().Be(DefaultFishCount - 1);
        }

        [Test]
		public void HaveCorrectObjectCount()
        {
            _aquarium.GetObjects().ToList().Count.Should().Be(DefaultFishCount + 1);
        }

	}
}
