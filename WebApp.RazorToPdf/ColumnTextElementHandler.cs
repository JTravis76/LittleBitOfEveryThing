using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline;

namespace WebApp.RazorToPdf
{
    public class ColumnTextElementHandler : IElementHandler
    {
        public ColumnTextElementHandler(ColumnText ct)
        {
            this.ct = ct;
        }

        ColumnText ct = null;

        public void Add(IWritable w)
        {
            if (w is WritableElement)
            {
                foreach (IElement e in ((WritableElement)w).Elements())
                {
                    ct.AddElement(e);
                }
            }
        }

    }
}