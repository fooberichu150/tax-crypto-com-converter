using System.Globalization;
using CryptoComTax.Core.Domain;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.Abra;
using CryptoComTax.Core.Extensions;
using CryptoComTax.Core.Tests.TestFixtures;
using FluentAssertions;
using Xunit;

namespace CryptoComTax.Core.Tests.Engines.Abra
{
	public class AbraTransactionConverterTests
	{
		private AbraTransactionConverter Target { get; }

		public AbraTransactionConverterTests()
		{
			Target = new AbraTransactionConverter();
		}

		[Fact]
		public void TransferToInterestBearing_IsInvalid()
		{
			// arrange
			var transaction = new AbraTransaction()
				.Default()
				.WithProduct($"{AbraConstants.InterestEarning} {CryptoTokenConstants.Ada}")
				.WithDescription($"Transfer from {CryptoTokenConstants.Ada} to {AbraConstants.InterestEarning} {CryptoTokenConstants.Ada}");

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().BeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
		}

		[Fact]
		public void InboundTransfer_ParentIsInboundTransfer_ToToken_Success()
		{
			// arrange
			var transaction = new AbraTransaction()
				.Default()
				.WithTransactionType(AbraTransactionType.InboundTransfer)
				.WithDescription("Bank deposit from Plaid")
				.WithProduct(FiatConstants.Usd)
				.WithRate("1 USD = 1 USD");

			var second = new AbraTransaction()
				.Default();

			transaction
				.WithSecond(second);

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().NotBeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Buy);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(second.NetQuantity);
			result.ReceivedCurrency.Should().Be(second.Product);
			result.SentAmount.Should().Be(transaction.NetQuantity);
			result.SentCurrency.Should().Be(transaction.Product);
		}

		[Fact]
		public void InboundTransfer_ParentIsBuy_ToToken_Success()
		{
			// arrange
			var transaction = new AbraTransaction()
				.Default();

			var second = new AbraTransaction()
				.Default()
				.WithTransactionType(AbraTransactionType.InboundTransfer)
				.WithDescription("Bank deposit from Plaid")
				.WithProduct(FiatConstants.Usd)
				.WithRate("1 USD = 1 USD");

			transaction.WithSecond(second);

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().NotBeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Buy);
			result.Date.Should().Be(second.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.NetQuantity);
			result.ReceivedCurrency.Should().Be(transaction.Product);
			result.SentAmount.Should().Be(second.NetQuantity);
			result.SentCurrency.Should().Be(second.Product);
		}

		[Fact]
		public void InterestPayment_InterestEarningAda_AsAda_Success()
		{
			// arrange
			var transaction = new AbraTransaction()
				.Default()
				.WithTransactionType(AbraTransactionType.InterestPayment)
				.WithProduct($"{AbraConstants.InterestEarning} {CryptoTokenConstants.Ada}")
				.WithDescription($"Interest paid to {AbraConstants.InterestEarning} {CryptoTokenConstants.Ada}");

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().BeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Reward);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.NetQuantity);
			result.ReceivedCurrency.Should().Be(CryptoTokenConstants.Ada);
		}

		[Fact]
		public void Reward_Success()
		{
			// arrange
			var transaction = new AbraTransaction()
				.Default()
				.WithTransactionType(AbraTransactionType.Reward)
				.WithProduct(CryptoTokenConstants.Ada)
				.WithDescription("Historical Rewards");

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().BeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Reward);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.NetQuantity);
			result.ReceivedCurrency.Should().Be(transaction.Product);
		}

		[Fact]
		public void InboundTransfer_AsDeposit_Success()
		{
			// arrange
			var transaction = new AbraTransaction()
				.Default()
				.WithTransactionType(AbraTransactionType.InboundTransfer)
				.WithProduct(CryptoTokenConstants.DogeCoin)
				.WithDescription("Historical Rewards");

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().BeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Transfer);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.NetQuantity);
			result.ReceivedCurrency.Should().Be(transaction.Product);
			result.SentAmount.Should().Be(transaction.NetQuantity);
			result.SentCurrency.Should().Be(transaction.Product);
		}

		[Fact]
		public void Swap_ParentIsBuy_Success()
		{
			// arrange
			var transaction = new AbraTransaction()
				.WithProduct(CryptoTokenConstants.Ada)
				.Default();

			var second = new AbraTransaction()
				.Default()
				.WithTransactionType(AbraTransactionType.Sell);

			transaction
				.WithSecond(second);

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().NotBeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Swap);
			result.Date.Should().Be(second.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.NetQuantity);
			result.ReceivedCurrency.Should().Be(transaction.Product);
			result.SentAmount.Should().Be(transaction.NetQuantity);
			result.SentCurrency.Should().Be(transaction.Product);
		}

		[Fact]
		public void Swap_ParentIsSell_Success()
		{
			// arrange
			var transaction = new AbraTransaction()
				.Default()
				.WithTransactionType(AbraTransactionType.Sell);

			var second = new AbraTransaction()
				.WithProduct(CryptoTokenConstants.Ada)
				.Default();

			transaction
				.WithSecond(second);

			// act
			var result = Target.Convert(transaction);

			// assert
			transaction.Second.Should().NotBeNull();
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.TransactionType.Should().Be(TransactionType.Swap);
			result.Date.Should().Be(transaction.TransactionDate);
			result.ReceivedAmount.Should().Be(transaction.NetQuantity);
			result.ReceivedCurrency.Should().Be(transaction.Product);
			result.SentAmount.Should().Be(transaction.NetQuantity);
			result.SentCurrency.Should().Be(transaction.Product);
		}
	}
}