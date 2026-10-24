using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Picker()
        {
            return View();
        }

        [HttpGet]
        public string GET_PickerData(string group)
        {
            /*== This example uses the built-in MVC SelectListItem */

            var listItems = new List<SelectListItem>();

            using (var db = new DataContext.AdventureWorksEntities())
            {
                listItems = db.Products
                    .Where(x => x.Color != null)
                    .Select(x => new SelectListItem
                    {
                        Text = x.Color,
                        Value = x.Color,
                        Group = new SelectListGroup()
                        {
                            Name = group
                        }
                    })
                    //.Select(x => new { Value = x.Color, Text = x.Color})
                    .OrderBy(x => x.Text)
                    .Distinct()
                    .ToList();
            }
            return new JavaScriptSerializer().Serialize(listItems);
        }

        [HttpGet]
        public string GetPickerProducts(string group = "")
        {
            using (var db = new DataContext.AdventureWorksEntities())
            {
                var products = db.Products
                    //.Where(x => x.Color != null)
                    //.Select(x => new SelectListItem { Text = x.Color, Value = x.Color, Group = new SelectListGroup() { Name = group } })
                    //.Select(x => new { Value = x.ProductID, Text = x.Name, Title = "Some crazy long text about this really awesome jquery plugin that will blow your mind into the next universe!!"})
                    .Select(x => new { Value = x.ProductID, Text = x.Name, Title = x.ProductNumber })
                    //.Select(x => new { Value = x.ProductID, Text = x.Name })
                    .OrderBy(x => x.Text)
                    .Distinct()
                    .ToList();

                var json = new JavaScriptSerializer().Serialize(products);

                return json;
            }
        }

        public ActionResult Report(bool wm = false)
        {
            var model = new DataContext.Product();

            //return View();
            string headerText = RazorToPdf.Utilites.RenderViewToString(ControllerContext, "_Header", null, true);
            string footerText = RazorToPdf.Utilites.RenderViewToString(ControllerContext, "_Footer", null, true);

            //Add Header image
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Request.MapPath("~/Content/img/Fluor-BWXT_Logo.png"));
            img.ScalePercent(50f); //<- equivalent to percentage
            //img.SetAbsolutePosition(600, 550); //LETTER Landcape -draws from Lower-Left corner to Upper-Right corner
            img.SetAbsolutePosition(430, 740);

            //Add document watermark
            iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Request.MapPath("~/Content/img/FOR-INFO-ONLY-watermark.png"));
            watermark.ScalePercent(75.0f);
            watermark.SetAbsolutePosition(0, 0);

            //bool useWatermark = false;
            if (!wm)
                watermark = null;

            RazorToPdf.PdfPageEventHandler e = new RazorToPdf.PdfPageEventHandler()
            {
                HeaderImage = img,
                HtmlHeader = headerText,
                WatermarkImage = watermark,
                HtmlFooter = footerText
            };

            return new RazorToPdf.PdfActionResult("Report", model, (writer, document) =>
            {
                //set Landscape page
                //document.SetPageSize(iTextSharp.text.PageSize.LETTER.Rotate());
                document.SetPageSize(iTextSharp.text.PageSize.LETTER);
                //document.SetMargins(48, 48, 80, 60); //ONLY works on BODY, not HEADER (left, right, top, bottom)
                document.SetMargins(30, 30, 70, 40);
                writer.PageEvent = e;
                document.NewPage();
            });
        }

        public ActionResult Pagination(int? page, int? pagesize, string search = "")
        {
            //Predicate Filters
            var predicate = LinqKit.PredicateBuilder.New<DataContext.Product>();
            predicate = predicate.And(x => x.ProductID > 0); //<- Default Filter

            if (!string.IsNullOrEmpty(search))
            {
                var OrPredicate = LinqKit.PredicateBuilder.New<DataContext.Product>();
                OrPredicate = OrPredicate.Or(x => x.Name.Contains(search));
                OrPredicate = OrPredicate.Or(x => x.Color.Contains(search));

                //Add the OR statements
                predicate.And(OrPredicate);
            }

            //build view model
            var productPagination = new Models.ProductPagination()
            {
                PageIndex = page ?? 1,
                PageSize = pagesize ?? 25,
                Search = search
            };

            var db = new DataContext.AdventureWorksEntities();

            //Get total count
            int cnt = db.Products.Where(predicate).ToList().Count();

            //Pagination
            productPagination.PageCount = ((cnt + productPagination.PageSize) / productPagination.PageSize);
            int skipTo = (productPagination.PageIndex * productPagination.PageSize) - (productPagination.PageSize);

            //Query data
            productPagination.Products = db.Products
                .Where(predicate)
                .OrderBy(x => x.Name)
                //.ThenBy(x => x.)
                .Skip(skipTo)
                .Take(productPagination.PageSize)
                .ToList();

            db.Dispose();

            if (Request.IsAjaxRequest())
                return PartialView("_ProductGrid", productPagination);

            return View(productPagination);
        }

        public ActionResult Dashboard()
        {

            ViewData["Chart1"] = Services.ReportSvc.Report1();
            ViewData["Chart2"] = Services.ReportSvc.Report2();
            ViewData["Chart3"] = Services.ReportSvc.Report3();
            ViewData["Chart4"] = Services.ReportSvc.Report4();

            using (var db = new DataContext.AdventureWorksEntities())
            {
                ViewData["Years"] = new SelectList(db.Database.SqlQuery<System.Int32>("SELECT DISTINCT YEAR(ModifiedDate) FROM SalesLT.SalesOrderDetail ORDER BY YEAR(ModifiedDate) ASC;").ToList());

                ViewData["Products"] = new SelectList(db.Database.SqlQuery<System.String>("SELECT [Name] FROM SalesLT.Product ORDER BY [Name] ASC").ToList());
            }
            return View("Dashboard");
        }

        public PartialViewResult UpdateDashboard(FormCollection fc)
        {
            ViewData["Chart1"] = Services.ReportSvc.Report1(fc);

            return PartialView("_Charts");
        }

    }
}