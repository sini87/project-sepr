using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDDSS_API.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestClass]
    public class GetIssue
    {
        static IssueModel i;

        [TestInitialize]
        public void SetUp()
        {
            i = new IssueModel();
            RestClient.Instance.Login(Credentials.username, Credentials.password);
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