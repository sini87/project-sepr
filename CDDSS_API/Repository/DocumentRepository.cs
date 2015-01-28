using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CDDSS_API.Models.Domain;

namespace CDDSS_API.Repository
{
    /// <summary>
    /// Used for Operations with table document
    /// </summary>
    public class DocumentRepository : RepositoryBase
    {
        /// <summary>
        /// returns a list of documents which are attached to the issue
        /// </summary>
        /// <param name="issueId">issue id</param>
        /// <returns>list of documents</returns>
        public List<string> GetFilesByIssueId(int issueId)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            var query = from Documents in ctx.Documents
                        where
                          Documents.Issue == 1
                        select new
                        {
                            Documents.Name
                        };
            List<string> list = new List<string>();
            foreach (var filename in query.ToList())
            {
                list.Add(filename.Name);
            }
            return list;

        }
        
        /// <summary>
        /// checks if file already exists
        /// </summary>
        /// <param name="issue">issue id</param>
        /// <param name="filename">filename</param>
        /// <returns>true if file already exists</returns>
        public bool CheckFileExists(int issue, string filename)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            if (ctx.Documents.Where(x => x.Issue == issue && x.Name == filename).Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// loads a file into the database
        /// </summary>
        /// <param name="issue">issue id to wich file is attached</param>
        /// <param name="filename">name of the file</param>
        /// <param name="inputStream">stream of the file</param>
        /// <returns>returns false if upload was not successful, because a file with the same filename  exists</returns>
        public bool UploadFile(int issue, string filename, Stream inputStream )
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            if (!CheckFileExists(issue, filename))
            {
                Document entity = new Document();
                MemoryStream memoryStream = new MemoryStream();
                inputStream.CopyTo(memoryStream);
                entity.File = memoryStream.ToArray();
                entity.Issue = issue;
                entity.Name = filename;
                ctx.Documents.InsertOnSubmit(entity);
                ctx.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// returns a file from the database
        /// </summary>
        /// <param name="issueId">issue of the file</param>
        /// <param name="filename">filename</param>
        /// <returns>stream of the file</returns>
        public Stream GetFile(int issueId, string filename)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            var query = from Documents in ctx.Documents
                        where
                          Documents.Issue == issueId &&
                          Documents.Name == filename
                        select new
                        {
                            Documents.File
                        };
            if (query.FirstOrDefault() == null)
            {
                return null;
            }
            else
            {
                MemoryStream ms = new MemoryStream(query.FirstOrDefault().File.ToArray());
                return ms;
            }
        }

        public bool DeleteDocument(int issueId, string filename)
        {
            DataClassesDataContext ctx = new DataClassesDataContext();
            Document doc = ctx.Documents.Where(x => x.Issue == issueId).First(x => x.Name == filename);
            if (doc != null)
            {
                ctx.Documents.DeleteOnSubmit(doc);
                ctx.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}