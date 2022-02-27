namespace CryptoComTax.Core.Domain.Transactions
{
	public class UpholdTransaction
	{
		public DateTime Date { get; set; }
		public string Destination { get; set; } = string.Empty;
		public decimal DestinationAmount { get; set; }
		public string DestinationCurrency { get; set; } = string.Empty;
		public decimal? FeeAmount { get; set; }
		public string FeeCurrency { get; set; } = string.Empty;
		public Guid Id { get; set; }
		public string Origin { get; set; } = string.Empty;
		public decimal OriginAmount { get; set; }
		public string OriginCurrency { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
		public UpholdType Type { get; set; }
	}

	public enum UpholdType
	{
		In,
		Out,
		Transfer
	}
}
