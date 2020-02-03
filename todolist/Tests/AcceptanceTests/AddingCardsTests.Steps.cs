using System;
using System.Linq;
using LightBDD.XUnit2;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Tests.AcceptanceTests
{
    public partial class AddingCardsTests : FeatureFixture, IDisposable
    {
        private IWebDriver _webDriver;
        private string _title;
        private string _content;

        public AddingCardsTests()
        {
            _webDriver = new ChromeDriver();
            _title = Guid.NewGuid().ToString();
            _content = Guid.NewGuid().ToString();
        }

		private void I_am_on_the_main_page()
        {
            _webDriver.Url = "http://localhost:38978/";
            _webDriver.Navigate();
        }

		private void I_type_in_a_note_title()
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=title]")).SendKeys(_title);
        }

        private void I_type_in_a_note_contents()
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=content]")).SendKeys(_content);
        }

		private void I_click_the_add_card_button()
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm button")).Click();
        }

		private void I_should_see_the_card_appended_to_the_list()
        {
            var title = _webDriver.FindElements(By.CssSelector("span[name=title]")).FirstOrDefault(x => x.Text.Trim() == _title);

            Assert.NotNull(title);
            var form = title.FindElement(By.XPath("ancestor::form"));

            var content = form.FindElement(By.CssSelector("p[name=content]")).Text.Trim();
            Assert.Equal(_content, content);
        }

        public void Dispose()
        {
            _webDriver.Dispose();
            _webDriver = null;
        }
    }
}
