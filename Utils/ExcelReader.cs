using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceLabsAutomationPOM.Utils
{
    public class ExcelReader
    {
        private string filePath;
        private IWorkbook workbook;

        public ExcelReader(string excelFilePath)
        {
            filePath = excelFilePath;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fs);
            }
        }

        public Dictionary<string, string> GetDataByTestCase(string sheetName, string testCaseName)
        {
            Dictionary<string, string> testData = new Dictionary<string, string>();
            ISheet sheet = workbook.GetSheet(sheetName);

            if (sheet == null) throw new Exception($"Sheet {sheetName} not found!");

            int rowCount = sheet.LastRowNum;
            IRow headerRow = sheet.GetRow(0);

            for (int i = 1; i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null && row.GetCell(0).ToString().Equals(testCaseName, StringComparison.OrdinalIgnoreCase))
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        string columnName = headerRow.GetCell(j).ToString();
                        string cellValue = row.GetCell(j)?.ToString() ?? "";
                        testData[columnName] = cellValue;
                    }
                    break;
                }
            }
            return testData;
        }
    }
}
