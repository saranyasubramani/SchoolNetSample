using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Schoolnet.SeleniumWebHelper;
using SeleniumUITests.Main.Library.Utilities;


    public abstract class Form : BaseDriver
    {
        public virtual object DataObject { get; set; }
        public Page Parent { get; set; }
        protected DriverCommands DriverCommands { get; set; }

        public string ControlPrefix = "ctl00_MainContent_";
        public virtual By BySubmit { get { return By.CssSelector("input[type='submit']"); } }
        public WebElementWrapper Submit { get { return new WebElementWrapper(Driver, BySubmit); } }
        public virtual By ByCancel { get { return By.Id(ControlPrefix + "Cancel"); } }
        public WebElementWrapper Cancel { get { return new WebElementWrapper(Driver, ByCancel); } }

        /*
        private static string bySubmit = "input[id$='_Submit']";
        public By BySubmit = By.CssSelector(bySubmit);
        [FindsBy(How = How.CssSelector, Using = bySubmit)]
        public IWebElement Submit;

        private static string byCancel = "input[id$='_Cancel']";
        public By ByCancel = By.CssSelector(byCancel);
        [FindsBy(How = How.CssSelector, Using = byCancel)]
        public IWebElement Cancel;
        */

        public Form(IWebDriver driver) : base(driver)
        {
            DriverCommands = new DriverCommands(Driver);
        }

        public virtual void initElements()
        {
        }

        public string GetInvisibleErrorMessage(string element)
        {
            string pre = "Expected the element: '";
            string post = "' to be visible, but it is not visible.";
            return pre + element + post;
        }

        public virtual void CancelForm()
        {
            Cancel.Wait(3).Click();
        }
        //Override these methods
        public abstract void ClearForm();
        public abstract void InputFormFields();
        public abstract void InputAllFieldsExcept(List<string> excludeFields);
        public abstract Page ReturnPage();
        public virtual Page SubmitForm()
        {
            Submit.Wait(3).Click();
            DriverCommands.WaitAndMeasurePageLoadTime(30, 90);
            return ReturnPage();
        }
        public virtual void SubmitFormAndVerifyErrors()
        {
            Submit.Wait(3).Click();
        }
        public Page InputAndSubmitForm()
        {
            InputFormFields();
            return SubmitForm();
        }
        public void InputAndSubmitFormWithErrors()
        {
            InputFormFields();
            SubmitFormAndVerifyErrors();
        }
        //Override these methods
        public abstract void VerifyErrorsForRequiredFields();
        public abstract void VerifyErrorForRequiredField(List<string> requiredFields);
        public abstract void VerifyErrorForInvalidField(List<string> invalidFields);
        public abstract void VerifyFieldsAreDeselected();
        public abstract void VerifyFieldsAreEmpty();
        public abstract void VerifyFieldsExist();
        public abstract void VerifyFieldsValues();
        public abstract void VerifyFieldSelectedOption(List<string> requiredFields, Object parameter);
        
        public abstract void VerifyFieldsAreEnabled(List<string> includeFields);
        public abstract void VerifyFieldsAreDisabled(List<string> includeFields);
        public abstract void VerifyFieldsAreVisible(List<string> includeFields);
        public abstract void VerifyFieldsAreInvisible(List<string> includeFields);
        public abstract void VerifyFieldsListOfValues(List<string> includeFields);
    }

