namespace CryptoComTax.Core.Domain.Transactions
{
	public class AbraTransaction
	{
		public DateTime TransactionDate { get; set; }
		public string TransactionId { get; set; }
		public string Description { get; set; }
		public string Product { get; set; }
		public decimal Quantity { get; set; }
		public decimal NetworkFees { get; set; }
		public decimal NetQuantity { get; set; }
		public string Rate { get; set; }
		public decimal NetAmountUsd { get; set; }
	}
}
