using System.ComponentModel;

namespace CryptoComTax.Core.Domain.Transactions
{
	public class AbraTransaction
	{
		public DateTime TransactionDate { get; set; }
		public string TransactionId { get; set; } = string.Empty;
		public AbraTransactionType TransactionType { get; set; }
		public string Description { get; set; } = string.Empty;
		public string Product { get; set; } = string.Empty;
		public decimal GrossQuantity { get; set; }
		public decimal NetworkFees { get; set; }
		public decimal NetQuantity { get; set; }
		public string Rate { get; set; } = string.Empty;
		public decimal NetAmountUsd { get; set; }

		public AbraTransaction? Second { get; set; }
	}

	public enum AbraTransactionType
	{
		[Description("Inbound Transfer")]
		InboundTransfer,
		Sell,
		Buy,
		[Description("Interest Payment")]
		InterestPayment,
		Reward
	}
}
