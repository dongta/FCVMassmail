using FCVStockTool.Models;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    public class AccessoryExportController : CommonController
    {
        void BindList(int pageNo, List<AccessoryExport> cate)
        {
            if (cate != null)
            {
                ViewBag.AccessoryExports = cate.OrderByDescending(p => p.ExportOn).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = cate.Count();
            }
            else
            {
                ViewBag.AccessoryExports = db.AccessoryExports.OrderByDescending(p => p.ExportOn).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Accessories.Count();
            }
            
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Category
        public ActionResult Index(int pageNo = 1, int accesoryid =0, int id = 0, string searchString = "")
        {
            if (accesoryid > 0)
            {
                var accessory = db.Accessories.Find(accesoryid);
                ViewBag.AccessoryName = string.Format("{0} {1}", accessory.Category.Name, accessory.Name);
            }
                foreach (string key in TempData.Keys)
                {
                    ModelState.AddModelError(string.Empty, TempData[key].ToString());
                }
                
            
            var cate = from s in db.AccessoryExports where s.AccessoryId == accesoryid
                       select s ;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    cate = cate.Where(s => s.Name.Contains(searchString));
            //}

            var model = new AccessoryExport();
            if (id > 0) model = db.AccessoryExports.Find(id);
            model.AccessoryId = accesoryid;
            ViewBag.Employees = new SelectList(db.Employees.OrderBy(p=>p.UserName).ToList(), "Id", "UserName", model.ExportToEmployeeId);
            BindList(pageNo, cate.ToList());
            return View(model);
        }


        // GET: Category/Create
        public ActionResult Create(AccessoryExport model, int pageNo = 1)
        {
            var accessory =db.Accessories.Find(model.AccessoryId);
            var totalAmount = accessory.Amount;
            if (totalAmount < model.Amount)
            {
                ModelState.AddModelError("Invalid Amount", "Invalid Amount");
                TempData["Error"] = Language.AccessoryExport.AccessoryExport.OutOfAmountError;
            }
            if (model.ExportToEmployeeId == null)
            {
                ModelState.AddModelError("Invalid Amount", "Invalid Amount");
                TempData["Error1"] = Language.AccessoryExport.AccessoryExport.EmpError;
            }
            if (ModelState.IsValid)
            {
                
                var cookie = Request.Cookies["StockTool"];
                var UserID = "0";
                if (cookie != null && cookie["Role"] != null)
                {
                    UserID = cookie["UserID"].ToString();
                }
                int userid = int.Parse(UserID);
                model.ExportOn = DateTime.Now;
                model.ExportByUserId = userid;
                model.Cancel = false;
                db.AccessoryExports.Add(model);

                accessory.Amount = totalAmount - model.Amount;
                db.Entry(accessory).State = EntityState.Modified;

                db.SaveChanges();
            }

            return RedirectToAction("Index", new { PageNo = pageNo, accesoryid = model.AccessoryId });
        }


        // GET: Category/Edit/5
        public ActionResult Cancel(int id=0)
        {
            var accessExport = db.AccessoryExports.Find(id);
            accessExport.Cancel = true;
            db.Entry(accessExport).State = EntityState.Modified;

            var accessory = db.Accessories.Find(accessExport.AccessoryId);
            accessory.Amount = accessory.Amount + accessExport.Amount;
            db.Entry(accessory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = 1, accesoryid = accessExport.AccessoryId });
        }



        // GET: Category/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            if (id > 0)
            {
                db.Entry(db.AccessoryExports.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public ActionResult Import(HttpPostedFileBase file)
        {
            DataTable dt = file.ToDataSet().Tables[0];
            dt.Columns.Add("Status");
            dt.Columns.Add("Note");
            string _importDescription = string.Empty;
            if (dt != null)
            {
                //foreach (DataTable dt in ds.Tables)
                //{
                    foreach (DataRow r in dt.Rows)
                    {
                        try
                        {
                            var category = new Category();
                            string _categoryName = r[0].ToString();
                            string _name = r[1].ToString();
                            string _amount = r[2].ToString();
                            if (_name == string.Empty)
                            {
                                _importDescription = "Tên không thể trống";

                            }
                            if (_categoryName == string.Empty)
                            {
                                _importDescription = "Nhóm linh kiện không thể trống";

                            }

                            var iAmount = _amount != string.Empty ? int.Parse(_amount) : 0;
                            if (iAmount < 0 || iAmount > 100000)
                            {
                                _importDescription = string.Format("Số lượng phải trong khoảng 0 và 100000");
                            }

                            if (_importDescription == string.Empty)
                            {
                                if (_categoryName != string.Empty)
                                {
                                    category = db.Categories.Where(c => c.Name.Equals(_categoryName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                    if (category == null)
                                    {
                                        category = new Category();
                                        category.Code = AutoNumberHelper.GenerateNumber(ObjectType.Category, db);
                                        category.Name = _categoryName;
                                        db.Categories.Add(category);
                                        db.SaveChanges();
                                    }
                                }
                                Accessory item = db.Accessories.Where(p => p.Name.ToLower().Equals(_name.ToLower()) && p.CategoryId == category.Id).FirstOrDefault();
                                if (item != null)
                                {
                                    item.CategoryId = category.Id;
                                    item.Name = _name;
                                    item.Amount = iAmount;
                                    db.Entry(item).State = EntityState.Modified;
                                }
                                else
                                {
                                    item = new Accessory()
                                   {
                                       CategoryId = category.Id,
                                       Name = _name,
                                       Amount = iAmount,
                                   };
                                    db.Accessories.Add(item);
                                }
                            }
                        
                            if (_importDescription != string.Empty)
                            {
                                r[3] = "Không thành công";
                                r[4] = _importDescription;
                            }
                            else
                            {
                                r[3] = "Thành công";
                            }
                        }catch(Exception ex)
                        {
                            r[3] = "Lỗi";
                            r[4] = string.Format("Message: {0} - Inner exception: {1}",ex.Message, ex.InnerException);
                        }
                    }
                //}
                db.SaveChanges();
            }
            FileExcel(dt, "Accessory");
            return RedirectToAction("Index", "Accessory");
        }

        public ActionResult Export(string searchString = "")
        {
            var cates = db.FilteredAccessories.AsQueryable();
            if (searchString != string.Empty)
            {
                cates = cates.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
            }
            FileExcel(queryToDataTable<FilteredAccessory>(cates.AsEnumerable()), "FilteredAccessory");
            return View("Index","Accessory");
        }
    }
}