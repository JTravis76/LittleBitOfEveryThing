using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace WebApp.RazorToPdf
{
    public class PdfPageEventHandler : PdfPageEventHelper
    {
        public PdfPageEventHandler() { ShowPageNumber = true; }

        public Image HeaderImage { get; set; }
        public Image WatermarkImage { get; set; }
        public string HtmlHeader { get; set; }
        public string HtmlFooter { get; set; }
        public bool ShowPageNumber { get; set; }

        /// <summary>
        /// Writes Header/Footer. Calls at end of each page creation.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            ColumnText ct = new ColumnText(writer.DirectContent);

            if(HtmlHeader != null)
            {
                /*=========================================================
                 * Create a Header from a HTML string.
                 * XMLWorkerHelper ignores the <img> tag. So I passed a
                 * image file with SetAbsolutePosition().
                 ==========================================================*/
            
                XMLWorkerHelper.GetInstance().ParseXHtml(new ColumnTextElementHandler(ct), new System.IO.StringReader(HtmlHeader));
                ct.SetSimpleColumn(document.Left, document.Top, document.Right, document.GetTop(-50), 10, Element.ALIGN_MIDDLE);
                ct.Go();
            }

            if (HeaderImage != null)
                document.Add(HeaderImage);

            if(WatermarkImage != null)
                document.Add(WatermarkImage);

            


            /*=========================================================
             * Create Header via PdfPTable
             * found here: http://kuujinbo.info/cs/itext_img_hdr.aspx
             ==========================================================*/
            //// PDF document size 
            //Rectangle page = document.PageSize;

            //// create three column table
            //PdfPTable head = new PdfPTable(3);
            //head.TotalWidth = page.Width -100;
            
            //PdfPCell c;                   

            ////add Report date
            //c = new PdfPCell(new Phrase(
            //    string.Format("Date: {0:d-MMM-yyyy}", DateTime.Now ),
            //    new Font(Font.FontFamily.TIMES_ROMAN, 10)
            //    ));            
            //c.Border = PdfPCell.NO_BORDER;
            //c.VerticalAlignment = Element.ALIGN_MIDDLE;
            //c.FixedHeight = document.TopMargin;
            //head.AddCell(c);

            ////add Page Title
            //c = new PdfPCell(new Phrase(
            //    "FBP Overtime Canvass Polling Report",
            //    new Font(Font.FontFamily.TIMES_ROMAN, 20)
            //    ));
            //c.Border = PdfPCell.NO_BORDER;
            //c.VerticalAlignment = Element.ALIGN_TOP;
            //c.HorizontalAlignment = Element.ALIGN_CENTER;
            //c.FixedHeight = document.TopMargin -100;
            //head.AddCell(c);

            ////add image; PdfPCell() overload sizes image to fit cell
            //c = new PdfPCell(ImgHeader, false);
            //c.HorizontalAlignment = Element.ALIGN_RIGHT;
            //c.FixedHeight = document.TopMargin;
            //c.Border = PdfPCell.NO_BORDER;            
            //head.AddCell(c);

            ////since the table header is implemented using a PdfPTable, we call
            ////WriteSelectedRows(), which requires absolute positions!
            //head.WriteSelectedRows(
            //    // first/last row, zero index; -1 flags all write all rows
            //  0, -1,
            //    // left offset
            //  50,
            //    // ** bottom** yPos of the table 
            //  page.Height - document.TopMargin + head.TotalHeight -20,
            //  writer.DirectContent
            //);

            if(this.HtmlFooter != null)
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(new ColumnTextElementHandler(ct), new System.IO.StringReader(HtmlFooter));
                ct.SetSimpleColumn(document.Left, 60, document.Right, -10, 10, Element.ALIGN_MIDDLE);
                
                ct.Go();
            } 

            /*===================================
            * Writes Footer section.
            * Take note that Bottom Margin
            * will push outside of page view
            ====================================*/
            if (ShowPageNumber)
            {
                PdfContentByte cb = writer.DirectContent;
                Phrase p = new Phrase(string.Format("Page: {0}", 
                    document.PageNumber),
                    new Font(Font.FontFamily.TIMES_ROMAN, 10));

                ColumnText.ShowTextAligned(cb, 
                    Element.ALIGN_CENTER, 
                    p,
                    (document.Right - document.Left) / 2 + document.LeftMargin,
                    document.Bottom - 10, 
                    0);  
            }
          
        }

    }//END CLASS
}