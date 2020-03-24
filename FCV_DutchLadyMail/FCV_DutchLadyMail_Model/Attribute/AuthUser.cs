using FCV_DutchLadyMail_Model.Helpers;
using FCV_DutchLadyMail_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace FCV_DutchLadyMail_Model.Attribute
{
    public class AuthUser
    {
        public int User_Id { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private List<UserRole> Roles = new List<UserRole>();
        
        public AuthUser(int id)
        {
            this.User_Id = id;
            this.IsSysAdmin = false;
            GetDatabaseUserRolesPermissions();
        }

        private void GetDatabaseUserRolesPermissions()
        {
            using (MassMailsDbContext _model = new MassMailsDbContext())
            {
                USER _user = _model.USERS.Where(u => u.User_Id == this.User_Id).FirstOrDefault();
                if (_user != null)
                {
                    foreach (ROLE _role in _user.ROLES)
                    {
                        UserRole _userRole = new UserRole { Role_Id = _role.Role_Id, RoleName = _role.RoleName };
                        foreach (PERMISSION _permission in _role.PERMISSIONS)
                        {
                            _userRole.Permissions.Add(new RolePermission { Permission_Id = _permission.Permission_Id,
                                PermissionName = _permission.PermissionName });
                        }
                        this.Roles.Add(_userRole);

                        if (!this.IsSysAdmin)
                            this.IsSysAdmin = _role.IsSysAdmin;
                    }
                }
            }
        }

        public bool HasPermission(string requiredPermission)
        {
            bool bFound = false;
            foreach (UserRole role in this.Roles)
            {
                bFound = (role.Permissions.Where(p => p.PermissionName.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
                if (bFound)
                    break;
            }
            return bFound;
        }
        public bool HasRole(string role)
        {
            return (Roles.Where(p => p.RoleName == role).ToList().Count > 0);
        }
        public bool HasRoles(string roles)
        {
            bool bFound = false;
            string[] _roles = roles.ToLower().Split(';');
            foreach (UserRole role in this.Roles)
            {
                try
                {
                    bFound = _roles.Contains(role.RoleName.ToLower());
                    if (bFound)
                        return bFound;
                }
                catch (Exception)
                {
                }
            }
            return bFound;
        }
    }
}
public class UserRole
{
    public int Role_Id { get; set; }
    public string RoleName { get; set; }
    public List<RolePermission> Permissions = new List<RolePermission>();
}

public class RolePermission
{
    public int Permission_Id { get; set; }
    public string PermissionName { get; set; }
    public string PermissionDescription { get; set; }
}