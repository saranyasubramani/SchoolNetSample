using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

   public class DriverInitializer
    {
        private static IWebDriver _webDriverInstance;
       // private static WebHelperConfigSection Config { get { return (WebHelperConfigSection)ConfigurationManager.GetSection("SeleniumWebHelper"); } }

        public DriverInitializer(){}


        public static IWebDriver InvokeWebDriver()
        {

            Console.WriteLine("Browser:" + Environment.GetEnvironmentVariables());
            foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                string name = (string)env.Key;
                string value = (string)env.Value;
                Console.WriteLine("{0}={1}", name, value);
            }
            Func<IWebDriver> getRawWebDriverFunc = () => GetRawWebDriver(Environment.GetEnvironmentVariable("selenium_browser"));
            string startingUrl = null;
            startingUrl = StartingUrlSingleTenant;
            IWebDriver webDriver = getRawWebDriverFunc();
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(120));
            webDriver.Navigate().GoToUrl(startingUrl);
            /*
            bool found = wait.Until((driver) =>
            {
                //can't do a wait.until on navigate.gotourl since it is a void method
                driver.Navigate().GoToUrl(startingUrl);
                try
                {
                    //do a wait.until on string contains - see if the driver.url contains the base url
                    return driver.Url.Contains(startingUrl);
                }
                catch (WebDriverException wde)
                {
                    throw new Exception("Encountered some problem while loading the page at: " + startingUrl, wde);
                }
            });
            */
            //IWebDriver driverWrapper = null;
            //if (WebDriverType == WebHelperConfigSection.WebDriverTypes.Dummy)
            //{
            //    driverWrapper = new DummyDriver();
            //    //can only assign this if the class is within the Schoolnet.SeleniumWebHelper DLL
            //    ((DummyDriver)driverWrapper).StartingUrl = startingUrl;
            //    ((DummyDriver)driverWrapper).WhatAmI = whatAmI;
            //}
            //else
            //{
            //    driverWrapper = new DriverWrapper(webDriver);
            //    //can only assign this if the class is within the Schoolnet.SeleniumWebHelper DLL
            //    ((DriverWrapper)driverWrapper).StartingUrl = startingUrl;
            //    ((DriverWrapper)driverWrapper).WhatAmI = whatAmI;
            //}
            //set local driver instance
            _webDriverInstance = webDriver;
            return webDriver;
        }

        public static void QuitWebDriver()
        {
            _webDriverInstance.Close();
            _webDriverInstance.Quit();
        }

        /// <summary>
        /// gets a Selenium Web Driver instance, effectively the web browser, initialized using the provided Web Driver Type
        /// </summary>
        /// <param name="driverType">enum of the supported browsers, currently Firefox is the preferred browser</param>
        /// <returns>Selenium Web Driver instance, effectively the web browser</returns>
        public static IWebDriver GetRawWebDriver(String driverType)
        {
            //TODO: more refactoring is needed for the drivers...
            IWebDriver driver = null;

            

            switch (driverType)
            {
                case "chrome":
                    Debug.WriteLine("ChromeDriver: " + ChromeDriverServerDirectory);
                    
                    DesiredCapabilities capabillities = new DesiredCapabilities();
                    Console.WriteLine("Version:" + Environment.GetEnvironmentVariable("selenium_version"));
                    Console.WriteLine("Platform:" + Environment.GetEnvironmentVariable("selenium_platform"));
                    Console.WriteLine("User:" + Environment.GetEnvironmentVariable("sauce_username"));
                    Console.WriteLine("Key:" + Environment.GetEnvironmentVariable("sauce_access_key"));
                    capabillities.SetCapability(CapabilityType.BrowserName, Environment.GetEnvironmentVariable("selenium_browser"));
                    capabillities.SetCapability(CapabilityType.Version, Environment.GetEnvironmentVariable("selenium_version"));
                    capabillities.SetCapability(CapabilityType.Platform, Environment.GetEnvironmentVariable("selenium_platform"));
                    


                   /* capabillities.SetCapability(CapabilityType.BrowserName, "chrome");
                    capabillities.SetCapability(CapabilityType.Version, "45");
                    capabillities.SetCapability(CapabilityType.Platform, "Windows 7");
                    capabillities.SetCapability("deviceName", "");
                    capabillities.SetCapability("deviceOrientation", "");*/

                    capabillities.SetCapability("username", Environment.GetEnvironmentVariable("sauce_username"));
                    capabillities.SetCapability("accessKey", Environment.GetEnvironmentVariable("sauce_access_key"));
                    driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com/wd/hub"), capabillities, TimeSpan.FromSeconds(600));
                    //driver = new RemoteWebDriver(new Uri("http://"+Environment.GetEnvironmentVariable("SAUCE_USERNAME")+":"+Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY")+"@ondemand.saucelabs.com:80/wd/hub"),capabillities);
                    //driver = new ChromeDriver(ChromeDriverServerDirectory);
                   break;

                case "Firefox":
                    driver = GetFirefoxInstance();
                    break;

                case "FirefoxWithLocalProxy":
                    FirefoxProfile profile = new FirefoxProfile();
                    profile.SetPreference("network.proxy.http", "localhost");
                    profile.SetPreference("network.proxy.http_port", "7080");
                    profile.SetPreference("network.proxy.type", "1");
                    break;

                case "InternetExplorer":
                    Debug.WriteLine("InternetExplorerDriverServer: " + InternetExplorerDriverServerDirectory);
                    DesiredCapabilities desiredCapabilities = DesiredCapabilities.InternetExplorer();
                    var options = new InternetExplorerOptions();
                    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    driver = new InternetExplorerDriver(InternetExplorerDriverServerDirectory, options);
                    break;


                case "RemoteFirefox":
                    capabillities = DesiredCapabilities.Firefox();
                    /*
                    capabillities.SetCapability("version", "5");
                    capabillities.Platform = new Platform(PlatformType.XP);
                    capabillities.SetCapability("name", "Testing Selenium 2 with C# on Sauce");
                    capabillities.SetCapability("username", "dna64pie");
                    capabillities.SetCapability("accessKey", "099c73db-929b-418c-bef6-d929381fa07f");
                    driver = new RemoteWebDriver(new Uri("http://zipgun2:099c73db-929b-418c-bef6-d929381fa07f@ondemand.saucelabs.com:80/wd/hub"), capabillities);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
                    */
                    driver = new RemoteWebDriver(capabillities);
                    break;


                default:
                    driver = new FirefoxDriver();
                    break;
            }
            if (driver != null)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(ImplicitTimeOut));
                driver.Manage().Window.Maximize();
            }
            return driver;
        }

        public static IWebDriver GetFirefoxInstance(FirefoxProfile profile = null)
        {
            IWebDriver retval = null;
            int tries = 0;
            while (retval == null)
            {
                try
                {
                    if (profile != null)
                        retval = new FirefoxDriver(profile);
                    else
                        retval = new FirefoxDriver();
                }
                catch (Exception ex)
                {

                    if (retval != null)
                    {
                        retval.Quit();
                    }

                    if (tries > 3)
                        throw new ApplicationException("web driver failed to initialize", ex);

                    tries++;

                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                if (retval != null)
                    break;
            }

            //retval.Manage().Window.Maximize();

            return retval;
        }

        public static string InternetExplorerDriverServerDirectory { get { return "C:\\IEDriver\\"; } }
        public static string ChromeDriverServerDirectory { get { return "\\chromedriver\\"; } }
        public static double ImplicitTimeOut { get { return Double.Parse("15"); } }

        /// <summary>
        /// the Admin username you have stored in the app.config. used to login as admin
        /// </summary>
        public static string AdminUsername { get { return "sn_qa"; } }

        /// <summary>
        /// the Admin password you have stored in the app.config. used to login as admin
        /// </summary>
        public static string AdminPassword { get { return "sch00lnet"; } }

        /// <summary>
        /// type of web browser that will be used when tests are run, stored in the app.config. 
        /// </summary>
        //public static WebHelperConfigSection.WebDriverTypes WebDriverType { get { return Config.WebDriverType; } }

        /// <summary>
        /// Url which will be loaded initially by the browser, stored in the app.config
        /// </summary>
        public static string StartingUrlSingleTenant { get { return "https://team-automation-st.sndev.net/"; } }

               /// <summary>
        /// Path stored in app.config where the downloaded files will be available
        /// </summary>
        public static string FileDownloadDirectory { get { return AppDomain.CurrentDomain.BaseDirectory; } }
    }
