using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines.Voyager
{
	public class VoyagerTransactionConverter : ITransactionConverter<VoyagerTransaction>
	{
		public CryptoTransaction Convert(VoyagerTransaction source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var destination = new CryptoTransaction
			{
				Date = source.TransactionDate
			};

			switch (source.TransactionType)
			{
				case VoyagerTransactionType.Bank:
					return new CryptoTransaction { IsValid = false };
				case VoyagerTransactionType.Reward:
					{
						ConvertReward(destination, source);
						break;
					}
				case VoyagerTransactionType.Trade:
					{
						ConvertTrade(destination, source);
						break;
					}
			}

			return destination;
		}

		private void ConvertReward(CryptoTransaction destination, VoyagerTransaction source)
		{
			if (source.TransactionDirection != VoyagerTransactionDirection.Deposit)
			{
				throw new ArgumentOutOfRangeException(nameof(source.TransactionDirection), "Rewards must be deposit");
			}

			destination.TransactionType = TransactionType.Reward;
			destination.ReceivedAmount = source.Quantity;
			destination.ReceivedCurrency = source.BaseAsset;
			destination.ReceivedNetWorth = source.NetAmount;
		}

		private void ConvertTrade(CryptoTransaction destination, VoyagerTransaction source)
		{
			if (source.TransactionDirection != VoyagerTransactionDirection.Buy
				&& source.TransactionDirection != VoyagerTransactionDirection.Sell)
			{
				throw new ArgumentOutOfRangeException(nameof(source.TransactionDirection), "Trades only support Buy and Sell");
			}

			if (source.TransactionDirection == VoyagerTransactionDirection.Buy)
			{
				destination.TransactionType = TransactionType.Buy;
				destination.ReceivedAmount = source.Quantity;
				destination.ReceivedCurrency = source.BaseAsset;
				//output.ReceivedNetWorth // only if a TRADE between cryptos
				destination.SentAmount = source.NetAmount;
				destination.SentCurrency = source.QuoteAsset;
				//output.SentNetWorth // only if a TRADE between cryptos
			}
			else if (source.TransactionDirection == VoyagerTransactionDirection.Sell)
			{
				destination.TransactionType = TransactionType.Sell;
				destination.SentAmount = source.Quantity;
				destination.SentCurrency = source.BaseAsset;
				//output.ReceivedNetWorth // only if a TRADE between cryptos
				destination.ReceivedAmount = source.NetAmount;
				destination.ReceivedCurrency = source.QuoteAsset;
				//output.SentNetWorth // only if a TRADE between cryptos
			}
		}
	}
}