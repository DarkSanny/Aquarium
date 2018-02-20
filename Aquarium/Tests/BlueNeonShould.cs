<<<<<<< HEAD
﻿using System.Drawing;
using Aquarium.Aquariums;
=======
﻿using System.Collections.Generic;
using System.Drawing;
>>>>>>> 434b59ca8a11ada678b999d53a55121cd5b064b5
using Aquarium.Fishes;
using FakeItEasy;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
<<<<<<< HEAD
using Aquarium.Brains;
using System;
using System.Linq;
using System.Collections.Generic;
=======
using System;
using Aquarium.Brains;
>>>>>>> 434b59ca8a11ada678b999d53a55121cd5b064b5

namespace Aquarium.Tests
{
    [TestFixture]
    public class BlueNeonShould
    {
        private IAquarium _aquarium;
        private Point _startPosition;
        private double _startDirection;
        private BlueNeon _neon;
        private Size _defaultSize;
        private Size _defaulNeontSize;
        private IEnumerable<GameObject> _objects;
        private BlueNeonBrain _brain;

        [SetUp]
        public void SetUp()
        {
            _aquarium = A.Fake<IAquarium>();
            _startPosition = new Point(10, 10);
            _startDirection = 0;
            _defaultSize = new Size(100, 50);
            _defaulNeontSize = new Size(20, 10);
            _neon = new BlueNeon(_aquarium, _startPosition, _startDirection, _defaulNeontSize);
            A.CallTo(() => _aquarium.GetSize()).Returns(_defaultSize);
            _objects = new List<GameObject> { _neon };
            A.CallTo(() => _aquarium.GetObjects()).Returns(_objects);
            A.CallTo(() => _aquarium.GetFishes()).Returns(_objects.OfType<Fish>());
            _brain = new BlueNeonBrain(_neon, _aquarium);
        }

        [Test]
        public void HaveSameStartDirection()
        {
            _neon.Direction.Should().Be(_startDirection);
        }

        [Test]
        public void HaveSameRectangle()
        {
            _neon.Rectangle().Should().Be(new Rectangle(0, 5, 20, 10));
        }

        [Test]
        public void HaveSameNeonSize()
        {
            _neon.GetSize().Should().Be(_defaulNeontSize);
        }

        [Test]
        public void HaveSameStartPosition()
        {
            _neon.GetLocation().Should().Be(_startPosition);
        }
    }
}
