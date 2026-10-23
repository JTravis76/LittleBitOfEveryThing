using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DataContext())
            {
                // == Sections -> SectionPages -> Page
                SectionPage sectionPage = db.Sections
                    .Include(x => x.SectionPages.Select(z => z.Page))
                    .SelectMany(x => x.SectionPages)
                    //.Where(x => x.PageId == 1)
                    .FirstOrDefault(x => x.PageId == 1);
                    //.ToList()

                // == Page -> SectionPage -> Section
                Page page = db.Pages
                    .Include(x => x.SectionPages.Select(z => z.Section))
                    .SelectMany(x => x.SectionPages)
                    .Select(x => x.Page)
                    .FirstOrDefault(x => x.Id == 1);
                    //.ToList();

                // == SectionPages -> Page -> Section
                List<Section> sections = db.SectionPages
                    .Include(x => x.Page)
                    .Where(x => x.PageId == 1)
                    .SelectMany(x => x.Page.SectionPages.Select(z => z.Section))
                    .ToList();

                System.Diagnostics.Debug.Print("");
                //Console.ReadKey();
            }
        }
    }

    [Table("Page")]
    public class Page
    {
        public Page()
        {
            SectionPages = new HashSet<SectionPage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string Name { get; set; }

        [ForeignKey("PageId")]
        public virtual ICollection<SectionPage> SectionPages { get; set; }
    }

    [Table("Section")]
    public class Section
    {
        public Section()
        {
            SectionPages = new HashSet<SectionPage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("SectionId")]
        public virtual ICollection<SectionPage> SectionPages { get; set; }
    }

    [Table("SectionPage")]
    public class SectionPage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column( Order = 0)]
        public int PageId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public int SectionId { get; set; }

        [ForeignKey("PageId")]
        public virtual Page Page { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }
    }
}
