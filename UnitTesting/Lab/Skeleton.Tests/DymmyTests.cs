using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DymmyTests
    {
        private Dummy dummy;
        [SetUp]
        public void SetUp()
        {
            this.dummy = new Dummy(20, 10);
        }

        [Test]
        public void DummyLosesHealthAfterAttack()
        {
            dummy.TakeAttack(5);

            Assert.That(dummy.Health, Is.EqualTo(15));
        }

        [Test]
        public void DeadDummyThrowsExceptionIfAttacked()
        {

            dummy.TakeAttack(20);

            Assert.That(() => dummy.TakeAttack(5), Throws.InvalidOperationException
                .With.Message.EqualTo("Dummy is dead."));
        }
        [Test]
        public void DeadDummyCanGiveExperience()
        {

            dummy.TakeAttack(20);

            Assert.That(dummy.GiveExperience(), Is.EqualTo(10));
        }

        [Test]
        public void AliveDummyCantGiveExperience()
        {

            dummy.TakeAttack(5);

            Assert.That(() => dummy.GiveExperience(), Throws
                .InvalidOperationException
                .With
                .Message
                .EqualTo("Target is not dead."));
        }
    }
}
