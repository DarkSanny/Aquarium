using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Fishes;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;


namespace Aquarium.Tests
{
    class SwordfishCollisionShould
    {
        private IAquarium _aquarium;
        private Size _defaultSize;
        private Swordfish _swordfish1;
        private Point _defaultPostition;

        [SetUp]
        public void SetUp()
        {
            _aquarium = A.Fake<IAquarium>();
            _defaultSize = new Size(150, 150);
            _defaultPostition = new Point(10, 10);
            _swordfish1 = new Swordfish(_aquarium, _defaultPostition, 0, new Size(30, 10));
        }
        [Test]
        public void ShouldHaveCorrectType()
        {
            _swordfish1.GetCollisionType().Should().Be(ObjectType.Swordfish);
        }
        [Test]
        public void NotCollise_WithNeon()
        {
            var neon = new BlueNeon(_aquarium, _defaultPostition, 0, _defaultSize);
            _swordfish1.IsShouldCollise(neon).Should().BeFalse();
        }

        [Test]
        public void NotCollise_WithPiranha()
        {
            var piranha = new Piranha(_aquarium, _defaultPostition, 0, _defaultSize);
            _swordfish1.IsShouldCollise(piranha).Should().BeFalse();
        }
        [Test]
        public void NotCollise_WithCatfish()
        {
            var catfish = new Catfish(_aquarium, _defaultPostition, 0, _defaultSize);
            _swordfish1.IsShouldCollise(catfish).Should().BeFalse();
        }
        [Test]
        public void NotCollise_WithSwordFish()
        {
            var swordfish = new Swordfish(_aquarium, _defaultPostition, 0, _defaultSize);
            _swordfish1.IsShouldCollise(swordfish).Should().BeFalse();
        }
        [Test]
        public void DieWhenColision()
        {
            var _size = new Size(_defaultSize.Height + 10, _defaultSize.Width + 10);
            var anyFishWishCollise = new Swordfish(_aquarium, _defaultPostition, 0, _defaultSize);
            var counter = 0;
            _swordfish1.ShouldDie += () => counter++;
            _swordfish1.Collision(anyFishWishCollise);
            counter.Should().Be(1);
        }
    }
}
