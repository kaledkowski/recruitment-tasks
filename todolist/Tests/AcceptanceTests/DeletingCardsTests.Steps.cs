using LightBDD.XUnit2;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using Xunit;

namespace Tests.AcceptanceTests
{
    public partial class DeletingCardsTests : FeatureFixture, IDisposable
    {
        private IWebDriver _webDriver;
        private string _title;
        private IWebElement _form;

        public DeletingCardsTests()
        {
            _webDriver = new ChromeDriver();
            _title = Guid.NewGuid().ToString();
        }

        private void I_am_on_the_main_page()
        {
            _webDriver.Url = "http://localhost:38978/";
            _webDriver.Navigate();
        }

        private void There_is_a_sticky_note()
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=title]")).SendKeys(_title);
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=content]")).SendKeys("fake content");
            _webDriver.FindElement(By.CssSelector("#addCardForm button")).Click();

            var titleElem = _webDriver.FindElements(By.CssSelector(".d-flex span[name=title]")).Last();
            Assert.Equal(_title, titleElem.Text.Trim());

            _form = titleElem.FindElement(By.XPath("ancestor::form"));
        }

        private void I_click_the_x_icon()
        {
            _form.FindElement(By.CssSelector(".close")).Click();
        }

        private void I_should_not_see_the_card_anymore()
        {
            var cards = _webDriver.FindElements(By.CssSelector("span[name=title]")).Where(x => x.Text.Trim() == _title);
            Assert.Empty(cards);
        }

        public void Dispose()
        {
            _webDriver.Dispose();
            _webDriver = null;
        }
    }
}
