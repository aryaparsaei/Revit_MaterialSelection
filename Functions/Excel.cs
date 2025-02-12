using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace RevitAddIn
{
    class Excel
    {
        private readonly string _path;
        private Workbook _workbook;
        private readonly Worksheet _worksheet;

        public Excel(string path,int sheet)
        {
            _path = path;
            _workbook = new Application().Workbooks.Open(path);
            _worksheet = _workbook.Worksheets[sheet];
        }

        private Range UsedRange()
        {
            return _worksheet.UsedRange;
        }

        public int RowCount()
        {
            return UsedRange().Rows.Count;
        }

        public int ColumnCount()
        {
            return UsedRange().Columns.Count;
        }

        public string ReadCell(int rowNumber, int columnNumber)
        {
            var cells = (_worksheet.Cells[rowNumber, columnNumber] as Range).Value2;
            return cells.ToString();
        }

        public string[,] ReadRange(int start_i, int start_j, int end_i, int end_j)
        {
            Range range = _worksheet.Range[_worksheet.Cells[start_i, start_j] as Range, _worksheet.Cells[end_i, end_j] as Range];
            Object[,] cellsObjects = range.Value2;
            string[,] stringResult = new string[end_i - start_i + 1, end_j - start_j + 1];

            for (int i = 1; i <= end_i; i++)
            {
                for (int j = 1; j <= end_j; j++)
                {
                    if (cellsObjects[i, j] != null)
                        stringResult[i - 1, j - 1] = cellsObjects[i, j].ToString();
                    else
                    {
                        stringResult[i - 1, j - 1] = "";
                    }
                }
            }

            return stringResult;
        }

        public string[,] ReadRange()
        {
            Range range = _worksheet.Range[_worksheet.Cells[1, 1] as Range, _worksheet.Cells[RowCount(), ColumnCount()] as Range];
            Object[,] cellsObjects = range.Value2;
            string[,] stringResult = new string[RowCount(), ColumnCount()];

            for (int i = 1; i <= RowCount(); i++)
            {
                for (int j = 1; j <= ColumnCount(); j++)
                {
                    if (cellsObjects[i, j] != null)
                        stringResult[i - 1, j - 1] = cellsObjects[i, j].ToString();
                    else
                    {
                        stringResult[i - 1, j - 1] = "";
                    }
                }
            }

            return stringResult;
        }

        public static int GetRowNumber(string[,] array, string strValue)
        {
            var i = array.GetLength(0);
            var j = array.GetLength(1);

            for (int k = 0; k < i; k++)
            {
                for (int l = 0; l < j; l++)
                {
                    if (array[k, l] == strValue)
                        return k;
                }
            }
            return -1;
        }
        public int GetRowNumber(string strValue)
        {
            int row = -1;
            string[,] strRange = ReadRange();
            for (int i = 0; i < RowCount(); i++)
            {
                for (int j = 0; j < ColumnCount(); j++)
                {
                    if (strValue == strRange[i, j])
                    {
                        row = i;
                        break;
                    }
                }
                if (row != -1)
                {
                    break;
                }
            }
            return row;
        }
        public static int GetColumnNumber(string[,] array, string strValue)
        {
            var i = array.GetLength(0);
            var j = array.GetLength(1);

            for (int k = 0; k < i; k++)
            {
                for (int l = 0; l < j; l++)
                {
                    if (array[k, l] == strValue)
                        return l;
                }
            }
            return -1;
        }
        public int GetColumnNumber(string strValue)
        {
            int column = -1;
            string[,] strRange = ReadRange();
            for (int i = 0; i < RowCount(); i++)
            {
                for (int j = 0; j < ColumnCount(); j++)
                {
                    if (strValue == strRange[i, j])
                    {
                        column = j;
                        break;
                    }
                }
                if (column != -1)
                {
                    break;
                }
            }
            return column;
        }

        // General Functions | Save / Save As... / Create New File
        public void CreateNewFile()
        {
            _workbook = new Application().Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        }
        
        public void Save()
        {
            _workbook.Save();
        }

        public void SaveAs(string path)
        {
            _workbook.SaveAs(path);
        }

        public void Close()
        {
            _workbook.Close();
        }
    }
}
