using Excel;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    public class CategoryController : CommonController
    {
        void BindList(int pageNo, List<Category> cate)
        {
            if (cate != null)
            {
                ViewBag.Categories = cate.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = cate.Count();
            }
            else
            {
                ViewBag.Categories = db.Categories.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Categories.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Category
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var cate = from s in db.Categories
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cate = cate.Where(s => s.Name.Contains(searchString) || s.Code.Contains(searchString));
            }

            var model = new Category();
            if (id > 0) model = db.Categories.Find(id);
            BindList(pageNo, cate.ToList());
            return View(model);
        }


        // GET: Category/Create
        public ActionResult Create(Category model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                model.Code = AutoNumberHelper.GenerateNumber(ObjectType.Category, db);
                db.Categories.Add(model);
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Category/Edit/5
        public ActionResult Edit(Category model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Category/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Categories.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
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
                            string _code = r[0].ToString();
                            string _name = r[1].ToString();
                            string _description = r[2].ToString();
                            if (_name == string.Empty)
                            {
                                _importDescription = "Tên không thể trống";

                            }
                            else
                            {

                                Category item = db.Categories.Where(p => p.Code.ToLower().Equals(_code.ToLower()) || p.Name.ToLower().Equals(_name.ToLower())).FirstOrDefault();
                                if (item != null)
                                {
                                    item.Code = item.Code != string.Empty ? item.Code : _code;
                                    item.Name = _name;
                                    item.Description = _description;
                                    db.Entry(item).State = EntityState.Modified;
                                }
                                else
                                {
                                    if (_code == string.Empty) _code = AutoNumberHelper.GenerateNumber(ObjectType.Category, db);
                                    item = new Category()
                                   {
                                       Code = _code,
                                       Name = _name,
                                       Description = _description
                                   };
                                    db.Categories.Add(item);
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
            FileExcel(dt, "Category");
            return RedirectToAction("Index","Category");
        }

        public ActionResult Export(string searchString = "")
        {
            var cates = db.Categories.AsQueryable();
            if (searchString != string.Empty)
            {
                cates = cates.Where(p => p.Name.ToLower().Contains(searchString.ToLower()) || p.Code.ToLower().Contains(searchString.ToLower()));
            }
            FileExcel(queryToDataTable<Category>(cates.AsEnumerable()), "Category");
            return View("Index","Category");
        }
    }
}
