
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using System;
using System.Threading;

using OpenQA.Selenium.Remote;

namespace SeleniumUITests.Tests.Library.UI.Assess
{
    [TestFixture]
    public class CreateNewItemTests
    {
        private TestContext _testContextInstance;

        ///<summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        //public TestContext TestContext { get; set; }
        public TestContext TestContext
        {
            get { return _testContextInstance; }
            set { _testContextInstance = value; }
        }

        [SetUp]
        public void Initialize()
        {
            //TODO: put the WebDriver initializer here...
            //IWebDriver driver = DriverInitializer.InvokeWebDriver(whatAmI);
        }

        [TearDown]
        public void Cleanup()
        {
            DriverInitializer.QuitWebDriver();
        }

       

        /// <summary>
        /// Create a Matching item from the Create New Item page
        /// </summary>
        /// <remarks>
        /// Test Case #48169
        /// SingleTenant
        /// </remarks>
        [Test]
        public void Create_A_Matching_Item_From_The_Create_New_Item_Page_ST()
        {
            Create_A_Matching_Item_From_The_Create_New_Item_Page();
        }

        /// <summary>
        /// Create a Matching item from the Create New Item page
        /// </summary>
        /// <remarks>Test Case #48169</remarks>
        /// <param name="whatAmI">Global test properties describing what this test instance is.</param>
        public void Create_A_Matching_Item_From_The_Create_New_Item_Page()
        {
            //GIVEN the user logs into the application
            IWebDriver driver = DriverInitializer.InvokeWebDriver();
            try
            {
                AuthenticationPage authentication = new AuthenticationPage(driver);
                authentication.DataObject = getLoginData();
                HomePage home = authentication.Login();
                home.Logout();
                authentication.VerifySignedOut();
            }
            catch (Exception e)
            {
                //new DriverCommands(driver).GetScreenshotAndPageSource();
                throw new Exception("\nInnerException:\n" + e.InnerException + "\nStackTrace:\n" + e.StackTrace, e);
            }
            
        }

        private AuthenticationData getLoginData()
        {
            AuthenticationData data = new AuthenticationData();
            //WebHelperConfigSection config = (WebHelperConfigSection)ConfigurationManager.GetSection("SeleniumWebHelper");
            data.DistrictName = "";
            data.Username = "sn_qa";
            data.Password = "sch00lnet";
            return data;
        }

        

    }
}
