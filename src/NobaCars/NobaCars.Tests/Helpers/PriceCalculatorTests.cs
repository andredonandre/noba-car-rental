using NobaCars.Core.Helpers;
using NobaCars.Infra.Entities;
using Xunit;

namespace NobaCars.Tests.Helpers
{
    public class PriceCalculatorTests
    {
        private const int Precision = 6;
        private static readonly DateTime PickupDate = new(2026, 1, 1, 10, 0, 0);
        private const int StartMileage = 10_000;

        #region Spec categories

        // Small car: Price = baseDayRental * numberOfDays
        private static CarCategory SmallCar(int baseDayRental, int baseKmPrice) => new()
        {
            Name = "SmallCar",
            BaseDayRentalPrice = baseDayRental,
            BaseKmPrice = baseKmPrice,
            DayRateMultipler = 1,
            DistanceRateMultipler = 0
        };

        // Combi: Price = baseDayRental * numberOfDays * 1.3 + baseKmPrice * numberOfKm
        private static CarCategory Combi(int baseDayRental, int baseKmPrice) => new()
        {
            Name = "Combi",
            BaseDayRentalPrice = baseDayRental,
            BaseKmPrice = baseKmPrice,
            DayRateMultipler = 1.3,
            DistanceRateMultipler = 1
        };

        // Truck: Price = baseDayRental * numberOfDays * 1.5 + baseKmPrice * numberOfKm * 1.5
        private static CarCategory Truck(int baseDayRental, int baseKmPrice) => new()
        {
            Name = "Truck",
            BaseDayRentalPrice = baseDayRental,
            BaseKmPrice = baseKmPrice,
            DayRateMultipler = 1.5,
            DistanceRateMultipler = 1.5
        };

        private static Rental ReturnedRental(int days, int kilometers) => new(PickupDate, StartMileage)
        {
            EndDate = PickupDate.AddDays(days),
            EndMileage = StartMileage + kilometers
        };

        #endregion

        #region Small car

        [Theory]
        [InlineData(500, 2, 3, 250, 1500)]
        [InlineData(100, 5, 1, 0, 100)]
        [InlineData(250, 10, 7, 1000, 1750)]
        [InlineData(0, 5, 4, 100, 0)]
        public void GivenSmallCarRental_WhenCalculatingPrice_ThenPriceIsBaseDayRentalTimesNumberOfDays(
            int baseDayRental, int baseKmPrice, int days, int kilometers, double expected)
        {
            // Given
            var category = SmallCar(baseDayRental, baseKmPrice);
            var rental = ReturnedRental(days, kilometers);

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then
            Assert.Equal(expected, price, Precision);
        }

        [Fact]
        public void GivenSmallCarRental_WhenDistanceDrivenChanges_ThenPriceIsUnaffected()
        {
            // Given
            var category = SmallCar(baseDayRental: 300, baseKmPrice: 10);
            var shortTrip = ReturnedRental(days: 2, kilometers: 10);
            var longTrip = ReturnedRental(days: 2, kilometers: 5000);

            // When
            var shortTripPrice = PriceCalculator.CalculatePrice(shortTrip, category);
            var longTripPrice = PriceCalculator.CalculatePrice(longTrip, category);

            // Then
            Assert.Equal(shortTripPrice, longTripPrice, Precision);
        }

        #endregion

        #region Combi

        [Theory]
        [InlineData(500, 2, 3, 250, 2450)]
        [InlineData(100, 5, 1, 0, 130)]
        [InlineData(250, 10, 7, 1000, 12275)]
        [InlineData(100, 0, 2, 500, 260)]
        [InlineData(0, 3, 0, 100, 300)]
        public void GivenCombiRental_WhenCalculatingPrice_ThenPriceIsDayRateTimes1Point3PlusKmPriceTimesKm(
            int baseDayRental, int baseKmPrice, int days, int kilometers, double expected)
        {
            // Given
            var category = Combi(baseDayRental, baseKmPrice);
            var rental = ReturnedRental(days, kilometers);

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then
            Assert.Equal(expected, price, Precision);
        }

        #endregion

        #region Truck

        [Theory]
        [InlineData(500, 2, 3, 250, 3000)]
        [InlineData(100, 5, 1, 0, 150)]
        [InlineData(250, 10, 7, 1000, 17625)]
        [InlineData(100, 0, 2, 500, 300)]
        [InlineData(0, 4, 0, 100, 600)]
        public void GivenTruckRental_WhenCalculatingPrice_ThenPriceIsDayRateTimes1Point5PlusKmPriceTimesKmTimes1Point5(
            int baseDayRental, int baseKmPrice, int days, int kilometers, double expected)
        {
            // Given
            var category = Truck(baseDayRental, baseKmPrice);
            var rental = ReturnedRental(days, kilometers);

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then
            Assert.Equal(expected, price, Precision);
        }

        #endregion

        #region Category comparison

        [Fact]
        public void GivenSameRentalAndBasePrices_WhenCalculatingPricePerCategory_ThenSmallCarIsCheapestAndTruckIsMostExpensive()
        {
            // Given
            var rental = ReturnedRental(days: 3, kilometers: 200);

            // When
            var smallCarPrice = PriceCalculator.CalculatePrice(rental, SmallCar(400, 3));
            var combiPrice = PriceCalculator.CalculatePrice(rental, Combi(400, 3));
            var truckPrice = PriceCalculator.CalculatePrice(rental, Truck(400, 3));

            // Then
            Assert.True(smallCarPrice < combiPrice);
            Assert.True(combiPrice < truckPrice);
        }

        [Fact]
        public void GivenNewCategoryWithCustomMultipliers_WhenCalculatingPrice_ThenGenericFormulaIsApplied()
        {
            // Given
            var category = new CarCategory
            {
                Name = "Van",
                BaseDayRentalPrice = 200,
                BaseKmPrice = 4,
                DayRateMultipler = 2,
                DistanceRateMultipler = 0.5
            };
            var rental = ReturnedRental(days: 2, kilometers: 100);

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then  (200 * 2 * 2) + (4 * 100 * 0.5)
            Assert.Equal(1000, price, Precision);
        }

        [Fact]
        public void GivenCategoryWithDefaultMultipliers_WhenCalculatingPrice_ThenPriceIsDayRentalPlusKmPrice()
        {
            // Given
            var category = new CarCategory { BaseDayRentalPrice = 100, BaseKmPrice = 2 };
            var rental = ReturnedRental(days: 3, kilometers: 50);

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then  (100 * 3) + (2 * 50)
            Assert.Equal(400, price, Precision);
        }

        #endregion

        #region Rental period edge cases

        [Fact]
        public void GivenRentalReturnedSameDay_WhenCalculatingPrice_ThenNoDaysAreCharged()
        {
            // Given
            var category = Combi(baseDayRental: 500, baseKmPrice: 2);
            var rental = new Rental(PickupDate, StartMileage)
            {
                EndDate = PickupDate.AddHours(5),
                EndMileage = StartMileage + 40
            };

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then  only the distance is charged: 2 * 40
            Assert.Equal(80, price, Precision);
        }

        [Fact]
        public void GivenRentalWithPartialDay_WhenCalculatingPrice_ThenOnlyCompletedDaysAreCharged()
        {
            // Given
            var category = SmallCar(baseDayRental: 500, baseKmPrice: 2);
            var rental = new Rental(PickupDate, StartMileage)
            {
                EndDate = PickupDate.AddDays(2).AddHours(23).AddMinutes(59),
                EndMileage = StartMileage
            };

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then
            Assert.Equal(1000, price, Precision);
        }

        [Fact]
        public void GivenRentalNotYetReturned_WhenCalculatingPrice_ThenNoDaysOrDistanceAreCharged()
        {
            // Given
            var category = Truck(baseDayRental: 500, baseKmPrice: 2);
            var rental = new Rental(PickupDate, StartMileage);

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then
            Assert.Equal(0, price, Precision);
        }

        [Fact]
        public void GivenReturnDateBeforePickupDate_WhenCalculatingPrice_ThenNoDaysAreCharged()
        {
            // Given
            var category = Truck(baseDayRental: 500, baseKmPrice: 2);
            var rental = new Rental(PickupDate, StartMileage)
            {
                EndDate = PickupDate.AddDays(-3),
                EndMileage = StartMileage + 100
            };

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then  only the distance is charged: 2 * 100 * 1.5
            Assert.Equal(300, price, Precision);
        }

        #endregion

        #region Meter reading edge cases

        [Fact]
        public void GivenReturnMeterReadingEqualToPickup_WhenCalculatingPrice_ThenNoDistanceIsCharged()
        {
            // Given
            var category = Combi(baseDayRental: 500, baseKmPrice: 2);
            var rental = ReturnedRental(days: 2, kilometers: 0);

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then  only the days are charged: 500 * 2 * 1.3
            Assert.Equal(1300, price, Precision);
        }

        [Fact]
        public void GivenReturnMeterReadingLowerThanPickup_WhenCalculatingPrice_ThenNoDistanceIsCharged()
        {
            // Given
            var category = Combi(baseDayRental: 500, baseKmPrice: 2);
            var rental = new Rental(PickupDate, StartMileage)
            {
                EndDate = PickupDate.AddDays(2),
                EndMileage = StartMileage - 500
            };

            // When
            var price = PriceCalculator.CalculatePrice(rental, category);

            // Then  only the days are charged: 500 * 2 * 1.3
            Assert.Equal(1300, price, Precision);
        }

        #endregion
    }
}
