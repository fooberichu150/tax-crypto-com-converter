namespace CryptoComTax.Core.Domain.Transactions
{
	public class UpholdTransaction
	{
		public DateTime Date { get; set; }
		public string Destination { get; set; }
		public decimal DestinationAmount { get; set; }
		public string DestinationCurrency { get; set; }
		public decimal FeeAmount { get; set; }
		public string FeeCurrency { get; set; }
		public Guid Id { get; set; }
		public string Origin { get; set; }
		public decimal OriginAmount { get; set; }
		public string OriginCurrency { get; set; }
		public string Status { get; set; }
		public string Type { get; set; } // such as in, out
	}
}
