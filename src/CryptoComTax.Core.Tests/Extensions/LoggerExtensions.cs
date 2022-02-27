using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace CryptoComTax.Tests.Extensions
{
    /// <summary>
    /// LoggingExtensions as borrowed (and modified) from https://adamstorr.azurewebsites.net/blog/mocking-ilogger-with-moq
    /// </summary>
	public static class LoggerExtensions
    {
        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger,
            string expectedMessage = null,
            LogLevel expectedLogLevel = LogLevel.Debug,
            Times? times = null)
        {
            times ??= Times.Once();

            Func<object, Type, bool> state = null;
            if (!string.IsNullOrWhiteSpace(expectedMessage))
            {
                state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;
            }
            else
            {
                state = (v, t) => true;
            }

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times)times);

            return logger;
        }
    }
}
