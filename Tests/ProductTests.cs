using NUnit.Framework;
using SauceDemoAutomation.PageObjects;
using SauceDemoAutomation.Utilities;
using System;
using System.Collections.Generic;

namespace SauceDemoAutomation.Tests
{
    /// <summary>
    /// ProductTests class contains all test cases related to products page functionality
    /// </summary>
    [TestFixture]
    public class ProductTests : BaseTest
    {
        private LoginPage loginPage;
        private ProductsPage productsPage;

        private const string VALID_USERNAME = "standard_user";
        private const string VALID_PASSWORD = "secret_sauce";

        /// <summary>
        /// Setup before each test - login to reach products page
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            loginPage = new LoginPage(driver);
            productsPage = new ProductsPage(driver);

            // Login to reach products page
            loginPage.Login(VALID_USERNAME, VALID_PASSWORD);
            WaitForSeconds(2);

            Console.WriteLine("=== Starting Product Test ===");
        }

        /// <summary>
        /// Cleanup after each test
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            Console.WriteLine("=== Product Test Completed ===");
        }

        // ============== PAGE VERIFICATION TESTS ==============

        /// <summary>
        /// TC-013: Verify products page is displayed after login
        /// Priority: High | Type: Smoke Test
        /// </summary>
        [Test]
        [Category("Smoke")]
        [Description("TC-013: Verify products page loads after successful login")]
        public void Test_VerifyProductsPageDisplayed()
        {
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(productsPage.IsProductsPageDisplayed(), Is.True,
                    "Products page should be displayed");
                Assert.That(productsPage.GetPageTitle(), Is.EqualTo("Products"),
                    "Page title should be 'Products'");
            });

            Console.WriteLine("✓ Test Passed: Products page displayed successfully");
        }

        /// <summary>
        /// TC-014: Verify all 6 products are displayed
        /// Priority: High | Type: Smoke Test
        /// </summary>
        [Test]
        [Category("Smoke")]
        [Description("TC-014: Verify all products are displayed on the page")]
        public void Test_VerifyAllProductsDisplayed()
        {
            // Act
            int productCount = productsPage.GetProductCount();

            // Assert
            Assert.That(productCount, Is.EqualTo(6),
                "6 products should be displayed on the page");

            Console.WriteLine($"✓ Test Passed: {productCount} products are displayed");
        }

        /// <summary>
        /// TC-015: Verify all products have names displayed
        /// Priority: Medium | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-015: Verify all products have names")]
        public void Test_VerifyProductNamesDisplayed()
        {
            // Act
            List<string> productNames = productsPage.GetAllProductNames();

            // Assert
            Assert.That(productNames, Has.Count.EqualTo(6),
                "All 6 products should have names");
            Assert.That(productNames, Has.All.Not.Empty,
                "No product name should be empty");

            Console.WriteLine("✓ Test Passed: All products have names");
            productNames.ForEach(name => Console.WriteLine($"  - {name}"));
        }

        /// <summary>
        /// TC-016: Verify all products have prices displayed
        /// Priority: Medium | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-016: Verify all products have prices")]
        public void Test_VerifyProductPricesDisplayed()
        {
            // Act
            List<string> productPrices = productsPage.GetAllProductPrices();

            // Assert
            Assert.That(productPrices, Has.Count.EqualTo(6),
                "All 6 products should have prices");
            Assert.That(productPrices, Has.All.Contain("$"),
                "All prices should contain '$' symbol");

            Console.WriteLine("✓ Test Passed: All products have prices");
            productPrices.ForEach(price => Console.WriteLine($"  - {price}"));
        }

        // ============== SORTING TESTS ==============

        /// <summary>
        /// TC-017: Verify products can be sorted A to Z
        /// Priority: High | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-017: Verify products sort alphabetically A to Z")]
        public void Test_SortProductsAtoZ()
        {
            // Act
            productsPage.SortByNameAtoZ();
            WaitForSeconds(1);

            // Assert
            Assert.That(productsPage.AreProductsSortedAtoZ(), Is.True,
                "Products should be sorted alphabetically A to Z");

            Console.WriteLine("✓ Test Passed: Products sorted A to Z");
        }

        /// <summary>
        /// TC-018: Verify products can be sorted Z to A
        /// Priority: High | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-018: Verify products sort alphabetically Z to A")]
        public void Test_SortProductsZtoA()
        {
            // Act
            productsPage.SortByNameZtoA();
            WaitForSeconds(1);

            // Assert
            Assert.That(productsPage.AreProductsSortedZtoA(), Is.True,
                "Products should be sorted alphabetically Z to A");

            Console.WriteLine("✓ Test Passed: Products sorted Z to A");
        }

        /// <summary>
        /// TC-019: Verify products can be sorted by price (low to high)
        /// Priority: High | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-019: Verify products sort by price low to high")]
        public void Test_SortProductsByPriceLowToHigh()
        {
            // Act
            productsPage.SortByPriceLowToHigh();
            WaitForSeconds(1);

            // Assert
            Assert.That(productsPage.AreProductsSortedByPriceLowToHigh(), Is.True,
                "Products should be sorted by price from low to high");

            Console.WriteLine("✓ Test Passed: Products sorted by price (low to high)");
        }

        /// <summary>
        /// TC-020: Verify products can be sorted by price (high to low)
        /// Priority: High | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-020: Verify products sort by price high to low")]
        public void Test_SortProductsByPriceHighToLow()
        {
            // Act
            productsPage.SortByPriceHighToLow();
            WaitForSeconds(1);

            // Assert
            Assert.That(productsPage.AreProductsSortedByPriceHighToLow(), Is.True,
                "Products should be sorted by price from high to low");

            Console.WriteLine("✓ Test Passed: Products sorted by price (high to low)");
        }

        // ============== ADD TO CART TESTS ==============

        /// <summary>
        /// TC-021: Verify adding a single product to cart
        /// Priority: High | Type: Smoke Test
        /// </summary>
        [Test]
        [Category("Smoke")]
        [Description("TC-021: Verify user can add a product to cart")]
        public void Test_AddSingleProductToCart()
        {
            // Arrange
            string productName = "Sauce Labs Backpack";

            // Act
            productsPage.AddProductToCart(productName);
            WaitForSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(productsPage.IsCartBadgeDisplayed(), Is.True,
                    "Cart badge should be displayed");
                Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo(1),
                    "Cart badge should show 1 item");
            });

            Console.WriteLine($"✓ Test Passed: Added {productName} to cart");
        }

        /// <summary>
        /// TC-022: Verify adding multiple products to cart
        /// Priority: High | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-022: Verify cart badge updates when adding multiple products")]
        public void Test_AddMultipleProductsToCart()
        {
            // Act
            productsPage.AddProductToCart("Sauce Labs Backpack");
            WaitForSeconds(1);
            productsPage.AddProductToCart("Sauce Labs Bike Light");
            WaitForSeconds(1);
            productsPage.AddProductToCart("Sauce Labs Bolt T-Shirt");
            WaitForSeconds(1);

            // Assert
            Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo(3),
                "Cart badge should show 3 items");

            Console.WriteLine("✓ Test Passed: Added 3 products to cart, badge shows correct count");
        }

        /// <summary>
        /// TC-023: Verify cart badge increments correctly
        /// Priority: Medium | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-023: Verify cart badge count increments with each product added")]
        public void Test_VerifyCartBadgeIncrement()
        {
            // Initially, cart should be empty
            Assert.That(productsPage.IsCartBadgeDisplayed(), Is.False,
                "Cart badge should not be displayed when cart is empty");

            // Add first product
            productsPage.AddProductToCart("Sauce Labs Backpack");
            WaitForSeconds(1);
            Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo(1),
                "Cart badge should show 1 after adding first product");

            // Add second product
            productsPage.AddProductToCart("Sauce Labs Bike Light");
            WaitForSeconds(1);
            Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo(2),
                "Cart badge should show 2 after adding second product");

            Console.WriteLine("✓ Test Passed: Cart badge increments correctly");
        }

        /// <summary>
        /// TC-024: Verify removing product from cart on products page
        /// Priority: Medium | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-024: Verify user can remove product from products page")]
        public void Test_RemoveProductFromProductsPage()
        {
            // Arrange - Add product first
            string productName = "Sauce Labs Backpack";
            productsPage.AddProductToCart(productName);
            WaitForSeconds(1);

            // Verify product was added
            Assert.That(productsPage.GetCartBadgeCount(), Is.EqualTo(1),
                "Product should be added to cart");

            // Act - Remove product
            productsPage.RemoveProductFromCart(productName);
            WaitForSeconds(1);

            // Assert
            Assert.That(productsPage.IsCartBadgeDisplayed(), Is.False,
                "Cart badge should not be displayed after removing the only product");

            Console.WriteLine("✓ Test Passed: Product removed from cart successfully");
        }

        /// <summary>
        /// TC-025: Verify clicking on product opens product details
        /// Priority: Medium | Type: Regression
        /// </summary>
        [Test]
        [Category("Regression")]
        [Description("TC-025: Verify clicking product name opens product details")]
        public void Test_ClickProductToViewDetails()
        {
            // Arrange
            string productName = "Sauce Labs Backpack";

            // Act
            productsPage.ClickProductByName(productName);
            WaitForSeconds(2);

            // Assert
            Assert.That(driver.Url, Does.Contain("inventory-item.html"),
                "URL should contain 'inventory-item.html' for product details page");

            Console.WriteLine($"✓ Test Passed: Clicked on {productName} and details page opened");
        }

        /// <summary>
        /// TC-026: Verify shopping cart icon is clickable
        /// Priority: High | Type: Smoke Test
        /// </summary>
        [Test]
        [Category("Smoke")]
        [Description("TC-026: Verify clicking cart icon navigates to cart page")]
        public void Test_ClickShoppingCartIcon()
        {
            // Act
            productsPage.ClickShoppingCart();
            WaitForSeconds(2);

            // Assert
            Assert.That(driver.Url, Does.Contain("cart.html"),
                "URL should contain 'cart.html' after clicking cart icon");

            Console.WriteLine("✓ Test Passed: Shopping cart icon navigated to cart page");
        }
    }
}
