using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary3
{
    public class GooglePageFactoryExample : IDisposable
    {
        private IWebDriver _driver;

        public GooglePageFactoryExample()
        {
            //_driver = new FirefoxDriver();
            _driver = new ChromeDriver();
        }

        [Theory,
            InlineData("code sprinters", "Code Sprinters - Agile Experts"),
            InlineData("microsoft", "Microsoft — oficjalna strona główna")
            ]
        public void Can_search_term_in_google(string query, string expected)
        {
            //arrange
            var googleMainPage = new GoogleMainPage(_driver);

            //act
            var resultPage = googleMainPage.Search(query);

            //assert
            Assert.True(resultPage.Contains(expected));
        }

        public void Dispose()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    internal class GoogleMainPage
    {
        private const string GoogleMainPageUrl = "https://www.google.com/";
        private readonly IWebDriver _driver;

        public GoogleMainPage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(GoogleMainPageUrl);

            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "lst-ib")]
        public IWebElement QueryBox;


        public ResultPage Search(string query)
        {
            QueryBox.SendKeys(query);
            QueryBox.Submit();

            return new ResultPage(_driver);
        }
    }

    public class ResultPage
    {
        private IWebDriver _driver;

        public ResultPage(IWebDriver driver)
        {
            _driver = driver;

            WaitForClickable(By.CssSelector("h3.r"));
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h3.r")]
        public IList<IWebElement> SearchResults;


        public bool Contains(string expected)
        {
            return SearchResults.Any(s => s.Text.Contains(expected));
        }

        private void WaitForClickable(By by, int seconds = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
    }
}
