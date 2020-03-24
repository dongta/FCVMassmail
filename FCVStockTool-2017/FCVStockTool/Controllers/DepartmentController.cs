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
    public class DepartmentController : CommonController
    {
        void BindList(int pageNo,List<Department> dept)
        {
            if (dept != null)
            {
                ViewBag.Departments = dept.OrderBy(p => p.Id).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = dept.Count();
            }
            else
            {
                ViewBag.Departments = db.Departments.OrderBy(p => p.Id).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Departments.Count();
            }
            
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Department
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {

            var dept = from s in db.Departments
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                dept = dept.Where(s => s.Name.ToLower().Contains(searchString.ToLower()) || s.Id.ToString().Contains(searchString));
            }

            var model = new Department();
            if (id > 0) model = db.Departments.Find(id);
            BindList(pageNo,dept.ToList());
            return View(model);
        }


        // GET: Department/Create
        public ActionResult Create(Department model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                //if (db.Departments.Where(d => d.Id == model.Id || d.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefault() != null)
                //{
                //    ModelState.AddModelError("Error", "Phòng ban đã tồn tại!");
                //    return View("Index", model);
                //}
                //else
                //{
                    db.Departments.Add(model);
                    db.SaveChanges();
                //}
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Department/Edit/5
        public ActionResult Edit(Department model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Department/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Departments.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
        public ActionResult Export(string searchString = "")
        {
            var cates = db.Departments.AsQueryable();
            if (searchString != string.Empty)
            {
                cates = cates.Where(p => p.Name.ToLower().Contains(searchString.ToLower()) || p.Id.ToString().Contains(searchString.ToLower()));
            }
            FileExcel(queryToDataTable<Department>(cates.AsEnumerable()), "Department");
            return View("Index", "Department");
        }


        public ActionResult Import(HttpPostedFileBase file)
        {
            DataTable dt = file.ToDataSet().Tables[0];
            Department item;
            string _importDescription = string.Empty;
            dt.Columns.Add("Status");
            dt.Columns.Add("Note");
            if (dt != null)
            {
                //foreach (DataTable dt in ds.Tables)
                //{
                foreach (DataRow r in dt.Rows)
                {
                    try
                    {
                        //string _id = r[0].ToString();
                        string _name = r[0].ToString();
                        if (_name == string.Empty)
                        {
                            _importDescription = "Tên phòng ban không thể trống";
                        }
                        else
                        {
                            item = db.Departments.Where(p => p.Name.ToLower().Equals(_name.ToLower())).FirstOrDefault();
                            if (item != null)
                            {
                                item.Name = _name;
                                db.Entry(item).State = EntityState.Modified;
                            }
                            else
                            {
                                item = new Department()
                                {
                                    Name = _name
                                };
                                db.Departments.Add(item);
                            }
                            db.SaveChanges();
                        }
                        if (_importDescription != string.Empty)
                        {
                            r["Status"] = "Không thành công";
                            r["Note"] = _importDescription;
                        }
                        else
                        {
                            r["Status"] = "Thành công";
                        }
                    }
                    catch (Exception ex)
                    {
                        r["Status"] = "Lỗi";
                        r["Note"] = string.Format("Message: {0} -  Inner exception: {1}", ex.Message, ex.InnerException);
                    }
                }
                //}
                db.SaveChanges();
            }
            FileExcel(dt, "Product");
            return View("Index", "Product");
        }
    }
}
