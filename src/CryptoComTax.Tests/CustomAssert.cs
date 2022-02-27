namespace CryptoComTax.Tests
{
	public static class CustomAssert
	{
		public static void Fail(string message = "Not implemented")
			=> throw new Xunit.Sdk.XunitException(message);
	}
}