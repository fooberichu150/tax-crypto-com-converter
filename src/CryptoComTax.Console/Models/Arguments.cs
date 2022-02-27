using CommandLine;
using CryptoComTax.Core.Domain.Transactions;

namespace CryptoComTax.Console.Models
{
	public class Arguments
	{
		[Option('e', "exchange", Required = true, HelpText = "Exchange file to load: Abra, Robinhood, Uphold, Voyager")]
		public TransactionSource Exchange { get; set; }

		[Option('i', "input", Required = true, HelpText = "Full path to input file")]
		public string InputFile { get; set; } = string.Empty;

		[Option('o', "output", Required = true, HelpText = "Folder path to place output file")]
		public string OutputFolder { get; set; } = string.Empty;

		[Option('f', "filename", Required = false, HelpText = "Optional filename to name output file")]
		public string OutputFile { get; set; } = string.Empty;
	}
}
