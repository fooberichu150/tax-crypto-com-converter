using System;
using CryptoComTax.Core.Domain;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Tests.TestFixtures
{
	public static class UpholdTransactionExtensions
	{
		/// <summary>
		/// Default transaction type: TRANSFER (buy)
		/// </summary>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public static UpholdTransaction Default(this UpholdTransaction transaction)
		{
			return transaction.WithDate(DateTime.Now)
				.WithDestination()
				.WithDestinationAmount(50)
				.WithDestinationCurrency(CryptoTokenConstants.Vechain)
				.WithId()
				.WithOrigin()
				.WithOriginAmount(10)
				.WithOriginCurrency(FiatConstants.Usd)
				.WithStatus()
				.WithType(UpholdType.Transfer);
		}

		public static UpholdTransaction WithDate(this UpholdTransaction transaction, DateTime date)
		{
			transaction.Date = date;
			return transaction;
		}

		public static UpholdTransaction WithDestination(this UpholdTransaction transaction, string destination = "uphold")
		{
			transaction.Destination = destination;
			return transaction;
		}

		public static UpholdTransaction WithDestinationAmount(this UpholdTransaction transaction, decimal destinationAmount)
		{
			transaction.DestinationAmount = destinationAmount;
			return transaction;
		}

		public static UpholdTransaction WithDestinationCurrency(this UpholdTransaction transaction, string destinationCurrency)
		{
			transaction.DestinationCurrency = destinationCurrency;
			return transaction;
		}

		public static UpholdTransaction WithFeeAmount(this UpholdTransaction transaction, decimal? feeAmount = null)
		{
			transaction.FeeAmount = feeAmount;
			return transaction;
		}

		public static UpholdTransaction WithFeeCurrency(this UpholdTransaction transaction, string feeCurrency)
		{
			transaction.FeeCurrency = feeCurrency;
			return transaction;
		}

		public static UpholdTransaction WithId(this UpholdTransaction transaction, Guid id = default)
		{
			transaction.Id = id == Guid.Empty ? Guid.NewGuid() : id;
			return transaction;
		}

		public static UpholdTransaction WithOrigin(this UpholdTransaction transaction, string origin = "uphold")
		{
			transaction.Origin = origin;
			return transaction;
		}

		public static UpholdTransaction WithOriginAmount(this UpholdTransaction transaction, decimal originAmount)
		{
			transaction.OriginAmount = originAmount;
			return transaction;
		}

		public static UpholdTransaction WithOriginCurrency(this UpholdTransaction transaction, string originCurrency)
		{
			transaction.OriginCurrency = originCurrency;
			return transaction;
		}

		public static UpholdTransaction WithStatus(this UpholdTransaction transaction, string status = "completed")
		{

			transaction.Status = status;
			return transaction;
		}

		public static UpholdTransaction WithType(this UpholdTransaction transaction, UpholdType upholdType)
		{

			transaction.Type = upholdType;
			return transaction;
		}
	}
}
