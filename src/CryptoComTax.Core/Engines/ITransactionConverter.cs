using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines
{
	public interface ITransactionConverter<T>
	{
		public CryptoTransaction Convert(T source);
	}
}