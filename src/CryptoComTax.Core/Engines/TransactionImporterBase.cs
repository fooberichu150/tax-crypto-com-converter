using System.Globalization;
using CryptoComTax.Core.Domain.Transactions;
using CsvHelper;
using Microsoft.Extensions.Logging;

namespace CryptoComTax.Core.Engines
{
	public abstract class TransactionImporterBase<T> : ITransactionImporter where T : class
	{
		private readonly ILogger _logger;
		private readonly ITransactionConverter<T> _transactionConverter;
		public TransactionImporterBase(ILogger logger, ITransactionConverter<T> transactionConverter)
		{
			_logger = logger;
			_transactionConverter = transactionConverter;
		}

		protected abstract Type ClassMapType { get; }

		public virtual IEnumerable<CryptoTransaction> ConvertFile(string filePath)
		{
			var records = default(List<T>);

			_logger.LogDebug("Attempting to import {fileName}", filePath);
			using (var reader = new StreamReader(filePath))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				csv.Context.RegisterClassMap(ClassMapType);

				records = csv
					.GetRecords<T>()
					.ToList();
			}
			_logger.LogDebug("Imported {count} records", records.Count);

			var convertedRecords = ConvertRecords(records)
				.ToArray();

			_logger.LogDebug("Converted {count} records", convertedRecords.Length);

			return convertedRecords;
		}

		protected virtual IEnumerable<CryptoTransaction> ConvertRecords(IEnumerable<T> records)
		{
			var convertedRecords = records
				.Select(record => _transactionConverter.Convert(record))
				.ToArray();

			return convertedRecords;
		}
	}
}
