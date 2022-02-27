using CryptoComTax.Core.Engines.Robinhood;
using Xunit;

namespace CryptoComTax.Core.Tests.Engines.Robinhood
{
	public class RobinhoodTransactionConverterTests
	{
		private RobinhoodTransactionConverter Target { get; }

		public RobinhoodTransactionConverterTests()
		{
			Target = new RobinhoodTransactionConverter();
		}

		[Fact]
		public void DoStuff()
		{
			// arrange

			// act

			// assert
			CustomAssert.Fail();
		}
	}
}