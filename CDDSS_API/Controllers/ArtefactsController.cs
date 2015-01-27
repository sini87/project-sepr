using CDDSS_API.Models;
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
    /// controller for artefacts
    /// </summary>
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class ArtefactsController : ApiController
    {
        private ArtefactsRepository artRepo;
        
        public ArtefactsController()
        {
            artRepo = new ArtefactsRepository();
        }

        /// <summary>
        /// returns all artefacts
        /// </summary>
        /// <returns></returns>
        public List<ArtefactModel> GET()
        {
            return artRepo.GetAllArtefacts();
        }
    }
}
