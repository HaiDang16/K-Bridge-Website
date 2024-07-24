using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace K_Bridge.Attributes
{
    public class AdminAuthAttribute : TypeFilterAttribute
    {
        public AdminAuthAttribute() : base(typeof(AdminAuthFilter))
        {
        }

        private class AdminAuthFilter : IPageFilter
        {
            public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
            {
                var adminId = context.HttpContext.Session.GetInt32("AdminID");
                if (!adminId.HasValue)
                {
                    context.Result = new RedirectToPageResult("/Admin/Login");
                }
            }

            public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }
            public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
        }
    }
}