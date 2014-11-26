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
    [EnableCors("http://localhost:51853", "*", "*")]
    [Authorize]
    public class StakeholdersController : ApiController
    {
        private StakeholdersRepository stakeRepo;

        public StakeholdersController()
        {
            stakeRepo = new StakeholdersRepository();
        }

        /// <summary>
        /// returns all stakeholders
        /// </summary>
        /// <returns></returns>
        public List<StakeholderModel> Get()
        {
            return stakeRepo.GetAllStakeholders();
        }
    }
}
