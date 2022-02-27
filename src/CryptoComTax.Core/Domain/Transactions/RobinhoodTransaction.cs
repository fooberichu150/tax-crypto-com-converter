namespace CryptoComTax.Core.Domain.Transactions
{
	public class RobinhoodTransaction
	{
		public string AssetName { get; set; } = string.Empty;
		public DateTime ReceivedDate { get; set; }
		public decimal CostBasisUsd { get; set; }
		public DateTime? SoldDate { get; set; }
		public decimal? Proceeds { get; set; }
		public decimal AmountSold { get; set; }
		public string Duration { get; set; } = string.Empty;
		public bool NonCovered { get; set; }
		public bool BasisReported { get; set; }

		/// <summary>
		/// Magic value, doesn't come from the CSV; used for later conversion routine
		/// </summary>
		public RobinhoodTransactionType TransactionType { get; set; } = RobinhoodTransactionType.Buy;
	}

	public enum RobinhoodTransactionType
	{
		Unknown,
		Buy,
		Sell
	}
}
