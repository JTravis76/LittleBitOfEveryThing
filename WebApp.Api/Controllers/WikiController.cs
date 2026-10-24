using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Database;
using WebApp.Api.Entities;

namespace WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public WikiController(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("articles")]
        [ProducesResponseType(typeof(List<WikiContent>), 200)]
        public async Task<IActionResult> Get_Articles(string search = "")
        {
            var predicate = LinqKit.PredicateBuilder.New<WikiContent>();
            predicate.Start(x => x.Id > 0); // <= default filter

            if (!String.IsNullOrEmpty(search)) {
                string[] words = search.Split(new[] { ' ' });

                foreach (var word in words)
                  predicate.Or(x => x.Tags.Contains(word));
            };

            return Ok(await _dataContext.WikiContents
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(x => x.Title)
                .ToListAsync());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("article")]
        [ProducesResponseType(typeof(WikiContent), 200)]
        public async Task<IActionResult> Get_ArticleById(int id)
        {
            return Ok(await _dataContext.WikiContents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("article")]
        [ProducesResponseType(typeof(WikiContent), 200)]
        public IActionResult Post_Article(WikiContent content)
        {
            if (ModelState.IsValid)
            {
                // if (content.IsHome) //TODO: Reset home page

                content.ModifiedDttm = DateTime.Now;
                content.ModifiedBy = User.Identity!.Name;
                content.Version = content.Version == null ? 1 : content.Version + 1;

                if (content.Id.Equals(0))
                    _dataContext.Add(content);
                else
                    _dataContext.Update(content);
            }
            return Ok(content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="markdown"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("parse")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post_ParseMarkdown(string markdown)
        {
            return Content(CommonMark.CommonMarkConverter.Convert(markdown), "text/plain", System.Text.Encoding.UTF8);
        }


    }
}
