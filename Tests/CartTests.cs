using NUnit.Framework;
using SauceDemoAutomation.PageObjects;
using SauceDemoAutomation.Utilities;
using System;

namespace SauceDemoAutomation.Tests
{
    [TestFixture]
    public class CartTests : BaseTest
    {
        private LoginPage loginPage;
        private ProductsPage productsPage;
        private CartPage cartPage;

        [SetUp]
        public void TestSetup()
        {
            loginPage = new LoginPage(driver);
            productsPage = new ProductsPage(driver);
            cartPage = new CartPage(driver);

            // Login first
            loginPage.Login("standard_user", "secret_sauce");
            WaitForSeconds(2);
        }

        [Test]
        [Category("Smoke")]
        public void Test_VerifyCartPage()
        {
            // Navigate to cart
            productsPage.ClickShoppingCart();
            WaitForSeconds(2);

            // Verify
            Assert.That(cartPage.GetPageTitle(), Is.EqualTo("Your Cart"));
        }
    }
}