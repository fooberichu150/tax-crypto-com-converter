namespace CryptoComTax.Core.Domain.Transactions
{
	// https://help.crypto.com/en/articles/5019792-data-import
	public class CryptoTransaction
	{
		public bool IsValid { get; set; } = true;

		// MM/DD/YYYY HH:MM:SS
		public DateTime Date { get; set; }
		public TransactionType TransactionType { get; set; }
		public string ReceivedCurrency { get; set; } = string.Empty;
		public decimal ReceivedAmount { get; set; }
		public decimal? ReceivedNetWorth { get; set; }
		public string SentCurrency { get; set; } = string.Empty;
		public decimal? SentAmount { get; set; }
		public decimal? SentNetWorth { get; set; }
		public string? FeeCurrency { get; set; }
		public decimal? FeeAmount { get; set; }
		public decimal? FeeNetWorth { get; set; }
	}
}
