namespace CryptoComTax.Core.Domain.Transactions
{
	public class RobinhoodTransaction
	{
		public string AssetName { get; set; }
		public DateTime ReceivedDate { get; set; }
		public decimal CostBasisUsd { get; set; }
		public DateTime SoldDate { get; set; }
		public decimal Proceeds { get; set; }
		public decimal AmountSold { get; set; }
		public string Duration { get; set; }
		public bool NonCovered { get; set; }
		public bool BasisReported { get; set; }
	}
}
