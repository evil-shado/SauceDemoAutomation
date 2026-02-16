using OpenQA.Selenium;
using System;

namespace SauceDemoAutomation.PageObjects
{
    public class CartPage
    {
        private IWebDriver driver;

        // Locators
        private By cartTitle = By.ClassName("title");

        // Constructor
        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Methods
        public string GetPageTitle()
        {
            return driver.FindElement(cartTitle).Text;
        }
    }
}