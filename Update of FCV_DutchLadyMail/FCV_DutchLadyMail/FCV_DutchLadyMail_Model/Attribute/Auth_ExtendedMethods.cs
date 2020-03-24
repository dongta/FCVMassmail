﻿using FCV_DutchLadyMail_Model.Attribute;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

//Get requesting user's roles/permissions from database tables...      
public static class Auth_ExtendedMethods
{
    public static bool HasRole(this ControllerBase controller, string role, int userId)
    {
        bool bFound = false;
        try
        {
            //Check if the requesting user has the specified role...
            bFound = new AuthUser(userId).HasRole(role);            
        }
        catch { }
        return bFound;
    }

    public static bool HasRoles(this ControllerBase controller, string roles, int userId)
    {
        bool bFound = false;
        try
        {
            //Check if the requesting user has any of the specified roles...
            //Make sure you separate the roles using ; (ie "Sales Manager;Sales Operator"
            bFound = new AuthUser(userId).HasRoles(roles);
        }
        catch { }
        return bFound;
    }

    public static bool HasPermission(this ControllerBase controller, string permission, int userId)
    {
        bool bFound = false;
        try
        {
            //Check if the requesting user has the specified application permission...
            bFound = new AuthUser(userId).HasPermission(permission);
        }
        catch { }
        return bFound;
    }

    public static bool IsSysAdmin(this ControllerBase controller, int userId)
    {        
        bool bIsSysAdmin = false;
        try
        {
            //Check if the requesting user has the System Administrator privilege...
            bIsSysAdmin = new AuthUser(userId).IsSysAdmin;
        }
        catch { }
        return bIsSysAdmin;
    }
}
