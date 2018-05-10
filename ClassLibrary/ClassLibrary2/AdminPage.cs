using OpenQA.Selenium;
using System.Linq;

namespace ClassLibrary2
{
    internal class AdminPage
    {
        private IWebDriver _driver;

        public AdminPage(IWebDriver driver)
        {
            _driver = driver;
            var menuElements = _driver.FindElements(By.ClassName("wp-menu-name"));
            var PostsMenu = menuElements.Where((c => c.Text.Contains("Posts")));
            var postmenu = PostsMenu.First();
            postmenu.Click();
            var PostSubMenuElements = _driver.FindElements(By.CssSelector(".wp - submenu > li > a"));
            var PostSubMenuElement = PostSubMenuElements.Where(e => e.Text.Contains("Add New"));
            var postmenuelement = PostSubMenuElement.First();
            postmenuelement.Click();
        }
    }
}