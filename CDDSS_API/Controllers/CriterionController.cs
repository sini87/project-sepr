using CDDSS_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CDDSS_API.Controllers
{
    /// <summary>
    /// CriterionController
    /// </summary>
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class CriterionController : ApiController
    {
        CriterionRepository cRep;

        CriterionController()
        {
            cRep = new CriterionRepository();
        }

        /// <summary>
        /// Post Criterion
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Post()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
