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
        private List<TableRow> tagsTRs, stakeholdersTRs, factorsTRs, artefactsTRs, documentsTRs, accessRTRs, criteriaTRs, criterionWeightTRs, alternativesTRs, ratingsTRs;
        private int nextTTRKey, nextSTRKey, nextFTRKey, nextATRKey, nextAccessTRKey, nextDocTRKey, nextCritTRKey, nextAltKey;
        private IssueModel detailIssue;
        private List<Control> issueTags;
        private TextBox titleText, descriptionText;
        private List<String> docsToDelete, messages;
        private List<int> criteriasToDelete, alternativesToDelete;
        private int finalDecisionIssueId;
        private List<RadioButton> finDecAlternativesRBs;
        private bool finalDecisionExists;
       
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
            messages = new List<string>();

            issueTags = new List<Control>();
            accessRTRs = new List<TableRow>();
            nextAccessTRKey = 0;
            nextDocTRKey = 0;
            criteriaTRs = new List<TableRow>();
            nextCritTRKey = 0;
            criteriasToDelete = new List<int>();
            criterionWeightTRs = new List<TableRow>();
            alternativesTRs = new List<TableRow>();
            nextAltKey = 0;
            alternativesToDelete = new List<int>();
            ratingsTRs = new List<TableRow>();
            finDecAlternativesRBs = new List<RadioButton>();
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
            nextDocTRKey = 0;
            criteriaTRs = new List<TableRow>();
            nextCritTRKey = 0;
            criteriasToDelete = new List<int>();
            criterionWeightTRs = new List<TableRow>();
            alternativesTRs = new List<TableRow>();
            nextAltKey = 0;
            alternativesToDelete = new List<int>();
            finDecAlternativesRBs = new List<RadioButton>();
        }

        public List<int> CriteriasToDelete
        {
            get { return criteriasToDelete; }
            set { criteriasToDelete = value; }
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

        public string NextCritTRKey
        {
            get
            {
                nextCritTRKey++;
                return nextCritTRKey.ToString();
            }
        }

        public List<TableRow> CriteriaTRs
        {
            get { return criteriaTRs; }
            set { criteriaTRs = value; }
        }

        public List<TableRow> AccessRTRs
        {
            get { return accessRTRs; }
            set { accessRTRs = value; }
        }

        /// <summary>
        /// returns next key for document table
        /// </summary>
        public string NextDocTRKey
        {
            get
            {
                nextDocTRKey++;
                return nextDocTRKey.ToString();
            }
        }

        public List<TableRow> CriterionWeightTRs
        {
            get { return criterionWeightTRs; }
            set { criterionWeightTRs = value; }
        }

        public List<String> Messages
        {
            get { return messages; }
        }

        public List<TableRow> AlternativesTRs
        {
            get { return alternativesTRs; }
            set { alternativesTRs = value; }
        }
 
        public int NextAltKey
        {
            get {
                nextAltKey++;
                return nextAltKey; 
            }
            set { nextAltKey = value; }
        }

        public List<int> AlternativesToDelete
        {
            get { return alternativesToDelete; }
            set { alternativesToDelete = value; }
        }

        public List<TableRow> RatingsTRs
        {
            get { return ratingsTRs; }
            set { ratingsTRs = value; }
        }

        public bool FinalDecisionExists
        {
            get
            {
                return finalDecisionExists;
            }

            set
            {
                finalDecisionExists = value;
            }
        }

        public List<RadioButton> FinDecAlternativesRBs
        {
            get
            {
                return finDecAlternativesRBs;
            }

            set
            {
                finDecAlternativesRBs = value;
            }
        }

        public int FinalDecisionIssueId
        {
            get
            {
                return finalDecisionIssueId;
            }

            set
            {
                finalDecisionIssueId = value;
            }
        }

    }

}