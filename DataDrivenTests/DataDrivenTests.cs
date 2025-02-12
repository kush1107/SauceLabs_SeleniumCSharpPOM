using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using SauceLabsAutomationPOM.Utils;

namespace SauceLabsAutomationPOM.DataDrivenTests
{
    public class DataDrivenTests
    {
        private ExcelReader excelReader;
        public static string workingDirectory = Directory.GetCurrentDirectory();
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        private string excelPath = Path.Combine(projectDirectory, "TestData.xlsx");

        [SetUp]
        public void SetUp()
        {
            excelReader = new ExcelReader(excelPath);
        }

        [Test]
        public void TestSpecificData()
        {
            //GetTestDataByColumn(<Your Excel Sheet Name>, <Your Testcase Name>, <Your Column Name>)

            string testData1 = excelReader.GetTestDataByColumn("UserData", "LoginTest", "Username");
            Console.WriteLine($"Extracted Data: {testData1}");

            string testData2 = excelReader.GetTestDataByColumn("UserData", "LoginTest", "Password");
            Console.WriteLine($"Extracted Data: {testData2}");
        }

    }
}
