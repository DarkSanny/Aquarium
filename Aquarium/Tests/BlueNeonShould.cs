using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Aquarium.Tests
{
	[TestFixture]
	public class BlueNeonShould
	{
		private IAquarium _aquarium;
		private Point _startPosition;
		private double _startDirection;
		private BlueNeon _neon;

		[SetUp]
		public void SetUp()
		{
			_aquarium = A.Fake<IAquarium>();
			_startPosition = new Point(0, 0);
			_startDirection = 0;
			_neon = new BlueNeon(_aquarium, _startPosition, _startDirection, new Size(20, 10));
		}


		[Test]
		public void SimpleTestForExample()
		{
			_neon.GetCollisionType().Should().Be(ObjectType.BlueNeon);
		}

	}
}
