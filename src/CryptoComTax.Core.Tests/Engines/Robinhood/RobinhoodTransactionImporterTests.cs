using System.IO;
using System.Linq;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines;
using CryptoComTax.Core.Engines.Robinhood;
using CryptoComTax.Core.Tests.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CryptoComTax.Core.Tests.Engines.Robinhood
{
	public class RobinhoodTransactionImporterTests
	{
		private readonly Mock<ILogger<RobinhoodTransactionImporter>> _mockLogger;
		private readonly Mock<ITransactionConverter<RobinhoodTransaction>> _mockConverter;

		private RobinhoodTransactionImporter Target { get; }

		public RobinhoodTransactionImporterTests()
		{
			_mockLogger = new Mock<ILogger<RobinhoodTransactionImporter>>();
			_mockConverter = new Mock<ITransactionConverter<RobinhoodTransaction>>();

			Target = new RobinhoodTransactionImporter(_mockLogger.Object, _mockConverter.Object);
		}

		[Fact]
		public void ConvertFile_Success()
		{
			// arrange
			var path = TestConstants.RobinhoodTestFile;

			// act
			var results = Target
				.ConvertFile(path)
				.ToList();

			// assert
			results.Should().NotBeEmpty();
			results.Count.Should().Be(TestConstants.RobinhoodConvertCount);
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