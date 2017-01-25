using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace FourSquareAPITests
{
    [TestClass]
    public class PhotosTests:TestBase
    {
        [TestMethod]
        public void TestGetPhotoById()
        {
            var photo = client.GetPhoto("4d0fb8162d39a340637dc42b");

            Assert.IsNotNull(photo);
 

        }
    }
}
