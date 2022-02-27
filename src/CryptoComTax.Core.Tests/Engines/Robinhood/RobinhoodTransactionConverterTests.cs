using System;
using CryptoComTax.Core.Domain;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.Robinhood;
using CryptoComTax.Core.Tests.TestFixtures;
using FluentAssertions;
using Xunit;

namespace CryptoComTax.Core.Tests.Engines.Robinhood
{
	public class RobinhoodTransactionConverterTests
	{
		private RobinhoodTransactionConverter Target { get; }

		public RobinhoodTransactionConverterTests()
		{
			Target = new RobinhoodTransactionConverter();
		}

		[Fact]
		public void NullTransaction_Failure()
		{
			// arrange
			var transaction = default(RobinhoodTransaction);

			// act
#pragma warning disable CS8604 // Possible null reference argument.
			var result = () => Target.Convert(transaction);
#pragma warning restore CS8604 // Possible null reference argument.

			// assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void BuyTransaction_Buy_Success()
		{
			// arrange
			var transaction = new RobinhoodTransaction()
				.Default();

			var calculatedQuantity = transaction.AmountSold > 0
				? transaction.CostBasisUsd / transaction.AmountSold
				: 0m;

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Buy);
			result.Date.Should().Be(transaction.ReceivedDate);
			result.ReceivedAmount.Should().Be(calculatedQuantity);
			result.ReceivedCurrency.Should().Be(transaction.AssetName);
			result.SentAmount.Should().Be(transaction.CostBasisUsd);
			result.SentCurrency.Should().Be(FiatConstants.Usd);
		}

		[Fact]
		public void BuyTransaction_MissingAmountSold_ShouldBe_Invalid()
		{
			// arrange
			var transaction = new RobinhoodTransaction()
				.Default()
				.WithAmountSold(0);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void BuyTransaction_Sell_Success()
		{
			// arrange
			var transaction = new RobinhoodTransaction()
				.Default()
				.WithTransactionType(RobinhoodTransactionType.Sell)
				.WithSoldDate(DateTime.Now)
				.WithProceeds(5m);

			var calculatedQuantity = transaction.AmountSold > 0
				? transaction.CostBasisUsd / transaction.AmountSold
				: 0m;

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Sell);
			result.Date.Should().Be(transaction.SoldDate);
			result.ReceivedAmount.Should().Be(transaction.Proceeds);
			result.ReceivedCurrency.Should().Be(FiatConstants.Usd);
			result.SentAmount.Should().Be(calculatedQuantity);
			result.SentCurrency.Should().Be(transaction.AssetName);
		}

		public static readonly object[][] SellMissingDataParams =
			{
#pragma warning disable CS8601 // Possible null reference assignment.
				new object[] { 0m, new DateTime?(), default(decimal?)},
				new object[] { 0m, new DateTime(2018, 12, 31), default(decimal?)},
				new object[] { 0m, new DateTime?(), 5m},
				new object[] { 5m, new DateTime?(), default(decimal?) },
				new object[] { 5m, new DateTime(2018,12,31), default(decimal?)},
				new object[] { 5m, new DateTime?(), 5m}
#pragma warning restore CS8601 // Possible null reference assignment.
			};

		[Theory, MemberData(nameof(SellMissingDataParams))]
		public void SellTransaction_MissingData_ShouldBe_Invalid(decimal amountSold, DateTime? soldDate, decimal? proceeds)
		{
			// arrange
			var transaction = new RobinhoodTransaction()
				.Default()
				.WithTransactionType(RobinhoodTransactionType.Sell)
				.WithAmountSold(amountSold)
				.WithSoldDate(soldDate)
				.WithProceeds(proceeds);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
		}
	}
}