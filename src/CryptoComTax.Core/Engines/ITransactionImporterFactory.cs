using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines.Abra;
using CryptoComTax.Core.Engines.Robinhood;
using CryptoComTax.Core.Engines.Uphold;
using CryptoComTax.Core.Engines.Voyager;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoComTax.Core.Engines
{
	public interface ITransactionImporterFactory
	{
		public ITransactionImporter GetTransactionImporter(TransactionSource transactionSource);
	}

	public class TransactionImporterFactory : ITransactionImporterFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public TransactionImporterFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public ITransactionImporter GetTransactionImporter(TransactionSource transactionSource)
		{
			switch (transactionSource)
			{
				case TransactionSource.Abra:
					return _serviceProvider.GetRequiredService<AbraTransactionImporter>();
				case TransactionSource.Robinhood:
					return _serviceProvider.GetRequiredService<RobinhoodTransactionImporter>();
				case TransactionSource.Uphold:
					return _serviceProvider.GetRequiredService<UpholdTransactionImporter>();
				case TransactionSource.Voyager:
					return _serviceProvider.GetRequiredService<VoyagerTransactionImporter>();
			}

			throw new NotImplementedException();
		}
	}
}
