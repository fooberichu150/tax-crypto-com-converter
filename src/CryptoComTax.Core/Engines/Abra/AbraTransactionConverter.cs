using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Core.Engines.Abra
{
	public class AbraTransactionConverter : ITransactionConverter<VoyagerTransaction>
	{
		public AbraTransactionConverter()
		{

		}

		public CryptoTransaction Convert(VoyagerTransaction source)
		{
			throw new NotImplementedException();
		}
	}

	public class AbraTransactionImporter : ITransactionImporter
	{
		public AbraTransactionImporter()
		{

		}

		public IEnumerable<CryptoTransaction> ConvertFile(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}
