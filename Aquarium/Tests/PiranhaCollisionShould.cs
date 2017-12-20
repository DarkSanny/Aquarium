﻿using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Fishes;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;


namespace Aquarium.Tests
{
    class PiranhaCollisionShould
    {
        private IAquarium _aquarium;
        private Size _defaultSize;
        private Piranha _piranha1;
        private Point _defaultPostition;

        [SetUp]
        public void SetUp()
        {
            _aquarium = A.Fake<IAquarium>();
            _defaultSize = new Size(150, 150);
            _defaultPostition = new Point(10, 10);
            _piranha1 = new Piranha(_aquarium, _defaultPostition, 0, new Size(30, 10));
        }
        [Test]
        public void ShouldHaveCorrectType()
        {
            _piranha1.GetCollisionType().Should().Be(ObjectType.Piranha);
        }
        [Test]
        public void NotCollise_WithNeon()
        {
            var neon = new BlueNeon(_aquarium, _defaultPostition, 0, _defaultSize);
            _piranha1.IsShouldCollise(neon).Should().BeTrue();
        }

        [Test]
        public void NotCollise_WithPiranha()
        {
            var piranha = new Piranha(_aquarium, _defaultPostition, 0, _defaultSize);
            _piranha1.IsShouldCollise(piranha).Should().BeFalse();
        }
        [Test]
        public void NotCollise_WithCatfish()
        {
            var catfish = new Catfish(_aquarium, _defaultPostition, 0, _defaultSize);
            _piranha1.IsShouldCollise(catfish).Should().BeFalse();
        }
        [Test]
        public void NotCollise_WithSwordFish()
        {
            var swordfish = new Swordfish(_aquarium, _defaultPostition, 0, _defaultSize);
            _piranha1.IsShouldCollise(swordfish).Should().BeFalse();
        }
        [Test]
        public void DieWhenColision()
        {
            var anyFishWishCollise = new Swordfish(_aquarium, _defaultPostition, 0, _defaultSize);
            var counter = 0;
            _piranha1.ShouldDie += () => counter++;
            _piranha1.Collision(anyFishWishCollise);
            counter.Should().Be(1);
        }

    }
}