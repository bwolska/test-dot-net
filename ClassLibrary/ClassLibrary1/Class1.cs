using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWeb
{

    public class HelloWebTests : IDisposable
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
            var comments = browser.FindElements(By.ClassName("comment-body"));
            var my_comment = comments.Where(c => c.FindElement(By.CssSelector(".comment-content > p")).Text == TekstNotatki);

            Assert.Single(my_comment);
        }

        [Fact]
        public void Can_add_reply_to_existing_comment()
        {

            // open home page
            browser.Navigate().GoToUrl(BlogUrl);

            // find and click on "commenst" link for the first note
            IWebElement commentsLink = browser.FindElement(By.CssSelector(".comments-link > a"));
            commentsLink.Click();

            // find latest comment by www and click reply link
            IWebElement replyLink = ReplyToLastCommentMadeBy("www");
            replyLink.Click();

            var TekstOdpowiedzi = Guid.NewGuid().ToString();
            var replylink = browser.FindElement(By.CssSelector(".reply > a"));
            replylink.Click();
            var textarealocator = By.CssSelector("textarea#comment");
            WaitForClickable(textarealocator, 10);
            var replyfield = browser.FindElement(textarealocator);
            replyfield.SendKeys(TekstOdpowiedzi);

            var emailfield = browser.FindElement(By.Id("email"));
            emailfield.SendKeys(GenerateEmail());

            var authorfield = browser.FindElement(By.Id("author"));
            authorfield.SendKeys("www");

            browser.FindElement(By.Id("comment-submit")).Click();

            // assert

            //var replies = browser.FindElements(By.ClassName("comment-body"));
            //var my_reply = replies.Where(c => c.FindElement(By.CssSelector(".reply > p")).Text == TekstOdpowiedzi);

            //Assert.Single(my_reply);

        }

        private IWebElement ReplyToLastCommentMadeBy(string authorName)
        {
            // find all comments for the note
            IReadOnlyCollection<IWebElement> allComments = browser.FindElements(By.ClassName("comment-body"));

            // in all comments find those written by user with provided name
            IEnumerable<IWebElement> commentsByAuthor = allComments.Where(cmt => cmt.FindElement(By.CssSelector(".comment-author > cite")).Text == authorName);

            // Fetch the latest comment - it will be first in the collection
            IWebElement latestCommentBAuthor = commentsByAuthor.First();

            // For this last comment find and click on "Reply" link
            return latestCommentBAuthor.FindElement(By.ClassName("comment-reply-link"));
        }

        private string GenerateEmail()
        {
            var user = Guid.NewGuid().ToString();
            return $"{user}@nonexistent.test.com";
        }


        private void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        private void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
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
