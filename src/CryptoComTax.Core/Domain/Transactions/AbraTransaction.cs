namespace CryptoComTax.Core.Domain.Transactions
{
	public class AbraTransaction
	{
		public DateTime TransactionDate { get; set; }
		public string TransactionId { get; set; } = string.Empty;
		public string TransactionType { get; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Product { get; set; } = string.Empty;
		public decimal Quantity { get; set; }
		public decimal NetworkFees { get; set; }
		public decimal NetQuantity { get; set; }
		public string Rate { get; set; } = string.Empty;
		public decimal NetAmountUsd { get; set; }
	}
}
