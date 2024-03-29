﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using CDDSS_API.Models;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Issue
    {
        static IssueModel i = new IssueModel();

        static ArtefactModel a, a2;
        static InfluenceFactorModel ifm;
        static TagModel t;
        static StakeholderModel s;

        [TestInitialize]
        public void SetUp()
        {
            RestClient.Instance.Login(Credentials.username, Credentials.password);
        }

        [TestCleanup]
        public void TearDown()
        {
            RestClient.Instance.LogOut();
        }

        [TestMethod]
        public void createIssueSuccessfull()
        {
            try
            {
                
                a = new ArtefactModel("TestArtefact");
                a2 = new ArtefactModel(1, "TestArtefact2");
                ifm = new InfluenceFactorModel("TestBudget", true, "max 100K");
                t = new TagModel(1, "TestTag");
                s = new StakeholderModel("TestManager");

                i.Title = "Test Issue";
                i.Description = "Description";
                i.InfluenceFactors.Add(ifm);
                i.Stakeholders.Add(s);
                i.Artefacts.Add(a);
                i.Artefacts.Add(a2);
                i.Tags.Add(t);

                RestClient.Instance.EndPoint = "api/Issue/Create";
                RestClient.Instance.Method = HttpVerb.POST;
                RestClient.Instance.PostData = JsonConvert.SerializeObject(i);

                var resp = RestClient.Instance.MakeRequest();

                resp = resp.TrimStart('\"');
                resp = resp.TrimEnd('\"');

                i.Id = int.Parse(resp);

                Assert.AreNotEqual(resp, "");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void getAllIssuesOfUserSuccessBecauseLoggedIn()
        {
            try
            {
                RestClient.Instance.EndPoint = "api/Issue/OfUser";
                RestClient.Instance.Method = HttpVerb.GET;
                var json = RestClient.Instance.MakeRequest();

                if (!json.Equals("null"))
                {
                    List<IssueModel> issue = JsonConvert.DeserializeObject<List<IssueModel>>(json);
                    foreach (IssueModel iss in issue)
                    {
                        i.Id = iss.Id;
                    }
                }

                Assert.AreNotEqual(json, "");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void editIssueSuccessfull()
        {
            try
            {
                i.Title = "Edited Issue";
                i.Description = "Edited Description";


                RestClient.Instance.EndPoint = "api/Issue/Edit";
                RestClient.Instance.Method = HttpVerb.POST;
                RestClient.Instance.PostData = JsonConvert.SerializeObject(i);

                var resp = RestClient.Instance.MakeRequest();

                Assert.AreEqual(resp, "OK");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void setIssueToNextStageSuccessfull()
        {
            try
            {
                RestClient.Instance.EndPoint = "api/Issue/" + i.Id + "/nextStage";
                RestClient.Instance.Method = HttpVerb.POST;
                var resp = RestClient.Instance.MakeRequest();

                Assert.AreEqual(resp, "OK");
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
                RestClient.Instance.EndPoint = "api/Issue";
                RestClient.Instance.Method = HttpVerb.GET;
                var resp = RestClient.Instance.MakeRequest("?issueId=" + i.Id);

                Issue iss = JsonConvert.DeserializeObject<Issue>(resp);

                Assert.AreNotEqual(resp, "");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void getIssueByIdNotSuccessfull()
        {
            try
            {
                RestClient.Instance.EndPoint = "api/Issue";
                RestClient.Instance.Method = HttpVerb.GET;
                var resp = RestClient.Instance.MakeRequest("?issueId=11111");

                Assert.AreEqual(resp, "null");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        public void deleteIssueSuccessfull()
        {
            try
            {
                RestClient.Instance.EndPoint = "api/Issue";
                RestClient.Instance.Method = HttpVerb.DELETE;
                var resp = RestClient.Instance.MakeRequest("?issueId=" + i.Id);

                Assert.AreEqual(resp, "OK");
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

                Assert.AreEqual(json, "");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }        
    }
}
