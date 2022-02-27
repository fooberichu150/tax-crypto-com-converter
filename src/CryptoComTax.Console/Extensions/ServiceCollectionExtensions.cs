using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines;
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
				.AddSingleton<VoyagerTransactionImporter>();
			
			services
				.AddSingleton<ITransactionExporter, CryptoComTransactionExporter>();

			services
				.AddSingleton<ITransactionConverter<VoyagerTransaction>, VoyagerTransactionConverter>();

			return services;
		}
	}
}
