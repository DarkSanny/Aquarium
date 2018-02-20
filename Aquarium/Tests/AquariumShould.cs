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
        private SimpleAquarium _aquarium;
        private GameObject _object;
        private readonly Size _defaultSize = new Size(1000, 1000);
		private const int _defaultFishCount = 4;
        private List<GameObject> _objects;
        private BlueNeon _blueNeon;
        private Catfish _catfish;
        private Swordfish _swordfish;
        private Piranha _piranha;
        private IObjectProvider _provider;

        [SetUp]
		public void SetUp ()
        {
            _object = A.Fake<GameObject>();
            _aquarium = new SimpleAquarium(_defaultSize);
            _blueNeon = new BlueNeon(_aquarium, new Point(10, 10), 0, new Size(10, 10));
            _catfish = new Catfish(_aquarium, new Point(10, 10), 0, new Size(10, 5));
            _swordfish = new Swordfish(_aquarium, new Point(10, 10), 0, new Size(20, 10));
            _piranha = new Piranha(_aquarium, new Point(10, 10), 0, new Size(10, 10));
            _provider = A.Fake<IObjectProvider>();
            _objects = new List<GameObject> { _blueNeon, _catfish, _swordfish, _piranha, _object };
            A.CallTo(() => _provider.GetObjects()).Returns(_objects);
            _aquarium.Start(_provider);
        }

        [Test]
        public void HaveSameSize()
        {
            _aquarium.GetSize().Should().Be(_defaultSize);
        }

        [Test]
		public void HaveSameCountFishes()
        {
            _aquarium.GetFishes().ToList().Count.Should().Be(_defaultFishCount);
        }

        [Test]
        public void HaveSameObjects()
        {
            _aquarium.GetObjects().ShouldAllBeEquivalentTo(_objects);
        }

        [Test]
        public void NotKillFishesBeforeUpdate_WhenTheyDied()
        {
            var fish = _aquarium.GetFishes().OfType<BlueNeon>().FirstOrDefault();
	        fish.Collision(_piranha);
	        _aquarium.GetFishes().ToList().Count.Should().Be(_defaultFishCount);
        }

        [Test]
		public void KillFishesAfterUpdate_WhenTheyDied()
        {
            var fish = _aquarium.GetFishes().OfType<BlueNeon>().FirstOrDefault();
	        fish.Collision(_piranha);
	        _aquarium.Update();
            _aquarium.GetFishes().ToList().Count.Should().Be(_defaultFishCount - 1);
        }

        [Test]
		public void HaveCorrectObjectCount()
        {
            _aquarium.GetObjects().ToList().Count.Should().Be(_objects.Count);
        }
	}
}
