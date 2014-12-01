using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    public class RatingRepository : RepositoryBase
    {
        internal Rating getRating(int criterionId, int alternativeId)
        {
            RatingModel rating = new RatingModel();
            return null;
        }
    }
}