using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDDSS_API.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestClass]
    public class GetStakeholder
    {
        [TestMethod]
        public void getAllStakeholders()
        {
            try
            {

                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Stakeholders";
                RestClient.Instance.Method = HttpVerb.GET;
                var resp = RestClient.Instance.MakeRequest();

                if (!resp.Equals("null"))
                {
                    List<StakeholderModel> list = JsonConvert.DeserializeObject<List<StakeholderModel>>(resp);
                }
                

                Assert.AreNotEqual(resp, "");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }

        }
    }
}
