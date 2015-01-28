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
            try
            {
                Assert.IsTrue(RestClient.Instance.Login(Credentials.username, Credentials.password));
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
            
        }

        [TestMethod]
        public void LogOutSuccess()
        {
            try
            {
                Assert.IsTrue(RestClient.Instance.LogOut());
            }
            catch (Exception e)
            {
                Assert.Fail("Exception: " + e.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void LoginFailWrongUser()
        {
            var login = RestClient.Instance.Login("randomUser", "xxx");           
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void LoginFailNoUser()
        {
            var login = RestClient.Instance.Login("", Credentials.password);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void LoginFailNoPassword()
        {
            var login = RestClient.Instance.Login(Credentials.username, "");
        }
    }
}
