using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines.Abra
{
	public class AbraTransactionConverter : ITransactionConverter<AbraTransaction>
	{
		public AbraTransactionConverter()
		{

		}

		public CryptoTransaction Convert(AbraTransaction source)
		{
			throw new NotImplementedException();
		}
	}
}
