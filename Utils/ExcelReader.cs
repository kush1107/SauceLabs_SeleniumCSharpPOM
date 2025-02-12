using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SauceLabsAutomationPOM.Utils
{
    public class ExcelReader
    {
        private readonly string filePath;
        private readonly Dictionary<string, List<Dictionary<string, string>>> sheetData;

        public ExcelReader(string excelFilePath)
        {
            filePath = excelFilePath;
            sheetData = new Dictionary<string, List<Dictionary<string, string>>>();
            LoadExcelData();
        }

        private void LoadExcelData()
        {
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(file);

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    var sheetName = workbook.GetSheetName(i);
                    var dataList = new List<Dictionary<string, string>>();

                    IRow headerRow = sheet.GetRow(0);
                    int colCount = headerRow.LastCellNum;
                    var columnNames = new List<string>();
                    for (int col = 0; col < colCount; col++)
                    {
                        columnNames.Add(headerRow.GetCell(col)?.ToString().Trim() ?? string.Empty);
                    }

                    for (int row = 1; row <= sheet.LastRowNum; row++)
                    {
                        IRow currentRow = sheet.GetRow(row);
                        if (currentRow == null) continue;

                        var rowData = new Dictionary<string, string>();
                        for (int col = 0; col < colCount; col++)
                        {
                            rowData[columnNames[col]] = currentRow.GetCell(col)?.ToString().Trim() ?? "";
                        }
                        dataList.Add(rowData);
                    }
                    sheetData[sheetName] = dataList;
                }
            }
        }

        public List<string> GetSheetNames()
        {
            return new List<string>(sheetData.Keys);
        }

        public string GetTestDataByColumn(string sheetName, string testCaseName, string columnName)
        {
            if (!sheetData.ContainsKey(sheetName))
                throw new Exception($"Sheet '{sheetName}' not found in {filePath}");

            var testCaseRow = sheetData[sheetName].Find(row => row.ContainsKey("TestCase") && row["TestCase"].Equals(testCaseName, StringComparison.OrdinalIgnoreCase));
            if (testCaseRow == null)
                throw new Exception($"Test case '{testCaseName}' not found in sheet '{sheetName}'");

            if (!testCaseRow.ContainsKey(columnName))
                throw new Exception($"Column '{columnName}' not found in sheet '{sheetName}'");

            return testCaseRow[columnName];
        }
    }
}
