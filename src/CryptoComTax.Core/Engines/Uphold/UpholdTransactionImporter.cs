using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.TypeConverters;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoComTax.Core.Engines.Uphold
{
	public class UpholdTransactionImporter : TransactionImporterBase<UpholdTransaction>
	{
		public UpholdTransactionImporter(ILogger<UpholdTransactionImporter> logger,
			ITransactionConverter<UpholdTransaction> transactionConverter)
			: base(logger, transactionConverter)
		{

		}

		protected override Type ClassMapType => typeof(UpholdTransactionCsvMap);
	}

	public class UpholdTransactionCsvMap : ClassMap<UpholdTransaction>
	{
		public UpholdTransactionCsvMap()
		{
			Map(x => x.Date).Name("Date").TypeConverter<UpholdDateConverter>();
			Map(x => x.Destination).Name("Destination");
			Map(x => x.DestinationAmount).Name("Destination Amount");
			Map(x => x.DestinationCurrency).Name("Destination Currency");
			Map(x => x.FeeAmount).Name("Fee Amount");
			Map(x => x.FeeCurrency).Name("Fee Currency");
			Map(x => x.Id).Name("Id");
			Map(x => x.Origin).Name("Origin");
			Map(x => x.OriginAmount).Name("Origin Amount");
			Map(x => x.OriginCurrency).Name("Origin Currency");
			Map(x => x.Status).Name("Status");
			Map(x => x.Type)
				.Name("Type")
				.TypeConverter<CustomEnumConverter<UpholdType>>();
		}
	}
}
