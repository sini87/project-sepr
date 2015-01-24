using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace UnitTests
{
    [TestClass]
    public class Authentication
    {
        [TestMethod]
        public void LoginSuccess()
        {
            Assert.IsTrue(RestClient.Instance.Login(Credentials.username, Credentials.password));
        }

        [TestMethod]
        public void LogOut()
        {
            Assert.IsTrue(RestClient.Instance.LogOut());
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void LoginFail()
        {
            var login = RestClient.Instance.Login("randomUser", "xxx");           
        }
    }
}
