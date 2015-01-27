using CDDSS_API.Models;
using CDDSS_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// class for database operations regarding an Artefact
    /// </summary>
    public class ArtefactsRepository : RepositoryBase
    {
        /// <summary>
        /// returns all artefacts
        /// </summary>
        /// <returns></returns>
         public List<ArtefactModel> GetAllArtefacts(){
             DataClassesDataContext ctx = new DataClassesDataContext();
             List<ArtefactModel> list = new List<ArtefactModel>();
             foreach (Artefact art in ctx.Artefacts)
             {
                 list.Add(new ArtefactModel(art.Id, art.Name));
             }
             return list;
         }   
    }
}