using WebApp.Api.Database;

namespace WebApp.Api.Services.PDF
{
    /// <summary>
    /// 
    /// </summary>
    public class PDFSvc : Interfaces.IPDF
    {
        private readonly DataContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PDFSvc(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public byte[] Generate_A630(string location)
        {
            //var pdf = new WebApp.PDF.Creation();
            //List<WebApp.PDF.Material> materials = new List<WebApp.PDF.Material>();

            //var containers = _context.Containers.Where(x => x.LOC == location);

            //foreach (var container in containers)
            //{
            //    materials.Add(new WebApp.PDF.Material
            //    {
            //        ContainerNumber = container.CONTAINER_ID.Substring(2, container.CONTAINER_ID.Length - 2),
            //        TransferNumber = container.TRANSFER,
            //        Gross = container.GROSS,
            //        Tare = (decimal)container.TARE,
            //        Type = container.CONTAINER_ID.Substring(0, 2),
            //        IDC = container.CODE,
            //        Assay = (decimal)container.CASSAY,
            //        UraniumConcentation = (decimal)container.GRAMSU
            //    });

            //    //TODO: break after 1st one, as PDF form NOT completed
            //    break;
            //}

            //TODO: split container into two group (liquids vs non-liquids), then set Unit of Measure below

            //var data = new WebApp.PDF.A630
            //{
            //    ActualDate = DateTime.Now,
            //    Account = "610", //TODO: Where is this???
            //    UnitOfMeasure = WebApp.PDF.UnitOfMeasure.Grams,
            //    Location = location,
            //    Materials = materials
            //};

            List<byte[]> pages = new List<byte[]>();

            //TODO: math calculation to determine the number of pages
            // using (2) for testing
            //for (int i = 1; i < 2; i++)
            //{
            //    data.PageNumber = i.ToString();
            //    pages.Add(pdf.A630(data));
            //}


            return pages[0];// pdf.Concat(pages);
        }
    }
}
