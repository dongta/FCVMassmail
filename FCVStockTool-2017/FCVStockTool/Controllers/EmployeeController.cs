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
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    public class EmployeeController : CommonController
    {
        void BindList(int pageNo, List<Employee> emp)
        {
            //check null emp
            if (emp != null)
            {
                ViewBag.Employees = emp.OrderBy(p => p.Id).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = emp.Count();
            }
            else
            {
                ViewBag.Employees = db.Employees.OrderBy(p => p.Id).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
                ViewBag.TotalRecords = db.Employees.Count();
            }
            
            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: Employee
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            //search
            var emp = from s in db.Employees
                      select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                emp = emp.Where(s => s.Name.ToLower().Contains(searchString.ToLower()) || s.Id.ToString().Contains(searchString));
            }
            //end search

            var model = new Employee();
            if (id > 0) model = db.Employees.Find(id);

            //add list emp into BindList
            BindList(pageNo, emp.ToList());

            ViewBag.Departments = new SelectList(db.Departments.OrderBy(p=>p.Name).ToList(), "Id", "Name", model.DepartmentId);
            ViewBag.SearchEmployees = new SelectList(db.Employees.OrderBy(p => p.Name).ToList(), "Name", "Name");
            //var c = new { id=1,Name="Name"};
            //ViewBag.Gender = new SelectList( , "Id", "Name", model.Gender);
            return View(model);
        }


        // GET: Employee/Create
        public ActionResult Create(Employee model, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }


        // GET: Employee/Edit/5
        public ActionResult Edit(Employee model, int pageNo = 1)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.Employees.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
        //public ActionResult FileExcel(DataTable dt)
        //{
        //    var grid = new GridView();
        //    grid.DataSource = dt;
        //    grid.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=Employee_Import.xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.Charset = "utf-8";

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        //        {
        //            grid.RenderControl(htw);
        //            Response.Output.Write(sw.ToString());
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    return RedirectToAction("Index", "Invoice");
        //}

        public ActionResult Import(HttpPostedFileBase file)
        {
            DataTable dt = new LdapAuthentication("").FindAllFCVADUsers();
            dt.Columns.Add("Decription");
            
            foreach (DataRow row in dt.Rows)
            {
                string description = string.Empty;
                try
                {
                    string _userName = row[0].ToString();
                    string _displayName = row[1].ToString();
                    string _department = row[2] != null? row[2].ToString():"";
                    string _title = row[3] != null ? row[3].ToString() : "";
                    string _pager = row[4] != null? row[4].ToString():"";

                    Department dept = db.Departments.Where(d => d.Name.ToLower() == _department.ToLower()).SingleOrDefault();
                    if (dept == null)
                    {
                        if (_department != string.Empty)
                        {
                            dept = new Department()
                            {
                                Name = _department
                            };
                            db.Departments.Add(dept);
                            db.SaveChanges();
                            description += "Tạo mới phòng " + _department + " ;";
                        }
                    }
                    Employee item = db.Employees.Where(d => d.UserName.ToLower() == _userName.ToLower()).SingleOrDefault();
                    if (item != null)
                    {
                        item.Code = _pager;
                        item.Name = _displayName;
                        item.Position = _title;
                        item.UserName = _userName;
                        item.DepartmentId = dept != null ? dept.Id : (int?)null;
                        db.Entry(item).State = EntityState.Modified;
                        description += "Cập nhật nhân viên " + _displayName + " ;";
                    }
                    else
                    {
                        item = new Employee()
                        {
                            Code = _pager,
                            Name = _displayName,
                            UserName = _userName,
                            Position = _title,
                            DepartmentId = dept != null ? dept.Id : (int?)null
                        };
                        db.Employees.Add(item);
                        description += "Thêm mới nhân viên " + _displayName + " ;";
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    description += "Message: " + ex.Message + "; Inner: " + ex.InnerException + ";";
                }
                row[5] = description;
            }
            FileExcel(dt, "Employee");
            #region import
            //DataSet ds = file.ToDataSet();
            //DataTable dt = ds.Tables[0];
            //dt.Columns.Add("Status", typeof(string));
            //dt.Columns.Add("Description", typeof(string));
            //Employee item;
            //if (ds != null)
            //{
            //    //foreach (DataTable dt in ds.Tables)
            //    //{
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        try
            //        {
            //            string importDescription = string.Empty;
            //            string username = string.Empty, deptCode, deptName = string.Empty, empCode = string.Empty,
            //                empName = string.Empty, position = string.Empty;
            //            empCode = r[0].ToString().Trim();
            //            empName = r[1].ToString().Trim();
            //            username = r[2].ToString().Trim();
            //            position = r[3].ToString();
            //            deptCode = r[4].ToString();
            //            deptName = r[5].ToString();
            //            Department dept;
            //            if (empName == string.Empty || empCode == string.Empty || deptName == string.Empty)
            //            {
            //                importDescription = "Tên phòng ban, mã nhân viên, tên nhân viên không thể trống";
            //            }
            //            if (importDescription == string.Empty)
            //            {
            //                dept = db.Departments.Where(d => d.Id.ToString() == deptCode || d.Name.ToLower() == deptName.ToLower()).SingleOrDefault();
            //                if (dept == null)
            //                {
            //                    dept = new Department()
            //                    {
            //                        Id = int.Parse(deptCode),
            //                        Name = deptName
            //                    };
            //                    db.Departments.Add(dept);
            //                    db.SaveChanges();
            //                }
            //                item = db.Employees.Find(int.Parse(empCode));
            //                if (item != null)
            //                {
            //                    item.Code = empCode;
            //                    item.Name = empName;
            //                    item.Position = position;
            //                    item.DepartmentId = dept.Id;
            //                    db.Entry(item).State = EntityState.Modified;
            //                }
            //                else
            //                {
            //                    item = new Employee()
            //                    {
            //                        Code = empCode,
            //                        Name = empName,
            //                        Position = position,
            //                        DepartmentId = dept.Id
            //                    };
            //                    db.Employees.Add(item);
            //                }

            //                db.SaveChanges();
            //                r["Status"] = "Thành công";
            //            }
            //            else
            //            {
            //                r["Status"] = "Không thành công";
            //                r["Note"] = importDescription;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            r["Status"] = "Lỗi";
            //            r["Note"] = string.Format("Message: {0} -  Inner exception: {1}", ex.Message, ex.InnerException);
            //        }
            //    }
            //    //}
            //}
            #endregion
            //FileExcel(dt);
            return RedirectToAction("Index", "Employee");
        }
    }
}
