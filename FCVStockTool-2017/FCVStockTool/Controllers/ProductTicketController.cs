using Excel;
using FCVStockTool.Models;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FCVStockTool.Controllers
{
    [Filters.UserRole]
    public class ProductTicketController : CommonController
    {
        
        void BindList(int pageNo)
        {
            var cookie = Request.Cookies["StockTool"];
            var RoleName = string.Empty;
            var UserID = "0";
            if (cookie != null && cookie["Role"] != null)
            {
                RoleName = cookie["Role"].ToString();
                UserID = cookie["UserID"].ToString();
            }
            var pros = db.ProductTickets.AsQueryable();
            if (!RoleName.Equals("Administrators", StringComparison.OrdinalIgnoreCase))
            {
                int userid = int.Parse(UserID);
                pros = pros.Where(c => c.RequestById == userid);
            }
            ViewBag.ProductTickets = pros.OrderByDescending(p => p.RequestOn).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            ViewBag.TotalRecords = pros.Count();
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: ProductTicket
        public ActionResult Index(int pageNo = 1, int id = 0)
        {
            
            var model = new ProductTicket();
            if (id > 0)
            {
                model = db.ProductTickets.Find(id);
                //ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.Where(t => t.Id >=model.TicketStatusId).ToList(), "Id", "Text", model.TicketStatusId);
            }
            else
            {
                model.TicketStatusId = 1;
                //ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.Where(t=>t.Id==1).ToList(), "Id", "Text", model.TicketStatusId);
            }
            BindList(pageNo);
            ViewBag.TicketStatuses = new SelectList(db.TicketStatuses.ToList(), "Id", "Text", model.TicketStatusId);
            ViewBag.Categories = new SelectList(db.Categories.OrderBy(p => p.Name).ToList(), "Id", "Name", model.CategoryId);
            int deptID=Request.GetUser().DepartmentId;
            //if (Request.GetUser().RoleName !="Administrators")
            
            //ViewBag.Employees = new SelectList(db.Employees.Where(e => e.DepartmentId == deptID).ToList(), "Id", "Name", model.EmployeeId);
            //else
            ViewBag.Employees = new SelectList(db.Employees.Select(e => new { Id = e.Id, Name = e.Name + (e.Department != null ? (" - " + e.Department.Name) : "") }).OrderBy(p => p.Name).ToList(), "Id", "Name", model.EmployeeId);
            ViewBag.ProductStatuses = new SelectList(db.ProductStatuses.ToList(), "Id", "Text");
            ViewBag.StockDiaryReasons = new SelectList(db.StockDiaryReasons.Where(p => p.Type == 2).ToList(), "Id", "Text");
            return View(model);
        }


        // GET: ProductTicket/Create
        public ActionResult Create(ProductTicket model, int pageNo = 1)
        {

            model.Code = AutoNumberHelper.GenerateNumber(ObjectType.ProductTicket, db);
            model.RequestOn = DateTime.Now;
            model.RequestById = Request.GetUser().Id; 
            model.TicketStatusId = 1;

            if (ModelState.IsValid)
            {
                db.ProductTickets.Add(model);
                db.SaveChanges();
                //try
                //{
                    var proname = (from a in db.Categories
                                   where a.Id == model.CategoryId
                                   select a.Name).FirstOrDefault();
                    var emp = (from b in db.Employees
                               where b.Id == model.EmployeeId
                               select b.Name).FirstOrDefault();

                    var empre = (from d in db.Users
                                 where d.Id == model.RequestById
                                 select d.DisplayName).FirstOrDefault();

                    var status = (from c in db.TicketStatuses
                                  where c.Id == model.TicketStatusId
                                  select c.Text).FirstOrDefault();
                    //Send Email
                    //get file html
                    string html = System.IO.File.ReadAllText(Server.MapPath("~/Files/Email Templates/RequestTicket.html"));

                    html = html.Replace("@product", proname);
                    html = html.Replace("@employee", emp);
                    html = html.Replace("@reason", model.Reason);

                    html = html.Replace("@empRequest", empre);
                    html = html.Replace("@dateRequest", model.RequestOn.ToString());
                    html = html.Replace("@description", model.Description);

                    string[] email = (from a in db.ReceiveEmails
                                      select a.Email).ToArray();

                    var configEmail = (db.ConfigEmails.Where(c => c.Id.Equals(1))).FirstOrDefault();

                    MailHelper.Send(configEmail.Host, 25, configEmail.EmailFrom, configEmail.Password, configEmail.Name, email, null, null, configEmail.Subject, html, null, true);
                    //MailHelper.sendExchange(configEmail.Host, configEmail.EmailFrom, configEmail.Username, configEmail.Password, configEmail.Name, email, null, null, configEmail.Subject, html, true);
                //}
                //catch { }
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: ProductTicket/Edit/5
        public ActionResult Edit(ProductTicket model, int pageNo = 1)
        {
            var upModel = db.ProductTickets.Find(model.Id);
            upModel.CategoryId = model.CategoryId;
            upModel.EmployeeId = model.EmployeeId;
            upModel.Reason = model.Reason;
            upModel.Description = model.Description;
            
            db.Entry(upModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: ProductTicket/Edit/5
        public ActionResult Approve(ProductTicket model, int pageNo = 1)
        {
            var upModel = db.ProductTickets.Find(model.Id);
            upModel.TicketStatusId = 2;
            upModel.ApproveById = Request.GetUser().Id; 
            upModel.ApproveOn = DateTime.Now;
            upModel.CategoryId = model.CategoryId;
            upModel.EmployeeId = model.EmployeeId;
            upModel.Reason = model.Reason;
            upModel.Description = model.Description;
            db.Entry(upModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: ProductTicket/Edit/5
        public ActionResult Cancel(ProductTicket model, int pageNo = 1)
        {
            var upModel = db.ProductTickets.Find(model.Id);
            upModel.TicketStatusId = 4;
            upModel.CancelById = Request.GetUser().Id; 
            upModel.CancelOn = DateTime.Now;
            upModel.CategoryId = model.CategoryId;
            upModel.EmployeeId = model.EmployeeId;
            upModel.Reason = model.Reason;
            upModel.Description = model.Description;
            db.Entry(upModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: ProductTicket/Edit/5
        public ActionResult Export(ProductTicket model, int pageNo = 1)
        {
            var upModel = db.ProductTickets.Find(model.Id);
            upModel.TicketStatusId = 3;
            upModel.ExportById = Request.GetUser().Id; 
            upModel.ExportOn = DateTime.Now;
            upModel.CategoryId = model.CategoryId;
            upModel.EmployeeId = model.EmployeeId;
            upModel.Reason = model.Reason;
            upModel.Description = model.Description;
            db.Entry(upModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: ProductTicket/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.ProductTickets.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        [HttpGet]
        public ActionResult ExportToExcel()
        {
            GridView gv = new GridView();
            gv.DataSource = db.ProductTickets.OrderByDescending(p => p.RequestOn).Select(p => new
            {
                Code = p.Code,
                Category = p.Category != null ? p.Category.Name : "",
                Serial = p.SerialNo,
                Status = p.TicketStatus != null ? p.TicketStatus.Text : "",
                RequestOn = p.RequestOn,
                RequestBy = p.RequestBy != null ? p.RequestBy.DisplayName : "",
                ApproveOn = p.ApproveOn,
                ApproveBy = p.ApproveBy != null ? p.ApproveBy.DisplayName : "",
                ExportOn = p.ExportOn,
                ExportBy = p.ExportBy != null ? p.ExportBy.DisplayName : "",
                CancelOn = p.CancelOn,
                CancelBy = p.CancelBy != null ? p.CancelBy.DisplayName : "",
                Description = p.Description
            }).ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=\"Danh sách phiếu yêu cầu.xls\"");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }

        public JsonResult AssignEmployee(int ticketId, int employeeId, int productId, string serial,int productItemID)
        {
            var productTicket = db.ProductTickets.Find(ticketId);
            var productItem = db.ProductItems.Find(productItemID);
            productItem.EmployeeId = productTicket.EmployeeId;
            db.Entry(productItem).State = EntityState.Modified;
            StockDiary diary = new StockDiary()
            {
                Date = DateTime.Now,
                ProductTicketId = ticketId,
                ProductItemId = productItem.Id,
                Type = "Production",
                Note =  productTicket.Employee.Name
            };
            db.StockDiaries.Add(diary);
            db.SaveChanges();
            return Json(new { Succeed = true }, JsonRequestBehavior.AllowGet);
        }
    }
}