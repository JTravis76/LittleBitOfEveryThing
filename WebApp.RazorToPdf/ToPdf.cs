using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace WebApp.RazorToPdf
{
    public class ToPdf
    {
        public byte[] GeneratePdfOutput(ControllerContext context, object model = null, string viewName = null, Action<PdfWriter, Document> configureSettings = null)
        {
            if (viewName == null)
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = model;

            byte[] output;
            using (var document = new Document())
            {
                using (var workStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, workStream);
                    writer.CloseStream = false;

                    if (configureSettings != null)
                    {
                        configureSettings(writer, document);
                    }
                    document.Open();

                    /* ============================================================================
                     * This method seems to break while adding a header from a parse HTML file.
                     * Works fine when PDF is a single page. Breaks if multi-page document.
                     * Also works fine if using the PdfPTable to build header.                     
                     * ============================================================================*/
                    //using (var reader = new StringReader(RenderRazorView(context, viewName)))
                    //{
                    //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);

                    //    document.Close();
                    //    output = workStream.ToArray();
                    //}


                    /* ============================================================================
                     * This method works fine when adding a header from a parse HTML file.
                     * Both single and multi-page will produce header.                   
                     * ============================================================================*/
                    ElementList el = XMLWorkerHelper.ParseToElementList(RenderRazorView(context, viewName), null);
                    foreach (IElement e in el)
                    {
                        document.Add(e);
                    }
                    document.Close();
                    output = workStream.ToArray();
                }
            }
            return output;
        }

        public string RenderRazorView(ControllerContext context, string viewName)
        {
            IView viewEngineResult = ViewEngines.Engines.FindView(context, viewName, null).View;
            var sb = new StringBuilder();
            
            using (TextWriter tr = new StringWriter(sb))
            {
                var viewContext = new ViewContext(context, viewEngineResult, context.Controller.ViewData,
                    context.Controller.TempData, tr);
                viewEngineResult.Render(viewContext, tr);
            }
            return sb.ToString();
        }
    }
}