using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FourSquareAPITests
{
    [TestClass]
    public class TipsTests:TestBase
    {
        [TestMethod]
        public void TestGetTipById()
        {
            var tip = client.GetTip("4d0fb8162d39a340637dc42b");

            Assert.IsNotNull(tip);
            Assert.AreEqual("4b5e662a70c603bba7d790b4", tip.id);
            Assert.AreEqual("Get two slices and a can of soda for only $2.75!", tip.text);
            Assert.AreEqual("4a89cd2ff964a520100920e3", tip.venue.id);
        }

        [TestMethod]
        public void TestGetTipByIdWithNoId()
        {
            Exception exception = null;
            try
            {
                client.GetVenue("");
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message == "The remote server returned an error: (404) Not Found.");
        }

        [TestMethod]
        public void TestGetTipByIdWithBadId()
        {
            Exception exception = null;
            try
            {
                client.GetVenue("-1");
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message == "The remote server returned an error: (400) Bad Request.");
        }

        //don't want to run in production
        [Ignore]
        [TestMethod]
        public void TestAddTip()
        {
            var venueId = "4fb807cbe4b04c16895353bc";
            var text = "The donuts are tasty!";
            var tip=client.AddTip(new System.Collections.Generic.Dictionary<string, string>
            {
                {"venueId",  venueId },
                {"text", text}
            });

            Assert.IsNull(tip);
            Assert.AreEqual(venueId, tip.venue.id);
            Assert.AreEqual(text, tip.text);
        }

        //don't want to run in production
        [Ignore]
        [TestMethod]
        public void TestAddTipWithoutTextThrowsException()
        {
            var venueId = "4fb807cbe4b04c16895353bc";

            Exception exception = null;
            try
            {
                var tip = client.AddTip(new System.Collections.Generic.Dictionary<string, string>
            {
                {"venueId",  venueId },
            });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            
        }

        //don't want to run in production
        [Ignore]
        [TestMethod]
        public void TestTipsToDo()
        {
            var tipId = "4bb8f41970c603bb64bf96b4";
            var todo=client.SetTipToDo(tipId);

            Assert.IsNull(todo);
            Assert.AreEqual(tipId, todo.tip);
        }


    }
}
