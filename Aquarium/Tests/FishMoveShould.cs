using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Fishes;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Aquarium.Brains;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Aquarium.Tests
{
    [TestFixture]
    class FishShould
    {
        private SimpleAquarium _aquarium;
        private Point _startPosition = new Point(10, 10);
        private Size _defaultSize = new Size(100, 50);
        private Size _defaulNeontSize = new Size(20, 10);
        private IObjectProvider _objects;

        [SetUp]
        public void SetUp()
        {
            _aquarium = new SimpleAquarium(_defaultSize);
            _objects = new ObjectRandomizer(_aquarium);
        }
        [Test]
        public void HaveSameMoveLeft()
        {
            var _fish = new BlueNeon(_aquarium, _startPosition, Math.PI, _defaulNeontSize);
            _objects.GetObjects().Add(_fish);
            _aquarium.Start(_objects);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X - (int)_fish.Speed, _startPosition.Y));
        }

        [Test]
        public void HaveSameMoveUp()
        {
            var _fish = new BlueNeon(_aquarium, _startPosition, Math.PI / 2, _defaulNeontSize);
            _objects.GetObjects().Add(_fish);
            _aquarium.Start(_objects);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X, _startPosition.Y + (int)_fish.Speed));
        }

        [Test]
        public void HaveSameMoveRight()
        {
            var _fish = new BlueNeon(_aquarium, _startPosition, 0, _defaulNeontSize);
            _objects.GetObjects().Add(_fish);
            _aquarium.Start(_objects);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X + (int)_fish.Speed, _startPosition.Y));
        }

        [Test]
        public void HaveSameMoveDown()
        {
            var _fish = new Piranha(_aquarium, _startPosition, -Math.PI / 2, _defaulNeontSize);
            _objects.GetObjects().Add(_fish);
            _aquarium.Start(_objects);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X, _startPosition.Y - (int)_fish.Speed));
        }
    }
}
