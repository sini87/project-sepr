using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDDSS_API.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestClass]
    public class Issue
    {
        int issueId;
        IssueModel i = new IssueModel();

        [TestMethod]
        public void postCompleteIssueSuccessfull()
        {
            try
            {
                i.Title = "Test Issue";
                i.Description = "Description";
                i.InfluenceFactors.Add(new InfluenceFactorModel("Budget", true, "max 100K"));
                i.Stakeholders.Add(new StakeholderModel("Manager"));
                i.Artefacts.Add(new ArtefactModel("Artefact"));
                i.Artefacts.Add(new ArtefactModel(1, "ein artefacwerdt"));
                i.Tags.Add(new TagModel(1, ""));

                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Issue/Create";
                RestClient.Instance.Method = HttpVerb.POST;
                RestClient.Instance.PostData = JsonConvert.SerializeObject(i);

                RestClient.Instance.MakeRequest();
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        public void EditIssueSuccessfull()
        {
            try
            {
                i.Title = "Edited Issue";
                i.Description = "Edited Description";
                
                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Issue/Edit";
                RestClient.Instance.Method = HttpVerb.POST;
                RestClient.Instance.PostData = JsonConvert.SerializeObject(i);

                RestClient.Instance.MakeRequest();
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }
        //[TestMethod]
        //public void postIncompleteIssueSuccessfull()
        //{
        //    try
        //    {
        //        IssueModel i = new IssueModel();
        //        i.Title = "Test Issue";

        //        RestClient.Instance.Login(Credentials.username, Credentials.password);
        //        RestClient.Instance.EndPoint = "api/Issue/Create";
        //        RestClient.Instance.Method = HttpVerb.POST;
        //        RestClient.Instance.PostData = JsonConvert.SerializeObject(i);

        //        RestClient.Instance.MakeRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail("Exception: " + e.Message);
        //    }
        //}

        [TestMethod]
        public void getAllIssuesOfUserSuccessBecauseLoggedIn()
        {
            try
            {
                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Issue/OfUser";
                RestClient.Instance.Method = HttpVerb.GET;
                var json = RestClient.Instance.MakeRequest();

                List<IssueModel> issue = JsonConvert.DeserializeObject<List<IssueModel>>(json);
                foreach (IssueModel i in issue)
                {
                    issueId = i.Id;
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void getAllIssuesOfUserFailBecauseNotLoggedIn()
        {
            try
            {
                RestClient.Instance.LogOut();
                RestClient.Instance.EndPoint = "api/Issue/OfUser";
                RestClient.Instance.Method = HttpVerb.GET;
                var json = RestClient.Instance.MakeRequest();
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void getIssueByIdSuccessfull()
        {
            try
            {
                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Issue";
                RestClient.Instance.Method = HttpVerb.GET;
                RestClient.Instance.MakeRequest("?issueId=" + issueId);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void getIssueByIdNotSuccessfull()
        {
                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Issue";
                RestClient.Instance.Method = HttpVerb.GET;
                var json = RestClient.Instance.MakeRequest("?issueId=11111");
            
                Assert.AreEqual(json, "null");
        }
        
        [TestMethod]
        public void deleteIssueSuccessfull()
        {
            try
            {
                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Issue";
                RestClient.Instance.Method = HttpVerb.DELETE;
                RestClient.Instance.MakeRequest("?issueId=" + issueId);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }
    }
}