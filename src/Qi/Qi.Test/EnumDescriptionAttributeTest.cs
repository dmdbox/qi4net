using System.Globalization;
using System.Resources;
using System.Threading;
using Qi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Qi.Test
{


    /// <summary>
    ///This is a test class for EnumDescriptionAttributeTest and is intended
    ///to contain all EnumDescriptionAttributeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EnumDescriptionAttributeTest
    {
        private enum TestResource
        {
            [EnumDescription("String1", ResourceType = typeof(Resource1))]
            Apple,
            [EnumDescription("String2", ResourceType = typeof(Resource1))]
            Orange
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion



        [TestMethod]
        public void EnumDescriptionAttribute_ResourceKey_multiDistrict()
        {

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            var result = EnumHelper.ToString(TestResource.Orange);
            Assert.AreEqual(Resource1.String2, result);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("zh-CN");
            Resource1.Culture = Thread.CurrentThread.CurrentCulture;
            result = EnumHelper.ToString(TestResource.Orange);
            Assert.AreEqual(Resource1.String2, result);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("zh-TW");
            Resource1.Culture = Thread.CurrentThread.CurrentCulture;
            result = EnumHelper.ToString(TestResource.Apple);
            Assert.AreEqual(Resource1.String1, result);

        }
        /// <summary>
        ///A test for EnumDescriptionAttribute Constructor
        ///</summary>
        [TestMethod()]
        public void EnumDescriptionAttributeConstructorTest1()
        {
            string description = string.Empty; // TODO: Initialize to an appropriate value
            EnumDescriptionAttribute target = new EnumDescriptionAttribute(description);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Description
        ///</summary>
        [TestMethod()]
        public void DescriptionTest()
        {
            string description = string.Empty; // TODO: Initialize to an appropriate value
            EnumDescriptionAttribute target = new EnumDescriptionAttribute(description); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsDefault
        ///</summary>
        [TestMethod()]
        public void IsDefaultTest()
        {
            string description = string.Empty; // TODO: Initialize to an appropriate value
            EnumDescriptionAttribute target = new EnumDescriptionAttribute(description); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsDefault = expected;
            actual = target.IsDefault;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Language
        ///</summary>
        [TestMethod()]
        public void LanguageTest()
        {

            string description = string.Empty; // TODO: Initialize to an appropriate value
            EnumDescriptionAttribute target = new EnumDescriptionAttribute(description); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Language = expected;
            actual = target.Language;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
