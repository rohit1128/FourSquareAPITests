using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace FourSquareAPITests
{
    [TestClass]
    public class VenueTests : TestBase
    {
        [TestMethod]
        public void TestGetVenueById()
        {
            var venue = client.GetVenue("51659475e4b0c32fc37ddc71");

            Assert.AreEqual("Rocketmiles HQ", venue.name);
            Assert.AreEqual("Chicago", venue.location.city);
            Assert.AreEqual("IL", venue.location.state);
            Assert.AreEqual("United States", venue.location.country);
            Assert.AreEqual("60654", venue.location.postalCode);
        }

        [TestMethod]
        public void TestGetVenueByIdWithNonExistentId()
        {
            Exception exception = null;
            try
            {
                client.GetVenue("-1");
            }
            catch(Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message == "The remote server returned an error: (400) Bad Request.");
        }

        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void TestGetVenueByIdWithNoId()
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
        public void TestVenueSpecials()
        {
            var venue = client.GetVenue("4b19c7f3f964a5200de423e3");
            var specials = venue.specials;
            var special = venue.specials.items.FirstOrDefault();

            Assert.AreEqual("Catch 35 Chicago", venue.name);
            Assert.IsTrue(specials.items.Count > 0);
            Assert.AreEqual("57fe52bf38fa1ab6b3b58925", special.id);
            Assert.IsTrue(special.unlocked);
            Assert.AreEqual("MONDAY-FRIDAY • Half off bar food menu from 2-5pm. (excluding lobster items/Bar Area Only)", special.message);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void TestVenueSpecialsWithOutVenueId()
        {
                client.GetVenue("");
        }

        [TestMethod]
        public void TestVenueCategories()
        {
            var venueCategories = client.GetVenueCategories();
            var venueCategory = venueCategories.First(v => v.name == "Arts & Entertainment");

            Assert.IsNotNull(venueCategory);
            Assert.AreEqual("Arts & Entertainment", venueCategory.name);
            Assert.AreEqual("Arts & Entertainment", venueCategory.pluralName);
            Assert.AreEqual("4d4b7104d754a06370d81259", venueCategory.id);
            Assert.AreEqual("https://ss3.4sqi.net/img/categories_v2/arts_entertainment/default_", venueCategory.icon.prefix);
        }

        [TestMethod]
        public void TestVenueCategory()
        {
            var venueCategories = client.GetVenueCategories();
            var venueCategory = venueCategories.First(v => v.name == "Arts & Entertainment");
            var category = venueCategory.categories.First(vc => vc.name == "Arcade");

            Assert.IsNotNull(category);
            Assert.AreEqual("Arcade", category.name);
            Assert.AreEqual("Arcades", category.pluralName);
            Assert.AreEqual("4bf58dd8d48988d1e1931735", category.id);
            Assert.AreEqual("https://ss3.4sqi.net/img/categories_v2/arts_entertainment/arcade_", category.icon.prefix);
        }

        [Ignore]
        [TestMethod]
        public void TestVenueAdd()
        {
            string name = "testVenue";
            string address = "1234 Chicago Ave";
            string city = "Test City";
            string state = "Test State";
            string zip = "555555";
            string phone = "12234566";
            string primaryCategoryId = "4bf58dd8d48988d104941735";


            var venue= client.AddVenue(new Dictionary<string, string>
            {
                { "name", name},
                { "address" ,address},
                {" city" , city },
                { "state", state },
                { "zip", zip },
                { "phone" ,phone },
                { "primaryCategoryId" , primaryCategoryId}
            });

            Assert.IsNotNull(venue);
            Assert.AreEqual(name, venue.name);
            Assert.AreEqual(address, venue.location.address);
            Assert.AreEqual(city, venue.location.city);
            Assert.AreEqual(state, venue.location.state);
            Assert.AreEqual(zip, venue.location.postalCode);
            Assert.AreEqual(phone, venue.contact.phone);
            Assert.AreEqual(primaryCategoryId, venue.categories.First().id);

            string revisedName = "test 2";
            string revisedAddress = "test address";

            client.SetVenueProposeEdit(venue.id, new Dictionary<string, string>
            {
                { "name", revisedName},
                { "address" , revisedAddress}
            }
            );
        }

        [TestMethod]
        public void TestTrendingVenues()
        {
            var trendingVenues = client.GetTrendingVenues(new Dictionary<string, string>
            {
                { "ll","41.8,87.6" }
            }
            );
            var trendingVenue = trendingVenues.Where(v => v.name == "Cafecito");
            Assert.IsTrue(trendingVenue.Count() == 1);
            

        }

        [TestMethod]
        public void TestVenueTips()
        {
            // get venue tips for longman and eagle
            var venueTips = client.GetVenueTips("4b469be2f964a5206e2526e3");
            Assert.IsTrue(venueTips.Count > 0);
            var venueTip = venueTips.First(vt => vt.id == "57c5de01498eb62469105fc1");
            Assert.IsNotNull(venueTip);
            Assert.AreEqual("If you’re nursing a hangover, try the PBR breakfast—two eggs, bacon or sausage, maple glazed spam, house potatoes, can of PBR—along with a shot of whisky to help with the recovery process.", venueTip.text);
            Assert.AreEqual("Eater", venueTip.user.firstName);

        }


        [TestMethod]
        public void TestVenuesLinks() 
        {
            var venueLinks = client.GetVenueLinks("4a7636bcf964a5209ee21fe3");
            Assert.IsTrue(venueLinks.Count() > 0);
        
        }
    }
}
