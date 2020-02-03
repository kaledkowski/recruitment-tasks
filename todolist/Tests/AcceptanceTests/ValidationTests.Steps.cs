using LightBDD.XUnit2;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Tests.AcceptanceTests
{
    public partial class ValidationTests : FeatureFixture, IDisposable
    {
        private IWebDriver _webDriver;

        public ValidationTests()
        {
            _webDriver = new ChromeDriver();
        }

        private void I_am_on_the_main_page()
        {
            _webDriver.Url = "http://localhost:38978/";
            _webDriver.Navigate();
        }

        private void I_type_in_a_note_title(string title)
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=title]")).SendKeys(title);
        }

        private void I_type_in_a_note_contents(string content)
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=content]")).SendKeys(content);
        }

        private void I_click_the_add_card_button()
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm button")).Click();
        }

        private void I_should_see_validation_message_for_title()
        {
            var errorElem = _webDriver.FindElement(By.CssSelector("#addCardForm .card-header #title-error"));
            Assert.NotNull(errorElem);
            Assert.Equal("This field is required.", errorElem.Text.Trim());
        }

        private void I_should_see_validation_message_for_content()
        {
            var errorElem = _webDriver.FindElement(By.CssSelector("#addCardForm .card-body #content-error"));
            Assert.NotNull(errorElem);
            Assert.Equal("This field is required.", errorElem.Text.Trim());
        }

        public void Dispose()
        {
            _webDriver.Dispose();
            _webDriver = null;
        }
    }
}
