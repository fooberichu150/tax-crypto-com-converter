using System.Globalization;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.TypeConverters;
using CsvHelper;
using CsvHelper.Configuration;

namespace CryptoComTax.Core.Engines
{
	public interface ITransactionExporter
	{
		void WriteFile(string path, IEnumerable<CryptoTransaction> transactions);
	}

	public class CryptoComTransactionExporter : ITransactionExporter
	{
		public CryptoComTransactionExporter()
		{

		}

		public void WriteFile(string path, IEnumerable<CryptoTransaction> transactions)
		{
			using (var writer = new StreamWriter(path))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.Context.RegisterClassMap<CryptoComTransactionCsvMap>();
				csv.WriteRecords(transactions);
			}
		}
	}

	public class CryptoComTransactionCsvMap : ClassMap<CryptoTransaction>
	{
		public CryptoComTransactionCsvMap()
		{
			var columnIndex = 0;
			Map(x => x.Date).Index(columnIndex++).Name("Date").TypeConverter<CryptoComDateConverter>();
			Map(x => x.TransactionType).Index(columnIndex++).Name("Type");
			Map(x => x.ReceivedCurrency).Index(columnIndex++).Name("Received Currency");
			Map(x => x.ReceivedAmount).Index(columnIndex++).Name("Received Amount");
			Map(x => x.ReceivedNetWorth).Index(columnIndex++).Name("Received Net Worth");
			Map(x => x.SentCurrency).Index(columnIndex++).Name("Sent Currency");
			Map(x => x.SentAmount).Index(columnIndex++).Name("Sent Amount");
			Map(x => x.SentNetWorth).Index(columnIndex++).Name("Sent Net Worth");
			Map(x => x.FeeCurrency).Index(columnIndex++).Name("Fee Currency");
			Map(x => x.FeeAmount).Index(columnIndex++).Name("Fee Amount");
			Map(x => x.FeeNetWorth).Index(columnIndex++).Name("Fee Net Worth");
			Map(x => x.IsValid).Ignore();
		}
	}
}
