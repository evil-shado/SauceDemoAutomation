using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace SauceDemoAutomation.Utilities
{
    /// <summary>
    /// BaseTest class contains common setup and teardown methods for all tests
    /// All test classes will inherit from this class
    /// </summary>
    public class BaseTest
    {
        protected IWebDriver driver;
        protected string baseURL = "https://www.saucedemo.com/";

        /// <summary>
        /// This method runs before each test
        /// Sets up the Chrome browser and navigates to the base URL
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Initialize Chrome driver
            driver = new ChromeDriver();
            
            // Maximize browser window
            driver.Manage().Window.Maximize();
            
            // Set implicit wait - driver will wait up to 10 seconds for elements to appear
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            // Set page load timeout
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            
            // Navigate to the base URL
            driver.Navigate().GoToUrl(baseURL);
            
            Console.WriteLine("Browser opened and navigated to: " + baseURL);
        }

        /// <summary>
        /// This method runs after each test
        /// Closes the browser and cleans up
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            // Take screenshot if test fails
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }

            // Close and quit the browser
            if (driver != null)
            {
                driver.Quit();
                Console.WriteLine("Browser closed successfully");
            }
        }

        /// <summary>
        /// Takes a screenshot and saves it with the test name
        /// </summary>
        /// <param name="testName">Name of the test that failed</param>
        protected void TakeScreenshot(string testName)
        {
            try
            {
                // Create Screenshots directory if it doesn't exist
                string screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                if (!Directory.Exists(screenshotDirectory))
                {
                    Directory.CreateDirectory(screenshotDirectory);
                }

                // Take screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                
                // Create filename with timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{testName}_{timestamp}.png";
                string filePath = Path.Combine(screenshotDirectory, fileName);
                
                // Save screenshot
                screenshot.SaveAsFile(filePath);
                
                Console.WriteLine($"Screenshot saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to take screenshot: {ex.Message}");
            }
        }

        /// <summary>
        /// Helper method to wait for a specific time (use sparingly)
        /// </summary>
        /// <param name="seconds">Number of seconds to wait</param>
        protected void WaitForSeconds(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }
    }
}
