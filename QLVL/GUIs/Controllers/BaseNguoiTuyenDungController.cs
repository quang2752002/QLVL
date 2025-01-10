using GUIs.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GUIs.Controllers
{
    public class BaseNguoiTuyenDungController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int userid = DataServices.getUserId(HttpContext);
            if (userid <= 0)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Login", area = "" }));
            }
            else
            {
                string route = DataServices.getRouoter(HttpContext);
                if (route =="NguoiTuyenDung")
                {
                    // filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "NguoiTuyenDung", area = route }));
                }
                else
                {
                    if (route =="NguoiLaoDong")
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home", area = "" }));
                    }
                    else
                    {
                        if (route =="Admin")
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Admin", area = route }));
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Login", area = "" }));
                        }
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
