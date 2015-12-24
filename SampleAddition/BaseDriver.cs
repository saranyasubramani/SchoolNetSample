using System.Configuration;
using OpenQA.Selenium;


    public class BaseDriver
    {
        public IWebDriver Driver { get; private set; }
        public BaseDriver(IWebDriver driver)
        {
            this.Driver = driver;
        }
    }

