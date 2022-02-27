using CryptoComTax.Core.Domain.Transactions;
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
					break;
				case TransactionSource.Robinhood:
					break;
				case TransactionSource.Voyager:
					return _serviceProvider.GetRequiredService<VoyagerTransactionImporter>();
					//return new VoyagerTransactionImporter();
				case TransactionSource.Uphold:
					break;
			}

			throw new NotImplementedException();
		}
	}
}
