using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDDSS_API.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestClass]
    public class Tag
    {
        [TestMethod]
        public void getAllTags()
        {
            try
            {

                RestClient.Instance.Login(Credentials.username, Credentials.password);
                RestClient.Instance.EndPoint = "api/Tags";
                var resp = RestClient.Instance.MakeRequest();

                List<TagModel> t = JsonConvert.DeserializeObject<List<TagModel>>(resp);

                Assert.AreNotEqual(resp, "");
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }
    }
}
