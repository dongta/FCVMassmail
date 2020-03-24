using FCV_DutchLadyMail_Model.Common;
using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Data.Entity;
using FCV_DutchLadyMail_Model.Attribute;

namespace FCV_DutchLadyMail.Controllers
{
    public class FileController : BaseController
    {
        private MassMailsDbContext database = new MassMailsDbContext();
        // GET: File
        public ActionResult Index(int? page)
        {
            var folder = database.Folders.Where(ta => ta.Cate != 2).ToList();
            ViewBag.Folders = folder;
            int pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<FILE> _files = database.FILES
                              .OrderByDescending(wn => wn.created_at)
                              .Include(a => a.USER)
                              .ToList();
            return View(_files.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult GetListFile(string test)
        {
            int id = Convert.ToInt32(test);
            var temp = database.FILES.Where(ta=>ta.idFolders == id).OrderBy(ta=>ta.path).ToList();
            // some code
            return PartialView("_getListFileByFolders", temp);
        }
        public ActionResult Manager()
        {
            return View();
        }

        public ActionResult UploadFiles(string id)
        {
            if(id==null)
            { 
                var folder = database.Folders.Where(ta=>ta.Cate != 2).ToList();
                ViewBag.Folders = folder;
            }
            else
            {
                int idF = Convert.ToInt32(id);
                var _folder = database.Folders.Where(ta => ta.ID == idF).SingleOrDefault();
                var folder = database.Folders.Where(ta => ta.Cate != 2).ToList();
                ViewBag.Folders = new SelectList(folder, "ID", "Name", _folder);
                ViewBag.FoldersSelected = _folder.ID;
            }
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(FileModel _model, string Folders)
        {
            
            var Identity = (IdentityUser)Session["Identity"];
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase file in _model.files)
                {
                    if (file != null)
                    {
                        int _idFolder = Convert.ToInt32(Folders);
                        Folders _folder = database.Folders.Where(ta => ta.ID == _idFolder).SingleOrDefault();
                        _model.folder = _folder.Name;
                        var InputFileName = Path.GetFileName(file.FileName);
                        string path = _model.folder + "/";
                        bool exists = System.IO.Directory.Exists(Server.MapPath(path));
                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(path));
                        var ServerSavePath = Path.Combine(Server.MapPath(path) + InputFileName);
                        file.SaveAs(ServerSavePath);
                        string p = path + InputFileName;
                        FILE _file = database.FILES.Where(f => f.path.Equals(p)).FirstOrDefault();
                        if (_file != null)
                        {
                            _file.updated_at = System.DateTime.Now;
                            database.SaveChanges();
                        }
                        else
                        {
                            //Folders last_item = database.Folders.Where(ta=>ta.Cate !=2).OrderByDescending(ta => ta.ID).Take(1).Single();
                            int idFolder = Convert.ToInt32(Folders);
                            //Folders _folder = database.Folders.Where(ta => ta.ID == idFolder).SingleOrDefault();
                            FILE _f = new FILE();
                            _f.path = InputFileName;
                            _f.userId = Identity.UserId;
                            _f.created_at = System.DateTime.Now;
                            _f.idFolders = idFolder;
                            database.FILES.Add(_f);


                            database.SaveChanges();
                        }
                        ViewBag.UploadStatus = _model.files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Delete(int id)
        {
            FILE file = database.FILES.Find(id);
            database.Entry(file).State = EntityState.Deleted;
            database.SaveChanges();
            if (System.IO.File.Exists(Server.MapPath(file.path)))
            {
                System.IO.File.Delete(Server.MapPath(file.path));
            }
            return RedirectToAction("Index");
        }
    }
}
