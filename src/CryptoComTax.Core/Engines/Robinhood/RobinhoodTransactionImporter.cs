using CryptoComTax.Core.Domain.Transactions;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using CryptoComTax.Core.Extensions;

namespace CryptoComTax.Core.Engines.Robinhood
{
	public class RobinhoodTransactionImporter : TransactionImporterBase<RobinhoodTransaction>
	{
		private readonly ITransactionConverter<RobinhoodTransaction> _transactionConverter;

		public RobinhoodTransactionImporter(ILogger<RobinhoodTransactionImporter> logger,
			ITransactionConverter<RobinhoodTransaction> transactionConverter)
			: base(logger, transactionConverter)
		{
			_transactionConverter = transactionConverter;
		}

		protected override Type ClassMapType => typeof(RobinhoodTransactionCsvMap);

		protected override IEnumerable<CryptoTransaction> ConvertRecords(IEnumerable<RobinhoodTransaction> records)
		{
			var convertedRecords = records
				.SelectMany(SplitTransaction)
				.Select(record => _transactionConverter.Convert(record))
				.ToArray();

			return convertedRecords;
		}

		private IEnumerable<RobinhoodTransaction> SplitTransaction(RobinhoodTransaction transaction)
		{
			var transactions = new List<RobinhoodTransaction>();

			// poor-man's way of splitting transaction; we'll introduce our own transaction type but keep the rest
			// of the fields the same
			if (transaction.SoldDate.HasValue)
			{
				var sellTransaction = transaction.Clone();
				sellTransaction.TransactionType = RobinhoodTransactionType.Sell;
				transactions.Add(sellTransaction);
			}

			transactions.Add(transaction);

			return transactions;
		}
	}

	public class RobinhoodTransactionCsvMap : ClassMap<RobinhoodTransaction>
	{
		public RobinhoodTransactionCsvMap()
		{
			Map(x => x.AssetName).Name("ASSET NAME");
			Map(x => x.ReceivedDate).Name("RECEIVED DATE");
			Map(x => x.CostBasisUsd).Name("COST BASIS(USD)");
			Map(x => x.SoldDate).Name("DATE SOLD");
			Map(x => x.Proceeds).Name("PROCEEDS");
			Map(x => x.AmountSold).Name("AMOUNT SOLD");
			Map(x => x.Duration).Name("DURATION");
			Map(x => x.NonCovered).Name("NONCOVERED");
			Map(x => x.BasisReported).Name("BASIS REPORTED");
		}
	}
}
