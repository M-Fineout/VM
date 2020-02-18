using NUnit.Framework;
using System;

namespace ExtensionMethods.Tests.Shared
{
    public class DateExtensionsTests
    {
        [Test]
        public void FormatLegacyDateC0()
        {
            var dateTime = new DateTime(1921, 04, 25);
            var formatted = dateTime.Format();
            Assert.AreEqual("0210425", formatted);
        }

        [Test]
        public void FormatLegacyDateC1()
        {
            var dateTime = new DateTime(1932, 12, 31);
            var formatted = dateTime.Format();
            Assert.AreEqual("1321231", formatted);
        }
    }
}