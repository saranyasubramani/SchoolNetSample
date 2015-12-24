using System;
using System.Diagnostics;
using System.Drawing;
using OpenQA.Selenium;
using Schoolnet.SeleniumWebHelper;
using SeleniumUITests.Main.Library.Utilities;

public abstract class Detail : BaseDriver
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
        public Page Parent { get; set; }
        protected DriverCommands DriverCommands { get; set; }

        public string ControlPrefix = "ctl00_MainContent_";
        public Detail(IWebDriver driver)
            : base(driver)
        {
            DriverCommands = new DriverCommands(Driver);
            GlobalAppHeader = new WebElementWrapper(Driver, ByGlobalAppHeader);
            GlobalNav = new WebElementWrapper(Driver, ByGlobalNav);
            MainContentArea = new WebElementWrapper(Driver, ByMainContentArea);
            AppTitle = new WebElementWrapper(Driver, ByAppTitle);

            //DO NOT CALL initElements() HERE, IT WILL MESS UP THE GRID CODE - PER Martin
        }
        public virtual void initElements()
        {
        }

        protected By ByGlobalAppHeader { get { return By.ClassName("GlobalAppHeader"); } }
        protected WebElementWrapper GlobalAppHeader { get; set; }
        protected By ByGlobalNav { get { return By.ClassName("GlobalNav"); } }
        protected WebElementWrapper GlobalNav { get; set; }
        protected By ByMainContentArea { get { return By.ClassName("MainContentArea"); } }
        protected WebElementWrapper MainContentArea { get; set; }
        //protected By ByAppTitle { get { return By.ClassName("AppTitle"); } }
        protected By ByAppTitle=By.XPath("//*[@class='AppTitle' or @class='AppMainTitle']");
        protected WebElementWrapper AppTitle { get; set; }

        /// <summary>
        /// focus on main content area, to stop hover over menu
        /// </summary>
        //public void FocusOnMainContentArea()
        //{
        //    try
        //    {
        //        Point dummyPoint = new Point(0,0);
        //        Point point;

        //        //new DriverCommands(Driver).GetScreenshotAndPageSource();
        //        //move to the header
        //        GlobalAppHeader.Wait(3);
        //        GlobalAppHeader.LocationOnScreenOnceScrolledIntoView = dummyPoint;
        //        point = GlobalAppHeader.LocationOnScreenOnceScrolledIntoView;
        //        GlobalAppHeader.Location = dummyPoint;
        //        point = GlobalAppHeader.Location;
        //        GlobalAppHeader.MoveToElement();

        //        //new DriverCommands(Driver).GetScreenshotAndPageSource();
        //        //move to the global nav
        //        GlobalNav.Wait(3);
        //        GlobalNav.LocationOnScreenOnceScrolledIntoView = dummyPoint;
        //        point = GlobalNav.LocationOnScreenOnceScrolledIntoView;
        //        GlobalNav.Location = dummyPoint;
        //        point = GlobalNav.Location;
        //        GlobalNav.MoveToElement();

        //        //new DriverCommands(Driver).GetScreenshotAndPageSource();
        //        //move to the main content area
        //        MainContentArea.Wait(3);
        //        MainContentArea.LocationOnScreenOnceScrolledIntoView = dummyPoint;
        //        point = MainContentArea.LocationOnScreenOnceScrolledIntoView;
        //        MainContentArea.Location = dummyPoint;
        //        point = MainContentArea.Location;
        //        MainContentArea.MoveToElement();

        //        try
        //        {
        //            AppTitle.Wait(3);
        //            AppTitle.MoveToElement();
        //            AppTitle.Click();
        //        }
        //        catch (Exception ex)
        //        {
        //            //new DriverCommands(Driver).GetScreenshotAndPageSource();
        //            //click on main content area
        //            MainContentArea.Click();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("\nInnerException:\n" + e.InnerException + "\nStackTrace:\n" + e.StackTrace, e);
        //    }
        //}

        public string GetInvisibleErrorMessage(string element)
        {
            string pre = "Expected the element: '";
            string post = "' to be visible, but it is not visible.";
            return pre + element + post;
        }
        //Override these methods
        public abstract void VerifyFieldsExists();
        public abstract void VerifyContentExists();
    }