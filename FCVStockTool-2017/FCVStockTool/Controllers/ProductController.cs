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
    public class ProductController : CommonController
    {
        void BindList(int pageNo, int parentid, int type, List<Product> pro, int? categoryid = 0)
        {
            //if (pro != null)
            //{
                var products = db.Products.AsQueryable();
                if (parentid > 0) products = products.Where(p => p.ParentId == parentid);
                if (type > 0) products = products.Where(p => p.Type == type);
                if (categoryid > 0) products = products.Where(p => p.CategoryId == categoryid);
                ViewBag.Products = products.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = products.Count();
            //}
            //else
            //{
            //    ViewBag.Products = db.Products.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            //    ViewBag.TotalRecords = db.Products.Count();
            //    //var products = db.Products.AsQueryable();
            //    //if (parentid != 0) products = products.Where(p => p.ParentId == parentid);
            //    //if (type != 0) products = products.Where(p => p.Type == type);
            //    //ViewBag.Products = products.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            //    //ViewBag.TotalRecords = products.Count();
            //}



            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Product
        public ActionResult Index(int pageNo = 1, int id = 0, int parentid = 0, int type = 0, string searchString = "", Product upmodel = null)
        {

            var pro = from a in db.Products
                      select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                pro = pro.Where(a => a.Name.Contains(searchString));
            }

            var model = new Product();
            if (id > 0) model = db.Products.Find(id);
            if (model.Id < 1)
            {
                model.CategoryId = upmodel.CategoryId;
                model.Name = upmodel.Name;
            }
            BindList(pageNo, parentid, type, pro.ToList(), upmodel.CategoryId);
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", model.CategoryId);
            ViewBag.ParentProducts = new SelectList(db.Products.Where(p => p.Type == 1).ToList(), "Id", "Name", model.ParentId > 0 ? model.ParentId : (parentid > 0 ? parentid : 0));
            return View(model);
        }


        // GET: Product/Create
        public ActionResult Create(Product model, int pageNo = 1)
        {

            model.Code = AutoNumberHelper.GenerateNumber(ObjectType.Product, db);
            if (model.ParentId == null || model.ParentId < 1) model.Type = 1; else model.Type = 2;
            if (ModelState.IsValid)
            {
                db.Products.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Product/Edit/5
        public ActionResult Edit(Product model, int pageNo = 1)
        {
            if (model.ParentId == null || model.ParentId < 1) model.Type = 1; else model.Type = 2;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }



        // GET: Product/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Products.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
        public ActionResult Export(string searchString = "")
        {
            var cates = db.Products.AsQueryable();
            if (searchString != string.Empty)
            {
                cates = cates.Where(p => p.Name.ToLower().Contains(searchString.ToLower()) || p.Code.ToLower().Contains(searchString.ToLower()));
            }
            FileExcel(queryToDataTable<Product>(cates.AsEnumerable()), "Product");
            return View("Index", "Product");
        }


        public ActionResult Import(HttpPostedFileBase file)
        {
            DataTable dt = file.ToDataSet().Tables[0];
            Product item;
            string _importDescription=string.Empty;
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
                        string _code = r[0].ToString();
                        string _name = r[1].ToString();
                        string _cateName = r[2].ToString();
                        string _description = r[3].ToString();
                        if (_name == string.Empty)
                        {
                            _importDescription = "Tên sản phẩm không thể trống";
                        }
                        else
                        {
                            Category cate = null;
                            if (_cateName != string.Empty)
                            {
                                cate = db.Categories.Where(p => p.Code.ToLower().Equals(_cateName.ToLower()) || p.Name.ToLower().Equals(_cateName.ToLower())).FirstOrDefault();
                                if (cate == null)
                                {
                                    cate = new Category()
                                    {
                                        Code = AutoNumberHelper.GenerateNumber(ObjectType.Category, db),
                                        Name = _cateName
                                    };
                                    db.Categories.Add(cate);
                                    db.SaveChanges();
                                }
                            }
                            item = db.Products.Where(p => p.Code.ToLower().Equals(_code.ToLower()) || p.Name.ToLower().Equals(_name.ToLower())).FirstOrDefault();
                            if (item != null)
                            {
                                item.Name = _name;
                                item.Code = _code != string.Empty ? _code : item.Code;
                                item.CategoryId = cate != null ? cate.Id : (int?)null;
                                db.Entry(item).State = EntityState.Modified;
                            }
                            else
                            {
                                item = new Product()
                                    {
                                        Name = _name,
                                        Code = AutoNumberHelper.GenerateNumber(ObjectType.Product, db),
                                        CategoryId = cate != null ? cate.Id : (int?)null
                                    };
                                db.Products.Add(item);
                            }
                            db.SaveChanges();
                        }
                        if (_importDescription != string.Empty)
                        {
                            r["Status"] = "Không thành công";
                            r["Note"] = _importDescription;
                        }
                        else {
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
