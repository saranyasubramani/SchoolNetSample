using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;


    public class AuthenticationPage: Page
    {
        private AuthenticationData _dataObject;
        public override Object DataObject
        {
            get
            {
                return (AuthenticationData)_dataObject;
            }
            set
            {
                _dataObject = (AuthenticationData) value;
                this.Form.DataObject = _dataObject;
            }
        }

        public AuthenticationPage(IWebDriver driver)
            : base(driver)
        {
            this.PrintPageName("Authentication");
            this.ExpectedUrl = "/Authentication.aspx";
            this.VerifyCurrentUrl();
            this.Form = new AuthenticationForm(driver);
        }

        public HomePage Login()
        {
            return (HomePage) this.Form.InputAndSubmitForm();
        }

        public void VerifySignedOut()
        {
            ((AuthenticationForm) this.Form).Username.Wait(5);
            this.PrintPageName("Authentication");
            this.VerifyCurrentUrl();
        }
    }

    public class AuthenticationData
    {
        public string DistrictName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public AuthenticationData() {}
    }

    public class AuthenticationForm : Form
    {
        private AuthenticationData _dataObject;
        public override Object DataObject
        {
            get
            {
                return (AuthenticationData)_dataObject;
            }
            set
            {
                _dataObject = (AuthenticationData)value;
            }
        }

        public string ControlPrefix = "ctl02_";
        public By ByUsername { get { return By.Id(ControlPrefix + "TextBoxUsername"); } }
        public WebElementWrapper Username { get; set; }
        public By ByPassword { get { return By.Id(ControlPrefix + "TextBoxPassword"); } }
        public WebElementWrapper Password { get; set; }
        public override By BySubmit { get { return By.ClassName("SignInButton"); } }

        public AuthenticationForm(IWebDriver driver) : base(driver)
        {
            Username = new WebElementWrapper(Driver, ByUsername);
            Password = new WebElementWrapper(Driver, ByPassword);
        }

        public override void ClearForm()
        {
            throw new NotImplementedException();
        }

        public override void InputFormFields()
        {
            Username.SendKeys(_dataObject.Username);
            Password.SendKeys(_dataObject.Password);
        }

        public override void InputAllFieldsExcept(List<string> excludeFields)
        {
            throw new NotImplementedException();
        }

        public override Page ReturnPage()
        {
            return new HomePage(Driver);
        }

        public override void VerifyErrorsForRequiredFields()
        {
            throw new NotImplementedException();
        }

        public override void VerifyErrorForRequiredField(List<string> requiredFields)
        {
            throw new NotImplementedException();
        }

        public override void VerifyErrorForInvalidField(List<string> invalidFields)
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsAreDeselected()
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsAreEmpty()
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsExist()
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsValues()
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldSelectedOption(List<string> requiredFields, object parameter)
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsAreEnabled(List<string> includeFields)
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsAreDisabled(List<string> includeFields)
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsAreVisible(List<string> includeFields)
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsAreInvisible(List<string> includeFields)
        {
            throw new NotImplementedException();
        }

        public override void VerifyFieldsListOfValues(List<string> includeFields)
        {
            throw new NotImplementedException();
        }
    }

