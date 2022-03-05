using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.TypeConverters;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoComTax.Core.Engines.Abra
{
	public class AbraTransactionImporter : TransactionImporterBase<AbraTransaction>
	{
		private readonly ITransactionConverter<AbraTransaction> _transactionConverter;

		public AbraTransactionImporter(ILogger<AbraTransactionImporter> logger,
			ITransactionConverter<AbraTransaction> transactionConverter)
			: base(logger, transactionConverter)
		{
			_transactionConverter = transactionConverter;
		}

		protected override IEnumerable<CryptoTransaction> ConvertRecords(IEnumerable<AbraTransaction> records)
		{
			// aggregate pairs, convert
			var convertedRecords = records
				.GroupBy(rec => rec.TransactionId)
				.Select(CombineAndConvertPairedTransaction);

			return convertedRecords;
		}

		private CryptoTransaction CombineAndConvertPairedTransaction(IEnumerable<AbraTransaction> group)
		{
			if (group.Count() > 2)
				throw new ArgumentOutOfRangeException(nameof(group), "Pair cannot have more than 2 transactions");

			var sorted = group.OrderBy(rec => rec.TransactionType).ToArray();

			var firstTransaction = sorted.First();
			var secondTransaction = sorted.Skip(1).LastOrDefault();
			firstTransaction.Second = secondTransaction;

			return _transactionConverter.Convert(firstTransaction);
		}

		protected override Type ClassMapType => typeof(AbraTransactionCsvMap);
	}

	public class AbraTransactionCsvMap : ClassMap<AbraTransaction>
	{
		public AbraTransactionCsvMap()
		{
			Map(x => x.TransactionDate).Name("Transaction date (UTC)");
			Map(x => x.TransactionId).Name("Transaction ID");
			Map(x => x.TransactionType)
				.Name("Transaction type")
				.TypeConverter<CustomEnumConverter<AbraTransactionType>>();
			Map(x => x.Description).Name("Description");
			Map(x => x.Product).Name("Product");
			Map(x => x.GrossQuantity).Name("Gross Quantity");
			Map(x => x.NetworkFees).Name("Network Fees");
			Map(x => x.NetQuantity).Name("Net Quantity");
			Map(x => x.Rate).Name("Rate (USD)");
			Map(x => x.NetAmountUsd).Name("Net Amount (USD)");
		}
	}
}
