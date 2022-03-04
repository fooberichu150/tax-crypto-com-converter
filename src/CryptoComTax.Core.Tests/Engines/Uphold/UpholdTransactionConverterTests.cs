using System;
using CryptoComTax.Core.Domain;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.Uphold;
using CryptoComTax.Core.Tests.TestFixtures;
using FluentAssertions;
using Xunit;

namespace CryptoComTax.Core.Tests.Engines.Uphold
{
	public class UpholdTransactionConverterTests
	{
		private UpholdTransactionConverter Target { get; }

		public UpholdTransactionConverterTests()
		{
			Target = new UpholdTransactionConverter();
		}

		[Fact]
		public void NullTransaction_Failure()
		{
			// arrange
			var transaction = default(UpholdTransaction);

			// act
#pragma warning disable CS8604 // Possible null reference argument.
			var result = () => Target.Convert(transaction);
#pragma warning restore CS8604 // Possible null reference argument.

			// assert
			Assert.Throws<ArgumentNullException>(result);
		}

		[Fact]
		public void In_ShouldBeReward_Success()
		{
			// arrange
			var transaction = new UpholdTransaction()
				.Default()
				.WithType(UpholdType.In);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Reward);
			result.ReceivedAmount.Should().Be(transaction.DestinationAmount);
			result.ReceivedCurrency.Should().Be(transaction.DestinationCurrency);
			result.SentAmount.Should().BeNull();
			result.SentCurrency.Should().BeNullOrWhiteSpace();
			result.Date.Should().Be(transaction.Date);
		}

		[Fact]
		public void Out_Bank_ShouldBeInvalid()
		{
			// arrange
			var transaction = new UpholdTransaction()
				.Default()
				.WithType(UpholdType.Out)
				.WithDestination("bank");

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void Out_NotBank_ShouldBeGift_Success()
		{
			// arrange
			var transaction = new UpholdTransaction()
				.Default()
				.WithType(UpholdType.Out);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Gift);
			result.ReceivedAmount.Should().BeNull();
			result.ReceivedCurrency.Should().BeNullOrWhiteSpace();
			result.SentAmount.Should().Be(transaction.DestinationAmount);
			result.SentCurrency.Should().Be(transaction.DestinationCurrency);
			result.Date.Should().Be(transaction.Date);
		}

		[Fact]
		public void Transfer_InternalTransfer_ShouldBeInvalid()
		{
			// arrange
			var transaction = new UpholdTransaction()
				.Default()
				.WithType(UpholdType.Transfer)
				.WithOriginCurrency(CryptoTokenConstants.Vechain);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void Transfer_DestinationUsd_ShouldBeSell()
		{
			// arrange
			var transaction = new UpholdTransaction()
				.Default()
				.WithType(UpholdType.Transfer)
				.WithDestinationCurrency(FiatConstants.Usd)
				.WithOriginCurrency(CryptoTokenConstants.Vechain);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Sell);
			result.ReceivedAmount.Should().Be(transaction.DestinationAmount);
			result.ReceivedCurrency.Should().Be(transaction.DestinationCurrency);
			result.SentAmount.Should().Be(transaction.OriginAmount);
			result.SentCurrency.Should().Be(transaction.OriginCurrency);
			result.Date.Should().Be(transaction.Date);
		}

		[Fact]
		public void Transfer_OriginUsd_ShouldBeBuy()
		{
			// arrange
			var transaction = new UpholdTransaction()
				.Default();

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Buy);
			result.ReceivedAmount.Should().Be(transaction.DestinationAmount);
			result.ReceivedCurrency.Should().Be(transaction.DestinationCurrency);
			result.SentAmount.Should().Be(transaction.OriginAmount);
			result.SentCurrency.Should().Be(transaction.OriginCurrency);
			result.Date.Should().Be(transaction.Date);
		}
	}
}