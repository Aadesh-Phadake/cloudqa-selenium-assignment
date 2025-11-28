using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CloudQATests.PageObjects
{
    public class AutomationPracticeFormPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly string _url = "https://app.cloudqa.io/home/AutomationPracticeForm";

        public AutomationPracticeFormPage(IWebDriver driver, TimeSpan? timeout = null)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, timeout ?? TimeSpan.FromSeconds(10));
        }

        public void GoTo() => _driver.Navigate().GoToUrl(_url);


        // ------------------------------------------------------
        // BASIC WAIT HELPERS
        // ------------------------------------------------------

        private IWebElement WaitUntilVisible(By locator)
        {
            return _wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch
                {
                    return null;
                }
            });
        }

        private IWebElement WaitAndFind(By locator) => WaitUntilVisible(locator);


        // ------------------------------------------------------
        // FIELD LOCATORS
        // ------------------------------------------------------

        private By FirstNameLocator =>
            By.XPath("//label[contains(., 'First Name')]/following::input[1]");

        private By LastNameLocator =>
            By.XPath("//label[contains(., 'Last Name')]/following::input[1]");

        private By CountryLocator =>
            By.XPath("//label[contains(., 'State')]/following::select[1]");


        // ------------------------------------------------------
        // FIRST NAME
        // ------------------------------------------------------

        public void SetFirstName(string firstName)
        {
            var input = WaitAndFind(FirstNameLocator);
            input.Clear();
            input.SendKeys(firstName);
        }

        public string GetFirstName()
        {
            return WaitAndFind(FirstNameLocator).GetAttribute("value");
        }


        // ------------------------------------------------------
        // LAST NAME
        // ------------------------------------------------------

        public void SetLastName(string lastName)
        {
            var input = WaitAndFind(LastNameLocator);
            input.Clear();
            input.SendKeys(lastName);
        }

        public string GetLastName()
        {
            return WaitAndFind(LastNameLocator).GetAttribute("value");
        }


        // ------------------------------------------------------
        // COUNTRY DROPDOWN
        // ------------------------------------------------------

        public void SelectCountryByText(string country)
        {
            var selectElem = WaitAndFind(CountryLocator);
            var select = new SelectElement(selectElem);

            try
            {
                select.SelectByText(country);
            }
            catch
            {
                foreach (var option in select.Options)
                {
                    if (option.Text.Contains(country, StringComparison.OrdinalIgnoreCase))
                    {
                        select.SelectByText(option.Text);
                        return;
                    }
                }
                throw new NoSuchElementException($"Country '{country}' not found.");
            }
        }

        public string GetSelectedCountry()
        {
            var selectElem = WaitAndFind(CountryLocator);
            return new SelectElement(selectElem).SelectedOption.Text.Trim();
        }
    }
}
