using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCVStockTool.Utils;
using FCVStockTool.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace FCVStockTool.Controllers
{
    [Filters.AccountantRole]
    public class AccountantAssetController : Controller
    {
        StockDbContext db = new StockDbContext();
        string RetirementDate = "";
        string Acquisition_subcode = "";
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

        void BindList(int pageNo)
        {
            ViewBag.AccountantAsset = db.AccountantAssets.OrderBy(p => p.Group).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            ViewBag.TotalRecords = db.AccountantAssets.Count();
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: AccountantAssets
        public ActionResult Index(int pageNo = 1, int id = 0)
        {
            ViewBag.Mess = "Import File Excel vào bảng AccountantAssets";
            var model = new AccountantAsset();
            if (id > 0) model = db.AccountantAssets.Find(id);
            BindList(pageNo);
            return View(model);
        }

        //download File
        public ActionResult FileExcel(DataTable dt)
        {
            var grid = new GridView();
            grid.DataSource = dt;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=AccountantAssetsImport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    grid.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View("Index", "AccountantAsset");
        }

        public ActionResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            AccountantAsset item;

            DataTable tableResult = new DataTable();
            DataColumn Group = tableResult.Columns.Add("Group");
            DataColumn Class = tableResult.Columns.Add("Class");
            DataColumn Fixedassetcode = tableResult.Columns.Add("Fixed asset code");
            DataColumn Fixedassetname = tableResult.Columns.Add("Fixed asset name");
            DataColumn Costcenter = tableResult.Columns.Add("Cost center");
            DataColumn Usefullife = tableResult.Columns.Add("Useful life");
            DataColumn Depreciationdate = tableResult.Columns.Add("Depreciation date");
            DataColumn Acquisition = tableResult.Columns.Add("Acquisition");
            DataColumn Sub_code = tableResult.Columns.Add("Sub_code");
            DataColumn Description_subcode = tableResult.Columns.Add("Description_sub code");
            DataColumn Barcode = tableResult.Columns.Add("Barcode");
            DataColumn Acquisitionsubcode = tableResult.Columns.Add("Acquisition_subcode");
            DataColumn Retirementdate = tableResult.Columns.Add("Retirement date");
            DataColumn Status = tableResult.Columns.Add("Status");
            DataColumn Owner = tableResult.Columns.Add("Owner");
            DataColumn Remarks = tableResult.Columns.Add("Remarks");
            DataColumn Interpret = tableResult.Columns.Add("Interpret");
            DataColumn State = tableResult.Columns.Add("State");
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r[12].ToString() != "")
                        {
                            RetirementDate = r[12].ToString();
                        }
                        else
                        {
                            RetirementDate = DateTime.MinValue.ToString();
                        }

                        if (r[11].ToString() != "")
                        {
                            Acquisition_subcode = r[11].ToString();
                        }
                        else
                        {
                            Acquisition_subcode = "0";
                        }
                        item = new AccountantAsset()
                        {

                            Group = r[0].ToString(),
                            Class = r[1].ToString(),
                            FixedAssetCode = Convert.ToInt32(r[2].ToString()),
                            FixedAssetName = r[3].ToString(),
                            CostCenter = Convert.ToInt32(r[4].ToString()),
                            UsefulLife = Convert.ToInt32(r[5].ToString()),
                            DepreciationDate = Convert.ToDateTime(r[6].ToString()),
                            Acquisition = Convert.ToDecimal(r[7].ToString()),
                            Sub_code = r[8].ToString(),
                            Description_subCode = r[9].ToString(),
                            Barcode = r[10].ToString(),
                            Acquisition_subcode = Convert.ToDecimal(Acquisition_subcode),
                            RetirementDate = Convert.ToDateTime(RetirementDate),
                            Status = r[13].ToString(),
                            Owner = r[14].ToString(),
                            Remarks = r[15].ToString(),
                        };
                        string errorMassage = "";
                        string state = "";
                        //kiểm tra trùng Barcode
                        var conflict = db.AccountantAssets.Where(p => p.Barcode == item.Barcode && p.FixedAssetCode == item.FixedAssetCode).FirstOrDefault();

                        if (conflict != null)
                        {
                            errorMassage = "Conflicting code";
                            state = "Unsuccessful";
                            tableResult.Rows.Add(item.Group, item.Class, item.FixedAssetCode, item.FixedAssetName, item.CostCenter,
                                item.UsefulLife, item.DepreciationDate, item.Acquisition, item.Sub_code, item.Description_subCode,
                                item.Barcode, item.Acquisition_subcode, item.RetirementDate, item.Status, item.Owner, item.Remarks,
                                errorMassage, state);
                            continue;
                        }

                        errorMassage = "";
                        state = "Successful";
                        tableResult.Rows.Add(item.Group, item.Class, item.FixedAssetCode, item.FixedAssetName, item.CostCenter,
                            item.UsefulLife, item.DepreciationDate, item.Acquisition, item.Sub_code, item.Description_subCode,
                            item.Barcode, item.Acquisition_subcode, item.RetirementDate, item.Status, item.Owner, item.Remarks,
                            errorMassage, state);
                        db.AccountantAssets.Add(item);
                    }

                    db.SaveChanges();
                }
            }
            FileExcel(tableResult);
            return View("Index", "AccountantAsset");
            //return Json(new { success = true,table = table});
        }
    }
}