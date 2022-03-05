using CryptoComTax.Core.Domain;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines.Uphold
{
	public class UpholdTransactionConverter : ITransactionConverter<UpholdTransaction>
	{
		public CryptoTransaction Convert(UpholdTransaction source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			switch (source.Type)
			{
				default:
				case UpholdType.Out:
					return ConvertWithdrawal(source);
				case UpholdType.In:
					return ConvertDeposit(source);
				case UpholdType.Transfer:
					return ConvertTransfer(source);
			}
		}

		private CryptoTransaction ConvertWithdrawal(UpholdTransaction source)
		{
			// bank withdrawal
			if (string.Compare(source.Destination, "bank", StringComparison.OrdinalIgnoreCase) == 0) // .OriginCurrency == FiatConstants.Usd)
				return CryptoTransaction.Invalid();

			// may not always be the case, but with my transactions, they are BAT donations
			return new CryptoTransaction
			{
				TransactionType = TransactionType.Gift,
				Date = source.Date,
				SentAmount = source.DestinationAmount,
				SentCurrency = source.DestinationCurrency
			};
		}

		private CryptoTransaction ConvertDeposit(UpholdTransaction source)
		{
			return new CryptoTransaction
			{
				Date = source.Date,
				ReceivedAmount = source.DestinationAmount,
				ReceivedCurrency = source.DestinationCurrency,
				TransactionType = TransactionType.Reward
			};
		}

		private CryptoTransaction ConvertTransfer(UpholdTransaction source)
		{
			// internal transfers are "invalid" (like from BAT Browser to BAT Rewards, etc)
			if (source.DestinationCurrency == source.OriginCurrency)
			{
				//destination.TransactionType = TransactionType.Transfer;
				return CryptoTransaction.Invalid();
			}

			var destination = new CryptoTransaction
			{
				Date = source.Date
			};
			destination.ReceivedAmount = source.DestinationAmount;
			destination.ReceivedCurrency = source.DestinationCurrency;
			destination.SentAmount = source.OriginAmount;
			destination.SentCurrency = source.OriginCurrency;

			// sell
			if (source.DestinationCurrency == FiatConstants.Usd)
			{
				destination.TransactionType = TransactionType.Sell;
			}
			// buy
			else if (source.OriginCurrency == FiatConstants.Usd)
			{
				destination.TransactionType = TransactionType.Buy;
			}

			return destination;
		}
	}
}
