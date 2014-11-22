using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class RepositoryBase
    {
        protected static DataClassesDataContext ctx;

        public RepositoryBase()
        {
            if (ctx == null)
            {
                ctx = new DataClassesDataContext();
            }
        }
    }
}