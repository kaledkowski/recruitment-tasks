using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using Xunit;

namespace Tests.AcceptanceTests
{
    public class EditingContext : IDisposable
    {
        private IWebDriver _webDriver;
        private IWebElement _form;

        public EditingContext()
        {
            _webDriver = new ChromeDriver();
        }

        public void Given_I_am_on_the_main_page()
        {
            _webDriver.Url = "http://localhost:38978/";
            _webDriver.Navigate();
        }

        public void And_there_is_a_sticky_note(string title, string content)
        {
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=title]")).SendKeys(title);
            _webDriver.FindElement(By.CssSelector("#addCardForm [name=content]")).SendKeys(content);
            _webDriver.FindElement(By.CssSelector("#addCardForm button")).Click();

            var titleElem = _webDriver.FindElements(By.CssSelector(".d-flex span[name=title]")).Last();
            Assert.Equal(title, titleElem.Text.Trim());

            _form = titleElem.FindElement(By.XPath("ancestor::form"));
        }

        public void When_I_click_on_its_title()
        {
            _form.FindElement(By.CssSelector("span[name=title]")).Click();
        }

        public void When_I_click_on_its_content()
        {
            _form.FindElement(By.CssSelector("p[name=content]")).Click();
        }

        public void And_I_type_in_a_new_title(string title)
        {
            var element = _form.FindElement(By.CssSelector("input"));
            element.Clear();
            element.SendKeys(title);
        }

        public void And_I_type_in_a_new_content(string content)
        {
            var element = _form.FindElement(By.CssSelector("textarea"));
            element.Clear();
            element.SendKeys(content);
        }

        public void And_I_click_on_save_button()
        {
            _form.FindElement(By.CssSelector(".save-btn")).Click();
        }

        public void Then_I_should_see_the_note_with_new_title(string title, string content)
        {
            var titleElement = _webDriver.FindElements(By.CssSelector("span[name=title]")).FirstOrDefault(x => x.Text.Trim() == title);
            Assert.NotNull(titleElement);

            var form = titleElement.FindElement(By.XPath("ancestor::form"));

            var currentContent = form.FindElement(By.CssSelector("p[name=content]")).Text.Trim();
            Assert.Equal(content, currentContent);
        }

        public void Then_I_should_see_the_note_with_new_content(string title, string content)
        {
            var titleElement = _webDriver.FindElements(By.CssSelector("span[name=title]")).FirstOrDefault(x => x.Text.Trim() == title);
            Assert.NotNull(titleElement);

            var form = titleElement.FindElement(By.XPath("ancestor::form"));

            var currentContent = form.FindElement(By.CssSelector("p[name=content]")).Text.Trim();
            Assert.Equal(content, currentContent);
        }

        public void Dispose()
        {
            _webDriver.Dispose();
            _webDriver = null;
            _form = null;
        }
    }
}