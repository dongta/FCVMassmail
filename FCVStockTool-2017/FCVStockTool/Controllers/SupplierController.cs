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
    public class SupplierController : CommonController
    {
        void BindList(int pageNo, List<Supplier> sup)
        {
            if (sup != null)
            {
                ViewBag.Suppliers = sup.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = sup.Count();
            }
            else
            {
                ViewBag.Suppliers = db.Suppliers.OrderBy(p => p.Name).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Suppliers.Count();
            }

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Supplier
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            var sup = from s in db.Suppliers
                      select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sup = sup.Where(s => s.Name.Contains(searchString));
            }

            var model = new Supplier();
            if (id > 0) model = db.Suppliers.Find(id);
            BindList(pageNo, sup.ToList());
            return View(model);
        }

        // GET: Supplier/Create
        public ActionResult Create(Supplier model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                model.Code = AutoNumberHelper.GenerateNumber(ObjectType.Supplier, db);
                db.Suppliers.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Supplier/Edit/5
        public ActionResult Edit(Supplier model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Suppliers.Find(id)).State = EntityState.Deleted;
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
                        string _address = r[2].ToString();
                        string _email = r[3].ToString();
                        string _phone = r[4].ToString();
                        string _fax = r[5].ToString();
                        string _website = r[6].ToString();
                        string _decription = r[7].ToString();
                        if (_name == string.Empty)
                        {
                            _importDescription = "Tên không thể trống";

                        }
                        else
                        {

                            Supplier item = db.Suppliers.Where(p => p.Code.ToLower().Equals(_code.ToLower()) || p.Name.ToLower().Equals(_name.ToLower())).FirstOrDefault();
                            if (item != null)
                            {
                                item.Code = item.Code != string.Empty ? item.Code : _code;
                                item.Name = _name;
                                item.Address = _address;
                                item.Email = _email;
                                item.PhoneNumber = _phone;
                                item.Fax = _fax;
                                item.Website = _website;
                                item.Description = _decription;
                                db.Entry(item).State = EntityState.Modified;
                            }
                            else
                            {
                                if (_code == string.Empty) _code = AutoNumberHelper.GenerateNumber(ObjectType.Supplier, db);
                                item = new Supplier()
                                {
                                    Code = _code,
                                    Name = _name,
                                    Address = _address,
                                    PhoneNumber = _phone,
                                    Fax = _fax,
                                    Website = _website,
                                    Email = _email,
                                    Description = _decription
                                };
                                db.Suppliers.Add(item);
                            }
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
                        r["Note"] = string.Format("Message: {0} - Inner exception: {1}", ex.Message, ex.InnerException);
                    }
                }
                //}
                db.SaveChanges();
            }
            FileExcel(dt, "Supplier");
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult Export(string searchString = "")
        {
            var cates = db.Suppliers.AsQueryable();
            if (searchString != string.Empty)
            {
                cates = cates.Where(p => p.Name.ToLower().Contains(searchString.ToLower()) || p.Code.ToLower().Contains(searchString.ToLower()) || p.PhoneNumber.ToLower().Contains(searchString.ToLower()) || p.Email.ToLower().Contains(searchString.ToLower()));
            }
            FileExcel(queryToDataTable<Supplier>(cates.AsEnumerable()), "Supplier");
            return View("Index", "Supplier");
        }
    }
}
