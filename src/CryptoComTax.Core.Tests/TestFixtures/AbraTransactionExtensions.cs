using System;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Tests.TestFixtures
{
	public static class AbraTransactionExtensions
	{
		/// <summary>
		/// Default transaction BUY 100 TUSD (no tranasction pair)
		/// </summary>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public static AbraTransaction Default(this AbraTransaction transaction)
		{
			return transaction
				.WithTransactionDate(DateTime.Now)
				.WithTransactionId()
				.WithTransactionType(AbraTransactionType.Buy)
				.WithDescription("Asset exchange: buy TUSD")
				.WithProduct(CryptoTokenConstants.TrueUsd)
				.WithQuantity(100)
				.WithNetworkFees()
				.WithNetQuantity(100)
				.WithRate("1 TUSD = 1 USD")
				.WithNetAmountUsd(100);
		}

		public static AbraTransaction WithSecond(this AbraTransaction transaction, AbraTransaction second)
		{
			transaction.Second = second;
			return transaction;
		}

		public static AbraTransaction WithDescription(this AbraTransaction transaction, string description)
		{
			transaction.Description = description;
			return transaction;
		}

		public static AbraTransaction WithNetAmountUsd(this AbraTransaction transaction, decimal netAmountUsd)
		{
			transaction.NetAmountUsd = netAmountUsd;
			return transaction;
		}

		public static AbraTransaction WithNetQuantity(this AbraTransaction transaction, decimal netQuantity)
		{
			transaction.NetQuantity = netQuantity;
			return transaction;
		}

		public static AbraTransaction WithNetworkFees(this AbraTransaction transaction, decimal networkFees = 0m)
		{
			transaction.NetworkFees = networkFees;
			return transaction;
		}

		public static AbraTransaction WithProduct(this AbraTransaction transaction, string product)
		{
			transaction.Product = product;
			return transaction;
		}

		public static AbraTransaction WithQuantity(this AbraTransaction transaction, decimal quantity)
		{
			transaction.GrossQuantity = quantity;
			return transaction;
		}

		public static AbraTransaction WithRate(this AbraTransaction transaction, string rate)
		{
			transaction.Rate = rate;
			return transaction;
		}

		public static AbraTransaction WithTransactionDate(this AbraTransaction transaction, DateTime transactionDate)
		{
			transaction.TransactionDate = transactionDate;
			return transaction;
		}

		public static AbraTransaction WithTransactionId(this AbraTransaction transaction, string transactionId = "ABC1231")
		{
			transaction.TransactionId = transactionId;
			return transaction;
		}

		public static AbraTransaction WithTransactionType(this AbraTransaction transaction, AbraTransactionType transactionType)
		{
			transaction.TransactionType = transactionType;
			return transaction;
		}
	}
}
