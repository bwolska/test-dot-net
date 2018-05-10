using System;
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

        internal void LogMeIn(string user, string password)
        {
            var loginElement = _driver.FindElement(By.CssSelector("input#usernameOrEmail.form-text-input "));
            loginElement.SendKeys(user + "\n");
            var passwordElement = _driver.FindElement(By.CssSelector("input#password.form-text-input "));
            passwordElement.SendKeys(password + "\n");
        }
    }
}