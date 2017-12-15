using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;

namespace Aquarium.Tests
{
	[TestFixture]
	public class AquariumShould
	{
        private Aquarium _aquarium;
        private GameObject _object;
        private Size _defaultSize = new Size(100, 50);
        private int _defaultFishCount = 4;

        [SetUp]
        public void SetUp ()
        {
            _aquarium = new Aquarium(_defaultSize, _defaultFishCount);
            _object = A.Fake<GameObject>();
        }

        [Test]
        public void HaveSameSize()
        {
            _aquarium.GetSize().Should().Be(_defaultSize);
        }

        [Test]
        public void HaveSameCountFishes()
        {
            _aquarium.AddGameObject(_object);
            _aquarium.GetFishes().ToList().Count.Should().Be(_defaultFishCount);
        }

        [Test]
        public void NotKillFishesBeforeUpdate_WhenTheyDied()
        {
            var fish = _aquarium.GetFishes().FirstOrDefault();
            var fish2 = A.Fake<Fish>();
            fish.Collision(ObjectType.Piranha, fish2);
            _aquarium.GetFishes().ToList().Count.Should().Be(_defaultFishCount);
        }

        [Test]
        public void KillFishesAfterUpdate_WhenTheyDied()
        {
            var fish = _aquarium.GetFishes().FirstOrDefault();
            var fish2 = A.Fake<Fish>();
            fish.Collision(ObjectType.Piranha, fish2);
            _aquarium.Update();
            _aquarium.GetFishes().ToList().Count.Should().Be(_defaultFishCount - 1);
        }

        [Test]
        public void HaveCorrectObjectCount()
        {
            _aquarium.AddGameObject(_object);
            _aquarium.GetObjects().ToList().Count.Should().Be(_defaultFishCount + 1);
        }

	}
}
