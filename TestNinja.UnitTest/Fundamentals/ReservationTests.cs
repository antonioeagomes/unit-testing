using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest.Fundamentals
{
    [TestFixture]
    public class ReservationTests
    {
        // public void MethodToBeTested_Scenario_ExpectedBehavior()
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
            // Assert.That(result, Is.True);
            // Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancellingReservation_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();
            var user = new User();
            reservation.MadeBy = user;
            // Act
            var result = reservation.CanBeCancelledBy(user);

            // Assert
            // Assert.IsTrue(result);
            Assert.That(result, Is.True);
            // Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancellingReservation_ReturnsFalse()
        {
            // Arrange
            var reservation = new Reservation
            {
                MadeBy = new User()
            };
            // Act
            var result = reservation.CanBeCancelledBy(new User());

            // Assert
            // Assert.IsTrue(result);
            // Assert.That(result, Is.True);
            Assert.That(result == false);
        }
    }
}
