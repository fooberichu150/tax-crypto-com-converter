using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines;
using CryptoComTax.Core.Engines.Abra;
using CryptoComTax.Core.Engines.Robinhood;
using CryptoComTax.Core.Engines.Uphold;
using CryptoComTax.Core.Engines.Voyager;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoComTax.Console.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton<Application>();

			services
				.AddSingleton<ITransactionImporterFactory, TransactionImporterFactory>()
				.AddSingleton<AbraTransactionImporter>()
				.AddSingleton<RobinhoodTransactionImporter>()
				.AddSingleton<UpholdTransactionImporter>()
				.AddSingleton<VoyagerTransactionImporter>();

			services
				.AddSingleton<ITransactionExporter, CryptoComTransactionExporter>();

			services
				.AddSingleton<ITransactionConverter<AbraTransaction>, AbraTransactionConverter>()
				.AddSingleton<ITransactionConverter<RobinhoodTransaction>, RobinhoodTransactionConverter>()
				.AddSingleton<ITransactionConverter<UpholdTransaction>, UpholdTransactionConverter>()
				.AddSingleton<ITransactionConverter<VoyagerTransaction>, VoyagerTransactionConverter>();

			return services;
		}
	}
}
