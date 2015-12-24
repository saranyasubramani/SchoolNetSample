using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework;


    public class HomePage : Page
    {
        public HomePage(IWebDriver driver)
            : base(driver)
        {
            this.PrintPageName("Home");
            this.ExpectedUrl = "/myschoolnet/";
            //this.Header = new Header(driver);
            this.VerifyCurrentUrl();
            initElements();
        }
        public By BySignOut { get { return By.Id("ctl00_AppHeader_UserStatus_linkLogout"); } }
        public WebElementWrapper SignOut { get; set; }

        public override void initElements()
        {
            Detail = new HomeDetail(Driver);
            Detail.Parent = this;
            SignOut = new WebElementWrapper(Driver, BySignOut);
            //Detail.FocusOnMainContentArea();
           
        }

        public new HomeDetail Detail { get; private set; }

        public void GoHome()
        {
            //initially wait for the page to load, so a previous post will not fail
            this.WaitAndMeasurePageLoadTime();
            string baseUrl = "";
            //is this a mock driver for debugging?
            
                baseUrl = DriverInitializer.StartingUrlSingleTenant;
            
            string expectedUrl = "/myschoolnet";
            this.PrintPageName("Home");
            Debug.WriteLine("Loading a new page at URL: " + baseUrl + expectedUrl);
            Driver.Navigate().GoToUrl(baseUrl + expectedUrl);
            this.WaitAndMeasurePageLoadTime();
            string actualUrl = this.Driver.Url;
            Debug.WriteLine("Verifying the actual URL: '" + actualUrl + "' contains the expected URL: '" + expectedUrl + "'.");
            //perform verification test
            Assert.IsTrue(actualUrl.ToLower().Contains(expectedUrl.ToLower()),
                    "The actual page URL: '" + actualUrl + "' does not contain the expected page URL: '" + expectedUrl + "'.");
            Debug.WriteLine("Verified the actual URL: '" + actualUrl + "' contains the expected URL: '" + expectedUrl + "'.");
        }
        public void Logout()
        {
            GoHome();
            SignOut.Wait(5).Click();
            System.Threading.Thread.Sleep(5000); ;
        }


    }

    public class HomeDetail : Detail
    {
        public HomeDetail(IWebDriver driver) : base(driver)
        {
            initElements();
        }

        public override void initElements()
        {
        }

        public override void VerifyFieldsExists()
        {
            throw new System.NotImplementedException();
        }

        public override void VerifyContentExists()
        {
            throw new System.NotImplementedException();
        }
    }
