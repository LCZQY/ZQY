
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneralSurvey_Utility.Filter
{

    /// <summary>
    ///    添加过滤器没有登陆直接跳转到登陆页面
    /// </summary>
    public class PermissionRequiredAttribute : ActionFilterAttribute
    {
        // 注入Seesion ??  在外部调用的时候 如何实例化
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;

        //public PermissionRequiredAttribute(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isDefined = false;
            var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                   .Any(a => a.GetType().Equals(typeof(PermissionRequiredAttribute)));
            }
            if (isDefined) return;


            // Seesion 方式
            //if (_session.GetString("LoginInfo") == null)
            //{               
            //    filterContext.Result = new RedirectResult("/Admin/Index");
            //}
            // 变量方式
            if (Seesion.UserName == "1")
            {
                filterContext.Result = new RedirectResult("/Admin/Index");
            }
            base.OnActionExecuting(filterContext);

        }
    }

    /// <summary>
    /// 不需要登入
    /// </summary>
    public class NoPermissionRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }

}
