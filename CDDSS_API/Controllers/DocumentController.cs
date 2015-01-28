using CDDSS_API.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDDSS_API.Controllers
{
    /// <summary>
    /// controller used for document operations
    /// functionallity of deleting a document is not implemented yet
    /// </summary>
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class DocumentController : ApiController
    {
        private DocumentRepository docRep = new DocumentRepository();

        /// <summary>
        /// returns a list of Files attached to issue
        /// </summary>
        /// <returns>list of files</returns>
        [AllowAnonymous]
        [HttpGet]
        public List<String> Get(int issueId)
        {
            return docRep.GetFilesByIssueId(issueId);
        }

        /// <summary>
        /// used to upload documents, documents should be attachet to request
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns>returns statuscode created if everything is ok or list of files which cloud not be uploaded because the already exsist</returns>
        [HttpPost]
        public HttpResponseMessage Post(int issueId)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    if (!docRep.UploadFile(issueId, httpRequest.Files[file].FileName, httpRequest.Files[file].InputStream))
                    {
                        docfiles.Add(httpRequest.Files[file].FileName);
                    }
                }
                if (docfiles.Capacity == 0) { 
                    result = Request.CreateResponse(HttpStatusCode.Created);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.NotAcceptable,docfiles);
                }
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        /// <summary>
        /// Returns document by issueid and filename.
        /// If document does not exist then bad reqeust.
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="filename"></param>
        /// <returns>document</returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Get(int issueId, string filename)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            Stream file = docRep.GetFile(issueId, filename);
            if (file != null)
            {
                result.Content = new StreamContent(file);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        /// <summary>
        /// deletes a document 
        /// </summary>
        /// <param name="issueId"></param>
        /// <param name="filename"></param>
        /// <returns>statuscode OK if deleted, statuscode NotAcceptable if document does not exist</returns>
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(int issueId, string filename)
        {
            if (docRep.DeleteDocument(issueId, filename))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }
    }
}
