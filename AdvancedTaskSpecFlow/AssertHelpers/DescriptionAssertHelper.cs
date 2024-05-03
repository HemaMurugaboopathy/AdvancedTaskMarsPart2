using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.AssertHelpers
{
    public class DescriptionAssertHelper
    {
        public static void assertAddDescriptionSuccessMessage(String expected, String actual)
        {
            Assert.That(expected == actual, "Actual message and expected message do not match");
        }
    }
}
