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
    public class AccessoryController : CommonController
    {
        void BindList(int pageNo, List<Accessory> cate)
        {
            if (cate != null)
            {
                ViewBag.Accessories = cate.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = cate.Count();
            }
            else
            {
                ViewBag.Accessories = db.Accessories.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Accessories.Count();
            }
            
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Category
        public ActionResult Index(int pageNo = 1, int id = 0, int CategoryId =0,string searchString = "")
        {
            var cate = from s in db.Accessories
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cate = cate.Where(s => s.Name.Contains(searchString));
            }
            if (CategoryId > 0) cate = cate.Where(p => p.CategoryId == CategoryId);
            var model = new Accessory();
            if (id > 0) model = db.Accessories.Find(id);
            ViewBag.Categories = new SelectList(db.Categories.OrderBy(p => p.Name).ToList(), "Id", "Name", model.CategoryId);
            BindList(pageNo, cate.ToList());
            return View(model);
        }


        // GET: Category/Create
        public ActionResult Create(Accessory model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                var UserID = "0";
                var cookie = Request.Cookies["StockTool"];
                if (cookie != null && cookie["Role"] != null)
                {
                    UserID = cookie["UserID"].ToString();
                }
                int userid = int.Parse(UserID);

                var asset = db.Accessories.Where(p => p.CategoryId == model.CategoryId && p.Name.Equals(model.Name.Trim(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (asset != null)
                {
                    asset.Amount += model.Amount;
                    asset.ImportByUserId = userid;
                    asset.ImportOn = DateTime.Now;
                    db.Entry(asset).State = EntityState.Modified;

                    var acceImport = new AccessoryImport();
                    acceImport.AccessoryId = asset.Id;
                    acceImport.Amount = model.Amount;
                    acceImport.Description = model.Description;
                    acceImport.ImportOn = DateTime.Now;
                    acceImport.ImportByUserId = userid;
                    acceImport.Cancel = false;
                    db.AccessoryImports.Add(acceImport);
                }
                else
                {
                    model.ImportByUserId = userid;
                    model.ImportOn = DateTime.Now;
                    db.Accessories.Add(model);
                    var acceImport = new AccessoryImport();
                    acceImport.Accessory = model;
                    acceImport.Description = model.Description;
                    acceImport.Amount = model.Amount;
                    acceImport.ImportOn = DateTime.Now;
                    acceImport.Cancel = false;
                    acceImport.ImportByUserId = userid;
                    db.AccessoryImports.Add(acceImport);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Category/Edit/5
        public ActionResult Edit(Accessory model, int pageNo = 1)
        {
            var UserID = "0";
            var cookie = Request.Cookies["StockTool"];
            if (cookie != null && cookie["Role"] != null)
            {
                UserID = cookie["UserID"].ToString();
            }
            int userid = int.Parse(UserID);
            model.ImportByUserId = userid;
            model.ImportOn = DateTime.Now;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Category/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            if (id > 0)
            {
                db.Entry(db.Accessories.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        public ActionResult Import(HttpPostedFileBase file)
        {
            var cookie = Request.Cookies["StockTool"];
            var UserID = "0";
            if (cookie != null && cookie["Role"] != null)
            {
                UserID = cookie["UserID"].ToString();
            }
            int userid = int.Parse(UserID);
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
                            string _description = r[3].ToString();
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
                                    item.Amount += iAmount;
                                    item.Description = _description;
                                    db.Entry(item).State = EntityState.Modified;

                                    var acceImport = new AccessoryImport();
                                    acceImport.AccessoryId = item.Id;
                                    acceImport.Amount = item.Amount;
                                    acceImport.Description = item.Description;
                                    acceImport.ImportOn = DateTime.Now;
                                    acceImport.ImportByUserId = userid;
                                    acceImport.Cancel = false;
                                    db.AccessoryImports.Add(acceImport);
                                }
                                else
                                {
                                    
                                    item = new Accessory()
                                   {
                                       CategoryId = category.Id,
                                       Name = _name,
                                       Amount = iAmount,
                                       ImportByUserId = userid,
                                       Description = _description,
                                       ImportOn = DateTime.Now
                                   };
                                    db.Accessories.Add(item);

                                    var acceImport = new AccessoryImport();
                                    acceImport.Accessory = item;
                                    acceImport.Description = item.Description;
                                    acceImport.Amount = item.Amount;
                                    acceImport.ImportOn = DateTime.Now;
                                    acceImport.Cancel = false;
                                    acceImport.ImportByUserId = userid;
                                    db.AccessoryImports.Add(acceImport);
                                }
                            }
                        
                            if (_importDescription != string.Empty)
                            {
                                r[4] = "Không thành công";
                                r[5] = _importDescription;
                            }
                            else
                            {
                                r[4] = "Thành công";
                            }
                        }catch(Exception ex)
                        {
                            r[4] = "Lỗi";
                            r[5] = string.Format("Message: {0} - Inner exception: {1}",ex.Message, ex.InnerException);
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