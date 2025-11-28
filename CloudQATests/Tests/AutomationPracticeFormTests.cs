using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using CloudQATests.PageObjects;

namespace CloudQATests.Tests
{
    [TestFixture]
    public class AutomationPracticeFormTests
    {
        private IWebDriver _driver;
        private AutomationPracticeFormPage _formPage;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-dev-shm-usage");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            _formPage = new AutomationPracticeFormPage(_driver, TimeSpan.FromSeconds(10));
            _formPage.GoTo();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                _driver.Quit();
                _driver.Dispose();
            }
            catch { }
        }


        // ------------------------------------------------------
        // TEST 1 — FIRST NAME
        // ------------------------------------------------------

        [Test]
        public void FirstName_CanBeEnteredAndReadBack()
        {
            string firstName = "AadeshTest";

            _formPage.SetFirstName(firstName);
            string readBack = _formPage.GetFirstName();

            Assert.That(readBack, Is.EqualTo(firstName));
        }


        // ------------------------------------------------------
        // TEST 2 — LAST NAME
        // ------------------------------------------------------

        [Test]
        public void LastName_CanBeEnteredAndReadBack()
        {
            string lastName = "PhadakeTest";

            _formPage.SetLastName(lastName);
            string readBack = _formPage.GetLastName();

            Assert.That(readBack, Is.EqualTo(lastName));
        }


        // ------------------------------------------------------
        // TEST 3 — COUNTRY DROPDOWN
        // ------------------------------------------------------

        [Test]
        public void Country_CanBeSelectedByVisibleText()
        {
            string country = "India";

            _formPage.SelectCountryByText(country);
            string selected = _formPage.GetSelectedCountry();

            Assert.That(selected, Does.Contain("India").IgnoreCase);
        }
    }
}
