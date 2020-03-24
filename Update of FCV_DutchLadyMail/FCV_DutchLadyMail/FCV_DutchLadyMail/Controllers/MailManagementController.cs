using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCV_DutchLadyMail_Model.Models;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using PagedList;
using LinqToExcel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Entity.Validation;
using System.Net.Mail;
using FCV_DutchLadyMail_Model.Helpers;
using FCV_DutchLadyMail_Model.Common;


namespace FCV_DutchLadyMail.Controllers
{
    public class MailManagementController : BaseController
    {
        //public IdentityUser _Identity;
        //public void setSession()
        //{
        //    _Identity = new IdentityUser();
        //    _Identity.UserId = 8;
        //    _Identity.UserName = "CrmInstall";
        //    _Identity.FullName = "Crm";
        //    _Identity.Email = "admin@gmail.com";
        //    _Identity.Address = "admin@gmail.com";
            

        //    Session.Add("Identity", _Identity);
        //}
        private MassMailsDbContext db = new MassMailsDbContext();

        public ActionResult Details(string id)
        {
            SendMails temp = db.SendMails.Find(int.Parse(id));

            return PartialView("_BodyDetails", temp);
        }
        public ActionResult Details_blank(string id)
        {
            SendMails temp = db.SendMails.Find(int.Parse(id));

            return PartialView("_BodyDetails_blank", temp);
        }
        public ActionResult DownloadFile(string name, string id)
        {
            int idMail = Convert.ToInt32(id);
            var mail =  db.SendMails.Where(ta=>ta.ID == idMail).SingleOrDefault();
            byte[] fileBytes;
            if(mail.ContactListID != null)
            {
                string path = "D:/MassMail/AttachFiles/"  + name;
                //string path = "D:/Massmail/SentMails/"+mail.DistributorCode+"/" + mail.AttachedFolder + "/";
                fileBytes = System.IO.File.ReadAllBytes(path);
            }
            else
            {
                string path = "D:/MassMail/SentFiles/" + mail.DistributorCode + "/" + name;
                fileBytes = System.IO.File.ReadAllBytes(path);
            }
            
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        
        //
        // GET: /MailManagement/
        public ActionResult MailBoxs(string sSub, string sTo, string sCc, string sBcc, string dFrom, string dTo, int[] chkListMails)
        {
            var temp = db.SendMails.Select(ta => new {SentBy = ta.SentBy }).Distinct().ToList();
            ViewBag.SendMails = temp;
            var _list = from l in db.SendMails select l;

            if (!String.IsNullOrEmpty(sSub))
            {
                _list = _list.Where(s => s.Subject.Contains(sSub));
            }
            if (!String.IsNullOrEmpty(sTo))
            {
                _list = _list.Where(s => s.toAddress.Contains(sTo));
            }
            if (!String.IsNullOrEmpty(sCc))
            {
                _list = _list.Where(s => s.ccAddress.Contains(sCc));
            }
            if (!String.IsNullOrEmpty(sBcc))
            {
                _list = _list.Where(s => s.bccAddress.Equals(sBcc));
            }
            if (_list.ToList().Count() > 0)
                return View(_list.OrderBy(ta => ta.SentBy).ToList());
            else
                return View();
        }

        public ActionResult FilterMailsbyCode(string filterCode)
        {
            var temp = db.SendMails.Select(ta => new { SentBy = ta.SentBy }).Distinct().ToList();
            ViewBag.SendMails = temp;
            var _list = from l in db.SendMails select l;
            if (!String.IsNullOrEmpty(filterCode))
            {
                _list = _list.Where(s => s.DistributorCode.Contains(filterCode));
            }

            return PartialView("_getMailBoxs", _list.ToList());
        }
        public ActionResult SearchMailBoxs(string selectednumbers,string sSub, string sTo, string sCc, string sBcc, string dFrom, string dTo, int[] chkListMails)
        {
            var temp = db.SendMails.Select(ta => new { SentBy = ta.SentBy }).Distinct().ToList();
            ViewBag.SendMails = temp;
            var _list = from l in db.SendMails select l;
            if (!String.IsNullOrEmpty(sSub))
            {
                _list = _list.Where(s => s.Subject.Contains(sSub));
            }
            if (!String.IsNullOrEmpty(sTo))
            {
                _list = _list.Where(s => s.toAddress.Contains(sTo));
            }
            if (!String.IsNullOrEmpty(sCc))
            {
                _list = _list.Where(s => s.ccAddress.Contains(sCc));
            }
            if (!String.IsNullOrEmpty(sBcc))
            {
                _list = _list.Where(s => s.bccAddress.Equals(sBcc));
            }

            if (!String.IsNullOrEmpty(dTo))
            {
                DateTime startDate = Convert.ToDateTime(dFrom);
                DateTime endDate = Convert.ToDateTime(dTo);
                //_list = from a in db.SendMails where a.LastTry.Value.Date.CompareTo(dTo) > 0 select a;
                _list = _list.Where(j => j.LastTry >= startDate && j.LastTry < endDate);
            }

            return PartialView("_getMailBoxs", _list.ToList());
        }
        public ActionResult TestPass()
        {
            ActionExecutingContext filterContext = new ActionExecutingContext();
            //ViewBag.MessageCount = TempData["MessageCount"].ToString();
            filterContext.Result = new RedirectResult("Login/Index/1");
            return View();

        }
        public ActionResult SendMails(string id)
        {
            ViewBag.Message = "Sending mail in list.";
            var npp = db.Distributors.ToList();
            var selectList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "Manager Mails", Value = "1"},
                    new SelectListItem {Text = "Admin Mails", Value = "2"},
                    new SelectListItem {Text = "SE Mails", Value = "3"},
                    new SelectListItem {Text = "Ac Mails", Value = "4"},
                }, "Value", "Text");
            ViewBag.Group = selectList;
            ViewBag.NPP = new SelectList(npp, "ID", "Name");
            ViewBag.Region = new SelectList(db.Regions, "ID", "Name");
            ViewBag.Folder = db.Folders.OrderBy(ta => ta.ID).ToList();
            ViewBag.Contact = db.ContactLists.OrderBy(ta => ta.ID).ToList();
            ViewBag.Signature = db.Signatures.ToList();
            ViewBag.Template = db.Templates.ToList();
            
            _Identity = (IdentityUser)Session["Identity"];
            ViewBag.SessionIdentity = _Identity.Email;
            int count = 2;
            if(Session["CountGet"]!= null)
            {
                count = (int)Session["CountGet"];
                count++;
            }
            
            Session.Add("CountGet", count);
            if (TempData["MessageCount"] != null)
            {
                ViewBag.MessageCount = TempData["MessageCount"].ToString();   
                if(count != 2)
                {
                    TempData["MessageCount"] = ViewBag.MessageCount;
                }
            }
            if (!String.IsNullOrEmpty(id))
            {
                var idM = Convert.ToInt32(id);
                var data = db.SendMails.Where(ta => ta.ID == idM).SingleOrDefault();
                if (data.DistributorCode != null)
                {
                    data.Body = HttpUtility.HtmlDecode(data.Body);
                    return View(data);
                }
                else
                    return View();
            }
            else
                return View();
        }

       

        [HttpPost]
        public ActionResult SendMails(string To, string Cc, string Bcc, string Files, string Sub, string Bod, string Temp, string AddCC, string AddBCC)
        {
            To = To.Replace("\n", ""); Cc = Cc.Replace("\n", ""); Bcc = Bcc.Replace("\n", "");
            List<SendMails> LstMail = new List<SendMails>();
            _Identity = (IdentityUser)Session["Identity"];
            if (!String.IsNullOrEmpty(To))
            {
                List<string> npp = To.Split('[').ToList();
                
                foreach (var item in npp)
                {
                    if (item != "")
                    {
                        SendMails  _Mail = new SendMails();
                        List<string> s = item.Split(']').ToList();
                        string code = s[0];
                        _Mail.DistributorCode = code;
                        if (s[1] != "")
                        {
                            _Mail.toAddress = s[1];
                            //if (!String.IsNullOrEmpty(Files) && !Files.Equals("-- Please select --"))
                            if (!String.IsNullOrEmpty(Files))
                            {
                                int id = Convert.ToInt32(Files);
                                var folder = db.Folders.Where(ta => ta.ID == id).SingleOrDefault();
                                _Mail.AttachedFolder = folder.Path;
                            }
                        }
                            
                        LstMail.Add(_Mail);
                    }
                    
                }
            }
            if (!String.IsNullOrEmpty(Cc))
            {
                List<string> npp = Cc.Split('[').ToList();

                foreach (var item in npp)
                {
                    if (item != "")
                    {
                        SendListMails _Mail = new SendListMails();
                        List<string> s = item.Split(']').ToList();
                        string code = s[0];
                        var dt = LstMail.Where(ta => ta.DistributorCode.Equals(code)).SingleOrDefault();
                        if (s[1] != "")
                        {
                            dt.ccAddress=s[1];
                            if (!String.IsNullOrEmpty(AddCC))
                                dt.ccAddress = dt.ccAddress + AddCC;
                        }
                        else
                        {
                            dt.ccAddress = AddCC;
                        }
                        

                    }
                }
            }
            else
            {
                List<string> npp = To.Split('[').ToList();

                foreach (var item in npp)
                {
                    if (item != "")
                    {
                        SendMails _Mail = new SendMails();
                        List<string> s = item.Split(']').ToList();
                        string code = s[0];
                        _Mail.DistributorCode = code;
                        var dt = LstMail.Where(ta => ta.DistributorCode.Equals(code)).SingleOrDefault();
                      
                        if (!String.IsNullOrEmpty(AddCC)){
                                dt.ccAddress = AddCC;
                        }
                    }

                }
            }
            if (!String.IsNullOrEmpty(Bcc))
            {
                List<string> npp = Bcc.Split('[').ToList();

                foreach (var item in npp)
                {
                    if (item != "")
                    {
                        SendListMails _Mail = new SendListMails();
                        List<string> s = item.Split(']').ToList();
                        string code = s[0];
                        var dtBcc = LstMail.Where(ta => ta.DistributorCode.Equals(code)).SingleOrDefault();
                        if (s[1] != "")
                        {
                            dtBcc.bccAddress = s[1];
                            if (!String.IsNullOrEmpty(AddBCC))
                                dtBcc.bccAddress = dtBcc.bccAddress + AddBCC;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(AddBCC))
                                dtBcc.bccAddress = AddBCC;
                        }
                    }

                }
            }
            else
            {
                List<string> npp = To.Split('[').ToList();

                foreach (var item in npp)
                {
                    if (item != "")
                    {
                        SendMails _Mail = new SendMails();
                        List<string> s = item.Split(']').ToList();
                        string code = s[0];
                        _Mail.DistributorCode = code;
                        var dt = LstMail.Where(ta => ta.DistributorCode.Equals(code)).SingleOrDefault();

                        if (!String.IsNullOrEmpty(AddBCC))
                        {
                            dt.bccAddress = AddBCC;
                        }
                    }

                }
           }
            

            var config = db.SMTPProfiles.ToList();
            #region Xu ly luu vao queue mails database
            foreach(var item in LstMail)
            {

                var dis = db.Distributors.Where(ta => ta.Code == item.DistributorCode).SingleOrDefault();
                string BodTemp = Bod.Replace("#DistributorName", dis.Name);
                QueueMails _m = new FCV_DutchLadyMail_Model.Models.QueueMails();
                _m.SMTPProfile = 1;
                _m.DistributorCode = item.DistributorCode;
                _m.toAddress = item.toAddress;
                _m.ccAddress = item.ccAddress;
                _m.bccAddress = item.bccAddress;
                _Identity = (IdentityUser)Session["Identity"];
                _m.fromName = _Identity.FullName;
                _m.fromAddress = _Identity.Address;
                _m.SentBy = _Identity.UserName;


                var Body = HttpUtility.HtmlDecode(BodTemp);
                string s = "<meta http-equiv=\"Content-Type\"  content=\"text/html charset=UTF-8\" />";
                _m.Body = s + Body; 
                _m.Subject = Sub;
                _m.QueueTime = DateTime.Now;
                _m.AttachedFolder = item.AttachedFolder;
                try
                {
                    db.QueueMails.Add(_m);
                    db.SaveChanges();
                }
                catch (Exception ex) { }
            }
            #endregion
            Session.Add("CountGet", 0);
            TempData["MessageCount"] = LstMail.Count + " emails sent to queue !";
            return RedirectToAction("SendMails");
        }

        public ActionResult SendMailsL(string To, string Cc, string Bcc, string Files, string Sub, string Bod, string ContactID, bool? cctomeL)
        {
            List<SendMails> LstMail1 = new List<SendMails>();

            if (!String.IsNullOrEmpty(To))
            {
                List<string> s = To.Split(',').ToList();
                foreach (var item in s)
                {
                    if (item != "")
                    {
                        SendMails _Mail = new SendMails();

                        _Mail.toAddress = item;
                        if (!String.IsNullOrEmpty(Files))
                        {
                            _Mail.AttachedFiles = Files;
                            _Mail.AttachedFolder = @"D:\MassMail\AttachFiles\";
                        }
                        LstMail1.Add(_Mail);
                    }

                }

            }

            if (!String.IsNullOrEmpty(Cc))
            {

                foreach (var _item in LstMail1)
                {
                    _item.ccAddress = Cc;
                    //if (cctomeL == true)
                    //    _item.ccAddress = _item.ccAddress + _Identity.Email;
                }
            }
            //else
            //{
            //    if (cctomeL == true)
            //    {
            //        foreach (var _item in LstMail1)
            //        {
            //            _item.ccAddress = _Identity.Email;
            //        }
            //    }
            //}

            if (!String.IsNullOrEmpty(Bcc))
            {

                foreach (var _item in LstMail1)
                {
                    _item.bccAddress = Bcc;
                }
            }

            var config = db.SMTPProfiles.ToList();
            #region Xu ly luu vao queue mails database
            foreach (var item in LstMail1)
            {

                //Bod = Bod.Replace("#DistributorName", dis.Name);
                QueueMails _m = new FCV_DutchLadyMail_Model.Models.QueueMails();
                if (!String.IsNullOrEmpty(ContactID))
                {
                    int id = Convert.ToInt32(ContactID);
                    _m.ContactListID = id;
                }
                _m.SMTPProfile = 1;
                _m.toAddress = item.toAddress;
                _m.ccAddress = item.ccAddress;
                _m.bccAddress = item.bccAddress;
                _Identity = (IdentityUser)Session["Identity"];
                _m.fromName = _Identity.FullName;
                _m.fromAddress = _Identity.Address;
                _m.SentBy = _Identity.UserName;
                var Body = HttpUtility.HtmlDecode(Bod);
                string s = "<meta http-equiv=\"Content-Type\"  content=\"text/html charset=UTF-8\" />";
                _m.Body = s + Bod;
                _m.Subject = Sub;
                //if (!String.IsNullOrEmpty(Temp))
                //    _m.Template = Convert.ToInt32(Temp);
                _m.QueueTime = DateTime.Now;
                _m.AttachedFolder = item.AttachedFolder;
                _m.AttachedFiles = item.AttachedFiles;
                try
                {
                    db.QueueMails.Add(_m);
                    db.SaveChanges();
                }
                catch (Exception ex) { }
            }
            #endregion
            Session.Add("CountGet", 0);
            TempData["MessageCount"] = LstMail1.Count + " emails sent to queue !";
            return RedirectToAction("SendMails");
        }
        public ActionResult GetTemplateData(string test)
        {
            Templates temp = db.Templates.Find(int.Parse(test));
            // some code
            return Json(new { success = true, message = temp.Contents }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSigData(string signid)
        {
            Signatures sign = new Signatures();
            if (signid != "")
            {
                var id = int.Parse(signid);
                sign = db.Signatures.Where(ta => ta.ID == id).SingleOrDefault();
            }
            // some code
            return Json(new { success = true, message = sign.Contents }, JsonRequestBehavior.AllowGet);
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
                    string targetpath = Server.MapPath("~/Files/Excel/");
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
                    var dataExcel = from a in excelFile.Worksheet<Contact>(sheetName) select a;
                    List<Contact> Contacts = new List<Contact>();
                    
                    //var contact = db.Contacts.Where(m => m.State.Equals("-1"));
                    //db.Contacts.RemoveRange(contact);
                    //db.SaveChanges();
                    foreach (var item in dataExcel)
                    {
                        try
                        {
                            if (item.Email != "")
                            {
                                Contact TU = new Contact();
                                TU.Name = item.Name;
                                TU.Address = item.Address;
                                TU.ID = item.ID;
                                TU.Email = item.Email;
                                TU.Phone = item.Phone;
                                TU.Remarks = item.Remarks;
                                TU.Company = item.Company;
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
                    return PartialView("_GetListContacts", Contacts.ToList()); 

                }
            }

            return View();
        }


        [HttpPost]
        public ActionResult AttachFiles()
        {
            HttpFileCollectionBase files = Request.Files;
            List<string> data = new List<string>();
            for (int i = 0; i < files.Count;i++ )
            {
                HttpPostedFileBase FileUpload = files[i];
                if (FileUpload != null)
                {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/AttachFiles/");
                    FileUpload.SaveAs(targetpath + filename);
                    data.Add(filename);
                }
            }
            ViewBag.Files = data;
            return PartialView("_getListFiles");

        }
        public ActionResult GetListContact(string conid)
        {
            int id = int.Parse(conid);
            var temp = db.Contacts.Where(ta => ta.ContactListID == id);


            return PartialView("_GetContact", temp.ToList());
        }
        public ActionResult GetListContactNoID(string conid)
        {
            int id = int.Parse(conid);
            var temp = db.Contacts.Where(ta => ta.ContactListID == id);


            return PartialView("_GetContactNoID", temp.ToList());
        }
        public ActionResult GetListSearch(string selectednumbers,string paramJson, DistributorModel NPP, int? page = 1)
        {
            //int pageSize = 6;
            //int pageNumber = (page ?? 1);
            
            //string s1 = Name;
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
                data = data.Where(s => s.Code.ToString().ToUpper().Contains(NPP.Code.ToUpper()) );
            }
            if (!String.IsNullOrEmpty(NPP.Name))
            {
                data = data.Where(s => s.Name.ToString().ToUpper().Contains(NPP.Name.ToUpper()) );
            }
            if (!String.IsNullOrEmpty(NPP.SEMail))
            {
                data = data.Where(s => s.SEMail.ToString().ToUpper().Contains(NPP.SEMail.ToUpper()) );

            }
            if (!String.IsNullOrEmpty(NPP.ManagerMail))
            {
                data = data.Where(s => s.ManagerMail.ToString().ToUpper().Contains(NPP.ManagerMail.ToUpper()) );
            }
            if (!String.IsNullOrEmpty(NPP.AcMail))
            {
                data = data.Where(s => s.AcMail.ToString().ToUpper().Contains(NPP.AcMail.ToUpper()) );
            }
            if (!String.IsNullOrEmpty(NPP.AdminMail))
            {
                data = data.Where(s => s.AdminMail.ToString().ToUpper().Contains(NPP.AdminMail.ToUpper()) );
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
            return PartialView("_GetListSearch", lst.ToList());
        }

        public ActionResult GetMailDistributors(string paramJson, string Group)
        {
            

 
            string displayMail = "";
            List<string> lMail = new List<string>();
            if(!String.IsNullOrEmpty(paramJson))
            {
                var lstIdDis = paramJson.Split(',');
                if (!String.IsNullOrEmpty(Group))
                {
                    var lstIdGroup = Group.Split(',');
                    var Dis = db.Distributors.ToList();
                    foreach (var IdDis in lstIdDis)
                    {
                        int id = Convert.ToInt32(IdDis);
                        var _dis = Dis.Where(a => a.ID == id).Single();
                        string str = "["+ _dis.Code+"]";
                        foreach (var idGroup in lstIdGroup)
                        {
                            if (Convert.ToInt32(idGroup) == 1 && !String.IsNullOrEmpty(_dis.ManagerMail) )
                            {
                                if (!String.IsNullOrEmpty(displayMail))
                                {
                                    displayMail += "," + _dis.ManagerMail;
                                }
                                else
                                    displayMail += _dis.ManagerMail;
                                str+= _dis.ManagerMail+",";
                            }
                            else if (Convert.ToInt32(idGroup) == 2 && !String.IsNullOrEmpty(_dis.AdminMail))
                            {
                                if (!String.IsNullOrEmpty(displayMail))
                                {
                                    displayMail += "," + _dis.AdminMail;
                                }  
                                else
                                    displayMail += _dis.AdminMail;

                                str+= _dis.AdminMail+",";
                            }
                            else if (Convert.ToInt32(idGroup) == 3 && !String.IsNullOrEmpty(_dis.SEMail))
                            {
                                if (!String.IsNullOrEmpty(displayMail))
                                {
                                    displayMail += "," + _dis.SEMail;
                                }
                                else
                                    displayMail += _dis.SEMail;
                                str+= _dis.SEMail+",";
                            }
                            else if (Convert.ToInt32(idGroup) == 4 && !String.IsNullOrEmpty(_dis.AcMail))
                            {
                                if (!String.IsNullOrEmpty(displayMail))
                                {
                                    displayMail += "," + _dis.AcMail;
                                }
                                else
                                    displayMail += _dis.AcMail;
                                str+= _dis.AcMail+","; 
                            } 
                                
                        }
                        lMail.Add(str);
                    }
                }
                else
                {
         
                    var Dis = db.Distributors.ToList();
                    foreach (var IdDis in lstIdDis)
                    {
                        int id = Convert.ToInt32(IdDis);
                        var _dis = Dis.Where(a => a.ID == id).Single();
                        string str1 = "[" + _dis.Code + "]";

                        if (!String.IsNullOrEmpty(_dis.ManagerMail))
                        {
                            if (displayMail != "")
                            {
                                displayMail += "," + _dis.ManagerMail;
                            }
                            else
                                displayMail += _dis.ManagerMail;
                            str1 += _dis.ManagerMail + ",";
                        }

                        if (!String.IsNullOrEmpty(_dis.AdminMail))
                        {
                            if (displayMail != "")
                            {
                                displayMail += "," + _dis.AdminMail;
                            }
                            else
                                displayMail += _dis.AdminMail;
                            str1 += _dis.AdminMail + ",";
                        }

                        if (!String.IsNullOrEmpty(_dis.SEMail))
                        {
                            if (displayMail != "")
                            {
                                displayMail += "," + _dis.SEMail;
                            }
                            else
                                displayMail += _dis.SEMail;
                            str1 += _dis.SEMail + ",";
                        }
                        if (!String.IsNullOrEmpty(_dis.AcMail))
                        {
                            if (displayMail != "")
                            {
                                displayMail += "," + _dis.AcMail;
                            }
                            else
                                displayMail += _dis.AcMail;
                            str1 += _dis.ManagerMail + ",";
                        }
                        lMail.Add(str1);
                    }
                    
                }
            }
            displayMail = displayMail.Replace(";", ",");
            ViewBag.lMail = lMail;
            ViewBag.DisplayMail = displayMail;
            return PartialView("_ListMail");
        }

        public ActionResult GetAttachFiles(string paramJson)
        {

            var lstIdFiles = JsonConvert.DeserializeObject<List<int>>(paramJson);

            var _folders = db.Folders.ToList();
            List<Folders> _Lfiles = new List<Folders>();
            foreach (int Id in lstIdFiles)
            {
                var _f = _folders.Where(a => a.ID == Id).SingleOrDefault();
                _Lfiles.Add(_f);
            }

            return PartialView("_ListAttach", _Lfiles.ToList());
        }
        public JsonResult GetList()
        {
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
                           Regions = b
                       }; 

            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchDis(DistributorsView model)
        {
            var result = false;
            try
            {
                if (model.ID > 0)
                {
                    //tblStudent Stu = db.tblStudents.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == model.StudentId);
                    //Stu.StudentName = model.StudentName;
                    //Stu.Email = model.Email;
                    //Stu.DepartmentId = model.DepartmentId;
                    //db.SaveChanges();
                    result = true;
                }
                else
                {
                    //tblStudent Stu = new tblStudent();
                    //Stu.StudentName = model.StudentName;
                    //Stu.Email = model.Email;
                    //Stu.DepartmentId = model.DepartmentId;
                    //Stu.IsDeleted = false;
                    //db.tblStudents.Add(Stu);
                    //db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private string createEmailBody(string userName, string title, string message)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(Server.MapPath("~/Template/TemplateOne.html")))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{UserName}", userName); //replacing the required things  

            body = body.Replace("{Title}", title);

            body = body.Replace("{message}", message);

            return body;

        }  

    }
}
