using System.IO;
using System.Linq;
using CryptoComTax.Core.Domain.Transactions;
using CryptoComTax.Core.Engines;
using CryptoComTax.Core.Engines.Voyager;
using CryptoComTax.Tests.TestFixtures;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CryptoComTax.Tests
{
	public class VoyagerTransactionImporterTests
	{
		private readonly Mock<ILogger<VoyagerTransactionImporter>> _mockLogger;
		private readonly Mock<ITransactionConverter<VoyagerTransaction>> _mockConverter;

		private VoyagerTransactionImporter Target { get; }

		public VoyagerTransactionImporterTests()
		{
			_mockLogger = new Mock<ILogger<VoyagerTransactionImporter>>();
			_mockConverter = new Mock<ITransactionConverter<VoyagerTransaction>>();

			Target = new VoyagerTransactionImporter(_mockLogger.Object, _mockConverter.Object);
		}

		[Fact]
		public void ConvertFile_Success()
		{
			// arrange
			var path = TestConstants.VoyagerTestFile;

			// act
			var results = Target
				.ConvertFile(path)
				.ToList();

			// assert
			results.Should().NotBeEmpty();
			results.Count.Should().Be(TestConstants.VoyagerConvertCount);
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