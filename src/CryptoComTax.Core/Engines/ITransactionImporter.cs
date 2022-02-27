using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines
{
	public interface ITransactionImporter
	{
		public IEnumerable<CryptoTransaction> ConvertFile(string filePath);
	}
}
