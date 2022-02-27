using System.IO;
using System.Linq;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines;
using CryptoComTax.Core.Engines.Uphold;
using CryptoComTax.Core.Tests.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CryptoComTax.Core.Tests.Engines.Uphold
{
	public class UpholdTransactionImporterTests
	{
		private readonly Mock<ILogger<UpholdTransactionImporter>> _mockLogger;
		private readonly Mock<ITransactionConverter<UpholdTransaction>> _mockConverter;

		private UpholdTransactionImporter Target { get; }

		public UpholdTransactionImporterTests()
		{
			_mockLogger = new Mock<ILogger<UpholdTransactionImporter>>();
			_mockConverter = new Mock<ITransactionConverter<UpholdTransaction>>();

			Target = new UpholdTransactionImporter(_mockLogger.Object, _mockConverter.Object);
		}

		[Fact]
		public void ConvertFile_Success()
		{
			// arrange
			var path = TestConstants.UpholdTestFile;

			// act
			var results = Target
				.ConvertFile(path)
				.ToList();

			// assert
			results.Should().NotBeEmpty();
			results.Count.Should().Be(TestConstants.UpholdConvertCount);
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