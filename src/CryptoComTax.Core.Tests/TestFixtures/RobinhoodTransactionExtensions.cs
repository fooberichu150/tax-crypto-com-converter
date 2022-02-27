using System;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Tests.TestFixtures
{
	public static class RobinhoodTransactionExtensions
	{
		public static RobinhoodTransaction Default(this RobinhoodTransaction transaction)
		{
			return transaction
				.WithTransactionType(RobinhoodTransactionType.Buy)
				.WithAssetName(CryptoTokenConstants.DogeCoin)
				.WithReceivedDate(DateTime.Now)
				.WithCostBasisUsd(10)
				.WithSoldDate()
				.WithProceeds()
				.WithAmountSold(25)
				.WithDuration()
				.WithNonCovered()
				.WithBasisReported();
		}

		public static RobinhoodTransaction WithAmountSold(this RobinhoodTransaction transaction, decimal amountSold)
		{
			transaction.AmountSold = amountSold;
			return transaction;
		}

		public static RobinhoodTransaction WithAssetName(this RobinhoodTransaction transaction, string assetName)
		{
			transaction.AssetName = assetName;
			return transaction;
		}

		public static RobinhoodTransaction WithBasisReported(this RobinhoodTransaction transaction, bool basisReported = false)
		{
			transaction.BasisReported = basisReported;
			return transaction;
		}

		public static RobinhoodTransaction WithCostBasisUsd(this RobinhoodTransaction transaction, decimal costBasisUsd)
		{
			transaction.CostBasisUsd = costBasisUsd;
			return transaction;
		}

		public static RobinhoodTransaction WithDuration(this RobinhoodTransaction transaction, string duration = "short")
		{
			transaction.Duration = duration;
			return transaction;
		}

		public static RobinhoodTransaction WithNonCovered(this RobinhoodTransaction transaction, bool nonCovered = true)
		{
			transaction.NonCovered = nonCovered;
			return transaction;
		}

		public static RobinhoodTransaction WithProceeds(this RobinhoodTransaction transaction, decimal? proceeds = null)
		{
			transaction.Proceeds = proceeds;
			return transaction;
		}

		public static RobinhoodTransaction WithReceivedDate(this RobinhoodTransaction transaction, DateTime receivedDate)
		{
			transaction.ReceivedDate = receivedDate;
			return transaction;
		}

		public static RobinhoodTransaction WithSoldDate(this RobinhoodTransaction transaction, DateTime? soldDate = null)
		{
			transaction.SoldDate = soldDate;
			return transaction;
		}

		public static RobinhoodTransaction WithTransactionType(this RobinhoodTransaction transaction, RobinhoodTransactionType transactionType = RobinhoodTransactionType.Buy)
		{
			transaction.TransactionType = transactionType;
			return transaction;
		}
	}
}
