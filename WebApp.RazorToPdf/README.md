/*===============================
* WebApp.RazorToPdf v1.0.3
================================*/

v1.0.2 - 5/9/2017: Fix issue where HTML Footer text not rendering output.
v1.0.3 - 5/11/2017: Add option to show page number in the footer. Bool: true ? false
			Default is true



Design your basic HTML page with MVC Razor syntax when render to beautiful PDFs!


Code Example:
```cs
public ActionResult Report(bool wm = false)
{
	//build a model to pass to view
    var model = new Models.Product();

    string headerText = WebApp.RazorToPdf.Utilites.RenderViewToString(ControllerContext, "_Header", null, true);
    string footerText = WebApp.RazorToPdf.Utilites.RenderViewToString(ControllerContext, "_Footer", null, true);

    //Add Header image
    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Request.MapPath("~/img/logo.png"));
    img.ScalePercent(50f); //<- equivalent to percentage
    //img.SetAbsolutePosition(600, 550); //LETTER Landcape -draws from Lower-Left corner to Upper-Right corner
    img.SetAbsolutePosition(430, 740);

    //Add document watermark
    iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Request.MapPath("~/img/FOR-INFO-ONLY-watermark.png"));
    watermark.ScalePercent(75.0f);
    watermark.SetAbsolutePosition(0, 0);

    //bool useWatermark = false;
    if (!wm)
        watermark = null;

    WebApp.RazorToPdf.PdfPageEventHandler e = new WebApp.RazorToPdf.PdfPageEventHandler()
    {
        HeaderImage = img,
        HtmlHeader = headerText,
        WatermarkImage = watermark,
        HtmlFooter = footerText,
		ShowPageNumber = false
    };

    return new WebApp.RazorToPdf.PdfActionResult("Report", model, (writer, document) =>
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
```