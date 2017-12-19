using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System.Drawing;
using System.Linq;
using Aquarium.Fishes;
using Aquarium.Aquariums;

namespace Aquarium.Tests
{
	[TestFixture]
	public class AquariumShould
	{
        private SimpleAquarium _aquarium;
        private GameObject _object;
        private readonly Size _defaultSize = new Size(1000, 1000);
		private const int DefaultFishCount = 4;

		[SetUp]
        public void SetUp ()
        {
            _aquarium = new SimpleAquarium(_defaultSize, DefaultFishCount);
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
            _aquarium.GetFishes().ToList().Count.Should().Be(DefaultFishCount);
        }

        [Test]
        [Ignore("Not implemented")]
		public void NotKillFishesBeforeUpdate_WhenTheyDied()
        {
            var fish = _aquarium.GetFishes().FirstOrDefault();
            var fish2 = A.Fake<Fish>();
	        //fish?.Collision(ObjectType.Piranha, fish2);
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
            _aquarium.AddGameObject(_object);
            _aquarium.GetObjects().ToList().Count.Should().Be(DefaultFishCount + 1);
        }

	}
}
