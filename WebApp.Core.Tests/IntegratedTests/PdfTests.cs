using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UMAS.Tests.IntegratedTests
{
    [TestClass]
    public class PdfTests
    {
        //private readonly PDF _pdf;

        public PdfTests()
        {
            //_pdf = new PDF();
        }

        //[TestMethod]
        //public void PDF_GenerateA630_HasBytes()
        //{
        //    // Arrange
        //    var sampleData = new PDF.A630()
        //    {
        //        ActualDate = DateTime.Now,
        //        UnitOfMeasure = UMAS.PDF.UnitOfMeasure.Grams,
        //        Account = "610",
        //        Location = "240",
        //        PageNumber = "1",
        //        //InventoriedBy = "",
        //        //InventoriedBadge = "",
        //        //ReviewedBy = "",
        //        //ReviewedBadge = "",
        //        Materials = new List<PDF.Material>()
        //        {
        //            new PDF.Material
        //            {
        //                Description = null,
        //                TransferNumber = "109797",
        //                DMT = null,
        //                IDC = "111",
        //                Type = "2S",
        //                ContainerNumber = "031357",
        //                Gross = 3036m,
        //                Tare = 2216.00m,
        //                UraniumConcentation = 0.67619m,
        //                Assay = 0.3526m
        //            }
        //        }
        //    };

        //    // Act
        //    byte[] PDF = _pdf.GenerateA630(@"C:\Users\KDJ\Downloads\A630.pdf", sampleData);

        //    // Assert          
        //    Assert.IsNotNull(PDF);

        //    System.IO.File.WriteAllBytes(@"C:\Users\KDJ\Desktop\form.pdf", PDF);
        //}

    }
}
