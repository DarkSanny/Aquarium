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
        private IAquarium _aquarium;
        private Point _startPosition = new Point(10, 10);
        private Size _defaultSize = new Size(100, 50);
        private Size _defaulNeontSize = new Size(20, 10);

        [SetUp]
        public void SetUp()
        {
            _aquarium =A.Fake<IAquarium>();
        }
        [Test]
        public void MoveLeft_WhenDeirectionIsLeft()
        {
            var _fish = new BlueNeon(_aquarium, _startPosition, Math.PI, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X - (int)_fish.Speed, _startPosition.Y));
        }

        [Test]
        public void MoveUp_WhenDeirectionIsUp()
        {
            var _fish = new BlueNeon(_aquarium, _startPosition, Math.PI / 2, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X, _startPosition.Y + (int)_fish.Speed));
        }

        [Test]
        public void MoveRight_WhenDeirectionIsRight()
        {
            var _fish = new BlueNeon(_aquarium, _startPosition, 0, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X + (int)_fish.Speed, _startPosition.Y));
        }

        [Test]
        public void MoveDown_WhenDeirectionIsDown()
        {
            var _fish = new BlueNeon(_aquarium, _startPosition, -Math.PI / 2, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(_startPosition.X, _startPosition.Y - (int)_fish.Speed));
        }

        [Test]
        public void Move_WhenBorderIsRight()
        {
            var _fish = new BlueNeon(_aquarium, new Point(100,25), 0, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(100 - (int)_fish.Speed, 25));
        }

        [Test]
        public void Move_WhenBorderIsUp()
        {
            var _fish = new BlueNeon(_aquarium, new Point(50, 50), Math.PI / 2, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(50, 50 - (int)_fish.Speed));
        }

        [Test]
        public void Move_WhenBorderIsLeft()
        {
            var _fish = new BlueNeon(_aquarium, new Point(0, 25), Math.PI, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point((int)_fish.Speed, 25));
        }

        [Test]
        public void Move_WhenBorderIsDown()
        {
            var _fish = new BlueNeon(_aquarium, new Point(50, 0), -Math.PI/2, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _fish.Move();
            _fish.GetLocation().Should().Be(new Point(50, (int)_fish.Speed));
        }
    }
}
