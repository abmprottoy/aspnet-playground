using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagement.DTOs;
using System.Web.Mvc;

namespace LibraryManagement.Auth
{
    public class AuthorizedAccessAttribute : AuthorizeAttribute
    {
        public string Roles { get; set; }  
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.Session["User"] as UserDTO;
            if (user == null) return false;

            if (string.IsNullOrEmpty(Roles)) return false;

            var roles = Roles.Split(',');
            return roles.Any(role => role.Equals(user.Role, StringComparison.OrdinalIgnoreCase));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/AccessDenied");
        }
    }

}