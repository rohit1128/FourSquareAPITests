using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace FourSquareAPITests
{
    [TestClass]
    public class SpecialsTests : TestBase
    {
        [TestMethod]
        public void TestGetSpecialById()
        {
            var special = client.GetSpecial("57fe52bf38fa1ab6b3b58925");

            Assert.IsNotNull(special);
            Assert.AreEqual("MONDAY-FRIDAY • Half off bar food menu from 2-5pm. (excluding lobster items/Bar Area Only)", special.description);

        }


        [TestMethod]
        public void TestSpecialsSearch()
        {
            var specials = client.SearchSpecials(new Dictionary<string, string>
            {
                {"ll","40.7,73.9"}
            });

            Assert.IsNotNull(specials);
     

        }
    }
}
