using NUnit.Framework;
using Serilog;

namespace PublicPackages.Tests
{
    public class SerilogTests
    {
        [Test]
        public void ConfigureFileSink()
        {
            var log = new LoggerConfiguration()
                           .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                           .CreateLogger();

            log.Information("Log configured");
        }
    }
}
