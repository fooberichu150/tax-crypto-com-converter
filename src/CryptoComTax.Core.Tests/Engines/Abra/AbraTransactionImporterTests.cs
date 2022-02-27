using System.IO;
using System.Linq;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines;
using CryptoComTax.Core.Engines.Abra;
using CryptoComTax.Core.Tests.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CryptoComTax.Core.Tests.Engines.Abra
{
	public class AbraTransactionImporterTests
	{
		private readonly Mock<ILogger<AbraTransactionImporter>> _mockLogger;
		private readonly Mock<ITransactionConverter<AbraTransaction>> _mockConverter;

		private AbraTransactionImporter Target { get; }

		public AbraTransactionImporterTests()
		{
			_mockLogger = new Mock<ILogger<AbraTransactionImporter>>();
			_mockConverter = new Mock<ITransactionConverter<AbraTransaction>>();

			Target = new AbraTransactionImporter(_mockLogger.Object, _mockConverter.Object);
		}

		[Fact]
		public void ConvertFile_Success()
		{
			// arrange
			var path = TestConstants.AbraTestFile;

			// act
			var results = Target
				.ConvertFile(path)
				.ToList();

			// assert
			results.Should().NotBeEmpty();
			results.Count.Should().Be(TestConstants.AbraConvertCount);
		}

		[Fact]
		public void ConvertFile_NotFound_Error()
		{
			// arrange
			var path = TestConstants.NotFoundFile;

			// act
			var results = () => Target.ConvertFile(path);

			// assert
			Assert.Throws<FileNotFoundException>(results);
		}
	}
}