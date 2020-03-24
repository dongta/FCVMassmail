using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Common;
using FCV_DutchLadyMail_Model.Models;
using LinqToExcel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Entity.Validation;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FCV_DutchLadyMail.Controllers
{
    public class ContactListController : Controller
    {
        private MassMailsDbContext db = new MassMailsDbContext();
        //
        // GET: /ContactList/

        public ActionResult Index()
        {
            var ContactL = db.ContactLists.ToList();
            return View(ContactL);
        }
        public ActionResult ImportContactList( string id)
        {
            if (id == null)
            {
                int idCon = Convert.ToInt32(id);
                ViewBag.Contact = db.ContactLists.Where(ta => ta.ID == idCon).ToList();
            }
            else
            {
                int idCon = Convert.ToInt32(id);
                //ViewBag.ContactID = db.ContactLists.Where(ta => ta.ID == idCon).SingleOrDefault();
                var _ContactLID = db.ContactLists.Where(ta => ta.ID == idCon).SingleOrDefault();
                var Con = db.ContactLists.ToList();
                ViewBag.Contact = Con;

                ViewBag.ContactLSelected = _ContactLID.ID;
            }
            
            return View();
        }

        public ActionResult DownloadFile()
        {
            string path = Server.MapPath("/Files/Template/Excel_ContactList.xls");
            byte[] fileBytes;
            fileBytes = System.IO.File.ReadAllBytes(path);


            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Template_Contact.xls");
        }
        [HttpPost]
        public ActionResult ImportContactList(FileModel _model)
        {
            int id = Convert.ToInt32(_model.folder);
            var Identity = (IdentityUser)Session["Identity"];
            HttpPostedFileBase FileUpload = _model.files[0];
            if (ModelState.IsValid)
            {
                if (FileUpload != null)
                {
                    // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                    if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                       
                        string nameFolder = db.ContactLists.Where(ta => ta.ID == id).SingleOrDefault().Name; 
                        var InputFileName = Path.GetFileName(FileUpload.FileName);
                        string path = "/Files/ContactList/" + nameFolder+"/";
                        bool exists = System.IO.Directory.Exists(Server.MapPath(path));
                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(path));
                        var ServerSavePath = Path.Combine(Server.MapPath(path) + InputFileName);
                        FileUpload.SaveAs(ServerSavePath);

                        ViewBag.UploadStatus = _model.files.Count().ToString() + " files uploaded successfully.";
                        string pathToExcelFile = Server.MapPath(path) + FileUpload.FileName;
                        var connectionString = "";
                        if (InputFileName.EndsWith(".xls"))
                        {
                            connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                        }
                        else if (InputFileName.EndsWith(".xlsx"))
                        {
                            connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                        }

                        var adapter = new OleDbDataAdapter("SELECT * FROM [Excel_ContactList$]", connectionString);
                        var ds = new DataSet();

                        adapter.Fill(ds, "ExcelTable");

                        DataTable dtable = ds.Tables["ExcelTable"];

                        string sheetName = "Excel_ContactList";

                        var excelFile = new ExcelQueryFactory(pathToExcelFile);
                        var dataExcel = from a in excelFile.Worksheet<Contact>(sheetName) select a;
                        List<Contact> Contacts = new List<Contact>();

                        var _contactRemove = db.Contacts.Where(ta => ta.ContactListID == id).ToList();
                        if(_contactRemove.Count > 0)
                        {
                            foreach(var item in _contactRemove)
                            {
                                db.Contacts.Remove(item);
                                db.SaveChanges();
                            }
                            var ContactList = db.ContactLists.Where(ta => ta.ID == id).SingleOrDefault();
                            ContactList.ID = ContactList.ID;
                            ContactList.Name = ContactList.Name;
                            ContactList.Remarks = ContactList.Remarks;
                            ContactList.Description = ContactList.Description;
                            ContactList.CreatedBy = ContactList.CreatedBy;
                            ContactList.LastUpdatedBy = Identity.UserName;
                            ContactList.LastUpdateTime = DateTime.Now;
                            ContactList.LastImportFileName = InputFileName;
                            db.Entry(ContactList).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        
                        foreach (var item in dataExcel)
                        {
                            try
                            {
                                if (item.Email != "")
                                {
                                    Contact TU = new Contact();
                                    TU.Name = item.Name;
                                    TU.Address = item.Address;
                                    TU.Email = item.Email;
                                    TU.Phone = item.Phone;
                                    TU.Remarks = item.Remarks;
                                    TU.Company = item.Company;
                                    TU.ContactListID = id;
                                    db.Contacts.Add(TU);
                                    db.SaveChanges();
                                    Contacts.Add(TU);
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
                    }
                }

                
            }
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult ExportExcel(int id)
        {
            // Step 1 - get the data from database
            var data = db.Contacts.Where(ta=>ta.ContactListID == id).ToList();

            var gv = new GridView();
            gv.DataSource = data;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Excel_ContactList.xls");
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
        public ActionResult Details(string id)
        {
            int idC = Convert.ToInt32(id);
            ContactLists temp = db.ContactLists.Where(ta => ta.ID == idC).Include(a => a.CONTACTs).FirstOrDefault();

            return View("Details", temp);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ContactLists Contact)
        {

            if (Contact != null && ModelState.IsValid)
            {
                var Identity = (IdentityUser)Session["Identity"];
                Contact.CreationTime = DateTime.Now;
                Contact.CreatedBy = Identity.UserName;
                db.ContactLists.Add(Contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();

        }

        public ActionResult Edit(int id)
        {

            var Contact = db.ContactLists.Where(m => m.ID == id).Single();
            return View(Contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ContactLists Contact)
        {
            var Identity = (IdentityUser)Session["Identity"];
            Contact.LastUpdateTime = DateTime.Now;
            Contact.LastUpdatedBy = Identity.UserName;
            db.Entry(Contact).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            db.Signatures.Remove(db.Signatures.Find(id));
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
