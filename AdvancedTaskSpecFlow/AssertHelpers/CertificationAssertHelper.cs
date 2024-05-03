using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.AssertHelpers
{
    public class CertificationAssertHelper
    {
        public static void assertAddCertificationSuccessMessage(String expected, String actual)
        {
            Assert.That(actual, Does.Match(expected), $"Actual message '{actual}' does not match the expected pattern '{expected}'");
        }
    }
}
