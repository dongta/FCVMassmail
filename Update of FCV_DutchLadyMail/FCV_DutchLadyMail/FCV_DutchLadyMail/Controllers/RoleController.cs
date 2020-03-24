using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Helpers;
using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FCV_DutchLadyMail.Controllers
{
    [Auth]
    public class RoleController : BaseController
    {
        private MassMailsDbContext database = new MassMailsDbContext();
        // GET: Administrator/Role
        public ActionResult Index()
        {
            return View(database.ROLES.OrderBy(r => r.RoleName).ToList());
        }
        public ActionResult Create()
        {
            USER user = database.USERS.Where(r => r.User_Id == _Identity.UserId).FirstOrDefault();
            ViewBag.List_boolNullYesNo = StringHelper.List_boolNullYesNo();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ROLE _role)
        {
            if (_role.RoleDescription == null)
            {
                ModelState.AddModelError("Role Description", "Role Description must be entered");
            }
            USER user = database.USERS.Where(r => r.User_Id == _Identity.UserId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                database.ROLES.Add(_role);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.List_boolNullYesNo = StringHelper.List_boolNullYesNo();
            return View(_role);
        }

        public ActionResult Edit(int id)
        {
            USER user = database.USERS.Where(r => r.User_Id == _Identity.UserId).FirstOrDefault();

            ROLE _role = database.ROLES.Where(r => r.Role_Id == id)
                    .Include(a => a.PERMISSIONS)
                    .Include(a => a.USERS)
                    .FirstOrDefault();

            ViewBag.UserId = new SelectList(database.USERS, "User_Id", "Username");
            ViewBag.RoleId = id;

            ViewBag.PermissionId = new SelectList(database.PERMISSIONS.OrderBy(a => a.Permission_Id), "Permission_Id", "PermissionDescription");
            ViewBag.Role_Permission = new SelectList(_role.PERMISSIONS.OrderBy(a => a.Permission_Id), "Permission_Id", "PermissionDescription");
            ViewBag.List_boolNullYesNo = StringHelper.List_boolNullYesNo();

            return View(_role);
        }

        [HttpPost]
        public ActionResult Edit(ROLE _role)
        {
            if (string.IsNullOrEmpty(_role.RoleDescription))
            {
                ModelState.AddModelError("Role Description", "Role Description must be entered");
            }

            //EntityState state = database.Entry(_role).State;
            USER user = database.USERS.Where(r => r.User_Id == _Identity.UserId).FirstOrDefault();
            if (ModelState.IsValid)
            {

                database.Entry(_role).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Details", new RouteValueDictionary(new { id = _role.Role_Id }));
            }
            // USERS combo
            ViewBag.UserId = new SelectList(database.USERS, "User_Id", "Username");

            // Rights combo
            ViewBag.PermissionId = new SelectList(database.PERMISSIONS.OrderBy(a => a.Permission_Id), "Permission_Id", "PermissionDescription");
            ViewBag.List_boolNullYesNo = StringHelper.List_boolNullYesNo();
            return View(_role);
        }

        public ActionResult Delete(int id)
        {
            ROLE _role = database.ROLES.Find(id);
            if (_role != null)
            {
                _role.USERS.Clear();
                _role.PERMISSIONS.Clear();

                database.Entry(_role).State = EntityState.Deleted;
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ViewResult Details(int id)
        {
            USER user = database.USERS.Where(r => r.User_Id == _Identity.UserId).FirstOrDefault();
            ROLE role = database.ROLES.Where(r => r.Role_Id == id)
                   .Include(a => a.PERMISSIONS)
                   .Include(a => a.USERS)
                   .FirstOrDefault();

            // USERS combo
            ViewBag.UserId = new SelectList(database.USERS, "Id", "Username");
            ViewBag.RoleId = id;

            // Rights combo
            ViewBag.PermissionId = new SelectList(database.PERMISSIONS.OrderBy(a => a.PermissionDescription), "Permission_Id", "PermissionDescription");
            ViewBag.List_boolNullYesNo = StringHelper.List_boolNullYesNo();

            return View(role);
        }

        public ActionResult Import()
        {
            try
            {
                var _controllerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t != null
                    && t.IsPublic
                    && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                    && !t.IsAbstract
                    && typeof(IController).IsAssignableFrom(t));

                var _controllerMethods = _controllerTypes.ToDictionary(controllerType => controllerType,
                        controllerType => controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)));

                foreach (var _controller in _controllerMethods)
                {
                    string _controllerName = _controller.Key.Name;
                    foreach (var _controllerAction in _controller.Value)
                    {
                        string _controllerActionName = _controllerAction.Name;
                        if (_controllerName.EndsWith("Controller"))
                        {
                            _controllerName = _controllerName.Substring(0, _controllerName.LastIndexOf("Controller"));
                        }

                        string _roleName = _controllerName + " Manager";

                        string _permissionName = string.Format("{0}-{1}", _controllerName, _controllerActionName);
                        PERMISSION _permission = database.PERMISSIONS.Where(p => p.PermissionDescription == _permissionName).FirstOrDefault();
                        if (_permission == null)
                        {
                            if (ModelState.IsValid)
                            {
                                PERMISSION _perm = new PERMISSION();
                                _perm.PermissionDescription = _permissionName;
                                _perm.PermissionName = _permissionName;

                                database.PERMISSIONS.Add(_perm);
                                database.SaveChanges();
                                _permission = _perm;
                            }
                        }

                        ROLE _role = database.ROLES.Where(r => r.RoleName == _roleName).FirstOrDefault();
                        if (_role == null)
                        {
                            if (ModelState.IsValid)
                            {
                                ROLE __role = new ROLE();
                                __role.RoleName = _roleName;
                                __role.RoleDescription = _roleName;

                                database.ROLES.Add(__role);
                                database.SaveChanges();
                                _role = __role;
                            }
                        }
                        if (!_role.PERMISSIONS.Contains(_permission))
                        {
                            _role.PERMISSIONS.Add(_permission);
                            database.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddPermission2RoleReturnPartialView(int id, int permissionId)
        {
            ROLE role = database.ROLES.Find(id);
            PERMISSION _permission = database.PERMISSIONS.Find(permissionId);

            if (!role.PERMISSIONS.Contains(_permission))
            {
                role.PERMISSIONS.Add(_permission);
                database.SaveChanges();
            }
            return PartialView("_ListPermissions", role);
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddAllPermissions2RoleReturnPartialView(int id)
        {
            ROLE _role = database.ROLES.Where(p => p.Role_Id == id).FirstOrDefault();
            List<PERMISSION> _permissions = database.PERMISSIONS.ToList();
            foreach (PERMISSION _permission in _permissions)
            {
                if (!_role.PERMISSIONS.Contains(_permission))
                {
                    _role.PERMISSIONS.Add(_permission);

                }
            }
            database.SaveChanges();
            return PartialView("_ListPermissions", _role);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeletePermissionFromRoleReturnPartialView(int id, int permissionId)
        {
            ROLE _role = database.ROLES.Find(id);
            PERMISSION _permission = database.PERMISSIONS.Find(permissionId);

            if (_role.PERMISSIONS.Contains(_permission))
            {
                _role.PERMISSIONS.Remove(_permission);
                database.SaveChanges();
            }
            return PartialView("_ListPermissions", _role);
        }
       
    }
}