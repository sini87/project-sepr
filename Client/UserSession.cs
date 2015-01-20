using CDDSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public class UserSession
    {
        private List<TableRow> tagsTRs, stakeholdersTRs, factorsTRs, artefactsTRs, documentsTRs, accessRTRs;

        public List<TableRow> AccessRTRs
        {
            get { return accessRTRs; }
            set { accessRTRs = value; }
        }
        private int nextTTRKey, nextSTRKey, nextFTRKey, nextATRKey, nextAccessTRKey;
        private IssueModel detailIssue;
        private List<Control> issueTags;
        private TextBox titleText, descriptionText;
        private List<String> docsToDelete;

        public List<String> DocsToDelete
        {
            get { return docsToDelete; }
            set { docsToDelete = value; }
        }

        public TextBox DescriptionText
        {
            get { return descriptionText; }
            set { descriptionText = value; }
        }

        public TextBox TitleText
        {
            get { return titleText; }
            set { titleText = value; }
        }
        
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
            detailIssue = null;
            docsToDelete = new List<string>();

            issueTags = new List<Control>();
            accessRTRs = new List<TableRow>();
            nextAccessTRKey = 0;
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
            detailIssue = null;
            issueTags = new List<Control>();
            accessRTRs = new List<TableRow>();
            nextAccessTRKey = 0;
            docsToDelete = new List<string>();
        }

        public int NextAccesTRKey
        {
            get
            {
                nextAccessTRKey++;
                return nextAccessTRKey;
            }
        }

        public List<Control> IssueTags
        {
            get
            {
                return issueTags;
            }
        }

        public IssueModel DetailIssue
        {
            get
            {
                return detailIssue;
            }

            set
            {
                detailIssue = value;
            }
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
    }
}