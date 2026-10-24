using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.PDF
{
    public class Creation
    {
        /*== snippet to find fields names and numbered them to check form
            int cnt = 1;
            foreach (var f in acroFields.Fields)
            {
                System.Diagnostics.Debug.Print("Key: {0}", f.Key);

                bool result = acroFields.SetField(f.Key, cnt.ToString());
                if (!result)
                {
                    System.Diagnostics.Debug.Print("Unable to set field");
                }

                cnt += 1;
            }
         */


        /// <summary>
        /// Concatenate Bytes into a single Byte
        /// </summary>
        /// <param name="pages">collection of pdf pages</param>
        /// <returns></returns>
        public byte[] Concat(IEnumerable<byte[]> pages)
        {
            byte[] PDFmerged = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (iTextSharp.text.Document document = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfSmartCopy copy = new iTextSharp.text.pdf.PdfSmartCopy(document, ms)
                    {
                        CloseStream = false
                    };

                    document.Open();

                    iTextSharp.text.pdf.PdfImportedPage pg;
                    /* Stamper is needed when altering the page */
                    //iTextSharp.text.pdf.PdfCopy.PageStamp stamp;

                    //int pageNumber = 1;
                    //int pageCount = pages.Count();
                    foreach (byte[] page in pages)
                    {
                        ////Write Page Numbers
                        //iTextSharp.text.Phrase phrase = new iTextSharp.text.Phrase(string.Format("Page {0} of {1}", pageNumber, pageCount), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10));
                        //pageNumber += 1;

                        using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(page))
                        {
                            for (int p = 1; p <= reader.NumberOfPages; p++)
                            {
                                pg = copy.GetImportedPage(reader, p);
                                //stamp = copy.CreatePageStamp(pg);
                                //iTextSharp.text.pdf.ColumnText.ShowTextAligned(stamp.GetUnderContent(), iTextSharp.text.Element.ALIGN_CENTER, phrase, (document.Right - document.Left), document.Bottom - 10, 0);
                                //stamp.AlterContents();
                                copy.AddPage(pg);
                            }

                            reader.Close();
                        }
                    }
                    copy.Close();
                }

                PDFmerged = ms.ToArray();
                ms.Flush();
                ms.Close();
            }

            return PDFmerged;
        }

        /// <summary>
        /// Add watermark image (under content) for every page.
        /// </summary>
        /// <param name="pdf">PDF document</param>
        /// <returns></returns>
        public byte[] AddWatermark(byte[] pdf)
        {
            byte[] PDF = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(pdf))
                {
                    using (var stamper = new PdfStamper(reader, ms))
                    {
                        //Add document watermark
                        System.Drawing.Bitmap png = Resources.FOR_INFO_ONLY_watermark;

                        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(image: (System.Drawing.Image)png, color: null);
                        watermark.ScalePercent(85.0f); //<-- equivalent to percentage
                        watermark.SetAbsolutePosition(0, 0);

                        // Must start at 1 because 0 is not an actual page.
                        for (int p = 1; p <= reader.NumberOfPages; p++) 
                        {
                            PdfContentByte pg = stamper.GetUnderContent(p);
                            pg.AddImage(watermark);
                        }

                        stamper.Close();
                    }
                    reader.Close();
                }

                PDF = ms.ToArray();
                ms.Flush();
                ms.Close();
            }

            return PDF;
        }

        //public byte[] FRM_01127(BusinessObjects.Entities.ProjectPlan projectPlan)
        //{
        //    byte[] PDF = null;

        //    using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(Resources.FBP_FRM_01127, null))
        //    {
        //        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        //        {
        //            using (iTextSharp.text.pdf.PdfStamper stamper = new iTextSharp.text.pdf.PdfStamper(reader, memoryStream))
        //            {
        //                iTextSharp.text.pdf.AcroFields acroFields = stamper.AcroFields;

        //                acroFields.SetField("A1.Checklist#", projectPlan.PrefixId);
        //                acroFields.SetField("A2.ChecklistRev#", projectPlan.Revision.ToString());
        //                acroFields.SetField("A3.ProjectName", projectPlan.ProjectName);
        //                acroFields.SetField("A4.ProjectContactName", projectPlan.ContactName);
        //                acroFields.SetField("A5.ProjectContactPhone#", projectPlan.ContactPhone);

        //                if (projectPlan.OSWDFWasteStreams.Count > 0)
        //                {
        //                    foreach (var ws in projectPlan.OSWDFWasteStreams)
        //                    {
        //                        acroFields.SetField("MasterWasteProfile#", ws.MasterWasteProfile.PrefixId);
        //                        acroFields.SetField("MasterWasteProfileREV#", ws.MasterWasteProfile.Revision.ToString());
        //                        acroFields.SetField("WasteType(1-5)", ws.MasterWasteProfile.WasteType);
        //                        acroFields.SetField("WasteStreamDescription", ws.MasterWasteProfile.Description);

        //                        acroFields.SetField("10#", ws.PlannedWsVolume.ToString());
        //                        acroFields.SetField("10Unit", ws.PlannedWsUnit);
        //                        acroFields.SetField("ConversionFactor", ws.PlannedWsFactor);
        //                        acroFields.SetField("11#", ws.ActualWsVolume.ToString());
        //                        acroFields.SetField("11 Unit", ws.ActualWsUnit);
        //                        acroFields.SetField("MaterialSourceLocation#", ws.MaterialSourceLocation?.PrefixId);
        //                        acroFields.SetField("17TypeofcontainerrequiredforstagingorN/A", ws.ContainerType);
        //                        acroFields.SetField("18TypeofpackagerequiredforshipmenttotheOSWDF", ws.PackageType);
        //                        acroFields.SetField("20ConveyanceTypeRequired", ws.ConveyanceType);

        //                        if (ws.DataGroups != null && ws.DataGroups != "[]")
        //                        {
        //                            string[] dataGroups = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(ws.DataGroups);

        //                            acroFields.SetField("Data Group#1", dataGroups[0]);
        //                            //acroFields.SetField("DataGroup#2", dg);
        //                            //acroFields.SetField("DataGroup#3", "");
        //                        }

        //                        if (!string.IsNullOrEmpty(ws.GWMP_Prohibited))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Prohibited);
        //                            acroFields.SetField("15GWMPDocument#", Convert.ToString(dyn.document));
        //                            acroFields.SetField("15GWMPFileName(attach)", Convert.ToString(dyn.attachment.Name));
        //                            acroFields.SetField("15GWMPSection#", Convert.ToString(dyn.section));
        //                            acroFields.SetField("15GWMPPage#", Convert.ToString(dyn.page));
        //                            acroFields.SetField("15Description", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("15SupplementaryInformationFileName(attach)", Convert.ToString(dyn.supplementary.Name));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Physical))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Physical);
        //                            acroFields.SetField("16GWMPSection#", Convert.ToString(dyn.section));
        //                            acroFields.SetField("16GWMPPage#", Convert.ToString(dyn.page));
        //                            acroFields.SetField("16Description", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("16SupplementaryInformationFileName(attach)", Convert.ToString(dyn.supplementary.Name));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Packaging))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Packaging);
        //                            acroFields.SetField("19GWMPSection#", Convert.ToString(dyn.section));
        //                            acroFields.SetField("19GWMPPage#", Convert.ToString(dyn.page));
        //                            acroFields.SetField("19Description", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("19SupplementaryInformationFileName(attach)", Convert.ToString(dyn.supplementary.Name));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Transportation))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Transportation);
        //                            acroFields.SetField("21GWMPSection#", Convert.ToString(dyn.section));
        //                            acroFields.SetField("21GWMPPage#", Convert.ToString(dyn.page));
        //                            acroFields.SetField("21Description", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("21SupplementaryInfoFileName(attach)_1", Convert.ToString(dyn.supplementary.Name));
        //                            acroFields.SetField("21EquivalentLevelofSafetyFileName(attach)_1", Convert.ToString(dyn.safety.Name));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Safety))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Safety);
        //                            acroFields.SetField("22GWMPSection#", Convert.ToString(dyn.section));
        //                            acroFields.SetField("22GWMPPage#", Convert.ToString(dyn.page));
        //                            acroFields.SetField("22OrDescription", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("22SupplementaryInfoFileName(attach)_1", Convert.ToString(dyn.supplementary.Name));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Security))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Security);
        //                            acroFields.SetField("22GWMPSection#_2", Convert.ToString(dyn.section));
        //                            acroFields.SetField("22GWMP Page#_2", Convert.ToString(dyn.page));
        //                            acroFields.SetField("22OrDescription_2", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("22SupplementaryInfoFileName(attach)_2", Convert.ToString(dyn.supplementary.Name));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Criticality))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Criticality);
        //                            acroFields.SetField("22GWMPSection#_3", Convert.ToString(dyn.section));
        //                            acroFields.SetField("22GWMPPage#_3", Convert.ToString(dyn.page));
        //                            acroFields.SetField("22OrDescription_3", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("23SupplementaryInfoFileName(attach)_1", Convert.ToString(dyn.supplementary.Name));
        //                            acroFields.SetField("23CriticalityIncredibleDeterminationFileName( attach)", Convert.ToString(dyn.determination));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Hazard))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Hazard);
        //                            acroFields.SetField("23GWMPSection#_1", Convert.ToString(dyn.section));
        //                            acroFields.SetField("23GWMP Page#_1", Convert.ToString(dyn.page));
        //                            acroFields.SetField("23OrDescription_1", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("23SupplementaryInfoFileName(attach)_2", Convert.ToString(dyn.supplementary.Name));
        //                            acroFields.SetField("23HazardAnalysisFileName(attach)_1", Convert.ToString(dyn.determination));
        //                        }
        //                        if (!string.IsNullOrEmpty(ws.GWMP_Performance))
        //                        {
        //                            dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ws.GWMP_Performance);
        //                            acroFields.SetField("23GWMP Section#_2", Convert.ToString(dyn.section));
        //                            acroFields.SetField("23GWMPPage#_2", Convert.ToString(dyn.page));
        //                            acroFields.SetField("23OrDescription_2", Convert.ToString(dyn.desc));
        //                            acroFields.SetField("23SupplementaryInfoFileName(attach)_3", Convert.ToString(dyn.supplementary.Name));
        //                            acroFields.SetField("23NoImpactDeterminationFileName(attach)", Convert.ToString(dyn.determination));
        //                        }
        //                    }
        //                }

        //                acroFields.SetField("30PRojectRepName", projectPlan.ContactName);
        //                acroFields.SetField("30RPojectRepSignature", projectPlan.GeneratedBy);
        //                acroFields.SetField("30PRojectRepDate", projectPlan.GeneratedDate?.ToShortDateString());

        //                acroFields.SetField("31WAOName", projectPlan.ApproverName);
        //                acroFields.SetField("31WAOSignature", projectPlan.ApprovedBy);
        //                acroFields.SetField("31WAODate", projectPlan.ApprovedDate?.ToShortDateString());
        //                //acroFields.SetField("", "");

        //                stamper.FormFlattening = true;
        //                stamper.Close();
        //            }

        //            PDF = memoryStream.ToArray();
        //            memoryStream.Flush();
        //            memoryStream.Close();
        //        }
        //        reader.Close();
        //    }

        //    return PDF;
        //}


    }
}
