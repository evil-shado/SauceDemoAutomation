using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SauceDemoAutomation.PageObjects
{
    /// <summary>
    /// LoginPage class represents the Login page of SauceDemo application
    /// Contains all elements and actions related to the login functionality
    /// This follows the Page Object Model (POM) design pattern
    /// </summary>
    public class LoginPage
    {
        private IWebDriver driver;

        // ============== LOCATORS ==============
        // Define all web elements on the login page using different locator strategies

        // Input fields
        private By usernameField = By.Id("user-name");
        private By passwordField = By.Id("password");
        
        // Buttons
        private By loginButton = By.Id("login-button");
        
        // Error messages
        private By errorMessage = By.CssSelector("[data-test='error']");
        private By errorCloseButton = By.CssSelector(".error-button");
        
        // Other elements
        private By loginLogo = By.ClassName("login_logo");

        // ============== CONSTRUCTOR ==============
        /// <summary>
        /// Constructor to initialize the driver
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ============== ACTIONS/METHODS ==============
        // These methods represent actions a user can perform on the login page

        /// <summary>
        /// Enter username in the username field
        /// </summary>
        /// <param name="username">Username to enter</param>
        public void EnterUsername(string username)
        {
            driver.FindElement(usernameField).Clear(); // Clear existing text
            driver.FindElement(usernameField).SendKeys(username);
            Console.WriteLine($"Entered username: {username}");
        }

        /// <summary>
        /// Enter password in the password field
        /// </summary>
        /// <param name="password">Password to enter</param>
        public void EnterPassword(string password)
        {
            driver.FindElement(passwordField).Clear(); // Clear existing text
            driver.FindElement(passwordField).SendKeys(password);
            Console.WriteLine("Entered password");
        }

        /// <summary>
        /// Click the login button
        /// </summary>
        public void ClickLoginButton()
        {
            driver.FindElement(loginButton).Click();
            Console.WriteLine("Clicked login button");
        }

        /// <summary>
        /// Complete login action with username and password
        /// This is a combined method for convenience
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        /// <summary>
        /// Get the error message text
        /// </summary>
        /// <returns>Error message as string</returns>
        public string GetErrorMessage()
        {
            try
            {
                return driver.FindElement(errorMessage).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Check if error message is displayed
        /// </summary>
        /// <returns>True if error message is displayed, false otherwise</returns>
        public bool IsErrorMessageDisplayed()
        {
            try
            {
                return driver.FindElement(errorMessage).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Click the error close button (X button on error)
        /// </summary>
        public void ClickErrorCloseButton()
        {
            if (IsErrorMessageDisplayed())
            {
                driver.FindElement(errorCloseButton).Click();
                Console.WriteLine("Closed error message");
            }
        }

        /// <summary>
        /// Check if login page is displayed
        /// </summary>
        /// <returns>True if on login page, false otherwise</returns>
        public bool IsLoginPageDisplayed()
        {
            try
            {
                return driver.FindElement(loginLogo).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the page title
        /// </summary>
        /// <returns>Page title as string</returns>
        public string GetPageTitle()
        {
            return driver.Title;
        }

        /// <summary>
        /// Get current URL
        /// </summary>
        /// <returns>Current URL as string</returns>
        public string GetCurrentUrl()
        {
            return driver.Url;
        }

        /// <summary>
        /// Clear username field
        /// </summary>
        public void ClearUsername()
        {
            driver.FindElement(usernameField).Clear();
        }

        /// <summary>
        /// Clear password field
        /// </summary>
        public void ClearPassword()
        {
            driver.FindElement(passwordField).Clear();
        }

        /// <summary>
        /// Check if username field is enabled
        /// </summary>
        /// <returns>True if enabled, false otherwise</returns>
        public bool IsUsernameFieldEnabled()
        {
            return driver.FindElement(usernameField).Enabled;
        }

        /// <summary>
        /// Check if password field is enabled
        /// </summary>
        /// <returns>True if enabled, false otherwise</returns>
        public bool IsPasswordFieldEnabled()
        {
            return driver.FindElement(passwordField).Enabled;
        }

        /// <summary>
        /// Check if login button is enabled
        /// </summary>
        /// <returns>True if enabled, false otherwise</returns>
        public bool IsLoginButtonEnabled()
        {
            return driver.FindElement(loginButton).Enabled;
        }
    }
}
