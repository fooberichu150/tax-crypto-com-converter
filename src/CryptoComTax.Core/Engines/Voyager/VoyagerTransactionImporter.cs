﻿using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.TypeConverters;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoComTax.Core.Engines.Voyager
{
	public class VoyagerTransactionImporter : TransactionImporterBase<VoyagerTransaction>
	{
		private readonly ITransactionConverter<VoyagerTransaction> _transactionConverter;

		protected override Type ClassMapType => typeof(VoyagerTransactionCsvMap);

		public VoyagerTransactionImporter(ILogger<VoyagerTransactionImporter> logger,
			ITransactionConverter<VoyagerTransaction> transactionConverter)
			: base(logger, transactionConverter)
		{
			_transactionConverter = transactionConverter;
		}

		protected override IEnumerable<CryptoTransaction> ConvertRecords(IEnumerable<VoyagerTransaction> records)
		{
			var convertedRecords = records
				.Where(record => record.TransactionType != VoyagerTransactionType.Bank)
				.Select(record => _transactionConverter.Convert(record))
				.ToArray();

			return convertedRecords;
		}
	}

	public class VoyagerTransactionCsvMap : ClassMap<VoyagerTransaction>
	{
		public VoyagerTransactionCsvMap()
		{
			Map(x => x.BaseAsset).Name("base_asset");
			Map(x => x.NetAmount).Name("net_amount");
			Map(x => x.Price).Name("price");
			Map(x => x.Quantity).Name("quantity");
			Map(x => x.QuoteAsset).Name("quote_asset");
			Map(x => x.TransactionDate).Name("transaction_date");
			Map(x => x.TransactionDirection)
				.Name("transaction_direction")
				.TypeConverter<CustomEnumConverter<VoyagerTransactionDirection>>();
			Map(x => x.TransactionId).Name("transaction_id");
			Map(x => x.TransactionType)
				.Name("transaction_type")
				.TypeConverter<CustomEnumConverter<VoyagerTransactionType>>();
		}
	}
}
