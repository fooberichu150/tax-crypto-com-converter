using CryptoComTax.Core.Engines;
using Microsoft.Extensions.Logging;
using CommandLine;
using CommandLine.Text;

public class Application
{
	private readonly ITransactionImporterFactory _transactionImporterFactory;
	private readonly ILogger<Application> _logger;
	private readonly ITransactionExporter _transactionExporter;

	public Application(ILogger<Application> logger, 
		ITransactionImporterFactory transactionImporterFactory,
		ITransactionExporter transactionExporter)
	{
		_logger = logger;
		_transactionImporterFactory = transactionImporterFactory;
		_transactionExporter = transactionExporter;
	}

	public async Task<int> RunAsync(string[] args)
	{
		var exitCode = 0;

		var parser = new Parser(with =>
		{
			with.CaseInsensitiveEnumValues = true;
			with.HelpWriter = Console.Error;
		});

		var parsed = parser.ParseArguments<CryptoComTax.Console.Models.Arguments>(args);

		parsed.WithParsed(args =>
		{
			if (!File.Exists(args.InputFile))
			{
				throw new ArgumentException(nameof(args.InputFile), "File does not exist");
			}

			if (!Directory.Exists(args.OutputFolder))
			{
				throw new ArgumentException(nameof(args.OutputFolder), "Output folder does not exist");
			}

			var importer = _transactionImporterFactory.GetTransactionImporter(args.Exchange);
			var records = importer
				.ConvertFile(args.InputFile)
				.ToList();

			var outputFileName = !string.IsNullOrWhiteSpace(args.OutputFile)
				? args.OutputFile
				: $"converted-crypto-com-tax-{Path.GetFileName(args.InputFile)}";

			var outputPath = Path.Combine(args.OutputFolder, outputFileName);
			_transactionExporter.WriteFile(outputPath, records);
		});

		parsed.WithNotParsed(errs =>
		{
			var helpText = HelpText.AutoBuild(parsed, h => HelpText.DefaultParsingErrorsHandler(parsed, h), e => e);
			Console.Error.Write(helpText);

			exitCode = -2;
		});

		return await Task.FromResult(exitCode);
	}
}