using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FCVStockTool.Controllers
{
    [Filters.AccountantRole]
    public class InvoiceDetailController : Controller
    {
        StockDbContext db = new StockDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
        // GET: InvoiceDetail
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(InvoiceDetail model)
        {
            db.InvoiceDetails.Add(model);
            db.SaveChanges();
            return Json(new { success = true, id = model.Id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InvoiceDetails(int invoiceid)
        {
            var model = db.InvoiceDetails.Where(p => p.InvoiceId == invoiceid);
            return PartialView(model);
        }
        //download File
        public ActionResult FileExcel(DataTable dt)
        {
            var grid = new GridView();
            grid.DataSource = dt;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=InvoiceImport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";

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
            return RedirectToAction("Index", "Invoice");
        }

        public ActionResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            InvoiceDetail invoiceDetail;
            Invoice invoice;

            DataTable dt = ds.Tables[0];
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("Description", typeof(string));

            DateTime? PODate = null;
            string des = "";
            string state = "";
            int? Warranty = null;

            if (ds != null)
            {
                foreach (DataTable dt2 in ds.Tables)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r[0].ToString().Equals(""))
                        {
                            state = "Unsuccessful";
                            des = "Null PONo";
                            r[11] = state;
                            r[12] = des;
                        }
                        else
                        {
                            var PoNo = r[0].ToString();
                            invoice = db.Invoices.SingleOrDefault(e => e.PONo.Equals(PoNo, StringComparison.OrdinalIgnoreCase));

                            if (invoice == null)
                            {
                                if (!r[1].ToString().Equals(""))
                                {
                                    PODate = Convert.ToDateTime(r[1].ToString());
                                }

                                invoice = new Invoice()
                                    {
                                        PONo = r[0].ToString(),
                                        PODate = PODate
                                    };
                                if (!r[2].ToString().Equals(""))
                                {
                                    var supName = r[2].ToString();
                                    var sup = db.Suppliers.SingleOrDefault(a => a.Name.Equals(supName, StringComparison.OrdinalIgnoreCase));

                                    if (sup != null)
                                    {
                                        invoice.SupplierId = sup.Id;
                                    }
                                    else
                                    {
                                        string Code = AutoNumberHelper.GenerateNumber(ObjectType.Supplier, db);//code tự tăng
                                        Supplier supplier = new Supplier()
                                        {
                                            Code = Code,
                                            Name = r[2].ToString()
                                        };
                                        invoice.Supplier = supplier;
                                    }
                                }
                                db.Invoices.Add(invoice);
                                db.SaveChanges();
                            }
                            //Add InvoiceDetail

                            if (!r[4].ToString().Equals(""))
                            {
                                Warranty = Convert.ToInt32(r[4].ToString());
                            }

                            invoiceDetail = new InvoiceDetail()
                            {
                                InvoiceId = invoice.Id,
                                WarrantyPeriod = Warranty,
                                Quantity = Convert.ToInt32(r[5].ToString()),
                                UnitPrice = Convert.ToDecimal(r[7].ToString()),
                            };
                            if (!r[3].ToString().Equals(""))
                            {
                                var proName = r[3].ToString();
                                var pro = db.Products.SingleOrDefault(a => a.Name.Equals(proName, StringComparison.OrdinalIgnoreCase));
                                if (pro != null)
                                {
                                    invoiceDetail.ProductId = pro.Id;
                                }
                                else
                                {
                                    string Code = AutoNumberHelper.GenerateNumber(ObjectType.Product, db);//code tự tăng
                                    Product product = new Product()
                                    {
                                        Code = Code,
                                        Name = r[3].ToString()
                                    };
                                    invoiceDetail.Product = product;
                                }
                            }
                            db.InvoiceDetails.Add(invoiceDetail);
                            db.SaveChanges();

                            state = "Successful";
                            des = "Successful";

                            r[11] = state;
                            r[12] = des;
                        }
                    }
                }

            }
            FileExcel(dt);
            return RedirectToAction("Index", "Invoice");
        }
    }
}