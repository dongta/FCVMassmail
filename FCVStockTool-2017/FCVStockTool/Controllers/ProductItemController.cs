using Excel;
using FCVStockTool.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using FCVStockTool.Filters;
using FCVStockTool.Models;
using System.Configuration;
namespace FCVStockTool.Controllers
{
    [Filters.AdminRole]
    public class ProductItemController : CommonController
    {

        void BindList(int pageNo, string search)
        {
            var pros = db.ProductItems.AsQueryable();
            if (search != string.Empty)
            {
                pageNo = 1;
                pros = pros.Where(c => c.SerialNo.ToLower().Contains(search.ToLower()) || c.ShortCode.ToLower().Contains(search.ToLower()) || c.Name.ToLower().Contains(search.ToLower())
                    || c.Employee.Code.ToLower().Contains(search.ToLower()) || c.Employee.Name.ToLower().Contains(search.ToLower()) || c.Employee.UserName.ToLower().Contains(search.ToLower()) || c.Product.Name.Contains(search.ToLower()));
            }

            ViewBag.ProductItems = pros.OrderByDescending(p => p.PODate).Skip((pageNo - 1) * PagingHelper.PageSize()).Take(PagingHelper.PageSize());
            ViewBag.TotalRecords = pros.Count();

            ViewBag.TotalPage = (ViewBag.TotalRecords / PagingHelper.PageSize()) + 1;
            ViewBag.CurrentPage = pageNo;
        }

        // GET: ProductItem
        public ActionResult Index(int pageNo = 1, int id = 0, string searchString = "")
        {
            //search
            //var proItems = db.Products;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    proItem = proItem.Where(s => s.SerialNo.Contains(pro.Id));
            //}
            //end search

            var model = new ProductItem();
            if (id > 0) model = db.ProductItems.Find(id);

            //add list proItem into BindList
            BindList(pageNo, searchString);

            ViewBag.Products = new SelectList(db.Products.OrderBy(p => p.Name).ToList(), "Id", "Name", model.ProductId);
            ViewBag.ProductStatuses = new SelectList(db.ProductStatuses.ToList(), "Id", "Text", model.ProductStatusId);
            ViewBag.StockStatuses = new SelectList(db.StockStatuses.ToList(), "Id", "Text", model.StockStatusId);

            return View(model);
        }

        [RestoreModelStateFromTempData]
        public ActionResult Details(int id = 0)
        {
            if (TempData["Error"] != null) ModelState.AddModelError(string.Empty, TempData["Error"].ToString());
            var model = new ProductItem();
            if (id > 0) model = db.ProductItems.Find(id);

            if (model.InvoiceId > 0)
            {
                ViewBag.InvoiceDetail = db.InvoiceDetails.Where(d => d.InvoiceId == model.InvoiceId && d.ProductId == model.ProductId).FirstOrDefault();
            }

            ViewBag.Products = new SelectList(db.Products.OrderBy(p=>p.Name).ToList(), "Id", "Name", model.ProductId);
            ViewBag.ProductStatuses = new SelectList(db.ProductStatuses.ToList(), "Id", "Text", model.ProductStatusId);
            ViewBag.StockStatuses = new SelectList(db.StockStatuses.ToList(), "Id", "Text", model.StockStatusId);
           // ViewBag.StockDiaries = db.StockDiaries.Where(s => s.ProductId == model.Id && s.SerialNo == model.SerialNo).OrderByDescending(p => p.DateEvent);
            ViewBag.Buildings = new SelectList(db.Buildings.OrderBy(p => p.Name).ToList(), "Id", "Name", model.BuildingId);
            ViewBag.Employees = new SelectList(db.Employees.Select(e => new { Id = e.Id, Name = e.UserName }).ToList(), "Id", "Name", model.EmployeeId);
            ViewBag.Department = new SelectList(db.Departments.ToList(), "Id", "Name", model.DepartmentId);
            return View(model);
        }

        // GET: ProductItem/Create
        [SetTempDataModelState]
        public ActionResult Create(ProductItem model, int pageNo = 1)
        {
            var productItem = db.ProductItems.Where(p => p.ShortCode.Equals(model.ShortCode, StringComparison.OrdinalIgnoreCase) || p.Name.Equals(model.ShortCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (productItem != null)
            {
                TempData["Error"] = Language.Accessory.Accessory.DuplicateError;
                return RedirectToAction("Details", new { PageNo = pageNo });
            }

            if (ModelState.IsValid)
            {
                db.ProductItems.Add(model);
                db.SaveChanges();
                StockDiary diary = new StockDiary()
                {
                    Date = DateTime.Now,
                    Note = model.Description,
                    Type = db.StockStatuses.Find(model.StockStatusId).Text,
                    ProductItemId = model.Id
                };

                db.StockDiaries.Add(diary);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: ProductItem/Edit/5
        public ActionResult Edit(ProductItem model, int oldStockStatusId, int? oldEmployeeId, int pageNo = 1, string PONo = "")
        {
            var productItem = db.ProductItems.Where(p => (p.ShortCode.Equals(model.ShortCode, StringComparison.OrdinalIgnoreCase) || p.Name.Equals(model.ShortCode, StringComparison.OrdinalIgnoreCase)) && p.Id != model.Id).FirstOrDefault();
            if (productItem != null)
            {
                TempData["Error"] = Language.Accessory.Accessory.DuplicateError;
                return RedirectToAction("Details", new { id=model.Id, PageNo = pageNo });
            }
            string oldEmployeeName = string.Empty;
            if (oldEmployeeId != model.EmployeeId)
            {
                oldEmployeeName = (oldEmployeeId != null && oldEmployeeId > 0) ? db.Employees.Find(oldEmployeeId).Name : string.Empty;
            }

            string newEmployeeName = string.Empty;
            if (oldEmployeeId != model.EmployeeId)
            {
                newEmployeeName = (model.EmployeeId != null && model.EmployeeId > 0) ? db.Employees.Find(model.EmployeeId).Name : string.Empty;
            }

            if (oldStockStatusId != model.StockStatusId || (oldEmployeeId != model.EmployeeId))
            {
                StockDiary diary = new StockDiary()
                {
                    Date = DateTime.Now,
                    Note = string.Format("{0} => {1}",oldEmployeeName, newEmployeeName),
                    Type = db.StockStatuses.Find(model.StockStatusId).Text,
                    ProductItemId = model.Id
                };
                db.StockDiaries.Add(diary);
            }

            db.Entry<ProductItem>(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }

        // GET: ProductItem/Delete/5
        public ActionResult Delete(int id, int pageNo = 1)
        {
            db.Entry(db.ProductItems.Find(id)).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", new { PageNo = pageNo });
        }
        //download File

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            DataSet ds = file.ToDataSet();
            Utils.LdapAuthentication _ldap = new Utils.LdapAuthentication(string.Format("LDAP://{0}", ConfigurationManager.AppSettings["Domain"]));

            DataTable dt = ds.Tables[0];

            if (dt.Columns.Count == 5)
            {
                dt.Columns.Add("Kết quả");
                if (ds != null)
                {

                    //foreach (DataTable dt2 in ds.Tables)
                    //{
                    foreach (DataRow r in dt.Rows)
                    {
                        string importDescription = string.Empty;
                        string error = string.Empty;
                        string shortCode = string.Empty;
                        string _username = string.Empty;
                        string _stockStatusName = string.Empty;
                        string _POno = string.Empty;
                        string _description = string.Empty;
                        try
                        {
                            shortCode = r[0].ToString();
                            _username = r[1].ToString();
                            _stockStatusName = r[2].ToString();
                            _POno = r[3].ToString();
                            _description = r[4].ToString();

                            var productItem = db.ProductItems.Where(p => p.ShortCode.Equals(shortCode, StringComparison.OrdinalIgnoreCase) || p.Name.Equals(shortCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                            if (productItem == null)
                            {
                                importDescription = "ShortCode không tồn tại";
                                error = "Lỗi";
                            }
                            else
                            {
                                var employee = new Employee();
                                if (_username != string.Empty)
                                {
                                    //int empID = int.Parse(_eCode);
                                    employee = db.Employees.Where(e => (e.UserName.Equals(_username, StringComparison.OrdinalIgnoreCase) && _username != string.Empty)).FirstOrDefault();
                                    if (employee == null)
                                    {
                                        importDescription = "Nhân viên không tồn tại";
                                        //ImpersonatorUser imp = _ldap.Finduser(_username);
                                        //if (imp != null)
                                        //{
                                        //    Department department= null;
                                        //    if (imp.Department != string.Empty && imp.Department != null)
                                        //    {
                                        //        department = new Department()
                                        //       {
                                        //           Name = imp.Department
                                        //       };
                                        //        db.Departments.Add(department);
                                        //        db.SaveChanges();
                                        //    }

                                        //    employee = new Employee();
                                        //    employee.Code = imp.Pager;
                                        //    employee.Name = imp.DisplayName;
                                        //    if (department != null) employee.DepartmentId = department.Id;
                                        //    employee.UserName = imp.Username;
                                        //    db.Employees.Add(employee);
                                        //    db.SaveChanges();
                                        //    importDescription = "Tạo mới nhân viên";
                                        //}
                                        //else
                                        //{
                                        //    importDescription = "Nhân viên không tồn tại";
                                        //    error = "Lỗi";
                                        //}
                                    }
                                }
                                var stockStatus = new StockStatus();
                                if (_stockStatusName != string.Empty)
                                {
                                    stockStatus = db.StockStatuses.Where(s => s.Text.Equals(_stockStatusName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                    if (stockStatus == null)
                                    {
                                        importDescription += "Không tìm thấy trạng thái kho; ";
                                        //stockStatus = new StockStatus();
                                        //stockStatus.Text = _stockStatusName;
                                        //db.StockStatuses.Add(stockStatus);
                                        //db.SaveChanges();
                                    }
                                }
                                
                                if (error == string.Empty)
                                {
                                    if (productItem.StockStatusId != stockStatus.Id)
                                    {
                                        StockDiary diary = new StockDiary()
                                        {
                                            Date = DateTime.Now,
                                            Note = "",
                                            Type = _stockStatusName,
                                            ProductItemId = productItem.Id
                                        };

                                        db.StockDiaries.Add(diary);
                                    }

                                    if (employee != null && employee.Id > 0)
                                    {
                                        productItem.EmployeeId = employee.Id;
                                        productItem.StockStatusId = stockStatus.Id;
                                    }
                                    else
                                    {
                                        productItem.EmployeeId = null;
                                        productItem.StockStatusId = stockStatus.Id;
                                    }
                                    productItem.PONo = _POno;
                                    productItem.Description = _description;
                                    db.SaveChanges();

                                    importDescription = "Thành công";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            importDescription += "Error: " + ex.Message + " Inner: " + ex.InnerException;
                        }
                        r[0] = shortCode;
                        r[1] = _username;
                        r[2] = _stockStatusName;
                        r[3] = _POno;
                        r[4] = _description;
                        r[5] = importDescription;
                    }
                    FileExcel(dt, "ProductItem");
                }
            }
            else
            {
                
                    dt.Columns.Add("Import Status", typeof(string));
               
                    dt.Columns.Add("Import Detail", typeof(string));
               
                //dt.Columns.Add("Description", typeof(string));

               

                if (ds != null)
                {
                    //foreach (DataTable dt2 in ds.Tables)
                    //{
                    foreach (DataRow r in dt.Rows)
                    {
                        try
                        {
                            string _departmentName = string.Empty;
                            string _categoryName = string.Empty;
                            string _buildingName = string.Empty;
                            string _sectionName = string.Empty;
                            string _userCode = string.Empty, _userDisplayName = string.Empty;//, _userUserName = string.Empty _eCode = string.Empty;
                            string _shortCode = string.Empty, _shortName = string.Empty, _productName = string.Empty, _serialNo = string.Empty;
                            DateTime _buyDate = DateTime.MinValue;
                            DateTime _expiryDate = DateTime.MinValue;
                            string _stockStatusName;//, _productStatusName, _poNo = string.Empty, _cdROM = string.Empty, _remark = string.Empty;
                            string _supplierName = string.Empty;
                            
                            var productItem = new ProductItem();
                            var category = new Category();
                            var product = new Product();
                            var dept = new Department();
                            var employee = new Employee();
                            var productStatus = new ProductStatus();
                            var building = new Building();
                            var stockStatus = new StockStatus();
                            var supplier = new Supplier();
                            string _POno = string.Empty;
                            string _description = string.Empty;
                            string importDescription = string.Empty;

                            _shortCode = r[0].ToString();
                            _shortName = r[1].ToString();
                            //_buildingName = r[2].ToString();
                            _categoryName = r[3].ToString();
                            _productName = r[4].ToString();
                            _sectionName = r[5].ToString();
                            _buildingName = r[6].ToString();
                            _userCode = r[7].ToString();
                            _userDisplayName = r[8].ToString();
                            _departmentName = r[9].ToString();
                            _stockStatusName = r[10].ToString();
                            _serialNo = r[11].ToString();
                            _expiryDate = r[12] != null && r[12].ToString() != string.Empty? DateTime.Parse(r[12].ToString()) : DateTime.MinValue;
                            _buyDate = r[13] != null && r[13].ToString() != string.Empty ? DateTime.Parse(r[13].ToString()) : DateTime.MinValue;
                            _supplierName = r[14].ToString();
                            _POno = r[15].ToString();
                            _description = r[16].ToString();
                            #region check trùng dữ liệu
                            //if (db.ProductItems.Where(p => p.ShortCode.Equals(_shortCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)
                            //{
                            //    importDescription = "ShortCode đã tồn tại";
                            //}
                            //if (db.ProductItems.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)
                            //{
                            //    importDescription = "Name đã tồn tại";
                            //}
                            //if (db.ProductItems.Where(p => p.SerialNo.Equals(serialNo, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)
                            //{
                            //    importDescription = "Số Serial No đã tồn tại";
                            //}
                            #endregion

                            if (importDescription == string.Empty)
                            {

                                #region kiểm tra và tạo dữ liệu từ điển

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
                                if (_buildingName != string.Empty)
                                {
                                    building = db.Buildings.Where(c => c.Name.Equals(_buildingName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                    if (building == null)
                                    {
                                        building = new Building();
                                        building.Code = AutoNumberHelper.GenerateNumber(ObjectType.Building, db);
                                        building.Name = _buildingName;
                                        db.Buildings.Add(building);
                                        db.SaveChanges();
                                    }
                                }

                                product = db.Products.Where(p => p.Name.Equals(_productName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                if (product == null)
                                {
                                    product = new Product();
                                    product.Code = AutoNumberHelper.GenerateNumber(ObjectType.Product, db);
                                    product.Name = _productName;
                                    product.CategoryId = category.Id > 0 ? category.Id : (int?)null;
                                    db.Products.Add(product);
                                    db.SaveChanges();
                                }

                                //if (_departmentName != string.Empty)
                                //{
                                //    dept = db.Departments.Where(d => d.Name.Equals(_departmentName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                //    if (dept == null)
                                //    {
                                //        dept = new Department();
                                //        dept.Name = _departmentName;
                                //        db.Departments.Add(dept);
                                //        db.SaveChanges();
                                //    }
                                //}

                                if (_supplierName != string.Empty)
                                {
                                    supplier = db.Suppliers.Where(d => d.Name.Equals(_supplierName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                    if (supplier == null)
                                    {
                                        supplier = new Supplier();
                                        supplier.Name = _supplierName;
                                        db.Suppliers.Add(supplier);
                                        db.SaveChanges();
                                    }
                                }

                                if (_userDisplayName != string.Empty || _userCode != string.Empty)
                                {
                                    //int empID = int.Parse(_eCode);
                                    employee = db.Employees.Where(e =>(e.UserName.Equals(_userCode, StringComparison.OrdinalIgnoreCase) && _userCode != string.Empty)).FirstOrDefault();
                                    if (employee == null)
                                    {
                                        importDescription += "Không tìm thấy nhân viên; ";
                                    }
                                }

                                //if (_productStatusName != string.Empty)
                                //{
                                //    productStatus = db.ProductStatuses.Where(s => s.Text.Equals(_productStatusName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                //    if (productStatus == null)
                                //    {
                                //        productStatus = new ProductStatus();
                                //        productStatus.Text = _productStatusName;
                                //        db.ProductStatuses.Add(productStatus);
                                //        db.SaveChanges();
                                //    }
                                //}

                                if (_stockStatusName != string.Empty)
                                {
                                    stockStatus = db.StockStatuses.Where(s => s.Text.Equals(_stockStatusName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                    if (stockStatus == null)
                                    {
                                        importDescription += "Không tìm thấy trạng thái kho; ";
                                        //stockStatus = new StockStatus();
                                        //stockStatus.Text = _stockStatusName;
                                        //db.StockStatuses.Add(stockStatus);
                                        //db.SaveChanges();
                                    }
                                }
                                if (importDescription == string.Empty)
                                {
                                    productItem = db.ProductItems.Where(p => p.ShortCode.Equals(_shortCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                    if (productItem == null)
                                    {
                                        productItem = new ProductItem()
                                        {
                                            ShortCode = _shortCode,
                                            Name = _shortName,
                                            ProductId = product.Id,
                                            BuildingId = building != null && building.Id > 0 ? building.Id : (int?)null,
                                            SerialNo = _serialNo,
                                            PODate = _buyDate > DateTime.MinValue ? _buyDate : (DateTime?)null,
                                            ExpiryDate = _expiryDate > DateTime.MinValue ? _expiryDate : (DateTime?)null,
                                            ProductStatusId = productStatus != null && productStatus.Id > 0 ? productStatus.Id : (int?)null,
                                            StockStatusId = stockStatus != null && stockStatus.Id > 0 ? stockStatus.Id : (int?)null,
                                            //CDROM = _cdROM != string.Empty ? int.Parse(_cdROM) : (int?)null,
                                            EmployeeId = employee != null && employee.Id > 0 ? employee.Id : (int?)null,
                                            SupplierId = supplier != null && supplier.Id > 0 ? supplier.Id : (int?)null,
                                            PONo = _POno,
                                            Description = _description,
                                            CreatedOn = DateTime.Now
                                        };
                                        db.ProductItems.Add(productItem);
                                        db.SaveChanges();

                                        StockDiary diary = new StockDiary()
                                            {
                                                Date = DateTime.Now,
                                                Note = "",
                                                Type = _stockStatusName,
                                                ProductItemId = productItem.Id
                                            };

                                        db.StockDiaries.Add(diary);

                                        db.SaveChanges();

                                        importDescription += "Thêm mới";
                                    }
                                    else
                                    {


                                        string oldEmployeeName = string.Empty;
                                        if (employee.Id != productItem.EmployeeId)
                                        {
                                            oldEmployeeName = (productItem.EmployeeId != null && productItem.EmployeeId > 0) ? db.Employees.Find(productItem.EmployeeId).Name : string.Empty;
                                        }

                                        string newEmployeeName = string.Empty;
                                        if (employee.Id != productItem.EmployeeId)
                                        {
                                            newEmployeeName = (employee.Id != null && employee.Id > 0) ? db.Employees.Find(employee.Id).Name : string.Empty;
                                        }

                                        if (productItem.StockStatusId != stockStatus.Id || productItem.EmployeeId != employee.Id)
                                        {
                                            StockDiary diary = new StockDiary()
                                            {
                                                Date = DateTime.Now,
                                                Note = string.Format("{0} => {1}", oldEmployeeName, newEmployeeName),
                                                Type = _stockStatusName,
                                                ProductItemId = productItem.Id
                                            };

                                            db.StockDiaries.Add(diary);
                                        }

                                        productItem.ProductId = product.Id;
                                        productItem.Name = _shortName;
                                        productItem.BuildingId = building != null && building.Id > 0 ? building.Id : (int?)null;
                                        productItem.SerialNo = _serialNo;
                                        productItem.PODate = _buyDate > DateTime.MinValue ? _buyDate : (DateTime?)null;
                                        productItem.ProductStatusId = productStatus != null && productStatus.Id > 0 ? productStatus.Id : (int?)null;
                                        productItem.StockStatusId = stockStatus != null && stockStatus.Id > 0 ? stockStatus.Id : (int?)null;
                                        //productItem.CDROM = _cdROM != string.Empty ? int.Parse(_cdROM) : (int?)null;
                                        productItem.EmployeeId = employee != null && employee.Id > 0 ? employee.Id : (int?)null;
                                        productItem.SupplierId = supplier != null && supplier.Id > 0 ? supplier.Id : (int?)null;
                                        
                                        productItem.ExpiryDate = _expiryDate > DateTime.MinValue ? _expiryDate : (DateTime?)null;
                                        db.Entry(productItem).State = EntityState.Modified;
                                        productItem.PONo = _POno;
                                        productItem.Description = _description;
                                        db.SaveChanges();
                                        importDescription += "Cập nhật";
                                    }
                                    r[17] = "Thành công";
                                }
                                else
                                {
                                    r[17] = "Không thành công";
                                }
                                #endregion
                            }
                            else
                            {
                                r[17] = "Không thành công";
                            }
                            r[18] = importDescription;
                        }
                        catch (Exception ex)
                        {
                            r[17] = "Lỗi";
                            r[18] = string.Format("Message: {0} - Inner Exception: {1}", ex.Message, ex.InnerException);
                        }
                    }
                    //}

                }

                //foreach (DataRow r in dt.Rows)
                //{ 
                //}
                FileExcel(dt, "ProductItem");
            }
            return RedirectToAction("Index", "ProductItem");
        }

        public JsonResult CheckShortCode(string shortCode)
        {
            var product = db.ProductItems.Where(p => p.ShortCode.Equals(shortCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            var data = new
            {
                Exist = product != null,
                EmployeeName = product != null && product.Employee != null ? product.Employee.Name : "",
                ProductId = product != null ? product.ProductId : 0,
                ProductItemID = product != null ? product.Id : 0,
                EmployeeId = product != null ? product.EmployeeId : 0,
                Serial = product != null ? product.SerialNo : "",
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Export(string search = "")
        {
            var pros = db.FilteredProductItems.AsQueryable();
            if (search != string.Empty)
            {
                pros = pros.Where(c => c.SerialNo.ToLower().Contains(search.ToLower()) || c.ShortCode.ToLower().Contains(search.ToLower()) || c.Name.ToLower().Contains(search.ToLower())
                    || c.DisplayName.ToLower().Contains(search.ToLower()) || c.UserName.ToLower().Contains(search.ToLower()) || c.ProductName.Contains(search.ToLower()));
            }
            FileExcel(queryToDataTable<FilteredProductItem>(pros.AsEnumerable()), "FilteredProductItem");
            return View("Index", "ProductItem");
        }
    }
}
