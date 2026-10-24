using System;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WebApp.RazorToPdf
{
    public static class ToPdfExtensions
    {
        public static byte[] GeneratePdf(this ControllerContext context, object model=null, string viewName=null, Action<PdfWriter, Document> configureSettings=null)
        {
            return new ToPdf().GeneratePdfOutput(context, model, viewName, configureSettings);
        }
    }
}