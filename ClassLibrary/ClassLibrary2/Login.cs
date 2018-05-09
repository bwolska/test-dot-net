using OpenQA.Selenium;

namespace ClassLibrary2
{
    internal class Login
    {
        private IWebDriver _driver;
        private string LoginPageUrl = "https://autotestdotnet.wordpress.com/wp-admin";

        public Login(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(LoginPageUrl);
        }
    }
}