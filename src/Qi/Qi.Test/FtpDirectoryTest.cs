using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qi.Net.Ftp;

namespace Qi.Test
{
    /// <summary>
    ///This is a test class for FtpDirectoryTest and is intended
    ///to contain all FtpDirectoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class FtpDirectoryTest
    {
        const string ip = "10.169.1.6";
        const int port = 21;
        const string user = "leo";
        const string password = "leo128svt";
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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

        /// <summary>
        ///A test for Connect
        ///</summary>
        [TestMethod]
        public void ConnectTest()
        {


            FtpDirectory actual = FtpDirectory.Connect(ip, port, user, password);

            Assert.AreEqual(password, actual.Password);
            Assert.AreEqual(user, actual.UserName);
        }

        /// <summary>
        ///A test for GetFiles
        ///</summary>
        [TestMethod]
        public void GetFilesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            var target = new FtpDirectory_Accessor(param0); // TODO: Initialize to an appropriate value
            List<FtpFile> expected = null; // TODO: Initialize to an appropriate value
            List<FtpFile> actual;
            actual = target.GetFiles();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetFiles
        ///</summary>
        [TestMethod]
        public void GetFilesTest1()
        {
            FtpDirectory target = FtpDirectory.Connect("ftp://10.169.1.6:21", user, password);
            List<FtpFile> actual = target.GetFiles();

        }
    }
}