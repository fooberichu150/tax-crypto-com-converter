namespace CryptoComTax.Core.Domain.Transactions
{
	public enum VoyagerTransactionDirection
	{
		Buy,
		Deposit,
		Sell,
		Withdrawal
	}

	public enum VoyagerTransactionType
	{
		Bank,
		Reward,
		Trade
	}

	public class VoyagerTransaction
	{
		public DateTime TransactionDate { get; set; }

		public string TransactionId { get; set; } = string.Empty;

		public VoyagerTransactionDirection TransactionDirection { get; set; }

		public VoyagerTransactionType TransactionType { get; set; }

		public string BaseAsset { get; set; } = string.Empty;

		public string QuoteAsset { get; set; } = string.Empty;

		public decimal Quantity { get; set; }

		public decimal NetAmount { get; set; }

		public decimal Price { get; set; }
	}
}
