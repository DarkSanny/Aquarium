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
             _objects = new List<GameObject> { _neon};
             A.CallTo(() => _aquarium.GetObjects()).Returns(_objects);
             A.CallTo(() => _aquarium.GetFishes()).Returns(_objects.OfType<Fish>());
             _brain = new BlueNeonBrain(_neon, _aquarium);
         }
 
         [Test]
         public void SimpleTestForExample()
         {
             _neon.GetCollisionType().Should().Be(ObjectType.BlueNeon);
         }
 
         [Test]
         public void HaveSameStartDirection()
         {
             _neon.Direction.Should().Be(_startDirection);
         }
 
         [Test]
         public void HaveSameRectangle()
         {
             _neon.Rectangle().Should().Be(new Rectangle(_startPosition.X - _defaulNeontSize.Width / 2,
                 _startPosition.Y - _defaulNeontSize.Height / 2, _defaulNeontSize.Width, _defaulNeontSize.Height));
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
 
         [Test]
         public void HaveSameMoveLeft()
         {
             var _neon1 = new BlueNeon(_aquarium, _startPosition, Math.PI, _defaulNeontSize);
             var _object = new List<GameObject> { _neon1 };
             A.CallTo(() => _aquarium.GetFishes()).Returns(_object.OfType<Fish>());
             _neon1.Move();
             _neon1.GetLocation().Should().Be(new Point(_startPosition.X - (int) _neon1.Speed, _startPosition.Y));
         }
 
         [Test]
         public void HaveSameMoveUp()
         {
             var _neon1 = new BlueNeon(_aquarium, _startPosition, Math.PI / 2, _defaulNeontSize);
             var _object = new List<GameObject> { _neon1 };
             A.CallTo(() => _aquarium.GetFishes()).Returns(_object.OfType<Fish>());
             _neon1.Move();
             _neon1.GetLocation().Should().Be(new Point(_startPosition.X, _startPosition.Y + (int) _neon1.Speed));
         }
 
     }
}
