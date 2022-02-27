using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines.Robinhood
{
	public class RobinhoodTransactionConverter : ITransactionConverter<RobinhoodTransaction>
	{
		public CryptoTransaction Convert(RobinhoodTransaction source)
		{
			throw new NotImplementedException();
		}
	}
}
