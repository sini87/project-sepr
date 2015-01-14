using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Client
{
    public class UserSession
    {
        private List<TableRow> tagsTRs, stakeholdersTRs, factorsTRs, artefactsTRs, documentsTRs, alternativesTRs;
        private int nextTTRKey, nextSTRKey, nextFTRKey, nextATRKey, nextALTKey;
        
        public UserSession()
        {
            tagsTRs = new List<TableRow>();
            nextTTRKey = 0;
            stakeholdersTRs = new List<TableRow>();
            nextTTRKey = 0;
            factorsTRs = new List<TableRow>();
            nextFTRKey = 0;
            artefactsTRs = new List<TableRow>();
            nextATRKey = 0;
            documentsTRs = new List<TableRow>();
            nextALTKey = 0;
            alternativesTRs = new List<TableRow>();
        }

        public void CreateIssueEntered()
        {
            tagsTRs = new List<TableRow>();
            nextTTRKey = 0;
            stakeholdersTRs = new List<TableRow>();
            nextTTRKey = 0;
            factorsTRs = new List<TableRow>();
            nextFTRKey = 0;
            artefactsTRs = new List<TableRow>();
            nextATRKey = 0;
            documentsTRs = new List<TableRow>();
            nextALTKey = 0;
            alternativesTRs = new List<TableRow>();
        }

        public List<TableRow> TagsTRs
        {
            get
            {
                return tagsTRs;
            }
        }

        public List<TableRow> StakeholdersTRs
        {
            get
            {
                return stakeholdersTRs;
            }
        }

        public List<TableRow> FactorTRs
        {
            get
            {
                return factorsTRs;
            }
        }

        public List<TableRow> ArtefactsTRs
        {
            get
            {
                return artefactsTRs;
            }
        }

        public List<TableRow> DocumentsTRs
        {
            get
            {
                return documentsTRs;
            }
        }

        public List<TableRow> AlternativesTRs
        {
            get
            {
                return alternativesTRs;
            }
        }

        /// <summary>
        /// Next id for tags table row
        /// </summary>
        public string NextTTRID
        {
            get
            {
                nextTTRKey++;
                return nextTTRKey.ToString();
            }
        }

        /// <summary>
        /// Next id for stakeholders table row
        /// </summary>
        public string NextSTRID
        {
            get
            {
                nextSTRKey++;
                return nextSTRKey.ToString();
            }
        }

        /// <summary>
        /// Next id for influence factor table row
        /// </summary>
        public string NextFTRID
        {
            get
            {
                nextFTRKey++;
                return nextFTRKey.ToString();
            }
        }

        /// <summary>
        /// Next id for artefact table row
        /// </summary>
        public string NextATRID
        {
            get
            {
                nextATRKey++;
                return nextATRKey.ToString();
            }
        }

        /// <summary>
        /// Next id for alternative table row
        /// </summary>
        public string NextALTID
        {
            get
            {
                nextALTKey++;
                return nextALTKey.ToString();
            }
        }
    }
}