using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FCVStockTool.Utils
{
    public static class ExcelHelper
    {
        public static DataSet ToDataSet(this HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {

                IExcelDataReader excelReader;
                if (file.FileName.Substring(file.FileName.LastIndexOf(".")).Equals("xls", StringComparison.OrdinalIgnoreCase))
                {
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(file.InputStream);
                }
                else
                {
                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
                }

                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                // DataSet result = excelReader.AsDataSet();

                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet result = excelReader.AsDataSet();
                return result;
            }
            return null;
        }
    }
}