using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.RazorToPdf
{
    public static class Utilites
    {
        /*===========================================================================
         * Method to render a Razor View using a Empty Controller Route.
         * Will support razor tags, but since not routing through live Controller,
         * cannot pass ViewData, etc. to view.
         * Found source here:
         * http://www.codemag.com/article/1312081
         ============================================================================*/

        public static string RenderViewToString(System.Web.Mvc.ControllerContext context, string viewName, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            System.Web.Mvc.ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(context, viewName);
            else
                viewEngineResult = System.Web.Mvc.ViewEngines.Engines.FindView(context, viewName, null);

            if (viewEngineResult == null)
                throw new System.IO.FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new System.IO.StringWriter())
            {
                var ctx = new System.Web.Mvc.ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        public static T CreateController<T>(System.Web.Routing.RouteData routeData = null)
            where T : System.Web.Mvc.Controller, new()
        {
            // create a disconnected controller instance
            T controller = new T();

            // get context wrapper from HttpContext if available
            System.Web.HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
                wrapper = new System.Web.HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException(
                    "Can't create Controller Context if no " +
                    "active HttpContext instance is available.");

            if (routeData == null)
                routeData = new System.Web.Routing.RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}
