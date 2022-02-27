using System;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Tests.TestFixtures
{
	public static class VoyagerTransactionExtensions
	{
		const string TransactionId = "ABC12301";

		/// <summary>
		/// Default transaction type: TRADE, direction: BUY
		/// </summary>
		/// <returns></returns>
		public static VoyagerTransaction Default(this VoyagerTransaction voyagerTransaction)
		{
			return voyagerTransaction
				.WithTransactionDate(DateTime.Now)
				.WithTransactionId()
				.WithTransactionDirection(VoyagerTransactionDirection.Buy)
				.WithTransactionType(VoyagerTransactionType.Trade)
				.WithBaseAsset(CryptoTokenConstants.Vechain)
				.WithQuoteAsset(FiatConstants.Usd)
				.WithQuantity(50)
				.WithNetAmount(10)
				.WithPrice(0.2m);
		}

		public static VoyagerTransaction WithBaseAsset(this VoyagerTransaction voyagerTransaction, string baseAsset = CryptoTokenConstants.Vechain)
		{
			voyagerTransaction.BaseAsset = baseAsset;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithNetAmount(this VoyagerTransaction voyagerTransaction, decimal netAmount)
		{
			voyagerTransaction.NetAmount = netAmount;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithPrice(this VoyagerTransaction voyagerTransaction, decimal price)
		{
			voyagerTransaction.Price = price;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithQuantity(this VoyagerTransaction voyagerTransaction, decimal quantity)
		{
			voyagerTransaction.Quantity = quantity;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithQuoteAsset(this VoyagerTransaction voyagerTransaction, string quoteAsset = FiatConstants.Usd)
		{
			voyagerTransaction.QuoteAsset = quoteAsset;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithTransactionDate(this VoyagerTransaction voyagerTransaction, DateTime transactionDate)
		{
			voyagerTransaction.TransactionDate = transactionDate;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithTransactionDirection(this VoyagerTransaction voyagerTransaction, VoyagerTransactionDirection transactionDirection)
		{
			voyagerTransaction.TransactionDirection = transactionDirection;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithTransactionId(this VoyagerTransaction voyagerTransaction, string transactionId = TransactionId)
		{
			voyagerTransaction.TransactionId = transactionId;
			return voyagerTransaction;
		}

		public static VoyagerTransaction WithTransactionType(this VoyagerTransaction voyagerTransaction, VoyagerTransactionType transactionType)
		{
			voyagerTransaction.TransactionType = transactionType;
			return voyagerTransaction;
		}
	}
}
