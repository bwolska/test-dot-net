using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary2
{
    internal class ExampleComment
    {
        public string Name { get; }
        public string Email { get; }
        public string Text { get; }
        public string Reply { get; }

        public ExampleComment()
        {
            Name = GenerateUserName();
            Email = GenerateEmail();
            Text = GenerateComment();
            Reply = GenerateComment();
        }

        private string GenerateComment()
        {
            return Guid.NewGuid().ToString();
        }


        private string GenerateUserName()
        {
            //return Guid.NewGuid().ToString();
            return "www";
        }

        private string GenerateEmail()
        {
            var user = Guid.NewGuid().ToString();
            return $"{user}@nonexistent.test.com";
        }
    }

    internal class Note
    {
        private IWebDriver _driver;
        private readonly string _url;

        public Note(IWebDriver driver, string url)
        {
            _driver = driver;
            _url = url;
            _driver.Navigate().GoToUrl(url);
        }

        internal void AddComment(ExampleComment comment)
        {
            var commentElement = _driver.FindElement(By.Id("comment"));
            commentElement.SendKeys(comment.Text);

            var emailElement = _driver.FindElement(By.Id("email"));
            emailElement.SendKeys(comment.Email);

            var userElement = _driver.FindElement(By.Id("author"));
            userElement.SendKeys(comment.Name);

            var submitElement = _driver.FindElement(By.Id("comment-submit"));

            submitElement.Click();
        }

        internal void AddReply(ExampleComment testComment)
        {

            // find latest comment by user and click reply link
            IWebElement replyLink = ReplyToLastCommentMadeBy(testComment.Name);
            replyLink.Click();

            //var TekstOdpowiedzi = Guid.NewGuid().ToString();
            var replylink = _driver.FindElement(By.CssSelector(".reply > a"));
            replylink.Click();

            var textarealocator = By.CssSelector("textarea#comment");
            WaitForClickable(textarealocator, 10);

            var replyfield = _driver.FindElement(textarealocator);
            replyfield.SendKeys(testComment.Reply);

            _driver.FindElement(By.Id("comment-submit")).Click();
        }

        private void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        private void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }


        internal IEnumerable<IWebElement> SearchCommentsByText(ExampleComment comment)
        {
            var comments = _driver.FindElements(By.ClassName("comment-content"));

            return comments.Where(c => c.Text.Contains(comment.Text));
        }

        internal IEnumerable<IWebElement> SearchRepliesByText(ExampleComment comment)
        {
            var comments = _driver.FindElements(By.ClassName("comment-content"));
            var replies = comments.Where(c => c.Text.Contains(comment.Text));

            return comments.Where(c => c.Text.Contains(comment.Text));
        }

        private IWebElement ReplyToLastCommentMadeBy(string authorName)
        {
            // find all comments for the note
            IReadOnlyCollection<IWebElement> allComments = _driver.FindElements(By.ClassName("comment-body"));

            // in all comments find those written by user with provided name
            IEnumerable<IWebElement> commentsByAuthor = allComments.Where(cmt => cmt.FindElement(By.CssSelector(".comment-author > cite")).Text == authorName);

            // Fetch the latest comment - it will be first in the collection
            IWebElement latestCommentBAuthor = commentsByAuthor.First();

            // For this last comment find and click on "Reply" link
            return latestCommentBAuthor.FindElement(By.ClassName("comment-reply-link"));
        }

        internal void LogIn()
        {
            
        }
    }

    public class HelloWebWithPageObjectTests : IDisposable
    {
        private const string FirstNoteUrl = "https://autotestdotnet.wordpress.com/2018/05/07/witamy-na-warsztatach-automatyzacji-testow";
        private const string SecondNoteUrl = "https://autotestdotnet.wordpress.com/wp-admin";

        private IWebDriver driver;
        private readonly ExampleComment testComment;

        public HelloWebWithPageObjectTests()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            testComment = new ExampleComment();          
        }

        [Fact]
        public void Can_add_comment_to_existing_note()
        {
            //arrange
            var welcomeNote = new Note(driver, FirstNoteUrl);

            // act
            welcomeNote.AddComment(testComment);

            //assert
            var comments = welcomeNote.SearchCommentsByText(testComment);
            Assert.Single(comments);
        }

        [Fact]
        public void Can_add_reply_to_existing_comment()
        {
            //arrange
            var welcomeNote = new Note(driver, FirstNoteUrl);
            welcomeNote.AddComment(testComment);

            // act
            welcomeNote.AddReply(testComment);

            //assert
 
            var comment_blocks = driver.FindElements(By.CssSelector("li.comment"));
            var my_comment_block = comment_blocks.Where(x => x.FindElement(By.ClassName("comment-content")).Text.Contains(testComment.Text));
            Assert.Single(my_comment_block);

            var reply_blocks = my_comment_block.First().FindElements(By.CssSelector("ul.children"));
            var my_reply_block = reply_blocks.Where(y => y.FindElement(By.ClassName("comment-content")).Text.Contains(testComment.Reply));
            Assert.Single(my_reply_block);
        }

        [Fact]
        public void Can_add_note()
        {
            //arrange
            var LoginPage = new Login(driver);
            LoginPage.LogMeIn(user);
            
            //usernameOrEmail


        }

        public void Dispose()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
