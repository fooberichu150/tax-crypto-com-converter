using CryptoComTax.Core.Domain.Transactions;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoComTax.Core.Engines.Robinhood
{
	public class RobinhoodTransactionImporter : TransactionImporterBase<RobinhoodTransaction>
	{
		public RobinhoodTransactionImporter(ILogger<RobinhoodTransactionImporter> logger,
			ITransactionConverter<RobinhoodTransaction> transactionConverter)
			: base(logger, transactionConverter)
		{
		}

		protected override Type ClassMapType => typeof(RobinhoodTransactionCsvMap);
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
