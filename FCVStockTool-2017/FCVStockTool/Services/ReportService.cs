using FCVStockTool.ReportService2005;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;

namespace FCVStockTool.Services
{
    public class ReportService
    {
        ReportingService2005 rs = new ReportingService2005();

        private CatalogItem[] _returnedItems;
        //NetworkCredential netCredential;
        //ReportCredentials repCredential;
        public ReportService()
        {
            rs.Credentials = CredentialCache.DefaultCredentials;
            rs.Url = WebConfigurationManager.AppSettings["FCVStockTool_ReportService2005_ReportingService2005"].ToString();
            rs.UnsafeAuthenticatedConnectionSharing = false;
            rs.PreAuthenticate = true;
            ReportService2005.Role[] roles = rs.ListRoles(SecurityScopeEnum.Catalog);

        }
        public ReportService(string url)
        {
            rs.Credentials = CredentialCache.DefaultCredentials;
            rs.Url = url;
            rs.UnsafeAuthenticatedConnectionSharing = false;
            rs.PreAuthenticate = true;
        }
        public List<CatalogItem> getSubFolder(string path)
        {
            List<CatalogItem> li = new List<CatalogItem>();
            string _path = path.Replace("//", "/");
            // Condition criteria.
            SearchCondition[] conditions;

            // Condition criteria.
            SearchCondition condition = new SearchCondition();
            condition.Condition = ConditionEnum.Contains;
            condition.ConditionSpecified = false;
            condition.Name = "Name";
            condition.Value = "";
            conditions = new SearchCondition[1];
            conditions[0] = condition;


            this._returnedItems = rs.FindItems(_path, BooleanOperatorEnum.Or, conditions);

            // Iterate thro' each report properties to get the path.
            foreach (CatalogItem item in _returnedItems)
            {
                if ((item.Type == ItemTypeEnum.Folder || item.Type == ItemTypeEnum.DataSource) && !item.Name.Equals("/"))
                {
                    if (_path.Length > 1 && item.Path.Substring(_path.Length - 1).LastIndexOf('/') != 0)
                    {
                        li.Add(item);
                    }
                    else
                    {
                        if (item.Path.Substring(_path.Length - 1).LastIndexOf('/') == 0)
                            li.Add(item);
                    }
                }
            }
            return li;
        }
        public void AssignRole(string role, string grouporusername, string pathFolder)
        {
            try
            {
                ReportService2005.Role[] roles = rs.ListRoles(SecurityScopeEnum.Catalog);
                ReportService2005.Role[] policyRoles = new ReportService2005.Role[1];
                pathFolder = pathFolder.Replace("//", "/");
                foreach (ReportService2005.Role item in roles)
                {
                    if (item.Name.Equals(role))
                    {
                        policyRoles[0] = item;
                        break;
                    }
                }
                bool inheritParent = false;
                Policy[] policis = rs.GetPolicies(pathFolder, out inheritParent);
                ArrayList li = new ArrayList(policis);
                Policy poli = new Policy();
                poli.GroupUserName = grouporusername;
                poli.Roles = policyRoles;
                li.Add(poli);
                rs.SetPolicies(pathFolder, (Policy[])li.ToArray(typeof(Policy)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UnAssignRole(string grouporusername, string pathFolder)
        {
            try
            {
                ReportService2005.Role[] roles = rs.ListRoles(SecurityScopeEnum.Catalog);
                ReportService2005.Role[] policyRoles = new ReportService2005.Role[1];
                pathFolder = pathFolder.Replace("//", "/");
                bool inheritParent = false;
                Policy[] policis = rs.GetPolicies(pathFolder, out inheritParent);
                ArrayList li = new ArrayList(policis);
                foreach (Policy item in policis)
                {
                    if (item.GroupUserName.Equals(grouporusername))
                    {
                        li.Remove(item);
                    }
                }
                rs.SetPolicies(pathFolder, (Policy[])li.ToArray(typeof(Policy)));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckExistUSer(string username, string folderPath)
        {
            bool inheritParent = false;
            Policy[] policis = rs.GetPolicies(folderPath, out inheritParent);
            foreach (Policy item in policis)
            {
                if (!username.Contains("\\"))
                {
                    if (item.GroupUserName.Substring(item.GroupUserName.LastIndexOf('\\') + 1).ToLower().Equals(username.ToLower()))
                    {
                        return true;
                    }
                }
                else
                {
                    if (item.GroupUserName.ToLower().Equals(username.ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public string DoAssign(string user)
        {
            try
            {
                string folder = WebConfigurationManager.AppSettings["ReportsRootPath"].ToString();
                string datasource = WebConfigurationManager.AppSettings["MyDataSource"].ToString();
                if (!CheckExistUSer(user, folder))
                    AssignRole("Content Manager", user, folder);
                if (!CheckExistUSer(user, folder + datasource))
                    AssignRole("Content Manager", user, folder + datasource);
                return "";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DoUnAssign(string user)
        {
            try
            {
                string folder = WebConfigurationManager.AppSettings["ReportsRootPath"].ToString();
                string datasource = WebConfigurationManager.AppSettings["MyDataSource"].ToString();
                if (CheckExistUSer(user, folder))
                    UnAssignRole(user, folder);
                if (CheckExistUSer(user, folder + datasource))
                    UnAssignRole(user, folder + datasource);
                return "";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public Policy[] GetUserGroup()
        {
            string path = WebConfigurationManager.AppSettings["ReportsRootPath"].ToString();
            bool inheritParent = false;
            Policy[] policis = rs.GetPolicies(path, out inheritParent);
            return policis;
        }
        public DataSourceDefinition createDataSourceDefinition(string connectString)
        {
            DataSourceDefinition definition = new DataSourceDefinition();
            definition.CredentialRetrieval = CredentialRetrievalEnum.Integrated;
            definition.ConnectString = connectString; //connection string
            definition.Enabled = true;
            definition.EnabledSpecified = true;
            definition.Extension = "SQL";//use Sql server
            definition.ImpersonateUserSpecified = true;// use Impersonator authentication
            //Use the default prompt string.
            definition.Prompt = null;
            definition.WindowsCredentials = true;//use window authentication
            return definition;
        }
        public string createFolder(string name, string parent)
        {
            try
            {
                //check exist folder
                if (CheckExist(ItemTypeEnum.Folder, parent, name))
                {
                    return String.Format("Folder {0} is already exist", name);
                }
                //create folder
                rs.CreateFolder(name, parent, null);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string createDataSource(string name, string parent, string conecstring, bool overWrite)
        {
            try
            {
                //check exist data source
                if (CheckExist(ItemTypeEnum.DataSource, parent, name) && !overWrite)
                    return String.Format("Datasource {0} is already exist", name);
                //create datasource definition
                DataSourceDefinition definition = createDataSourceDefinition(conecstring);
                //create data source
                rs.CreateDataSource(name, parent, overWrite, definition, null);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string deleteFolder(string name, string parent)
        {
            try
            {
                if (!CheckExist(ItemTypeEnum.Folder, parent, name))
                    return String.Format("Folder {0} is not exist in parent folder {1}!", name, parent);
                rs.DeleteItem("/" + parent + "/" + name);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string deleteDataSource(string name, string parent)
        {
            try
            {
                if (!CheckExist(ItemTypeEnum.DataSource, parent, name))
                    return String.Format("Datasource {0} is not exist in parent folder {1}!", name, parent);
                rs.DeleteItem("/" + parent + "/" + name);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public DataTable Get_List_Report(string condition, string orderColumn, string orderExpress, int selectedPage, int pageSize, out int totalRecords, out int totalPages)
        {
            totalPages = 0;
            totalRecords = 0;
            DataTable dt = new DataTable();
            DataRow dr = null;
            #region Create Columns in datatable
            dt.Columns.Add("id", Type.GetType("System.String"));
            dt.Columns.Add("reportname", Type.GetType("System.String"));
            dt.Columns.Add("reportpath", Type.GetType("System.String"));
            dt.Columns.Add("description", Type.GetType("System.String"));
            dt.Columns.Add("modifiedon", Type.GetType("System.DateTime"));
            dt.Columns.Add("createdon", Type.GetType("System.DateTime"));
            #endregion
            try
            {
                CatalogItem[] items = rs.ListChildren(WebConfigurationManager.AppSettings["ReportsRootPath"].ToString(), true);
                //where clause
                var query = (from r in items
                             where r.Type == ReportService2005.ItemTypeEnum.Report
                                 && (r.Name.ToLower().Contains(condition.ToLower().Trim())
                                 || r.Description.ToLower().Contains(condition.ToLower().Trim()))
                             select new CatalogItem { ID = r.ID, Name = r.Name, Path = r.Path, Description = r.Description, CreationDate = r.CreationDate, ModifiedDate = r.ModifiedDate });
                if (query.Any())
                {
                    //pagination info
                    totalRecords = query.Count<CatalogItem>();
                    totalPages = totalRecords / pageSize;
                    if ((totalRecords % pageSize) > 0) totalPages++;
                    //order clause
                    switch (orderColumn.Trim().ToLower())
                    {
                        case "reportname":
                            if (orderExpress.Trim().Equals("asc", StringComparison.OrdinalIgnoreCase)) query = query.OrderBy(r => r.Name);
                            else query = query.OrderByDescending(r => r.Name);
                            break;
                        case "reportpath":
                            if (orderExpress.Trim().Equals("asc", StringComparison.OrdinalIgnoreCase)) query = query.OrderBy(r => r.Path);
                            else query = query.OrderByDescending(r => r.Path);
                            break;
                        case "description":
                            if (orderExpress.Trim().Equals("asc", StringComparison.OrdinalIgnoreCase)) query = query.OrderBy(r => r.Description);
                            else query = query.OrderByDescending(r => r.Description);
                            break;
                        case "modifiedon":
                            if (orderExpress.Trim().Equals("asc", StringComparison.OrdinalIgnoreCase)) query = query.OrderBy(r => r.ModifiedDate);
                            else query = query.OrderByDescending(r => r.ModifiedDate);
                            break;
                        case "createdon":
                            if (orderExpress.Trim().Equals("asc", StringComparison.OrdinalIgnoreCase)) query = query.OrderBy(r => r.CreationDate);
                            else query = query.OrderByDescending(r => r.CreationDate);
                            break;
                        default:
                            if (orderExpress.Trim().Equals("asc", StringComparison.OrdinalIgnoreCase)) query = query.OrderBy(r => r.Name);
                            else query = query.OrderByDescending(r => r.Name);
                            break;
                    }
                    //pagination
                    query = query.Skip(selectedPage - 1).Take(pageSize);
                    if (query.Any())
                    {
                        //if (query.)
                        foreach (CatalogItem item in query.ToArray<CatalogItem>())
                        {
                            /// Create new row
                            dr = dt.NewRow();
                            dr["id"] = item.ID;
                            dr["reportname"] = item.Name;
                            //dr["reportpath"] = item.Path;
                            dr["description"] = item.Description;
                            dr["createdon"] = item.CreationDate;
                            dr["modifiedon"] = item.ModifiedDate;
                            /// Add row to table
                            dt.Rows.Add(dr);
                        }
                    }
                }
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        public void ViewReport(ReportViewer rv, string reportID)
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            rv.ProcessingMode = ProcessingMode.Remote;
            rv.ServerReport.ReportServerUrl = new Uri((WebConfigurationManager.AppSettings["MyReportServerUrl"].ToString()));
            string reportPath = GetReportPath(reportID);
            if (reportPath != string.Empty)
                rv.ServerReport.ReportPath = reportPath;
            rv.ServerReport.Refresh();
        }

        public void ViewReport_Name(ReportViewer rv, string reportID)
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            rv.ProcessingMode = ProcessingMode.Remote;
            rv.ServerReport.ReportServerUrl = new Uri((WebConfigurationManager.AppSettings["MyReportServerUrl"].ToString()));
            //string reportPath = GetReportPath(reportID);
            //if (reportPath != string.Empty)
            rv.ServerReport.ReportPath = reportID;
            rv.ServerReport.Refresh();
        }
        public string SaveReport(string report, Stream file, string name, bool overwrite, ref Guid id)
        {
            id = Guid.Empty;
            string _report = report.ToLower().Replace(".rdl", "");
            if (this.CheckExist(ItemTypeEnum.Report, WebConfigurationManager.AppSettings["ReportsRootPath"].ToString(), (name == "" ? _report : name)) == true && overwrite == false)
            {
                return "The Report '" + (name == "" ? _report : name) + "' already exists";
            }
            Byte[] definition = null;
            try
            {

                System.IO.BinaryReader br = new System.IO.BinaryReader(file);
                definition = br.ReadBytes((Int32)file.Length);
                //Create Report
                rs.CreateReport((name == string.Empty ? _report : name), WebConfigurationManager.AppSettings["ReportsRootPath"].ToString(), overwrite, definition, null);
                //Set DataSource
                DataSourceReference Item1 = new DataSourceReference();

                DataSource[] datasources = rs.GetItemDataSources(WebConfigurationManager.AppSettings["ReportsRootPath"].ToString() + "/" + (name == "" ? _report : name));



                Item1.Reference = WebConfigurationManager.AppSettings["ReportsRootPath"].ToString() + WebConfigurationManager.AppSettings["MyDataSource"].ToString();

                datasources[0].Item = Item1;

                rs.SetItemDataSources(WebConfigurationManager.AppSettings["ReportsRootPath"].ToString() + "/" + (name == "" ? _report : name), datasources);

                string _id = GetReportID(WebConfigurationManager.AppSettings["ReportsRootPath"].ToString() + "/" + (name == "" ? _report : name));
                id = _id != string.Empty ? new Guid(_id) : Guid.Empty;

                return string.Format("Report: {0} created successfully", name == "" ? _report : name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
        public string DeleteReport(string report, string name)
        {
            rs.Url = WebConfigurationManager.AppSettings["FCVStockTool_ReportService2005_ReportingService2005"].ToString();
            try
            {
                rs.DeleteItem(report);
                return string.Format("Report: {0} deleted successfully", name);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// Checks if the folder exists or not.
        /// <param name="path">Parent folder path</param>
        /// <param name="folderName">Name of the folder to search</param>
        /// <returns>True if found, else false.</returns>
        private bool CheckExist(ItemTypeEnum type, string path, string folderName)
        {
            string _path = path + "/" + folderName;
            _path = _path.Replace("//", "/");
            // Condition criteria.
            SearchCondition[] conditions;

            // Condition criteria.
            SearchCondition condition = new SearchCondition();
            condition.Condition = ConditionEnum.Contains;
            condition.ConditionSpecified = true;
            condition.Name = "Name";
            condition.Value = "";
            conditions = new SearchCondition[1];
            conditions[0] = condition;

            this._returnedItems = rs.FindItems(path, BooleanOperatorEnum.Or, conditions);

            // Iterate thro' each report properties to get the path.
            foreach (CatalogItem item in _returnedItems)
            {
                if (item.Type == type)
                {
                    if (item.Path == _path)

                        return true;
                }
            }
            return false;
        }

        public string GetReportPath(string id)
        {
            try
            {
                CatalogItem[] items = rs.ListChildren(WebConfigurationManager.AppSettings["ReportsRootPath"].ToString(), true);
                CatalogItem item = (from r in items where r.ID.Equals(id, StringComparison.OrdinalIgnoreCase) select r).FirstOrDefault<CatalogItem>();
                if (item != null) return item.Path; else return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string GetReportID(string Path)
        {
            try
            {
                CatalogItem[] items = rs.ListChildren(WebConfigurationManager.AppSettings["ReportsRootPath"].ToString(), true);
                CatalogItem item = (from r in items where r.Path.Equals(Path, StringComparison.OrdinalIgnoreCase) select r).FirstOrDefault<CatalogItem>();
                if (item != null) return item.ID; else return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string DeleteReport(string id)
        {
            rs.Url = WebConfigurationManager.AppSettings["FCVStockTool_ReportService2005_ReportingService2005"].ToString();
            try
            {
                rs.DeleteItem(GetReportPath(id));
                return string.Format("Report: deleted successfully");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}