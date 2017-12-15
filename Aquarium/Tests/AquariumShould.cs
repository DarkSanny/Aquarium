using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System.Drawing;

namespace Aquarium.Tests
{
	[TestFixture]
	public class AquariumShould
	{
        private IAquarium _aquarium;
        private Size _size;
        private GameObject _object;


        [SetUp]
        public void SetUp ()
        {
            _aquarium = A.Fake<IAquarium>();
            _object = A.Fake<GameObject>();
            _size = new Size(100, 100);

        }


	}
}
