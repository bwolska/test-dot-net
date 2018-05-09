using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWeb
{

    public class HelloWebTests: IDisposable
    {
        private readonly string BlogUrl = "https://autotestdotnet.wordpress.com/";
        private IWebDriver browser;

        public HelloWebTests()
        {
            browser = new ChromeDriver();
            browser.Manage().Window.Maximize();
        }

        [Fact]
        public void Can_open_blog_and_hello_note_exits()
        {
 
            browser.Navigate().GoToUrl(BlogUrl);
            var article = browser.FindElement(By.Id("post-3096"));
            var header = article.FindElement(By.TagName("h1"));
            Assert.Equal("Witamy na warsztatach automatyzacji testów!", header.Text);

            Dispose();
        }

        [Fact]
        public void Can_add_comment_to_existing_note()
        {
            // arrange

            browser.Navigate().GoToUrl(BlogUrl);

            // wygeneruj tekst notatki
            var TekstNotatki = Guid.NewGuid().ToString();

            var commentslink = browser.FindElement(By.CssSelector(".comments-link > a"));
            commentslink.Click();
            var commentsfield = browser.FindElement(By.CssSelector("textarea#comment"));
            commentsfield.SendKeys(TekstNotatki);

            var emailfield = browser.FindElement(By.Id("email"));
            emailfield.SendKeys(GenerateEmail());

            var authorfield = browser.FindElement(By.Id("author"));
            authorfield.SendKeys("www");

            browser.FindElement(By.Id("comment-submit")).Click();

            // assert
            var  comments = browser.FindElements(By.ClassName("comment-body"));
            var my_comment = comments.Where(c => c.FindElement(By.CssSelector(".comment-content > p")).Text == TekstNotatki);

            Assert.Single(my_comment);
        }

        private string GenerateEmail()
        {
            var user = Guid.NewGuid().ToString();
            return $"{user}@nonexistent.test.com";
        }

        public void Dispose()
        {
            try
            {
                browser.Quit();
            }
            catch (Exception)
            {
            }
        }
    }
}
