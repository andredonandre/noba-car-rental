using NobaCars.Infra.Entities;
using Xunit;

namespace NobaCars.Tests.Entities
{
    public class RentalTests
    {
        private static readonly DateTime PickupDate = new(2026, 1, 1, 10, 0, 0);
        private const int StartMileage = 10_000;

        #region Constructor

        [Fact]
        public void GivenPickupDateAndMeterReading_WhenRentalIsCreated_ThenStartDateAndStartMileageAreSet()
        {
            // Given / When
            var rental = new Rental(PickupDate, StartMileage);

            // Then
            Assert.Equal(PickupDate, rental.StartDate);
            Assert.Equal(StartMileage, rental.StartMileage);
        }

        [Fact]
        public void GivenNewRental_WhenRentalIsCreated_ThenReturnDetailsAndPriceAreNotSet()
        {
            // Given / When
            var rental = new Rental(PickupDate, StartMileage);

            // Then
            Assert.Null(rental.EndDate);
            Assert.Equal(0, rental.EndMileage);
            Assert.Equal(0, rental.Price);
        }

        [Fact]
        public void GivenNewRental_WhenRentalIsCreated_ThenNoDaysOrDistanceAreRecorded()
        {
            // Given / When
            var rental = new Rental(PickupDate, StartMileage);

            // Then
            Assert.Equal(0, rental.NumberOfDays);
            Assert.Equal(0, rental.NumberOfKilometers);
        }

        #endregion

        #region NumberOfDays

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(30)]
        [InlineData(365)]
        public void GivenRentalReturnedAfterWholeDays_WhenGettingNumberOfDays_ThenWholeDaysAreReturned(int days)
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate.AddDays(days) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(days, numberOfDays);
        }

        [Fact]
        public void GivenRentalReturnedAtSameTime_WhenGettingNumberOfDays_ThenZeroIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(0, numberOfDays);
        }

        [Fact]
        public void GivenRentalReturnedWithinTheSameDay_WhenGettingNumberOfDays_ThenZeroIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate.AddHours(23).AddMinutes(59) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(0, numberOfDays);
        }

        [Fact]
        public void GivenRentalReturnedExactly24HoursLater_WhenGettingNumberOfDays_ThenOneIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate.AddHours(24) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(1, numberOfDays);
        }

        [Fact]
        public void GivenRentalWithPartialDay_WhenGettingNumberOfDays_ThenOnlyCompletedDaysAreCounted()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate.AddDays(2).AddHours(23).AddMinutes(59) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(2, numberOfDays);
        }

        [Fact]
        public void GivenRentalReturnedOnLaterCalendarDayButBeforePickupTime_WhenGettingNumberOfDays_ThenElapsedDaysAreCounted()
        {
            // Given  picked up 10:00, returned 08:00 two calendar days later (46 hours)
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate.Date.AddDays(2).AddHours(8) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(1, numberOfDays);
        }

        [Fact]
        public void GivenRentalSpanningLeapDay_WhenGettingNumberOfDays_ThenLeapDayIsCounted()
        {
            // Given
            var pickup = new DateTime(2028, 2, 28, 10, 0, 0);
            var rental = new Rental(pickup, StartMileage) { EndDate = new DateTime(2028, 3, 1, 10, 0, 0) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(2, numberOfDays);
        }

        [Fact]
        public void GivenRentalSpanningYearEnd_WhenGettingNumberOfDays_ThenDaysAcrossYearsAreCounted()
        {
            // Given
            var pickup = new DateTime(2026, 12, 30, 10, 0, 0);
            var rental = new Rental(pickup, StartMileage) { EndDate = new DateTime(2027, 1, 2, 10, 0, 0) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(3, numberOfDays);
        }

        [Fact]
        public void GivenRentalNotYetReturned_WhenGettingNumberOfDays_ThenZeroIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage);

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(0, numberOfDays);
        }

        [Fact]
        public void GivenRentalWithoutStartDate_WhenGettingNumberOfDays_ThenZeroIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage)
            {
                StartDate = null,
                EndDate = PickupDate.AddDays(5)
            };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(0, numberOfDays);
        }

        [Fact]
        public void GivenRentalWithoutStartOrEndDate_WhenGettingNumberOfDays_ThenZeroIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { StartDate = null };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(0, numberOfDays);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-3)]
        [InlineData(-365)]
        public void GivenReturnDateBeforePickupDate_WhenGettingNumberOfDays_ThenZeroIsReturned(int days)
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate.AddDays(days) };

            // When
            var numberOfDays = rental.NumberOfDays;

            // Then
            Assert.Equal(0, numberOfDays);
        }

        [Fact]
        public void GivenReturnDateIsChanged_WhenGettingNumberOfDays_ThenNewPeriodIsReflected()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndDate = PickupDate.AddDays(2) };

            // When
            rental.EndDate = PickupDate.AddDays(5);

            // Then
            Assert.Equal(5, rental.NumberOfDays);
        }

        #endregion

        #region CalculateDistance / NumberOfKilometers

        [Theory]
        [InlineData(10_000, 10_001, 1)]
        [InlineData(10_000, 10_250, 250)]
        [InlineData(0, 1_500, 1_500)]
        [InlineData(250_000, 300_000, 50_000)]
        public void GivenReturnMeterReadingHigherThanPickup_WhenCalculatingDistance_ThenDifferenceIsReturned(
            int startMileage, int endMileage, int expected)
        {
            // Given
            var rental = new Rental(PickupDate, startMileage) { EndMileage = endMileage };

            // When
            var distance = rental.CalculateDistance();

            // Then
            Assert.Equal(expected, distance);
        }

        [Fact]
        public void GivenReturnMeterReadingEqualToPickup_WhenCalculatingDistance_ThenZeroIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndMileage = StartMileage };

            // When
            var distance = rental.CalculateDistance();

            // Then
            Assert.Equal(0, distance);
        }

        [Theory]
        [InlineData(10_000, 9_999)]
        [InlineData(10_000, 5_000)]
        [InlineData(10_000, 0)]
        public void GivenReturnMeterReadingLowerThanPickup_WhenCalculatingDistance_ThenZeroIsReturned(
            int startMileage, int endMileage)
        {
            // Given
            var rental = new Rental(PickupDate, startMileage) { EndMileage = endMileage };

            // When
            var distance = rental.CalculateDistance();

            // Then
            Assert.Equal(0, distance);
        }

        [Fact]
        public void GivenRentalNotYetReturned_WhenCalculatingDistance_ThenZeroIsReturned()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage);

            // When
            var distance = rental.CalculateDistance();

            // Then
            Assert.Equal(0, distance);
        }

        [Theory]
        [InlineData(10_000, 10_250)]
        [InlineData(10_000, 10_000)]
        [InlineData(10_000, 9_000)]
        public void GivenAnyMeterReadings_WhenGettingNumberOfKilometers_ThenItMatchesCalculatedDistance(
            int startMileage, int endMileage)
        {
            // Given
            var rental = new Rental(PickupDate, startMileage) { EndMileage = endMileage };

            // When
            var numberOfKilometers = rental.NumberOfKilometers;

            // Then
            Assert.Equal(rental.CalculateDistance(), numberOfKilometers);
        }

        [Fact]
        public void GivenReturnMeterReadingIsChanged_WhenGettingNumberOfKilometers_ThenNewDistanceIsReflected()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage) { EndMileage = StartMileage + 100 };

            // When
            rental.EndMileage = StartMileage + 400;

            // Then
            Assert.Equal(400, rental.NumberOfKilometers);
        }

        #endregion

        #region Price

        [Fact]
        public void GivenCalculatedPrice_WhenPriceIsSet_ThenPriceIsStoredOnRental()
        {
            // Given
            var rental = new Rental(PickupDate, StartMileage);

            // When
            rental.Price = 2450.5;

            // Then
            Assert.Equal(2450.5, rental.Price);
        }

        #endregion
    }
}
