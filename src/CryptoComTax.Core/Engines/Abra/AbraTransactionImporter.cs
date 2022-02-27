using CryptoComTax.Core.Domain.Transactions;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoComTax.Core.Engines.Abra
{
	public class AbraTransactionImporter : TransactionImporterBase<AbraTransaction>
	{
		public AbraTransactionImporter(ILogger<AbraTransactionImporter> logger,
			ITransactionConverter<AbraTransaction> transactionConverter)
			: base(logger, transactionConverter)
		{
		}

		protected override Type ClassMapType => typeof(AbraTransactionCsvMap);
	}

	public class AbraTransactionCsvMap : ClassMap<AbraTransaction>
	{
		public AbraTransactionCsvMap()
		{
			Map(x => x.TransactionDate).Name("Transaction date (UTC)");
			Map(x => x.TransactionId).Name("Transaction ID");
			Map(x => x.TransactionType).Name("Transaction type");
			Map(x => x.Description).Name("Description");
			Map(x => x.Product).Name("Product");
			Map(x => x.Quantity).Name("Gross Quantity");
			Map(x => x.NetworkFees).Name("Network Fees");
			Map(x => x.NetQuantity).Name("Net Quantity");
			Map(x => x.Rate).Name("Rate (USD)");
			Map(x => x.NetAmountUsd).Name("Net Amount (USD)");
		}
	}
}
