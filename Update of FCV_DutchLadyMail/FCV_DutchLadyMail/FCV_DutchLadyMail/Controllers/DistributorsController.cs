using System;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Models;
using PagedList;
using LinqToExcel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Web.UI;  
using System.Web.UI.WebControls;
using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Common;
using System.IO;

namespace FCV_DutchLadyMail.Controllers
{
    public class DistributorsController : BaseController
    {
        private MassMailsDbContext db = new MassMailsDbContext();
        //
        // GET: /Distributors/

        public ActionResult Index(string sortOrder, int page = 1, int pageSize = 10 )
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CodeSortParm = sortOrder == "Code" ? "date_desc" : "code";

            ViewBag.lstRegion = db.Regions.ToList();
            ViewBag.Region = new SelectList(db.Regions.ToList(), "ID", "Name");
            var data = from a in db.Distributors
                       join b in db.Regions on a.Region equals b.ID
                       select new DistributorsView {
                           ID=a.ID,
                           Region=a.Region,
                           Name = a.Name,
                           Code = a.Code,
                           AdminMail = a.AdminMail,
                           SEMail = a.SEMail,
                           AcMail = a.AcMail,
                           ManagerMail = a.ManagerMail,
                           Address = a.Address,
                           State = a.State,                     
                           Regions = b
                       };


            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(s => s.Name);
                    break;
                case "code":
                    data = data.OrderBy(s => s.Code);
                    break;
                case "code_desc":
                    data = data.OrderByDescending(s => s.Code);
                    break;
                default:
                    data = data.OrderBy(s => s.Name);
                    break;
            }
            var _list = data.ToList();
            return View(_list);
        }
        public ActionResult GetListSearch(string selectednumbers, string paramJson, DistributorModel NPP, int? page = 1)
        {
            ViewBag.lstRegion = db.Regions.ToList();
            ViewBag.Region = new SelectList(db.Regions.ToList(), "ID", "Name");
            var data = from a in db.Distributors
                       join b in db.Regions on a.Region equals b.ID
                       select new DistributorsView
                       {

                           ID = a.ID,
                           Region = a.Region,
                           Name = a.Name,
                           Code = a.Code,
                           AdminMail = a.AdminMail,
                           SEMail = a.SEMail,
                           AcMail = a.AcMail,
                           ManagerMail = a.ManagerMail,
                           State = a.State,
                           Address = a.Address,
                           Regions = b
                       };

            if (!String.IsNullOrEmpty(NPP.Code))
            {
                data = data.Where(s => s.Code.ToString().ToUpper().Contains(NPP.Code.ToUpper()));
            }
            if (!String.IsNullOrEmpty(NPP.Name))
            {
                data = data.Where(s => s.Name.ToString().ToUpper().Contains(NPP.Name.ToUpper()));
            }
            if (!String.IsNullOrEmpty(NPP.SEMail))
            {
                data = data.Where(s => s.SEMail.ToString().ToUpper().Contains(NPP.SEMail.ToUpper()));

            }
            if (!String.IsNullOrEmpty(NPP.ManagerMail))
            {
                data = data.Where(s => s.ManagerMail.ToString().ToUpper().Contains(NPP.ManagerMail.ToUpper()));
            }
            if (!String.IsNullOrEmpty(NPP.AcMail))
            {
                data = data.Where(s => s.AcMail.ToString().ToUpper().Contains(NPP.AcMail.ToUpper()));
            }
            if (!String.IsNullOrEmpty(NPP.AdminMail))
            {
                data = data.Where(s => s.AdminMail.ToString().ToUpper().Contains(NPP.AdminMail.ToUpper()));
            }
            if (!String.IsNullOrEmpty(NPP.Address))
            {
                data = data.Where(s => s.Address.ToString().ToUpper().Contains(NPP.Address.ToUpper()));
            }
            var tempData = data;
            List<DistributorsView> lst = new List<DistributorsView>();

            if (selectednumbers == "")
                lst = data.ToList();
            else
            {
                string[] idR = selectednumbers.Split(',');
                lst = lst.ToList();
                foreach (string item in idR)
                {
                    int id = Convert.ToInt32(item);
                    tempData = data.Where(s => s.Region == id);
                    lst.AddRange(tempData);
                }
            }
            return PartialView("_GetListSearchDis", lst.ToList());
        }
   
        public ActionResult ExportExcel()
        {
            // Step 1 - get the data from database
            var data = db.Distributors.ToList();

            var gv = new GridView();
            gv.DataSource = data;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View();

        }
        [HttpPost]
        public ActionResult ImportExcel()
        {
            HttpFileCollectionBase files = Request.Files;
            HttpPostedFileBase FileUpload = files[0];
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    //string targetpath = Server.MapPath("~/Files/Excel");
                    string targetpath = "D:/MassMail/ImportFiles/";
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var dataExcel = from a in excelFile.Worksheet<DistributorModel>(sheetName) select a;
                    List<Contact> Contacts = new List<Contact>();

                    foreach (var item in dataExcel)
                    {
                        try
                        {
                            if (item.Code != "")
                            {
                                DistributorModel TU = new DistributorModel();
                                TU.Name = item.Name;
                                TU.Address = item.Address;
                                TU.ID = item.ID;
                                TU.Code = item.Code;
                                TU.AdminMail = item.AdminMail;
                                TU.SEMail = item.SEMail;
                                TU.AcMail = item.AcMail;
                                TU.ManagerMail = item.ManagerMail;
                                TU.Region = item.Region;
                                TU.State = true;
                                db.Distributors.Add(TU);
                                db.SaveChanges();
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }

                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return PartialView("_GetListContacts", Contacts.ToList());

                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult Insert(DistributorModel data)
        {
            if (data.Code != null)
            {
                data.State = true;
                data.Region = 2;
                db.Distributors.Add(data);
                db.SaveChanges();
                return Json("OK");
            }
            else
                return Json("Loi");

        }
        public ActionResult Create()
        {
            ViewBag.lstRegion = db.Regions.ToList();
            ViewBag.Region = new SelectList(db.Regions.ToList(), "ID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(DistributorModel dis)
        {

            if (dis != null)
            {
                db.Distributors.Add(dis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();

        }
        public ActionResult Details(string id)
        {
            int idC = Convert.ToInt32(id);
            DistributorModel temp = db.Distributors.Where(ta => ta.ID == idC).SingleOrDefault();

            return View("Details", temp);
        }
        public ActionResult Edit(int id)
        {
            //ViewBag.lstRegion = db.Regions.ToList();
            ViewBag.Region = new SelectList(db.Regions.ToList(), "ID", "Name");
            var dis = db.Distributors.Where(m => m.ID == id).Single();
            return View(dis);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(DistributorModel dis, bool? IsChecked)
        {
            if (ModelState.IsValid)
            {
                if (IsChecked == true)
                    dis.State = true;
                else
                    dis.State = false;
                db.Entry(dis).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            try
            {
                var dt = db.Distributors.Find(id);
                db.Distributors.Remove(dt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return new EmptyResult();
            }
        }

    }
}
