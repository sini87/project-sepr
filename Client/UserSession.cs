using CDDSS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    /// <summary>
    /// class is used for storing dynamically created GUI elements for a session
    /// </summary>
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
       
        /// <summary>
        /// constructor
        /// </summary>
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

        /// <summary>
        /// this method is called, when some site is entered (not post back)
        /// resets all GUI elements
        /// </summary>
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

        /// <summary>
        /// crterias to delete collections (criteria IDs)
        /// </summary>
        public List<int> CriteriasToDelete
        {
            get { return criteriasToDelete; }
            set { criteriasToDelete = value; }
        }

        /// <summary>
        /// returns next ID a new accessRights 
        /// </summary>
        public int NextAccesTRKey
        {
            get
            {
                nextAccessTRKey++;
                return nextAccessTRKey;
            }
        }

        /// <summary>
        /// list of issueTags controls in issuePanel
        /// </summary>
        public List<Control> IssueTags
        {
            get
            {
                return issueTags;
            }
        }

        /// <summary>
        /// stores current issue
        /// </summary>
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

        /// <summary>
        /// tablerows for tagsTable
        /// </summary>
        public List<TableRow> TagsTRs
        {
            get
            {
                return tagsTRs;
            }
        }

        /// <summary>
        /// table rows for stakeholder table
        /// </summary>
        public List<TableRow> StakeholdersTRs
        {
            get
            {
                return stakeholdersTRs;
            }
        }

        /// <summary>
        /// table rows for influence factor table
        /// </summary>
        public List<TableRow> FactorTRs
        {
            get
            {
                return factorsTRs;
            }
        }

        /// <summary>
        /// table rows for artefacts table
        /// </summary>
        public List<TableRow> ArtefactsTRs
        {
            get
            {
                return artefactsTRs;
            }
        }

        /// <summary>
        /// table rows for documents table
        /// </summary>
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

        /// <summary>
        /// stores documents to delete
        /// </summary>
        public List<String> DocsToDelete
        {
            get { return docsToDelete; }
            set { docsToDelete = value; }
        }

        /// <summary>
        /// stores description textBox of issue
        /// </summary>
        public TextBox DescriptionText
        {
            get { return descriptionText; }
            set { descriptionText = value; }
        }

        /// <summary>
        /// stores title textBox of issue
        /// </summary>
        public TextBox TitleText
        {
            get { return titleText; }
            set { titleText = value; }
        }

        /// <summary>
        /// next criteria Id for new crtieria
        /// </summary>
        public string NextCritTRKey
        {
            get
            {
                nextCritTRKey++;
                return nextCritTRKey.ToString();
            }
        }

        /// <summary>
        /// stores criteria table rows
        /// </summary>
        public List<TableRow> CriteriaTRs
        {
            get { return criteriaTRs; }
            set { criteriaTRs = value; }
        }

        /// <summary>
        /// stores access rights table rows
        /// </summary>
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

        /// <summary>
        /// stores criterion weight table rows
        /// </summary>
        public List<TableRow> CriterionWeightTRs
        {
            get { return criterionWeightTRs; }
            set { criterionWeightTRs = value; }
        }

        /// <summary>
        /// stores some user messages which have to be shown to the user
        /// </summary>
        public List<String> Messages
        {
            get { return messages; }
        }

        /// <summary>
        /// stores alternative table rows
        /// </summary>
        public List<TableRow> AlternativesTRs
        {
            get { return alternativesTRs; }
            set { alternativesTRs = value; }
        }
 
        /// <summary>
        /// returns next key for new alternative
        /// </summary>
        public int NextAltKey
        {
            get {
                nextAltKey++;
                return nextAltKey; 
            }
            set { nextAltKey = value; }
        }

        /// <summary>
        /// stores IDs of alternatives to delete
        /// </summary>
        public List<int> AlternativesToDelete
        {
            get { return alternativesToDelete; }
            set { alternativesToDelete = value; }
        }

        /// <summary>
        /// stores ratings table rows
        /// </summary>
        public List<TableRow> RatingsTRs
        {
            get { return ratingsTRs; }
            set { ratingsTRs = value; }
        }

        /// <summary>
        /// storing information about final decision, if exists
        /// </summary>
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

        /// <summary>
        /// storing radio button decisions of final decision
        /// </summary>
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

        /// <summary>
        /// stores issue ID of final decision
        /// </summary>
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