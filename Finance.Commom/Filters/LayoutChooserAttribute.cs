using System.Web.Mvc;

namespace Finance.Commom.Filters
{
    public class LayoutChooserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResult;

            string masterName = filterContext?.HttpContext?.Session["Template"] as string;

            if (!string.IsNullOrEmpty(masterName) && result?.MasterName != null)
                    result.MasterName = masterName;

            base.OnActionExecuted(filterContext);
        }
    }
}
