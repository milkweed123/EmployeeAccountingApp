using EmployeeAccountingApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAccountingApp.AuthAttribute
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!string.IsNullOrEmpty(auth))
            {
                var cred = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Login = cred[0], Pass = cred[1] };
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    foreach (var item in dbContext.Users)
                    {
                        if (BCrypt.Net.BCrypt.Verify(user.Pass, item.Password))
                        {
                        filterContext.HttpContext.Session["UserId"] = item.Id.ToString();
                            return;
                        }
                    }

                }
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", "EmployeeRealm"));
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}