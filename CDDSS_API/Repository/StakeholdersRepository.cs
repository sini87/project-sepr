using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// class for database operations regarding a stakeholder
    /// </summary>
    public class StakeholdersRepository : RepositoryBase
    {
        /// <summary>
        /// returns all stakeholders
        /// </summary>
        /// <returns></returns>
        public List<StakeholderModel> GetAllStakeholders()
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            List<StakeholderModel> list = new List<StakeholderModel>();
            foreach (Stakeholder st in ctx.Stakeholders)
            {
                list.Add(new StakeholderModel(st.Id, st.Name));
            }
            return list;
        }
    }
}