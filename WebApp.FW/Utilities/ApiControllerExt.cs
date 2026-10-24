/*
 * Extend the ApiController class to add new result types
 * FileResult() - 2/4/2019 by Jeremy Travis
 */
namespace System.Web.Http
{
    using System.Web.Http.Results;

    /// <summary>
    /// 
    /// </summary>
    public abstract class ApiControllerExt : ApiController
    {
        /// <summary>
        /// Creates an System.Web.Http.Results.FileResult
        /// </summary>
        /// <param name="data">in-memory data</param>
        /// <param name="fileName">optional: provide filename to the downloaded file</param>
        /// <returns>System.Web.Http.Results.FileResult</returns>
        protected internal virtual FileResult FileResult(byte[] data, string fileName = null)
        {
            return new FileResult(data, fileName);
        }
    }
}

namespace System.Web.Http.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Missing XML Comment", "CS1591")]
    public class FileResult : IHttpActionResult
    {
        private readonly string _fileName;
        private readonly byte[] _data;

        public FileResult(byte[] data, string fileName = null)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _fileName = fileName;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ByteArrayContent(_data)
            };

            if (!string.IsNullOrEmpty(_fileName))
            {
                httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = _fileName
                };
            }

            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");//"application/pdf"
            return Task.FromResult(httpResponseMessage);
        }
    }
}