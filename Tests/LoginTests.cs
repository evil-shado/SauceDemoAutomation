using NUnit.Framework;
using SauceDemoAutomation.PageObjects;
using SauceDemoAutomation.Utilities;
using System;

namespace SauceDemoAutomation.Tests
{
    /// <summary>
    /// LoginTests class contains all test cases related to login functionality
    /// Inherits from BaseTest to get browser setup and teardown
    /// </summary>
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private LoginPage loginPage;

        // Valid credentials for SauceDemo
        private const string VALID_USERNAME = "standard_user";
        private const string VALID_PASSWORD = "secret_sauce";

        /// <summary>
        /// Runs before each test in this class
        /// Initializes the LoginPage object
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            loginPage = new LoginPage(driver);
            Console.WriteLine("=== Starting Login Test ===");
        }

        /// <summary>
        /// Runs after each test in this class
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            Console.WriteLine("=== Login Test Completed ===");
        }

        // ============== POSITIVE TEST CASES ==============

        /// <summary>
        /// TC-001: Verify user can login with valid credentials
        /// Priority: High | Type: Smoke Test
        /// </summary>
        [Test]
        [Category("Smoke")]
        [Category("Positive")]
        [Description("TC-001: Verify user can login with valid credentials")]
        public void Test_LoginWithValidCredentials()
        {
            // Arrange
            Console.WriteLine("Test: Login with valid credentials");

            // Act
            loginPage.Login(VALID_USERNAME, VALID_PASSWORD);

            // Wait for page to load
            WaitForSeconds(2);

            // Assert
            Assert.That(driver.Url, Does.Contain("inventory.html"), 
                "User should be redirected to products/inventory page after successful login");
            
            Console.WriteLine("✓ Test Passed: User logged in successfully");
        }

        // ============== NEGATIVE TEST CASES ==============

        /// <summary>
        /// TC-002: Verify error message when login with invalid username
        /// Priority: High | Type: Negative Test
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-002: Verify error message displayed for invalid username")]
        public void Test_LoginWithInvalidUsername()
        {
            // Arrange
            string invalidUsername = "invalid_user";
            Console.WriteLine($"Test: Login with invalid username: {invalidUsername}");

            // Act
            loginPage.Login(invalidUsername, VALID_PASSWORD);
            WaitForSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                    "Error message should be displayed");
                Assert.That(loginPage.GetErrorMessage(), 
                    Does.Contain("Username and password do not match"), 
                    "Correct error message should be displayed");
            });

            Console.WriteLine("✓ Test Passed: Error message displayed correctly");
        }

        /// <summary>
        /// TC-003: Verify error message when login with invalid password
        /// Priority: High | Type: Negative Test
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-003: Verify error message displayed for invalid password")]
        public void Test_LoginWithInvalidPassword()
        {
            // Arrange
            string invalidPassword = "wrong_password";
            Console.WriteLine("Test: Login with invalid password");

            // Act
            loginPage.Login(VALID_USERNAME, invalidPassword);
            WaitForSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                    "Error message should be displayed");
                Assert.That(loginPage.GetErrorMessage(), 
                    Does.Contain("Username and password do not match"), 
                    "Correct error message should be displayed");
            });

            Console.WriteLine("✓ Test Passed: Error message displayed for invalid password");
        }

        /// <summary>
        /// TC-004: Verify error message when both username and password are invalid
        /// Priority: Medium | Type: Negative Test
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-004: Verify error message when both credentials are invalid")]
        public void Test_LoginWithInvalidCredentials()
        {
            // Arrange
            string invalidUsername = "wrong_user";
            string invalidPassword = "wrong_pass";
            Console.WriteLine("Test: Login with both invalid credentials");

            // Act
            loginPage.Login(invalidUsername, invalidPassword);
            WaitForSeconds(1);

            // Assert
            Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                "Error message should be displayed for invalid credentials");

            Console.WriteLine("✓ Test Passed: Error message displayed correctly");
        }

        /// <summary>
        /// TC-005: Verify error message when username field is empty
        /// Priority: High | Type: Negative Test
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-005: Verify error message when username is empty")]
        public void Test_LoginWithEmptyUsername()
        {
            // Arrange
            Console.WriteLine("Test: Login with empty username");

            // Act
            loginPage.EnterPassword(VALID_PASSWORD);
            loginPage.ClickLoginButton();
            WaitForSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                    "Error message should be displayed");
                Assert.That(loginPage.GetErrorMessage(), Does.Contain("Username is required"), 
                    "Error message should indicate username is required");
            });

            Console.WriteLine("✓ Test Passed: Username required error displayed");
        }

        /// <summary>
        /// TC-006: Verify error message when password field is empty
        /// Priority: High | Type: Negative Test
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-006: Verify error message when password is empty")]
        public void Test_LoginWithEmptyPassword()
        {
            // Arrange
            Console.WriteLine("Test: Login with empty password");

            // Act
            loginPage.EnterUsername(VALID_USERNAME);
            loginPage.ClickLoginButton();
            WaitForSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                    "Error message should be displayed");
                Assert.That(loginPage.GetErrorMessage(), Does.Contain("Password is required"), 
                    "Error message should indicate password is required");
            });

            Console.WriteLine("✓ Test Passed: Password required error displayed");
        }

        /// <summary>
        /// TC-007: Verify error message when both fields are empty
        /// Priority: High | Type: Negative Test
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-007: Verify error message when both fields are empty")]
        public void Test_LoginWithEmptyFields()
        {
            // Arrange
            Console.WriteLine("Test: Login with both fields empty");

            // Act
            loginPage.ClickLoginButton();
            WaitForSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                    "Error message should be displayed");
                Assert.That(loginPage.GetErrorMessage(), Does.Contain("Username is required"), 
                    "Error message should indicate username is required");
            });

            Console.WriteLine("✓ Test Passed: Empty fields validation working");
        }

        // ============== UI VALIDATION TEST CASES ==============

        /// <summary>
        /// TC-008: Verify login page elements are displayed correctly
        /// Priority: Medium | Type: UI Test
        /// </summary>
        [Test]
        [Category("Smoke")]
        [Category("UI")]
        [Description("TC-008: Verify all login page elements are present")]
        public void Test_VerifyLoginPageElements()
        {
            // Arrange
            Console.WriteLine("Test: Verify login page elements");

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsLoginPageDisplayed(), Is.True, 
                    "Login page should be displayed");
                Assert.That(loginPage.IsUsernameFieldEnabled(), Is.True, 
                    "Username field should be enabled");
                Assert.That(loginPage.IsPasswordFieldEnabled(), Is.True, 
                    "Password field should be enabled");
                Assert.That(loginPage.IsLoginButtonEnabled(), Is.True, 
                    "Login button should be enabled");
            });

            Console.WriteLine("✓ Test Passed: All login page elements are present and enabled");
        }

        /// <summary>
        /// TC-009: Verify page title on login page
        /// Priority: Low | Type: UI Test
        /// </summary>
        [Test]
        [Category("UI")]
        [Description("TC-009: Verify login page title")]
        public void Test_VerifyLoginPageTitle()
        {
            // Arrange
            Console.WriteLine("Test: Verify page title");

            // Act
            string pageTitle = loginPage.GetPageTitle();

            // Assert
            Assert.That(pageTitle, Does.Contain("Swag Labs"), 
                "Page title should contain 'Swag Labs'");

            Console.WriteLine($"✓ Test Passed: Page title is '{pageTitle}'");
        }

        /// <summary>
        /// TC-010: Verify URL on login page
        /// Priority: Low | Type: UI Test
        /// </summary>
        [Test]
        [Category("UI")]
        [Description("TC-010: Verify login page URL")]
        public void Test_VerifyLoginPageURL()
        {
            // Arrange
            Console.WriteLine("Test: Verify page URL");

            // Act
            string currentUrl = loginPage.GetCurrentUrl();

            // Assert
            Assert.That(currentUrl, Does.Contain("saucedemo.com"), 
                "URL should contain saucedemo.com");

            Console.WriteLine($"✓ Test Passed: Current URL is '{currentUrl}'");
        }

        // ============== SPECIAL TEST CASES ==============

        /// <summary>
        /// TC-011: Verify login with locked out user
        /// Priority: Medium | Type: Negative Test
        /// Note: SauceDemo has a special "locked_out_user" for testing
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-011: Verify error message for locked out user")]
        public void Test_LoginWithLockedOutUser()
        {
            // Arrange
            string lockedUsername = "locked_out_user";
            Console.WriteLine($"Test: Login with locked out user: {lockedUsername}");

            // Act
            loginPage.Login(lockedUsername, VALID_PASSWORD);
            WaitForSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                    "Error message should be displayed");
                Assert.That(loginPage.GetErrorMessage(), 
                    Does.Contain("Sorry, this user has been locked out"), 
                    "Error message should indicate user is locked out");
            });

            Console.WriteLine("✓ Test Passed: Locked user error displayed correctly");
        }

        /// <summary>
        /// TC-012: Verify case sensitivity of username
        /// Priority: Low | Type: Negative Test
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("Negative")]
        [Description("TC-012: Verify username is case sensitive")]
        public void Test_LoginWithUppercaseUsername()
        {
            // Arrange
            string uppercaseUsername = "STANDARD_USER";
            Console.WriteLine("Test: Login with uppercase username");

            // Act
            loginPage.Login(uppercaseUsername, VALID_PASSWORD);
            WaitForSeconds(1);

            // Assert
            Assert.That(loginPage.IsErrorMessageDisplayed(), Is.True, 
                "Error should be displayed for case-sensitive username mismatch");

            Console.WriteLine("✓ Test Passed: Username is case sensitive");
        }
    }
}
