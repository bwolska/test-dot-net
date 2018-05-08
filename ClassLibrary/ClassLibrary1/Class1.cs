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
