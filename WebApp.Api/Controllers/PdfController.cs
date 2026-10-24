using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Database;

namespace WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly RazorLight.IRazorLightEngine _razorEngine;
        private readonly DataContext _context;
        private readonly Interfaces.IPDF _pdf;

        public PdfController(RazorLight.IRazorLightEngine razorEngine, DataContext context, Interfaces.IPDF pdf)
        {
            _razorEngine = razorEngine ?? throw new ArgumentNullException(nameof(razorEngine));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _pdf = pdf ?? throw new ArgumentNullException(nameof(pdf));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpGet("checksheet/{room}")]
        public async Task<IActionResult> CheckSheet(string room)
        {
            using (var db = _context)
            {
                var model = new Models.CheckSheetReport
                {
                    Room = room,
                    //Containers = null //db.Containers.Where(x => x.LOC == room)
                };

                byte[] pdf = Array.Empty<byte>();
                string? location = System.Reflection.Assembly.GetEntryAssembly()?.Location;
                string? path = Path.GetDirectoryName(location);

                if (!string.IsNullOrWhiteSpace(path))
                {
                    string headerPath = Path.Combine(path, "Reports\\CheckSheet\\header.cshtml");
                    string bodyPath = Path.Combine(path, "Reports\\CheckSheet\\body.cshtml");

                    string header = await _razorEngine.CompileRenderAsync(headerPath, model);
                    string body = await _razorEngine.CompileRenderAsync(bodyPath, model);

                    var converter = new MacroInteraction.WkHtmlToPdf.HtmlToPdfConverter()
                    {
                        Orientation = MacroInteraction.WkHtmlToPdf.PageOrientation.Landscape,
                        Margins = new MacroInteraction.WkHtmlToPdf.PageMargins() { Top = 20 },
                        PageHeaderHtml = header,
                        CustomWkHtmlArgs = string.Format("--footer-right {0} --footer-line --footer-spacing {1}", "\"Page [page] of [topage]\"", 2.2)
                    };

                    pdf = converter.GeneratePdf(body);
                }

                return File(pdf, "application/pdf");
            }
        }

        //[HttpGet("a630/{room}")]
        //public async Task<IActionResult> A630(string room)
        //{
        //    using (var db = _context)
        //    {
        //        var pdfs = new List<byte[]>();

        //        // Get the current account info, if available, apply to filter
        //        var acc = db.Configs.Where(x => x.ConfigName == "Account").FirstOrDefault();

        //        // Unit Of Measure values are between 6 - 9
        //        for (int unitOfMeasure = 6; unitOfMeasure <= 9; unitOfMeasure++)
        //        {
        //            string unit = "KilogramContainers";
        //            switch (unitOfMeasure)
        //            {
        //                case 7:
        //                    unit = "PoundContainers";
        //                    break;
        //                case 8:
        //                    unit = "GramsContainers";
        //                    break;
        //                case 9:
        //                    unit = "LiterContainers";
        //                    break;
        //            }

        //            // Default filter to the requested room
        //            var predicate = LinqKit.PredicateBuilder.New<BusinessObjects.Entities.Container>(x => x.LOC == room);
        //            if (acc != null)
        //                predicate.And(x => x.ACCOUNT == acc.ConfigValue);

        //            List<string> codes = new List<string>();
        //            var config = db.Configs.Where(x => x.ConfigName == unit).FirstOrDefault();
        //            if (config != null)
        //                codes.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(config.ConfigValue));

        //            // build OR statement to capture starting container ids
        //            var predicateOr = LinqKit.PredicateBuilder.New<BusinessObjects.Entities.Container>();
        //            foreach (string code in codes)
        //                predicateOr.Or(x => x.CONTAINER_ID.StartsWith(code));

        //            // combine both AND and OR predicate
        //            predicate.And(predicateOr);

        //            // Get containers data based on filters
        //            var containers = db.Containers
        //                .Where(predicate)
        //                .OrderBy(x => x.CONTAINER_ID)
        //                .ToList();

        //            if (containers.Count() > 0)
        //            {
        //                //break in batches of (20) records
        //                for (int skip = 0; skip < containers.Count(); skip += 20)
        //                {
        //                    Models.A630ReportModel model = new Models.A630ReportModel
        //                    {
        //                        Account = acc?.ConfigValue,
        //                        Room = room,
        //                        Containers = containers.Skip(skip).Take(20),
        //                        UnitOfMeasure = unitOfMeasure.ToString()
        //                    };

        //                    // Create PDF page and add to collection
        //                    pdfs.Add(await A630ReportAsync(model));
        //                }
        //            }
        //        }

        //        byte[] pdf = null;

        //        // combine all pages into one document or a empty report
        //        if (pdfs.Count > 0)
        //            pdf = new PDF.Creation().Concat(pdfs);
        //        else
        //            pdf = await A630ReportAsync(new Models.A630ReportModel
        //            {
        //                Account = acc?.ConfigValue,
        //                Room = room,
        //                UnitOfMeasure = "-"
        //            });

        //        return File(pdf, "application/pdf");
        //    }
        //}

        //== Customer mention they do NOT want to use the Pro-Form
        //[Authorize]
        ////[HttpGet("a630")]
        ////[HttpGet("a630/{room}")]
        //public IActionResult A630(string room)
        //{
        //    //Since the A630 form is approved by Performance Documents
        //    // use the form fillable version instead of the HTML version in route above
        //    byte[] pdf = _pdf.Generate_A630(room);
        //    return File(pdf, "application/pdf");
        //}

        /// <summary>
        /// Reads the HTML Razor template in to a PDF with supplied data
        /// </summary>
        /// <param name = "model" > A630 Report Model</param>
        /// <returns></returns>
        private async Task<byte[]> A630ReportAsync(Models.A630Report a630)
        {
            byte[] pdf = Array.Empty<byte>();
            string? location = System.Reflection.Assembly.GetEntryAssembly()?.Location;
            string? path = Path.GetDirectoryName(location);

            if (!string.IsNullOrWhiteSpace(path))
            {
                string headerPath = Path.Combine(path, "Reports\\A630\\header.cshtml");
                string bodyPath = Path.Combine(path, "Reports\\A630\\body.cshtml");

                string header = await _razorEngine.CompileRenderAsync(headerPath, a630);
                string body = await _razorEngine.CompileRenderAsync(bodyPath, a630);

                var converter = new MacroInteraction.WkHtmlToPdf.HtmlToPdfConverter()
                {
                    Orientation = MacroInteraction.WkHtmlToPdf.PageOrientation.Landscape,
                    Margins = new MacroInteraction.WkHtmlToPdf.PageMargins() { Top = 40, Bottom = 25 },
                    PageHeaderHtml = header,
                    PageFooterHtml = "<table><tr><td>Inventoried By:</td><td>________________________</td><td>Badge #</td><td>_______________</td><td>Date</td><td>____________</td><td>Nuclear Material Custodian:</td><td>________________________</td><td>Badge #</td><td>_______________</td><td>Date</td><td>_____________</td></tr></table>"
                    //CustomWkHtmlArgs = string.Format("--footer-right {0} --footer-line --footer-spacing {1}", "\"Page [page] of [topage]\"", 2.2)
                };
                pdf = converter.GeneratePdf(body);
            }

            return pdf;
        }

    }
}