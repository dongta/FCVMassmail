using FCVStockTool.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FCVStockTool.Controllers
{
    public class CommonController : Controller
    {
        protected StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        public ActionResult FileExcel(DataTable sourceTable, string controllerName)
        {
            Response.ContentType = "application/vnd.ms-excel";
            //context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Charset = "UTF-8";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + controllerName + ".xls\"");
            Response.ContentEncoding = Encoding.UTF8;
            //context.Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            string excelDoc = string.Empty;
            const string startExcelXML = "<xml version>\r\n<Workbook " +
                  "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                  " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                  "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                  "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                  "office:spreadsheet\">\r\n <Styles>\r\n " +
                  "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                  "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                  "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                  "\r\n <Protection/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                  "x:Family=\"Swiss\"  ss:Color=\"#FFFFFF\" ss:Bold=\"1\"/>\r\n  " +
                  "<Interior ss:Color=\"#4F81BD\" ss:Pattern=\"Solid\"/>\r\n</Style>\r\n " +
                  "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                  " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                  "ss:Format=\"0.0000\"/>\r\n </Style>\r\n " +
                  "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                  "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                  "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                  "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                  "</Styles>\r\n ";
            const string endExcelXML = "</Workbook>";

            int rowCount = 0;
            int sheetCount = 1;

            excelDoc+=startExcelXML;
            excelDoc+="<Worksheet ss:Name=\"Sheet" + sheetCount + "\">";
            excelDoc+="<Table>";
            excelDoc+="<Row>";
            for (int x = 0; x < sourceTable.Columns.Count; x++)
            {
                excelDoc+="<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">";
                excelDoc+=sourceTable.Columns[x].ColumnName;
                excelDoc+="</Data></Cell>";
            }
            excelDoc+="</Row>";
            foreach (DataRow x in sourceTable.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output

                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc+="</Table>";
                    excelDoc+=" </Worksheet>";
                    excelDoc+="<Worksheet ss:Name=\"Sheet" + sheetCount + "\">";
                    excelDoc+="<Table>";
                }
                excelDoc+="<Row>"; //ID=" + rowCount + "

                for (int y = 0; y < sourceTable.Columns.Count; y++)
                {
                    System.Type rowType;
                    rowType = x[y].GetType();
                    switch (rowType.ToString())
                    {
                        case "System.String":
                        case "System.Guid":
                            string XMLstring = x[y].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&amp;");
                            XMLstring = XMLstring.Replace(">", "&gt;");
                            XMLstring = XMLstring.Replace("<", "&lt;");
                            excelDoc+="<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">";
                            excelDoc+=XMLstring;
                            excelDoc+="</Data></Cell>";
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  

                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000

                            //The Following Code puts the date stored in XMLDate 

                            //to the format above

                            DateTime XMLDate = (DateTime)x[y];
                            string XMLDatetoString = ""; //Excel Converted Date

                            XMLDatetoString = XMLDate.Year.ToString() +
                                 "-" +
                                 (XMLDate.Month < 10 ? "0" +
                                 XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                 "-" +
                                 (XMLDate.Day < 10 ? "0" +
                                 XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                 "T" +
                                 (XMLDate.Hour < 10 ? "0" +
                                 XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                 ":" +
                                 (XMLDate.Minute < 10 ? "0" +
                                 XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                 ":" +
                                 (XMLDate.Second < 10 ? "0" +
                                 XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                 ".000";
                            excelDoc+="<Cell ss:StyleID=\"DateLiteral\">" +
                                         "<Data ss:Type=\"DateTime\">";
                            excelDoc+=XMLDatetoString;
                            excelDoc+="</Data></Cell>";
                            break;
                        case "System.Boolean":
                            excelDoc+="<Cell ss:StyleID=\"StringLiteral\">" +
                                        "<Data ss:Type=\"String\">";
                            excelDoc+=x[y].ToString();
                            excelDoc+="</Data></Cell>";
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc+="<Cell ss:StyleID=\"Integer\">" +
                                    "<Data ss:Type=\"Number\">";
                            excelDoc+=x[y].ToString();
                            excelDoc+="</Data></Cell>";
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc+="<Cell ss:StyleID=\"Decimal\">" +
                                  "<Data ss:Type=\"Number\">";
                            excelDoc+=x[y].ToString();
                            excelDoc+="</Data></Cell>";
                            break;
                        case "System.DBNull":
                            excelDoc+="<Cell ss:StyleID=\"StringLiteral\">" +
                                  "<Data ss:Type=\"String\">";
                            excelDoc+="";
                            excelDoc+="</Data></Cell>";
                            break;
                        default:
                            XMLstring = x[y].ToString();
                            XMLstring = XMLstring.Trim();
                            XMLstring = XMLstring.Replace("&", "&amp;");
                            XMLstring = XMLstring.Replace(">", "&gt;");
                            XMLstring = XMLstring.Replace("<", "&lt;");
                            excelDoc+="<Cell ss:StyleID=\"StringLiteral\">" +
                                           "<Data ss:Type=\"String\">";
                            excelDoc+=XMLstring;
                            excelDoc+="</Data></Cell>";
                            break;
                    }
                }
                excelDoc+="</Row>";
            }
            excelDoc+="</Table>";
            excelDoc+=" </Worksheet>";
            excelDoc+=endExcelXML;

            Response.Write(excelDoc.ToString() + "\n");
            Response.Flush();
            Response.End();
            //var grid = new GridView();
            //grid.DataSource = dt;
            //grid.DataBind();
            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=" + controllerName + ".xls");
            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "utf-8";
            //ExcelHelper
            //using (StringWriter sw = new StringWriter())
            //{
            //    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            //    {
            //        grid.RenderControl(htw);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
            return RedirectToAction("Index", controllerName);
        }
        public DataTable queryToDataTable<T>(IEnumerable varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others
                // will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
	}
}