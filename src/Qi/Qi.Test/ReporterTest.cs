using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qi.DataTables;
using Qi.DataTables.Columns;

namespace Qi.Test
{
    /// <summary>
    ///This is a test class for ReporterTest and is intended
    ///to contain all ReporterTest Unit Tests
    ///</summary>
    [TestClass]
    public class ReporterTest
    {
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
      
        [TestMethod]
        public void BuildRepoert()
        {
            var items = new List<SettlementDetails>
                            {
                                new SettlementDetails
                                    {
                                        Name = "A",
                                        Amount = 1m,
                                    },
                                new SettlementDetails
                                    {
                                        Name = "B",
                                        Amount = 2m,
                                        Amount1 = 2,
                                    },
                                new SettlementDetails
                                    {
                                        Name = "C",
                                        Amount = 3m,
                                        Amount1 = 1,
                                    }
                            };
            var target = new DataTable<SettlementDetails>();
            target.Column("Name", s => s.Name);
            //計算列
            AbstractColumn<decimal?> colum4CalculateRow1 = target.Column("Amount0", s => s.Amount).ForSum();
            AbstractColumn<decimal?> colum4CalculateRow2 = target.Column("Amount1", s => s.Amount1).ForSum();
            target.Sum("Amount+Amount1", colum4CalculateRow1, colum4CalculateRow2);

            target.SetData(items);

            var columns = new[] { "Name", "Amount0", "Amount1", "Amount+Amount1" };
            int index = 0;
            //check column Name;
            foreach (IColumn column in target.Columns)
            {
                Assert.AreEqual(columns[index], column.Name);
                index++;
            }
            //check the row item0;
            index = 0;
            foreach (var item in target.Rows)
            {
                Assert.AreEqual(items[index].Name, item[0]);
                Assert.AreEqual(items[index].Amount, item[1]);
                Assert.AreEqual(items[index].Amount1, item[2]);
                Assert.AreEqual((items[index].Amount ?? 0) + (items[index].Amount1 ?? 0), item[3], "row is " + index);
                index++;
            }

            Assert.AreEqual(items.Sum(item => item.Amount), ((IColumn)colum4CalculateRow1).Sum());
            Assert.AreEqual(items.Sum(item => item.Amount1), ((IColumn)colum4CalculateRow2).Sum());
        }

        #region Nested type: SettlementDetails

        public class SettlementDetails
        {
            public string Name { get; set; }
            public decimal? Amount { get; set; }

            public decimal? Amount1 { get; set; }
        }

        #endregion
    }
}