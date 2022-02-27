namespace CryptoComTax.Core.Tests.TestFixtures
{
	public static class TestConstants
	{
		public const string TestFilesSourceFolder = "./Test_Files";
		public const string NotFoundFile = $"{TestFilesSourceFolder}/not-found.csv";

		public const string VoyagerTestFile = $"{TestFilesSourceFolder}/2021-voyager-transactions_2021-01-01_2022-01-01.csv";
		public const int VoyagerConvertCount = 7;

		public const string AbraTestFileFull = $"{TestFilesSourceFolder}/2021_abra_transactions_history_full.csv";
		public const string AbraTestFile = $"{TestFilesSourceFolder}/2021_abra_transactions_history_no_disclaimer.csv";
		public const int AbraConvertCount = 25;

		public const string RobinhoodTestFile = $"{TestFilesSourceFolder}/2021-robinhood-crypto.csv";
		public const int RobinhoodConvertCount = 7;

		public const string UpholdTestFile = $"{TestFilesSourceFolder}/2021-uphold-transactions.csv";
		public const int UpholdConvertCount = 48;
	}

	public static class CryptoTokenConstants
	{
		public const string DogeCoin = "DOGE";
		public const string Ethereum = "ETH";
		public const string Vechain = "VET";
	}
}
