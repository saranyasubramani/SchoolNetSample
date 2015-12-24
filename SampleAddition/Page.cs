using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;



    public abstract class Page : BaseDriver
    {
        private Object _dataObject;
        public virtual Object DataObject
        {
            get
            {
                return _dataObject;
            }
            set
            {
                _dataObject = value;
            }
        }
        private string _expectedUrl;
        public string ExpectedUrl
        {
            get
            {
                return _expectedUrl;
            }
            set
            {
                _expectedUrl = value;
            }
        }
        public Page Parent { get; set; }
        public string PageName { get; set; }
        public virtual Form Form { get; set; }
        public virtual Detail Detail { get; set; }
        //protected DriverCommands DriverCommands;
        public string ControlPrefix = "ctl00_MainContent_";
        public string CurrentWindowHandle { get; set; }

        public Page(IWebDriver driver) : base(driver)
        {
           // DriverCommands = new DriverCommands(Driver);
            try
            {
                WaitAndMeasurePageLoadTime();
                //get the current window that is in focus
                //CurrentWindowHandle = Driver.CurrentWindowHandle;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to get current window handle on page load: " + e.Message);
                try
                {
                    WaitAndMeasurePageLoadTime();
                    //get the current window that is in focus
                    CurrentWindowHandle = Driver.CurrentWindowHandle;
                }
                catch (Exception e2)
                {
                    throw new Exception("Failed to get current window handle on page load.", e2);
                }
            }
        }

        public virtual void initElements()
        {
        }

        protected void PrintPageName(string pageName)
        {
            PageName = pageName;
            Debug.WriteLine("\nLoading the page: '" + PageName + "'...");
        }

        public void GoToUrl(string url)
        {
            this.Driver.Navigate().GoToUrl(url);
        }

        public void VerifyCurrentUrl()
        {
            try
            {
               
                string actualUrl = this.Driver.Url;
                Debug.WriteLine("Verifying the actual URL: '" + actualUrl + "' contains the expected URL: '" + this.ExpectedUrl + "'.");
                //perform verification test
                Assert.IsTrue(actualUrl.ToLower().Contains(this.ExpectedUrl.ToLower()),
                        "The actual page URL: '" + actualUrl + "' does not contain the expected page URL: '" + this.ExpectedUrl + "'.");
                Debug.WriteLine("Verified the actual URL: '" + actualUrl + "' contains the expected URL: '" + this.ExpectedUrl + "'.");
            }
            catch (AssertionException e)
            {
                throw new AssertionException(e.Message, e);
            }
        }

        public void VerifyCurrentUrlQueryStringParameters(String queryStringParameter)
        {
            try
            {
                //is this a mock driver for debugging?
               
                string actualUrl = this.Driver.Url;
                Debug.WriteLine("Verifying the actual URL: '" + actualUrl + "' contains the expected query string parameter: '" + queryStringParameter + "'.");
                //perform verification test
                if (! actualUrl.ToLower().Contains(queryStringParameter.ToLower()))
                {
                    queryStringParameter = queryStringParameter.Replace("/", "%2F");
                    if (! actualUrl.ToLower().Contains(queryStringParameter.ToLower()))
                    {
                        Assert.Fail("The actual page URL: '" + actualUrl + "' does not contain the expected query string parameter: '" + queryStringParameter + "'.");
                    }
                }
                Debug.WriteLine("Verified the actual URL: '" + actualUrl + "' contains the expected query string parameter: '" + queryStringParameter + "'.");
            }
            catch (AssertionException e)
            {
                throw new AssertionException(e.Message, e);
            }
        }

        public void WaitAndMeasurePageLoadTime()
        {
            System.Threading.Thread.Sleep(5000);
        }

        //public void WaitForPageToLoad()
        //{
        //    DriverCommands.WaitForPageToLoad(90);
        //}

        //public void WaitForSpinnersToVanish()
        //{
        //    DriverCommands.WaitForSpinnersToVanish(30);
        //}

        //public void SwitchToWindow()
        //{
        //    DriverCommands.WaitToSwitchWindow(CurrentWindowHandle, 90.0);
        //}
    }
