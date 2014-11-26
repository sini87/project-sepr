using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class StakeholdersRepository : RepositoryBase
    {
        public List<StakeholderModel> GetAllStakeholders()
        {
            List<StakeholderModel> list = new List<StakeholderModel>();
            foreach (Stakeholder st in ctx.Stakeholders)
            {
                list.Add(new StakeholderModel(st.Id, st.Name));
            }
            return list;
        }
    }
}