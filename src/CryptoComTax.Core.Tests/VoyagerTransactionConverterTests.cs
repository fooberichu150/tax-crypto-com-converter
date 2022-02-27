using System;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.Voyager;
using CryptoComTax.Tests.TestFixtures;
using FluentAssertions;
using Xunit;

namespace CryptoComTax.Tests
{
	public class VoyagerTransactionConverterTests
	{
		private VoyagerTransactionConverter Target { get; }

		public VoyagerTransactionConverterTests()
		{
			Target = new VoyagerTransactionConverter();
		}

		[Fact]
		public void BankTransaction_ShouldBe_Invalid()
		{
			// arrange
			var transaction = new VoyagerTransaction()
				.Default()
				.WithTransactionType(VoyagerTransactionType.Bank);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void TradeTransaction_Buy_Success()
		{
			// arrange
			var transaction = new VoyagerTransaction()
				.Default();

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Buy);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.Quantity);
			result.ReceivedCurrency.Should().Be(transaction.BaseAsset);
			result.SentAmount.Should().Be(transaction.NetAmount);
			result.SentCurrency.Should().Be(transaction.QuoteAsset);
			result.ReceivedNetWorth.Should().BeNull();
			result.SentNetWorth.Should().BeNull();
			result.FeeCurrency.Should().BeNull();
			result.FeeAmount.Should().BeNull();
			result.FeeNetWorth.Should().BeNull();
		}

		[Fact]
		public void TradeTransaction_Sell_Success()
		{
			// arrange
			var transaction = new VoyagerTransaction()
				.Default()
				.WithTransactionDirection(VoyagerTransactionDirection.Sell);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Sell);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.NetAmount);
			result.ReceivedCurrency.Should().Be(transaction.QuoteAsset);
			result.SentAmount.Should().Be(transaction.Quantity);
			result.SentCurrency.Should().Be(transaction.BaseAsset);
			result.ReceivedNetWorth.Should().BeNull();
			result.SentNetWorth.Should().BeNull();
			result.FeeCurrency.Should().BeNull();
			result.FeeAmount.Should().BeNull();
			result.FeeNetWorth.Should().BeNull();
		}

		[Fact]
		public void DepositTransaction_Rewards_Success()
		{
			// arrange
			var transaction = new VoyagerTransaction()
				.Default()
				.WithTransactionType(VoyagerTransactionType.Reward)
				.WithTransactionDirection(VoyagerTransactionDirection.Deposit);

			// act
			var result = Target.Convert(transaction);

			// assert
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Reward);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.Quantity);
			result.ReceivedCurrency.Should().Be(transaction.BaseAsset);
			result.ReceivedNetWorth.Should().Be(transaction.NetAmount);
			result.SentAmount.Should().BeNull();
			result.SentCurrency.Should().BeNullOrWhiteSpace();
			result.SentNetWorth.Should().BeNull();
			result.FeeCurrency.Should().BeNull();
			result.FeeAmount.Should().BeNull();
			result.FeeNetWorth.Should().BeNull();
		}

		[Theory]
		[InlineData(VoyagerTransactionDirection.Withdrawal)]
		[InlineData(VoyagerTransactionDirection.Deposit)]
		public void TradeTransaction_NoBuyOrSell_Failure(VoyagerTransactionDirection direction)
		{
			// arrange
			var transaction = new VoyagerTransaction()
				.Default()
				.WithTransactionDirection(direction)
				.WithTransactionType(VoyagerTransactionType.Trade);

			// act
			var result = () => Target.Convert(transaction);

			// assert
			Assert.Throws<ArgumentOutOfRangeException>(result);
		}

		[Theory]
		[InlineData(VoyagerTransactionDirection.Buy)]
		[InlineData(VoyagerTransactionDirection.Sell)]
		[InlineData(VoyagerTransactionDirection.Withdrawal)]
		public void RewardTransaction_NotDeposit_Failure(VoyagerTransactionDirection direction)
		{
			// arrange
			var transaction = new VoyagerTransaction()
				.Default()
				.WithTransactionDirection(direction)
				.WithTransactionType(VoyagerTransactionType.Reward);

			// act
			var result = () => Target.Convert(transaction);

			// assert
			Assert.Throws<ArgumentOutOfRangeException>(result);
		}
	}
}