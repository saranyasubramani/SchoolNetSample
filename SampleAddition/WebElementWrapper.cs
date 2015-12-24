using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Support.UI;
using Schoolnet.SeleniumWebHelper;
using OpenQA.Selenium.Interactions;


public class WebElementWrapper : BaseDriver
{
    private const double DefaultTimeout = 90;
    public By By { get; private set; }
    public IWebElement Element { get; set; }
    public ReadOnlyCollection<IWebElement> Elements { get; private set; }

    public WebElementWrapper(IWebDriver driver)
        : base(driver)
    {
        this.Element = null;
    }
    public WebElementWrapper(IWebDriver driver, By by)
        : base(driver)
    {
        this.Element = null;
        this.By = by;
    }

    // Summary:
    //    Initializes a ReadOnlyCollection<IWebElement>.
    // Exceptions:
    // Remarks:
    //     This is called by most properties and methods to intialize the ReadOnlyCollection<IWebElement>
    //     if the Wait() method was NOT called first.
    public void InitializeWebElements()
    {
        if (this.Elements == null)
        {
            this.Elements = FindElements();
        }
    }
    // Summary:
    //    Initializes an IWebElement.
    // Exceptions:
    // Remarks:
    //     This is called by most properties and methods to intialize the IWebElement
    //     if the Wait() method was NOT called first.
    public void InitializeWebElement()
    {
        if (this.Element == null)
        {
            this.Element = FindElement();
        }
    }

    // Summary:
    //     Wait until the element is found and specify a timeout.
    // Parameters:
    //   by:
    //     A WebDriver By object.
    //   timeout:
    //     Time to wait in seconds.
    // Returns:
    //     A ReadOnlyCollection<IWebElement>.
    // Exceptions:
    private ReadOnlyCollection<IWebElement> WaitForElements(By by, double timeout)
    {
        Debug.WriteLine("Wait up to '" + timeout + "' seconds to find the elements by: '" + by.ToString() + "'.");
        ReadOnlyCollection<IWebElement> elements;
        try
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            elements = wait.Until<ReadOnlyCollection<IWebElement>>((driver) => driver.FindElements(@by));
        }
        catch (WebDriverTimeoutException exception)
        {
            Debug.WriteLine("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", exception);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to wait up to '" + timeout + "' seconds to find the elements by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to wait up to '" + timeout + "' seconds to find the elements by: '" + by.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Found the elements by: '" + by.ToString() + "'.");
        return elements;
    }
    // Summary:
    //     Wait until the element is found and specify a timeout.
    // Parameters:
    //   timeout:
    //     Time to wait in seconds.
    // Returns:
    //     A ReadOnlyCollection<IWebElement>.
    // Exceptions:
    public ReadOnlyCollection<IWebElement> WaitForElements(double timeout)
    {
        return WaitForElements(this.By, timeout);
    }
    // Summary:
    //     Wait until the element is found with a default timeout.
    // Returns:
    //     A ReadOnlyCollection<IWebElement>.
    // Exceptions:
    public ReadOnlyCollection<IWebElement> WaitForElements()
    {
        return WaitForElements(this.By, DefaultTimeout);
    }

    // Summary:
    //     Wait until the element is found and specify a timeout.
    // Parameters:
    //   by:
    //     A WebDriver By object.
    //   timeout:
    //     Time to wait in seconds.
    // Returns:
    //     An IWebElement.
    // Exceptions:
    private IWebElement WaitForElement(By by, double timeout)
    {
        Debug.WriteLine("Wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "'.");
        IWebElement element;
        try
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            //element = wait.Until<IWebElement>((driver) => driver.FindElement(@by));
            element = wait.Until<IWebElement>(ExpectedConditions.ElementExists(by));
        }
        catch (WebDriverTimeoutException exception)
        {
            Debug.WriteLine("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", exception);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Found the element by: '" + by.ToString() + "'.");
        return element;
    }
    // Summary:
    //     Wait until the element is found with a default timeout.
    // Parameters:
    //   by:
    //     A WebDriver By object.
    // Returns:
    //     An IWebElement.
    // Exceptions:
    private IWebElement WaitForElement(By by)
    {
        return WaitForElement(by, DefaultTimeout);
    }
    // Summary:
    //     Wait until the element is found and visible and specify a timeout.
    // Parameters:
    //   by:
    //     A WebDriver By object.
    //   timeout:
    //     Time to wait in seconds.
    // Returns:
    //     An IWebElement.
    // Exceptions:
    private IWebElement WaitForVisibleElement(By by, double timeout)
    {
        Debug.WriteLine("Wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "'.");
        IWebElement element;
        try
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            //element = wait.Until<IWebElement>((driver) => driver.FindElement(@by));
            if (Driver.GetType() == typeof(DummyDriver))
            {
                element = new DummyWebElement();
            }
            else
            {
                element = wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(by));
            }
        }
        catch (WebDriverTimeoutException exception)
        {
            Debug.WriteLine("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", exception);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to wait up to '" + timeout + "' seconds to find the element by: '" + by.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Found the element by: '" + by.ToString() + "'.");
        return element;
    }
    // Summary:
    //     Wait until the element is found and visible with a default timeout.
    // Parameters:
    //   by:
    //     A WebDriver By object.
    // Returns:
    //     An IWebElement.
    // Exceptions:
    private IWebElement WaitForVisibleElement(By by)
    {
        return WaitForVisibleElement(by, DefaultTimeout);
    }

    // Summary:
    //     Wait until the element is found and visible, and specify a timeout.
    // Parameters:
    //   timeout:
    //     Time to wait in seconds.
    // Returns:
    //     The WebElementWrapper.
    // Exceptions:
    public WebElementWrapper Wait(double timeout)
    {
        this.Element = WaitForVisibleElement(this.By, timeout);
        return this;
    }
    // Summary:
    //     Wait until the element is found and visible with a default timeout.
    // Returns:
    //     The WebElementWrapper.
    // Exceptions:
    public WebElementWrapper Wait()
    {
        this.Element = WaitForVisibleElement(this.By);
        return this;
    }
    // Summary:
    //     Wait until the element is found and visible, and specify a timeout.
    // Parameters:
    //   timeout:
    //     Time to wait in seconds.
    // Returns:
    //     The WebElementWrapper.
    // Exceptions:
    public WebElementWrapper WaitUntilVisible(double timeout)
    {
        this.Element = WaitForVisibleElement(this.By, timeout);
        return this;
    }
    // Summary:
    //     Wait until the element is found and visible with a default timeout.
    // Returns:
    //     The WebElementWrapper.
    // Exceptions:
    public WebElementWrapper WaitUntilVisible()
    {
        this.Element = WaitForVisibleElement(this.By);
        return this;
    }
    // Summary:
    //     Wait until the element is found and specify a timeout, but may be invisible.
    // Parameters:
    //   timeout:
    //     Time to wait in seconds.
    // Returns:
    //     The WebElementWrapper.
    // Exceptions:
    public WebElementWrapper WaitUntilExists(double timeout)
    {
        this.Element = WaitForElement(this.By, timeout);
        return this;
    }
    // Summary:
    //     Wait until the element is found with a default timeout, but may be invisible
    // Returns:
    //     The WebElementWrapper.
    // Exceptions:
    public WebElementWrapper WaitUntilExists()
    {
        this.Element = WaitForElement(this.By);
        return this;
    }

    // Summary:
    //     Gets a value indicating whether or not this element is displayed.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     The OpenQA.Selenium.IWebElement.Displayed property avoids the problem of
    //     having to parse an element's "style" attribute to determine visibility of
    //     an element.
    public bool Displayed
    {
        get
        {
            this.InitializeWebElement();
            bool display;
            try
            {
                display = this.Element.Displayed;
            }
            catch (WebDriverException exception)
            {
                Debug.WriteLine("Attempted to get the displayed attribute of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the displayed attribute of this element by: '" + this.By.ToString() + "', but failed.", exception);
            }
            Debug.WriteLine("Got the displayed attribute: '" + display + "' of this element by: '" + this.By.ToString() + "'.");
            return display;
        }
        set
        {
            this.InitializeWebElement();
            if (this.Element.GetType() == typeof(DummyWebElement))
            {
                ((DummyWebElement)Element).Displayed = value;
            }
        }
    }

    //
    // Summary:
    //     Gets a value indicating whether or not this element is enabled.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     The OpenQA.Selenium.IWebElement.Enabled property will generally return true
    //     for everything except explicitly disabled input elements.
    public bool Enabled
    {
        get
        {
            this.InitializeWebElement();
            bool enable;
            try
            {
                enable = this.Element.Enabled;
            }
            catch (WebDriverException exception)
            {
                Debug.WriteLine("Attempted to get the selected attribute of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the selected attribute of this element by: '" + this.By.ToString() + "', but failed.", exception);
            }
            Debug.WriteLine("Got the enabled attribute: '" + enable + "' of this element by: '" + this.By.ToString() + "'.");
            return enable;
        }
        set
        {
            this.InitializeWebElement();
            if (this.Element.GetType() == typeof(DummyWebElement))
            {
                ((DummyWebElement)Element).Enabled = value;
            }
        }
    }

    //
    // Summary:
    //     Gets a System.Drawing.Point object containing the coordinates of the upper-left
    //     corner of this element relative to the upper-left corner of the page.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //public Point Location
    //{
    //    get
    //    {
    //        this.InitializeWebElement();
    //        Point value;
    //        try
    //        {
    //            value = this.Element.Location;
    //        }
    //        catch (WebDriverException exception)
    //        {
    //            Debug.WriteLine("Attempted to get the location of this element by: '" + this.By.ToString() + "', but failed.");
    //            throw new Exception("Attempted to get the location of this element by: '" + this.By.ToString() + "', but failed.", exception);
    //        }
    //        Debug.WriteLine("Got the location: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
    //        return value;
    //    }
    //    set
    //    {
    //        this.InitializeWebElement();
    //        if (this.Element.GetType() == typeof(DummyWebElement))
    //        {
    //            ((DummyWebElement)Element).Location = value;
    //        }
    //    }
    //}
    //
    // Summary:
    //     Gets a value indicating whether or not this element is selected.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     This operation only applies to input elements such as checkboxes, options
    //     in a select element and radio buttons.
    public bool Selected
    {
        get
        {
            this.InitializeWebElement();
            bool value;
            try
            {
                value = this.Element.Selected;
            }
            catch (WebDriverException exception)
            {
                Debug.WriteLine("Attempted to get the selected attribute of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the selected attribute of this element by: '" + this.By.ToString() + "', but failed.", exception);
            }
            Debug.WriteLine("Got the selected attribute: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
            return value;
        }
        set
        {
            this.InitializeWebElement();
            if (this.Element.GetType() == typeof(DummyWebElement))
            {
                ((DummyWebElement)Element).Selected = value;
            }
        }
    }
    ////
    //// Summary:
    ////     Gets a OpenQA.Selenium.IWebElement.Size object containing the height and
    ////     width of this element.
    ////
    //// Exceptions:
    ////   OpenQA.Selenium.StaleElementReferenceException:
    ////     Thrown when the target element is no longer valid in the document DOM.
    //public Size Size
    //{
    //    get
    //    {
    //        this.InitializeWebElement();
    //        Size value;
    //        try
    //        {
    //            value = this.Element.Size;
    //        }
    //        catch (WebDriverException exception)
    //        {
    //            Debug.WriteLine("Attempted to get the size of this element by: '" + this.By.ToString() + "', but failed.");
    //            throw new Exception("Attempted to get the size of this element by: '" + this.By.ToString() + "', but failed.", exception);
    //        }
    //        Debug.WriteLine("Got the size: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
    //        return value;
    //    }
    //}
    //
    // Summary:
    //     Gets the tag name of this element.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     The OpenQA.Selenium.IWebElement.TagName property returns the tag name of
    //     the element, not the value of the name attribute. For example, it will return
    //     "input" for an element specified by the HTML markup <input name="foo" />.
    public string TagName
    {
        get
        {
            this.InitializeWebElement();
            string value;
            try
            {
                value = this.Element.TagName;
            }
            catch (WebDriverException exception)
            {
                Debug.WriteLine("Attempted to get the tag name of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the tag name of this element by: '" + this.By.ToString() + "', but failed.", exception);
            }
            Debug.WriteLine("Got the tag name: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
            return value;
        }
    }
    //
    // Summary:
    //     Gets the innerText of this element, without any leading or trailing whitespace,
    //     and with other whitespace collapsed.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    public string Text
    {
        get
        {
            this.InitializeWebElement();
            string value;
            try
            {
                value = this.Element.Text;
            }
            catch (WebDriverException exception)
            {
                Debug.WriteLine("Attempted to get the inner text of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the inner text of this element by: '" + this.By.ToString() + "', but failed.", exception);
            }
            Debug.WriteLine("Got the inner text: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
            return value;
        }
        set
        {
            this.InitializeWebElement();
            if (this.Element.GetType() == typeof(DummyWebElement))
            {
                ((DummyWebElement)Element).Text = value;
            }
        }
    }

    // Summary:
    //     Gets the coordinates identifying the location of this element using various
    //     frames of reference.
    public ICoordinates Coordinates
    {
        get
        {
            this.InitializeWebElement();
            ICoordinates value;
            try
            {
                value = ((ILocatable)this.Element).Coordinates;
            }
            catch (WebDriverException exception)
            {
                Debug.WriteLine("Attempted to get the coordinates of this element by: '" + this.By.ToString() + "', but failed.");
                throw new Exception("Attempted to get the coordinates of this element by: '" + this.By.ToString() + "', but failed.", exception);
            }
            Debug.WriteLine("Got the coordinates: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
            return value;
        }
        set
        {
            this.InitializeWebElement();
            if (this.Element.GetType() == typeof(DummyWebElement))
            {
                ((DummyWebElement)Element).Coordinates = new DummyICoordinates(); //value;
            }
        }
    }
    //
    // Summary:
    //     Gets the location of an element on the screen, scrolling it into view if
    //     it is not currently on the screen.
    //public Point LocationOnScreenOnceScrolledIntoView
    //{
    //    get
    //    {
    //        this.InitializeWebElement();
    //        Point value;
    //        try
    //        {
    //            value = ((ILocatable)this.Element).LocationOnScreenOnceScrolledIntoView;
    //        }
    //        catch (WebDriverException exception)
    //        {
    //            Debug.WriteLine("Attempted to get the Point of this element by: '" + this.By.ToString() + "', but failed.");
    //            throw new Exception("Attempted to get the Point of this element by: '" + this.By.ToString() + "', but failed.", exception);
    //        }
    //        Debug.WriteLine("Got the Point: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
    //        return value;
    //    }
    //    set
    //    {
    //        this.InitializeWebElement();
    //        if (this.Element.GetType() == typeof(DummyWebElement))
    //        {
    //            ((DummyWebElement)Element).LocationOnScreenOnceScrolledIntoView = new Point(10, 30);//value;
    //        }
    //    }
    //}

    // Summary:
    //     Clears the content of this element.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     If this element is a text entry element, the OpenQA.Selenium.IWebElement.Clear()
    //     method will clear the value. It has no effect on other elements. Text entry
    //     elements are defined as elements with INPUT or TEXTAREA tags.
    public void Clear()
    {
        this.InitializeWebElement();
        try
        {
            this.Element.Clear();
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to clear the content of this element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to clear the content of this element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Cleared the content of this element by: '" + this.By.ToString() + "'.");
    }
    //
    // Summary:
    //     Clicks this element.
    //
    // Exceptions:
    //   OpenQA.Selenium.ElementNotVisibleException:
    //     Thrown when the target element is not visible.
    //
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //      Click this element. If the click causes a new page to load, the OpenQA.Selenium.IWebElement.Click()
    //     method will attempt to block until the page has loaded. After calling the
    //     OpenQA.Selenium.IWebElement.Click() method, you should discard all references
    //     to this element unless you know that the element and the page will still
    //     be present. Otherwise, any further operations performed on this element will
    //     have an undefined.  behavior.
    //     If this element is not clickable, then this operation is ignored. This allows
    //     you to simulate a users to accidentally missing the target when clicking.
    public void Click()
    {
        this.InitializeWebElement();
        try
        {
            this.Element.Click();
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to click this element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to click this element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Clicked this element by: '" + this.By.ToString() + "'.");
    }
    //
    // Summary:
    //     Gets the value of the specified attribute for this element.
    //
    // Parameters:
    //   attributeName:
    //     The name of the attribute.
    //
    // Returns:
    //     The attribute's current value. Returns a null if the value is not set.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     The OpenQA.Selenium.IWebElement.GetAttribute(System.String) method will return
    //     the current value of the attribute, even if the value has been modified after
    //     the page has been loaded. Note that the value of the following attributes
    //     will be returned even if there is no explicit attribute on the element: Attribute
    //     nameValue returned if not explicitly specifiedValid element typescheckedcheckedCheck
    //     BoxselectedselectedOptions in Select elementsdisableddisabledInput and other
    //     UI elements
    public string GetAttribute(string attributeName)
    {
        this.InitializeWebElement();
        string value;
        try
        {
            value = this.Element.GetAttribute(attributeName);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to get the attribute: '" + attributeName + "' of this element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to get the attribute: '" + attributeName + "' of this element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        if (value == null)
        {
            Debug.WriteLine("Got the attribute: '" + attributeName + "' of this element by: '" + this.By.ToString() +
                            "'.");
        }
        else
        {
            Debug.WriteLine("Got the attribute: '" + attributeName + "' and value: '" + value + "' of this element by: '" + this.By.ToString() + "'.");
        }
        return value;
    }
    private string _fakeAttributeId;
    public string FakeAttributeId
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeId = ((DummyWebElement)this.Element).FakeAttributeId;
            }
            return _fakeAttributeId;
        }
        set
        {
            _fakeAttributeId = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeId = _fakeAttributeId;
            }
        }
    }
    private string _fakeAttributeName;
    public string FakeAttributeName
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeName = ((DummyWebElement)this.Element).FakeAttributeName;
            }
            return _fakeAttributeName;
        }
        set
        {
            _fakeAttributeName = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeName = _fakeAttributeName;
            }
        }
    }
    private string _fakeAttributeValue;
    public string FakeAttributeValue
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeValue = ((DummyWebElement)this.Element).FakeAttributeValue;
            }
            return _fakeAttributeValue;
        }
        set
        {
            _fakeAttributeValue = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeValue = _fakeAttributeValue;
            }
        }
    }
    private string _fakeAttributeClass;
    public string FakeAttributeClass
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeClass = ((DummyWebElement)this.Element).FakeAttributeClass;
            }
            return _fakeAttributeClass;
        }
        set
        {
            _fakeAttributeClass = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeClass = _fakeAttributeClass;
            }

        }
    }
    private string _fakeAttributeHref;
    public string FakeAttributeHref
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeHref = ((DummyWebElement)this.Element).FakeAttributeHref;
            }
            return _fakeAttributeHref;
        }
        set
        {
            _fakeAttributeHref = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeHref = _fakeAttributeHref;
            }

        }
    }
    private string _fakeAttributeStyle;
    public string FakeAttributeStyle
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeStyle = ((DummyWebElement)this.Element).FakeAttributeStyle;
            }
            return _fakeAttributeStyle;
        }
        set
        {
            _fakeAttributeStyle = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeStyle = _fakeAttributeStyle;
            }

        }
    }
    private string _fakeAttributeOnClick;
    public string FakeAttributeOnClick
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeOnClick = ((DummyWebElement)this.Element).FakeAttributeOnClick;
            }
            return _fakeAttributeOnClick;
        }
        set
        {
            _fakeAttributeOnClick = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeOnClick = _fakeAttributeOnClick;
            }

        }
    }
    private string _fakeAttributeDocid;
    public string FakeAttributeDocid
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeDocid = ((DummyWebElement)this.Element).FakeAttributeDocid;
            }
            return _fakeAttributeDocid;
        }
        set
        {
            _fakeAttributeDocid = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeDocid = _fakeAttributeDocid;
            }
        }
    }
    private string _fakeAttributeNk;
    public string FakeAttributeNk
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeNk = ((DummyWebElement)this.Element).FakeAttributeNk;
            }
            return _fakeAttributeNk;
        }
        set
        {
            _fakeAttributeNk = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeNk = _fakeAttributeNk;
            }
        }
    }
    private string _fakeAttributeKey;
    public string FakeAttributeKey
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeKey = ((DummyWebElement)this.Element).FakeAttributeKey;
            }
            return _fakeAttributeKey;
        }
        set
        {
            _fakeAttributeKey = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeKey = _fakeAttributeKey;
            }
        }
    }
    private string _fakeAttributeItemid;
    public string FakeAttributeItemid
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeItemid = ((DummyWebElement)this.Element).FakeAttributeItemid;
            }
            return _fakeAttributeItemid;
        }
        set
        {
            _fakeAttributeItemid = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeItemid = _fakeAttributeItemid;
            }
        }
    }
    private string _fakeAttributePassageid;
    public string FakeAttributePassageid
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributePassageid = ((DummyWebElement)this.Element).FakeAttributePassageid;
            }
            return _fakeAttributePassageid;
        }
        set
        {
            _fakeAttributePassageid = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributePassageid = _fakeAttributePassageid;
            }
        }
    }
    private string _fakeAttributeRubricid;
    public string FakeAttributeRubricid
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeRubricid = ((DummyWebElement)this.Element).FakeAttributeRubricid;
            }
            return _fakeAttributeRubricid;
        }
        set
        {
            _fakeAttributeRubricid = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeRubricid = _fakeAttributeRubricid;
            }
        }
    }
    private string _fakeAttributeDataKey;
    public string FakeAttributeDataKey
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeDataKey = ((DummyWebElement)this.Element).FakeAttributeDataKey;
            }
            return _fakeAttributeDataKey;
        }
        set
        {
            _fakeAttributeDataKey = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeDataKey = _fakeAttributeDataKey;
            }
        }
    }
    private string _fakeAttributeDataNgLabel;
    public string FakeAttributeDataNgLabel
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeDataNgLabel = ((DummyWebElement)this.Element).FakeAttributeDataNgLabel;
            }
            return _fakeAttributeDataNgLabel;
        }
        set
        {
            _fakeAttributeDataNgLabel = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeDataNgLabel = _fakeAttributeDataNgLabel;
            }
        }
    }
    private string _fakeAttributeDataNgModel;
    public string FakeAttributeDataNgModel
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeDataNgModel = ((DummyWebElement)this.Element).FakeAttributeDataNgModel;
            }
            return _fakeAttributeDataNgModel;
        }
        set
        {
            _fakeAttributeDataNgModel = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeDataNgModel = _fakeAttributeDataNgModel;
            }
        }
    }
    private string _fakeAttributeDataNgClick;
    public string FakeAttributeDataNgClick
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeDataNgClick = ((DummyWebElement)this.Element).FakeAttributeDataNgClick;
            }
            return _fakeAttributeDataNgClick;
        }
        set
        {
            _fakeAttributeDataNgClick = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeDataNgClick = _fakeAttributeDataNgClick;
            }
        }
    }
    private string _fakeAttributeNgIf;
    public string FakeAttributeNgIf
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeNgIf = ((DummyWebElement)this.Element).FakeAttributeNgIf;
            }
            return _fakeAttributeNgIf;
        }
        set
        {
            _fakeAttributeNgIf = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeNgIf = _fakeAttributeNgIf;
            }
        }
    }
    //aria-valuenow
    private string _fakeAttributeAriaValueNow;
    public string FakeAttributeAriaValueNow
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeAriaValueNow = ((DummyWebElement)this.Element).FakeAttributeAriaValueNow;
            }
            return _fakeAttributeAriaValueNow;
        }
        set
        {
            _fakeAttributeAriaValueNow = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeAriaValueNow = _fakeAttributeAriaValueNow;
            }
        }
    }
    //aria-label="Question 2 Unanswered"
    private string _fakeAttributeAriaLabel;
    public string FakeAttributeAriaLabel
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeAttributeAriaLabel = ((DummyWebElement)this.Element).FakeAttributeAriaLabel;
            }
            return _fakeAttributeAriaLabel;
        }
        set
        {
            _fakeAttributeAriaLabel = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeAttributeAriaLabel = _fakeAttributeAriaLabel;
            }
        }
    }
    //
    // Summary:
    //     Gets the value of a CSS property of this element.
    //
    // Parameters:
    //   propertyName:
    //     The name of the CSS property to get the value of.
    //
    // Returns:
    //     The value of the specified CSS property.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     The value returned by the OpenQA.Selenium.IWebElement.GetCssValue(System.String)
    //     method is likely to be unpredictable in a cross-browser environment. Color
    //     values should be returned as hex strings. For example, a "background-color"
    //     property set as "green" in the HTML source, will return "#008000" for its
    //     value.
    public string GetCssValue(string propertyName)
    {
        this.InitializeWebElement();
        string value;
        try
        {
            value = this.Element.GetCssValue(propertyName);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to get the CSS value: '" + propertyName + "' of this element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to get the CSS value: '" + propertyName + "' of this element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Got the CSS value: '" + propertyName + "' of this element by: '" + this.By.ToString() + "'.");
        return value;
    }
    private string _fakeCssValueStyle;
    public string FakeCssValueStyle
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeCssValueStyle = ((DummyWebElement)this.Element).FakeCssValueStyle;
            }
            return _fakeCssValueStyle;
        }
        set
        {
            _fakeCssValueStyle = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeCssValueStyle = _fakeCssValueStyle;
            }
        }
    }
    private string _fakeCssValueClass;
    public string FakeCssValueClass
    {
        get
        {
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                _fakeCssValueClass = ((DummyWebElement)this.Element).FakeCssValueClass;
            }
            return _fakeCssValueClass;
        }
        set
        {
            _fakeCssValueClass = value;
            if (Driver.GetType() == typeof(DummyDriver))
            {
                this.InitializeWebElement();
                ((DummyWebElement)this.Element).FakeCssValueClass = _fakeCssValueClass;
            }
        }
    }

    //
    // Summary:
    //     Simulates typing text into the element.
    //
    // Parameters:
    //   text:
    //     The text to type into the element.
    //
    // Exceptions:
    //   OpenQA.Selenium.InvalidElementStateException:
    //     Thrown when the target element is not enabled.
    //
    //   OpenQA.Selenium.ElementNotVisibleException:
    //     Thrown when the target element is not visible.
    //
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     The text to be typed may include special characters like arrow keys, backspaces,
    //     function keys, and so on. Valid special keys are defined in OpenQA.Selenium.Keys.
    public void SendKeys(string text)
    {
        this.InitializeWebElement();
        try
        {
            this.Element.SendKeys(text);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to type the text: '" + text + "' into this element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to type the text: '" + text + "' into this element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Typed the text: '" + text + "' into this element by: '" + this.By.ToString() + "'.");
    }
    //
    // Summary:
    //     Submits this element to the web server.
    //
    // Exceptions:
    //   OpenQA.Selenium.StaleElementReferenceException:
    //     Thrown when the target element is no longer valid in the document DOM.
    //
    // Remarks:
    //     If this current element is a form, or an element within a form, then this
    //     will be submitted to the web server. If this causes the current page to change,
    //     then this method will block until the new page is loaded.
    public void Submit()
    {
        this.InitializeWebElement();
        try
        {
            this.Element.Submit();
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to submit this element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to submit this element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Submitted this element.");
    }

    // Summary:
    //     Move to this element on the screen.
    //
    // Remarks:
    //     Moves the mouse to an offset from the top-left corner of the element. 
    public void MoveToElement()
    {
        this.InitializeWebElement();
        try
        {
            Actions action = new Actions(Driver);
            action.MoveToElement(this.Element).Perform();
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to move to this element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to move to this element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Move to this element.");
    }

    public void MouseOverAndClick(WebElementWrapper elementToClick)
    {
        this.InitializeWebElement();
        try
        {
            Actions action = new Actions(Driver);
            //still working on this...
            //system.argumentexception: must provide a location for a move action. parameter name: actionTarget
            action.MoveToElement(this.Element).MoveToElement(elementToClick.Element).Click().Build().Perform();
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to mouse over this element by: '" + this.By.ToString() + "' and click this element by: '" + elementToClick.By.ToString() + "', but failed.");
            throw new Exception("Attempted to mouse over this element by: '" + this.By.ToString() + "' and click this element by: '" + elementToClick.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Mouse over and click this element.");
    }

    // Summary:
    //     Finds the first OpenQA.Selenium.IWebElement using the given method.
    //
    // Parameters:
    //   by:
    //     The locating mechanism to use.
    //
    // Returns:
    //     The first matching OpenQA.Selenium.IWebElement on the current context.
    //
    // Exceptions:
    //   OpenQA.Selenium.NoSuchElementException:
    //     If no element matches the criteria.
    public IWebElement FindElement(By by)
    {
        IWebElement webElement = null;
        try
        {
            if (this.By == null)
            {
                this.By = by;
            }
            webElement = Driver.FindElement(by);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to find the element by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to find the element by: '" + by.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Found the element by: '" + by.ToString() + "'.");
        return webElement;
    }
    // Summary:
    //     Finds the first OpenQA.Selenium.IWebElement using the given method.
    //
    // Returns:
    //     The first matching OpenQA.Selenium.IWebElement on the current context.
    //
    // Exceptions:
    //   OpenQA.Selenium.NoSuchElementException:
    //     If no element matches the criteria.
    public IWebElement FindElement()
    {
        try
        {
            this.Element = Driver.FindElement(this.By);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to find the element by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to find the element by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Found the element by: '" + this.By.ToString() + "'.");
        return this.Element;
    }
    //
    // Summary:
    //     Finds all OpenQA.Selenium.IWebElement within the current context using the
    //     given mechanism.
    //
    // Parameters:
    //   by:
    //     The locating mechanism to use.
    //
    // Returns:
    //     A System.Collections.ObjectModel.ReadOnlyCollection<T> of all OpenQA.Selenium.IWebElement
    //     matching the current criteria, or an empty list if nothing matches.
    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        ReadOnlyCollection<IWebElement> webElements = null;
        try
        {
            if (this.By == null)
            {
                this.By = by;
            }
            webElements = Driver.FindElements(by);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to find the elements by: '" + by.ToString() + "', but failed.");
            throw new Exception("Attempted to find the elements by: '" + by.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Found the elements by: '" + by.ToString() + "'.");
        return webElements;
    }
    // Summary:
    //     Finds all OpenQA.Selenium.IWebElement within the current context using the
    //     given mechanism.
    //
    // Returns:
    //     A System.Collections.ObjectModel.ReadOnlyCollection<T> of all OpenQA.Selenium.IWebElement
    //     matching the current criteria, or an empty list if nothing matches.
    public ReadOnlyCollection<IWebElement> FindElements()
    {
        try
        {
            this.Elements = Driver.FindElements(this.By);
        }
        catch (WebDriverException exception)
        {
            Debug.WriteLine("Attempted to find the elements by: '" + this.By.ToString() + "', but failed.");
            throw new Exception("Attempted to find the elements by: '" + this.By.ToString() + "', but failed.", exception);
        }
        Debug.WriteLine("Found the elements by: '" + this.By.ToString() + "'.");
        return this.Elements;
    }

    //my methods
    /// <summary>
    /// Check the element
    /// </summary>
    public void Check()
    {
        this.Selected = false;
        if (this.Selected == false)
        {
            this.Click();
        }
    }
    /// <summary>
    /// Uncheck the element
    /// </summary>
    public void UnCheck()
    {
        this.Selected = true;
        if (this.Selected == true)
        {
            this.Click();
        }
    }
}
