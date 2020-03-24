using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.DirectoryServices;
using System.Data;
using System.Security.Principal;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace FCVStockTool.Utils
{
    public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;

        public LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            if (username.Contains(@"\")) domainAndUsername = username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (COMException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return true;
        }

        public ImpersonatorUser Finduser(string domainAndUsername)
        {
            ImpersonatorUser user = null;
            //try
            //{

                user = new ImpersonatorUser();
                using (HostingEnvironment.Impersonate())
                {
                    DirectoryEntry entry = new DirectoryEntry(_path);
                    //Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + domainAndUsername + "))";
                    //search.PropertiesToLoad.Add("cn");               
                    SearchResult results = search.FindOne();
                    if (results != null)
                    {
                        user.GUID = results.GetDirectoryEntry().Guid;
                        //row["SID"] = GetProperty(results, "sAMAccountName");
                        user.Username = GetProperty(results, "sAMAccountName");
                        user.DisplayName = GetProperty(results, "cn");
                        if (results.Path.IndexOf("OU=") > 0)
                        {
                            string temp = results.Path.Substring(results.Path.IndexOf("OU=") + 3);
                            user.OU = temp.Substring(0, temp.IndexOf(",")); ;
                        }
                        user.Department = GetProperty(results, "department");
                        user.Title = GetProperty(results, "title");
                        user.Email = GetProperty(results, "mail");
                        user.Phone = GetProperty(results, "mobile");
                        user.Address = GetProperty(results, "homePostalAddress");
                        user.Pager = GetProperty(results, "pager");
                        user.Role = GetGroups(results.GetDirectoryEntry().Path, GetProperty(results, "cn"));
                    }
                    else
                        return null;
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            //}
            return user;
        }

        public DataTable Findusers(string domainAndUsername)
        {
            string ee = "";
            DataTable dbActiveUser = new DataTable();
            dbActiveUser.Columns.Add("GUID");
            dbActiveUser.Columns.Add("SID");
            dbActiveUser.Columns.Add("userName");
            dbActiveUser.Columns.Add("DisplayName");
            dbActiveUser.Columns.Add("OU");
            dbActiveUser.Columns.Add("Department");
            dbActiveUser.Columns.Add("Title");
            dbActiveUser.Columns.Add("Email");
            dbActiveUser.Columns.Add("Phone");
            dbActiveUser.Columns.Add("Address");
            dbActiveUser.Columns.Add("Role");
            try
            {
                using (HostingEnvironment.Impersonate())
                {
                
                    DirectoryEntry entry = new DirectoryEntry(_path);
                    //Bind to the native AdsObject to force authentication.
                    if (entry == null) ee = "null";
                    string obj = entry.NativeGuid;
                   DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(&(objectCategory=user)(objectClass=user)(sAMAccountName=*" + domainAndUsername + "*))";
                    search.SearchScope = SearchScope.Subtree;
                    SearchResultCollection resultSets = search.FindAll();
                    foreach (SearchResult results in resultSets)
                    {
                        DataRow row = dbActiveUser.NewRow();
                        row["GUID"] = results.GetDirectoryEntry().Guid.ToString();
                        //row["SID"] = GetProperty(results, "sAMAccountName");
                        row["userName"] = GetProperty(results, "sAMAccountName");
                        row["DisplayName"] = GetProperty(results, "cn");
                        if (results.Path.IndexOf("OU=") > 0)
                        {
                            string temp = results.Path.Substring(results.Path.IndexOf("OU=") + 3);
                            row["OU"] = temp.Substring(0, temp.IndexOf(",")); ;
                        }
                        row["Department"] = GetProperty(results, "department");
                        row["Title"] = GetProperty(results, "title");
                        row["Email"] = GetProperty(results, "mail");
                        row["Phone"] = GetProperty(results, "mobile");
                        row["Address"] = GetProperty(results, "homePostalAddress");
                        row["Role"] = GetGroups(results.GetDirectoryEntry().Path, GetProperty(results, "cn"));
                        dbActiveUser.Rows.Add(row);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + _path + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return dbActiveUser;
        }

        public DataTable FindAllFCVADUsers()
        {
            string ee = "";
            DataTable dbActiveUser = new DataTable();
            //dbActiveUser.Columns.Add("GUID");
            //dbActiveUser.Columns.Add("SID");
            dbActiveUser.Columns.Add("userName");
            dbActiveUser.Columns.Add("DisplayName");
            //dbActiveUser.Columns.Add("OU");
            dbActiveUser.Columns.Add("Department");
            dbActiveUser.Columns.Add("Title");
            //dbActiveUser.Columns.Add("Email");
            //dbActiveUser.Columns.Add("Phone");
            //dbActiveUser.Columns.Add("Address");
            //dbActiveUser.Columns.Add("Role");
            dbActiveUser.Columns.Add("Pager");
            try
            {
                using (HostingEnvironment.Impersonate())
                {

                    DirectoryEntry entry = new DirectoryEntry("LDAP://ou=friesland foods dutch lady vietnam,dc=domaina,dc=int,dc=net");
                    //DirectoryEntry entry = new DirectoryEntry("LDAP://domaina");
                    //Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(&(objectCategory=person)(objectClass=user))";
                    search.PageSize = 500000;
                    //search.SearchScope = SearchScope.Subtree;
                    SearchResultCollection resultSets = search.FindAll();
                    foreach (SearchResult results in resultSets)
                    {
                        DataRow row = dbActiveUser.NewRow();
                        //row["GUID"] = results.GetDirectoryEntry().Guid.ToString();
                        //row["SID"] = GetProperty(results, "sAMAccountName");
                        row["userName"] = GetProperty(results, "sAMAccountName");
                        row["DisplayName"] = GetProperty(results, "cn");
                        //if (results.Path.IndexOf("OU=") > 0)
                        //{
                        //    string temp = results.Path.Substring(results.Path.IndexOf("OU=") + 3);
                        //    row["OU"] = temp.Substring(0, temp.IndexOf(",")); ;
                        //}
                        row["Department"] = GetProperty(results, "department");
                        row["Title"] = GetProperty(results, "title");
                        //row["Email"] = GetProperty(results, "mail");
                        //row["Phone"] = GetProperty(results, "mobile");
                        //row["Address"] = GetProperty(results, "homePostalAddress");
                        row["Pager"] = GetProperty(results, "pager");
                        //row["Role"] = GetGroups(results.GetDirectoryEntry().Path, GetProperty(results, "cn"));
                        dbActiveUser.Rows.Add(row);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + _path + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return dbActiveUser;
        }

        public string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return groupNames.ToString();
        }

        public string GetGroups(string path, string filterAttribute)
        {
            DirectorySearcher search = new DirectorySearcher(path);
            search.Filter = "(cn=" + filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return groupNames.ToString();
        }

        public void ChangePassword(string domain, string username, string opwd, string npwd)
        {
            try
            {
                string domainAndUsername = domain + @"\" + username;
                if (username.Contains(@"\")) domainAndUsername = username;
                using (DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, opwd, AuthenticationTypes.Secure))
                {
                    //Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;
                    //invoke change password
                    //entry.Invoke("SetPassword", new object[] { npwd });
                    entry.Invoke("ChangePassword", new object[] { opwd, npwd });
                    entry.Properties["LockOutTime"].Value = 0;
                    entry.Close();
                    //entry.CommitChanges();
                }
            }
            catch (COMException ex)
            {
                throw new Exception("COMError changepassword user. " + ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error changepassword user. " + ex.ToString());
            }
        }

        public void ResetPassword(string adomain, string adminsUser, string adminPass, string targetUsername, string targetPass)
        {
            try
            {
                Impersonation im = new Impersonation();
                if (im.impersonateValidUser(adminsUser, adomain, adminPass))
                {
                    string domainAndUsername = adomain + @"\" + adminsUser;
                    if (adminsUser.Contains(@"\")) domainAndUsername = adminsUser;
                    using (DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, adminPass, AuthenticationTypes.Secure))
                    {
                        var userEntry = new DirectorySearcher(entry) { SearchRoot = entry, Filter = "(&(objectCategory=user)(cn=" + targetUsername + "))" };
                        //Bind to the native AdsObject to force authentication.
                        object obj = entry.NativeObject;
                        //invoke change password
                        entry.Invoke("SetPassword", new object[] { targetPass });
                        //entry.Invoke("ChangePassword", new object[] { opwd, npwd });
                        entry.Properties["LockOutTime"].Value = 0;
                        entry.Close();
                        //entry.CommitChanges();
                    }
                    im.undoImpersonation();
                }
            }
            catch (COMException ex)
            {
                throw new Exception("COMError resetpassword user. " + ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error resetpassword user. " + ex.ToString());
            }
        }
    }

    public class WinNTAuthentication
    {
        public enum LogonType : int
        {
            Interactive = 2, //đăng nhập đc từ domain và local
            Network = 3,//đăng nhập đc từ domain và local
            Batch = 4, //Chỉ đăng nhập tài khoản từ máy local đó
            Service = 5,
            Unlock = 7,
            NetworkClearText = 8,
            NewCredentials = 9,
        }
        public enum LogonProvider : int
        {
            Default = 0,
        }

        #region Native Code
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool LogonUser(string userName, string domain, string password, LogonType logonType, LogonProvider logonProvider, out IntPtr userToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr duplication);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool ImpersonateLoggedOnUser(IntPtr userToken);
        #endregion
        private string _servername;
        private string _path;

        public WinNTAuthentication(string servername)
        {
            this._servername = servername;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            if (username.Contains(@"\"))
                domainAndUsername = username;
            bool _flat = false;
            try
            {
                IntPtr userToken = IntPtr.Zero;
                _flat = LogonUser(username, domain, pwd, LogonType.Interactive, LogonProvider.Default, out userToken);
                //DirectoryEntry _entry = new DirectoryEntry("WinNT://" + _servername, username, pwd, AuthenticationTypes.Signing);
                ////Bind to the native AdsObject to force authentication.
                //_entry.Children.SchemaFilter.Add("User");
                //if (username.Contains("\\"))
                //    username = username.Substring(username.LastIndexOf('\\') + 1);
                //foreach (DirectoryEntry _child in _entry.Children)
                //{
                //    if (_child.Name.Equals(username, StringComparison.OrdinalIgnoreCase))
                //    {
                //        _flat = true;
                //        break;
                //    }
                //}
            }
            catch (COMException)
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return _flat;
        }

        public ImpersonatorUser FindUser(string username)
        {
            ImpersonatorUser user = new ImpersonatorUser();
            try
            {
                DirectoryEntry _entry = new DirectoryEntry("WinNT://" + _servername + ",computer");
                _entry.Children.SchemaFilter.Add("User");
                if (username.Contains("\\"))
                    username = username.Substring(username.LastIndexOf('\\') + 1);
                foreach (DirectoryEntry _child in _entry.Children)
                {
                    if (_child.Name.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        SecurityIdentifier sid = new SecurityIdentifier((byte[])_child.Properties["objectSID"].Value, 0);
                        user.GUID = _child.Guid;
                        user.SID = sid.ToString();
                        user.Username = _child.Name;
                        user.DisplayName = _child.Properties["fullname"].Value.ToString();

                        object obGroups = _child.Invoke("Groups");
                        foreach (object ob in (IEnumerable)obGroups)
                        {
                            // emumerate through groups
                            DirectoryEntry obGpEntry = new DirectoryEntry(ob);
                            user.Role += string.Format("{0}|", obGpEntry.Properties["Name"].Value);
                        }
                        if (user.Role != string.Empty)
                            user.Role = user.Role.Remove(user.Role.LastIndexOf('|'));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return user;
        }

        public DataTable Findusers(string username)
        {
            DataTable dbActiveUser = new DataTable();
            dbActiveUser.Columns.Add("GUID");
            dbActiveUser.Columns.Add("SID");
            dbActiveUser.Columns.Add("userName");
            dbActiveUser.Columns.Add("DisplayName");
            dbActiveUser.Columns.Add("OU");
            dbActiveUser.Columns.Add("Department");
            dbActiveUser.Columns.Add("Title");
            dbActiveUser.Columns.Add("Email");
            dbActiveUser.Columns.Add("Phone");
            dbActiveUser.Columns.Add("Address");
            dbActiveUser.Columns.Add("Role");
            try
            {
                //using (HostingEnvironment.Impersonate())
                //{
                DirectoryEntry _entry = new DirectoryEntry("WinNT://" + _servername + ",computer");
                _entry.Children.SchemaFilter.Add("User");
                //
                if (username.Contains("\\"))
                    username = username.Substring(username.LastIndexOf('\\') + 1);
                foreach (DirectoryEntry _child in _entry.Children)
                {
                    if (_child.Name.ToLower().Contains(username.ToLower()))
                    {
                        DataRow row = dbActiveUser.NewRow();
                        SecurityIdentifier sid = new SecurityIdentifier((byte[])_child.Properties["objectSID"].Value, 0);
                        row["GUID"] = _child.Guid.ToString(); ;
                        row["SID"] = sid.ToString();
                        row["userName"] = _child.Name;
                        row["DisplayName"] = _child.Properties["fullname"].Value.ToString();

                        object obGroups = _child.Invoke("Groups");
                        foreach (object ob in (IEnumerable)obGroups)
                        {
                            // emumerate through groups
                            DirectoryEntry obGpEntry = new DirectoryEntry(ob);
                            row["Role"] += string.Format("{0}|", obGpEntry.Properties["Name"].Value);
                        }
                        if (row["Role"] != string.Empty)
                            row["Role"] = row["Role"].ToString().Remove(row["Role"].ToString().LastIndexOf('|'));
                        dbActiveUser.Rows.Add(row);
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
            }
            return dbActiveUser;
        }

        public void ChangePassword(string domain, string username, string opwd, string npwd)
        {
            try
            {
                string domainAndUsername = domain + @"\" + username;
                if (username.Contains(@"\")) domainAndUsername = username;
                //using (DirectoryEntry entry = new DirectoryEntry("WinNT://" + _servername + ",user", domainAndUsername, opwd, AuthenticationTypes.Secure))
                using (DirectoryEntry entry = new DirectoryEntry("WinNT://" + _servername + "/" + username))
                {
                    //Bind to the native AdsObject to force authentication.
                    //object obj = entry.NativeObject;
                    //invoke change password
                    //entry.Invoke("SetPassword", new object[] { npwd });
                    entry.Invoke("ChangePassword", new object[] { opwd, npwd });
                    //entry.Properties["LockOutTime"].Value = 0;
                    entry.Close();
                    //entry.CommitChanges();
                }
            }
            catch (COMException ex)
            {
                throw new Exception("COMError changepassword user. " + ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error changepassword user. " + ex.ToString());
            }
        }

        public void ResetPassword(string aUsername, string aPassword, string tarUsername, string tarNpwd)
        {
            try
            {
                
                Impersonation im = new Impersonation();
                if (im.impersonateValidUser(aUsername, _servername, aPassword))
                {
                    using (DirectoryEntry entry = new DirectoryEntry("WinNT://" + _servername + ",computer"))
                    {
                        entry.Children.SchemaFilter.Add("User");
                        if (tarUsername.Contains("\\"))
                            tarUsername = tarUsername.Substring(tarUsername.LastIndexOf('\\') + 1);
                        foreach (DirectoryEntry _child in entry.Children)
                        {
                            if (_child.Name.Equals(tarUsername, StringComparison.OrdinalIgnoreCase))
                            {
                                //Bind to the native AdsObject to force authentication.
                                //object obj = entry.NativeObject;
                                //invoke change password
                                _child.Invoke("SetPassword", new object[] { tarNpwd });
                                //entry.Invoke("ChangePassword", new object[] { opwd, npwd });
                                //entry.Properties["LockOutTime"].Value = 0;
                                _child.CommitChanges();
                                _child.Close();
                            }
                        }
                    }
                    im.undoImpersonation();
                }
            }
            catch (COMException ex)
            {
                throw new Exception("COMError resetpassword user. " + ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error resetpassword user. " + ex.ToString());
            }
        }
    }

    //public class FormAuthentication {
    //    public FormAuthentication()
    //    { 
    //    }

    //    public static bool IsAuthenticated(string username, string password)
    //    {
    //        bool _flat = false;
    //        try
    //        {
    //            _flat = DAOUser.VerifyLogin(username, password);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error authenticating user. Message {" + ex.Message + "} - Inner exception {" + ex.InnerException + "}");
    //        }
    //        return _flat;
    //    }
    //}

    public class ImpersonatorUser
    {
        public Guid GUID { get; set; }
        public string SID { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string OU { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Pager { get; set; }
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class Impersonation
    {
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        public bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        public void undoImpersonation()
        {
            impersonationContext.Undo();
        }
    }
}
