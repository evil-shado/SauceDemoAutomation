using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SauceDemoAutomation.PageObjects
{
    /// <summary>
    /// ProductsPage class represents the Products/Inventory page of SauceDemo
    /// This page is displayed after successful login
    /// </summary>
    public class ProductsPage
    {
        private IWebDriver driver;

        // ============== LOCATORS ==============

        // Page header and navigation
        private By pageTitle = By.ClassName("title");
        private By shoppingCartLink = By.ClassName("shopping_cart_link");
        private By cartBadge = By.ClassName("shopping_cart_badge");
        private By hamburgerMenu = By.Id("react-burger-menu-btn");

        // Product sorting dropdown
        private By sortDropdown = By.ClassName("product_sort_container");

        // Product items
        private By productItems = By.ClassName("inventory_item");
        private By productNames = By.ClassName("inventory_item_name");
        private By productPrices = By.ClassName("inventory_item_price");
        private By productDescriptions = By.ClassName("inventory_item_desc");
        private By productImages = By.ClassName("inventory_item_img");

        // Add to cart buttons
        private By addToCartButtons = By.CssSelector("button[id^='add-to-cart']");
        private By removeButtons = By.CssSelector("button[id^='remove']");

        // ============== CONSTRUCTOR ==============
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // ============== VERIFICATION METHODS ==============

        /// <summary>
        /// Verify if products page is displayed
        /// </summary>
        public bool IsProductsPageDisplayed()
        {
            try
            {
                return driver.FindElement(pageTitle).Displayed &&
                       driver.FindElement(pageTitle).Text.Equals("Products", StringComparison.OrdinalIgnoreCase);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get page title text
        /// </summary>
        public string GetPageTitle()
        {
            return driver.FindElement(pageTitle).Text;
        }

        // ============== PRODUCT RELATED METHODS ==============

        /// <summary>
        /// Get count of products displayed on the page
        /// </summary>
        public int GetProductCount()
        {
            return driver.FindElements(productItems).Count;
        }

        /// <summary>
        /// Get list of all product names
        /// </summary>
        public List<string> GetAllProductNames()
        {
            var nameElements = driver.FindElements(productNames);
            return nameElements.Select(element => element.Text).ToList();
        }

        /// <summary>
        /// Get list of all product prices
        /// </summary>
        public List<string> GetAllProductPrices()
        {
            var priceElements = driver.FindElements(productPrices);
            return priceElements.Select(element => element.Text).ToList();
        }

        /// <summary>
        /// Add specific product to cart by product name
        /// </summary>
        public void AddProductToCart(string productName)
        {
            // Convert product name to button ID format (lowercase, replace spaces with -)
            string buttonId = $"add-to-cart-{productName.ToLower().Replace(" ", "-")}";
            driver.FindElement(By.Id(buttonId)).Click();
            Console.WriteLine($"Added product to cart: {productName}");
        }

        /// <summary>
        /// Remove specific product from cart by product name
        /// </summary>
        public void RemoveProductFromCart(string productName)
        {
            string buttonId = $"remove-{productName.ToLower().Replace(" ", "-")}";
            driver.FindElement(By.Id(buttonId)).Click();
            Console.WriteLine($"Removed product from cart: {productName}");
        }

        /// <summary>
        /// Add first N products to cart
        /// </summary>
        public void AddFirstNProductsToCart(int count)
        {
            var addButtons = driver.FindElements(addToCartButtons);
            for (int i = 0; i < Math.Min(count, addButtons.Count); i++)
            {
                addButtons[i].Click();
            }
            Console.WriteLine($"Added {count} products to cart");
        }

        /// <summary>
        /// Click on a specific product to view details
        /// </summary>
        public void ClickProductByName(string productName)
        {
            var products = driver.FindElements(productNames);
            foreach (var product in products)
            {
                if (product.Text.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    product.Click();
                    Console.WriteLine($"Clicked on product: {productName}");
                    return;
                }
            }
            throw new Exception($"Product not found: {productName}");
        }

        // ============== SORTING METHODS ==============

        /// <summary>
        /// Select sorting option from dropdown
        /// </summary>
        public void SelectSortOption(string sortOption)
        {
            SelectElement sortSelect = new SelectElement(driver.FindElement(sortDropdown));
            sortSelect.SelectByText(sortOption);
            Console.WriteLine($"Selected sort option: {sortOption}");
        }

        /// <summary>
        /// Sort products by Name (A to Z)
        /// </summary>
        public void SortByNameAtoZ()
        {
            SelectSortOption("Name (A to Z)");
        }

        /// <summary>
        /// Sort products by Name (Z to A)
        /// </summary>
        public void SortByNameZtoA()
        {
            SelectSortOption("Name (Z to A)");
        }

        /// <summary>
        /// Sort products by Price (low to high)
        /// </summary>
        public void SortByPriceLowToHigh()
        {
            SelectSortOption("Price (low to high)");
        }

        /// <summary>
        /// Sort products by Price (high to low)
        /// </summary>
        public void SortByPriceHighToLow()
        {
            SelectSortOption("Price (high to low)");
        }

        /// <summary>
        /// Get currently selected sort option
        /// </summary>
        public string GetCurrentSortOption()
        {
            SelectElement sortSelect = new SelectElement(driver.FindElement(sortDropdown));
            return sortSelect.SelectedOption.Text;
        }

        // ============== CART RELATED METHODS ==============

        /// <summary>
        /// Get cart badge count (number of items in cart)
        /// </summary>
        public int GetCartBadgeCount()
        {
            try
            {
                string badgeText = driver.FindElement(cartBadge).Text;
                return int.Parse(badgeText);
            }
            catch (NoSuchElementException)
            {
                return 0; // No badge means cart is empty
            }
        }

        /// <summary>
        /// Check if cart badge is displayed
        /// </summary>
        public bool IsCartBadgeDisplayed()
        {
            try
            {
                return driver.FindElement(cartBadge).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Click shopping cart icon to go to cart page
        /// </summary>
        public void ClickShoppingCart()
        {
            driver.FindElement(shoppingCartLink).Click();
            Console.WriteLine("Clicked shopping cart icon");
        }

        // ============== MENU METHODS ==============

        /// <summary>
        /// Click hamburger menu
        /// </summary>
        public void ClickHamburgerMenu()
        {
            driver.FindElement(hamburgerMenu).Click();
            Console.WriteLine("Opened hamburger menu");
        }

        /// <summary>
        /// Logout from the application
        /// </summary>
        public void Logout()
        {
            ClickHamburgerMenu();
            // Wait for menu to open
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("logout_sidebar_link")).Click();
            Console.WriteLine("Logged out successfully");
        }

        // ============== HELPER METHODS ==============

        /// <summary>
        /// Verify products are sorted alphabetically A to Z
        /// </summary>
        public bool AreProductsSortedAtoZ()
        {
            List<string> productNames = GetAllProductNames();
            List<string> sortedNames = new List<string>(productNames);
            sortedNames.Sort();
            return productNames.SequenceEqual(sortedNames);
        }

        /// <summary>
        /// Verify products are sorted alphabetically Z to A
        /// </summary>
        public bool AreProductsSortedZtoA()
        {
            List<string> productNames = GetAllProductNames();
            List<string> sortedNames = new List<string>(productNames);
            sortedNames.Sort();
            sortedNames.Reverse();
            return productNames.SequenceEqual(sortedNames);
        }

        /// <summary>
        /// Verify products are sorted by price (low to high)
        /// </summary>
        public bool AreProductsSortedByPriceLowToHigh()
        {
            List<string> prices = GetAllProductPrices();
            List<decimal> priceValues = prices.Select(p => decimal.Parse(p.Replace("$", ""))).ToList();
            
            for (int i = 0; i < priceValues.Count - 1; i++)
            {
                if (priceValues[i] > priceValues[i + 1])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Verify products are sorted by price (high to low)
        /// </summary>
        public bool AreProductsSortedByPriceHighToLow()
        {
            List<string> prices = GetAllProductPrices();
            List<decimal> priceValues = prices.Select(p => decimal.Parse(p.Replace("$", ""))).ToList();
            
            for (int i = 0; i < priceValues.Count - 1; i++)
            {
                if (priceValues[i] < priceValues[i + 1])
                    return false;
            }
            return true;
        }
    }
}
