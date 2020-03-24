using FCV_DutchLadyMail_Model.Attribute;
using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;

namespace FCV_DutchLadyMail.Controllers
{
    [Auth]
    public class PermissionController : BaseController
    {
        private MassMailsDbContext database = new MassMailsDbContext();
        // GET: Administrator/Permission
        public ActionResult Index(int? page)
        {
            int pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<PERMISSION> _permissions = database.PERMISSIONS
                              .OrderBy(wn => wn.PermissionDescription)
                              .Include(a => a.ROLES)
                              .ToList();
            return View(_permissions.ToPagedList(pageIndex, pageSize));
        }
        public ViewResult Details(int id)
        {
            PERMISSION _permission = database.PERMISSIONS.Find(id);
            return View(_permission);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PERMISSION _permission)
        {
            if (_permission.PermissionDescription == null)
            {
                ModelState.AddModelError("Permission Description", "Permission Description must be entered");
            }

            if (ModelState.IsValid)
            {
                database.PERMISSIONS.Add(_permission);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_permission);
        }
        public ActionResult Edit(int id)
        {
            PERMISSION _permission = database.PERMISSIONS.Find(id);
            ViewBag.RoleId = new SelectList(database.ROLES.OrderBy(p => p.RoleDescription), "Role_Id", "RoleDescription");
            return View(_permission);
        }

        [HttpPost]
        public ActionResult Edit(PERMISSION _permission)
        {
            if (ModelState.IsValid)
            {
                database.Entry(_permission).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Details", new RouteValueDictionary(new { id = _permission.Permission_Id }));
            }
            return View(_permission);
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Delete(int id)
        {
            PERMISSION permission = database.PERMISSIONS.Find(id);
            if (permission.ROLES.Count > 0)
                permission.ROLES.Clear();

            database.Entry(permission).State = EntityState.Deleted;
            database.SaveChanges();
            return RedirectToAction("Index");
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

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult DeleteRoleFromPermissionReturnPartialView(int id, int permissionId)
        {
            ROLE role = database.ROLES.Find(id);
            PERMISSION permission = database.PERMISSIONS.Find(permissionId);

            if (role.PERMISSIONS.Contains(permission))
            {
                role.PERMISSIONS.Remove(permission);
                database.SaveChanges();
            }
            return PartialView("_ListRolesTable4Permission", permission);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult AddRole2PermissionReturnPartialView(int permissionId, int roleId)
        {
            ROLE role = database.ROLES.Find(roleId);
            PERMISSION _permission = database.PERMISSIONS.Find(permissionId);

            if (!role.PERMISSIONS.Contains(_permission))
            {
                role.PERMISSIONS.Add(_permission);
                database.SaveChanges();
            }
            return PartialView("_ListRolesTable4Permission", _permission);
        }
        public ActionResult Import()
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
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}