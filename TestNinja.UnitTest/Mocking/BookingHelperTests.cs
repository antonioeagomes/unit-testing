using Moq;
using NUnit.Framework;
using System;
using TestNinja.Mocking;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        Mock<IBookingRepository> _mockRepository;
        IQueryable<Booking> _bookings;
        Booking _existingBooking;

        [SetUp]
        public void Init()
        {
            _mockRepository = new Mock<IBookingRepository>();
            _existingBooking = new Booking
            {
                ArrivalDate = ArrivalOn(10, 1, 2020),
                DepartureDate = DepartOn(15, 1, 2020),
                Reference = "a",
                Status = "Active",
                Id = 2
            };
            _bookings = new List<Booking> {
                _existingBooking
            }.AsQueryable();
            _mockRepository.Setup(r => r.GetActiveBookings(1)).Returns(_bookings);
        }

        [TearDown]
        public void Dispose()
        {
            _mockRepository = null;
        }

        [Test]
        public void OverlappingBookingsExist_BookingIsCancelled_ReturnEmpty()
        {            
            var result = BookingHelper.OverlappingBookingsExist(new Booking { Status = "Cancelled" }, _mockRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingDoesNotOverlaps_ReturnEmpty()
        {
            var result =  BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 3),
                DepartureDate = Before(_existingBooking.ArrivalDate),
                Status = "Confirmed"

            }, _mockRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingOverlaps_ReturnBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Confirmed"

            }, _mockRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime ArrivalOn(int day, int month, int year)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int day, int month, int year)
        {
            return new DateTime(year, month, day, 12, 0, 0);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
    }
}
