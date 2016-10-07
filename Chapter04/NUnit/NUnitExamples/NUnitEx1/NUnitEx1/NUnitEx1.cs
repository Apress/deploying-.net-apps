using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
namespace NUnitEx1
{
    /// <summary>
    /// This is a very simple NUnit test.
    /// The <code>TestFixture</code> attribute signals to NUnit that this class contains
    /// NUnit test cases within it.
    /// </summary>
    [TestFixture]
    public class NUnitEx1
    {
        /// <summary>
        /// Method to test string comparison.
        /// The <code>Test</code> attribute tells NUnit that this is a test case.
        /// </summary>
        [Test]
        public void TestStringEqual()
        {
            string testString = "Hello NUnit";
            Assert.AreEqual(testString, testString);
            Assert.AreSame(testString, testString);
        }
        [Test]
        public void TestIntEqual()
        {
            int intValue = 5;
            Assert.AreEqual(intValue, intValue);
        }
        [Test]
        public void IsTest()
        {
            Assert.IsFalse(false);
            Assert.IsTrue(true);
        }
        /// <summary>
        /// Will not be executed by NUnit because no <code>Test</code> attribute
        /// </summary>
        public void NotATest()
        {
            Assert.IsTrue(false);
        }
    }
}
