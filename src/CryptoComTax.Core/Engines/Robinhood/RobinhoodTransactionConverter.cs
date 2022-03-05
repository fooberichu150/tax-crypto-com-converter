using CryptoComTax.Core.Domain;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines.Robinhood
{
	public class RobinhoodTransactionConverter : ITransactionConverter<RobinhoodTransaction>
	{
		public CryptoTransaction Convert(RobinhoodTransaction source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (source.TransactionType == RobinhoodTransactionType.Buy)
			{
				return ConvertBuy(source);
			}
			else if (source.TransactionType == RobinhoodTransactionType.Sell)
			{
				return ConvertSell(source);
			}

			return CryptoTransaction.Invalid();
		}

		private CryptoTransaction ConvertBuy(RobinhoodTransaction source)
		{
			var calculatedQuantity = source.AmountSold > 0
				? source.CostBasisUsd / source.AmountSold
				: 0m;

			if (calculatedQuantity == 0m)
				return CryptoTransaction.Invalid();

			var destination = new CryptoTransaction
			{
				TransactionType = TransactionType.Buy,
				ReceivedAmount = calculatedQuantity,
				ReceivedCurrency = source.AssetName,
				SentAmount = source.CostBasisUsd,
				SentCurrency = FiatConstants.Usd,
				Date = source.ReceivedDate
			};

			return destination;
		}

		private CryptoTransaction ConvertSell(RobinhoodTransaction source)
		{
			var calculatedQuantity = source.AmountSold > 0
				? source.CostBasisUsd / source.AmountSold
				: 0m;

			if (calculatedQuantity == 0m
				|| !source.SoldDate.HasValue
				|| !source.Proceeds.HasValue)
			{
				return CryptoTransaction.Invalid();
			}

			var destination = new CryptoTransaction
			{
				TransactionType = TransactionType.Sell,
				ReceivedAmount = source.Proceeds.Value,
				ReceivedCurrency = FiatConstants.Usd,
				SentAmount = calculatedQuantity,
				SentCurrency = source.AssetName,
				Date = source.SoldDate.Value
			};

			return destination;
		}
	}
}