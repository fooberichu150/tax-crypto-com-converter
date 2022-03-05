using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines.Abra
{
	public class AbraTransactionConverter : ITransactionConverter<AbraTransaction>
	{
		public AbraTransactionConverter()
		{

		}

		public CryptoTransaction Convert(AbraTransaction source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			switch (source.TransactionType, source.Second?.TransactionType)
			{
				case (AbraTransactionType.InterestPayment, null):
				case (AbraTransactionType.Reward, null):
					return ConvertReward(source);
				// deposit
				case (AbraTransactionType.InboundTransfer, null):
					return ConvertDeposit(source);
				// buy crypto from bank; should always be sorted as InboundTransfer/Buy, but handle both
				case (AbraTransactionType.InboundTransfer, AbraTransactionType.Buy):
				case (AbraTransactionType.Buy, AbraTransactionType.InboundTransfer):
					return ConvertTrade(source);
				// Buy+Sell pair is a "trade"; should always be sorted as Sell/Buy, but we handle both cases just in case
				case (AbraTransactionType.Sell, AbraTransactionType.Buy):
				case (AbraTransactionType.Buy, AbraTransactionType.Sell):
					return ConvertSwap(source);
				default:
					return CryptoTransaction.Invalid();
			}
		}

		private CryptoTransaction ConvertTrade(AbraTransaction source)
		{
			if (source.Second is null)
				return CryptoTransaction.Invalid();

			var transferTransaction = source.TransactionType == AbraTransactionType.InboundTransfer
				? source
				: source.Second;

			var buyTransaction = source.TransactionType == AbraTransactionType.Buy
				? source
				: source.Second;

			if (transferTransaction.TransactionType != AbraTransactionType.InboundTransfer
				&& buyTransaction.TransactionType != AbraTransactionType.Buy)
			{
				return CryptoTransaction.Invalid();
			}


			return new CryptoTransaction
			{
				TransactionType = TransactionType.Buy,
				Date = transferTransaction.TransactionDate,
				ReceivedAmount = buyTransaction.NetQuantity,
				ReceivedCurrency = buyTransaction.Product,
				SentAmount = transferTransaction.NetQuantity,
				SentCurrency = transferTransaction.Product,
			};
		}

		private CryptoTransaction ConvertDeposit(AbraTransaction source)
		{
			var destination = new CryptoTransaction
			{
				TransactionType = TransactionType.Transfer,
				Date = source.TransactionDate,
				ReceivedAmount = source.NetQuantity,
				ReceivedCurrency = source.Product,
				SentAmount = source.NetQuantity,
				SentCurrency = source.Product
			};

			return destination;
		}

		private CryptoTransaction ConvertReward(AbraTransaction source)
		{
			var product = source.Product.Replace(AbraConstants.InterestEarning, string.Empty).Trim();

			var destination = new CryptoTransaction
			{
				TransactionType = TransactionType.Reward,
				ReceivedAmount = source.NetQuantity,
				ReceivedCurrency = product,
				Date = source.TransactionDate
			};

			return destination;
		}

		private CryptoTransaction ConvertSwap(AbraTransaction source)
		{
			if (source.Second is null)
				return CryptoTransaction.Invalid();

			var buyTransaction = source.TransactionType == AbraTransactionType.Buy
				? source
				: source.Second;

			var sellTransaction = source.TransactionType == AbraTransactionType.Sell
				? source
				: source.Second;

			if (buyTransaction.TransactionType != AbraTransactionType.Buy
				&& sellTransaction.TransactionType != AbraTransactionType.Sell)
			{
				return CryptoTransaction.Invalid();
			}

			var destination = new CryptoTransaction
			{
				TransactionType = TransactionType.Swap,
				Date = sellTransaction.TransactionDate,
				ReceivedAmount = buyTransaction.NetQuantity,
				ReceivedCurrency = buyTransaction.Product,
				SentAmount = sellTransaction.NetQuantity,
				SentCurrency = sellTransaction.Product
			};

			return destination;
		}
	}
}
