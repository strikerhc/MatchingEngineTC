using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace MatchingEngineTC
{
    [TestFixture]
    public class MatchingEngineTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestRepertoireManagementModule()
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.matchingengine.com/");

                IWebElement modulesMenu = wait.Until(e => e.FindElement(By.XPath("//a[contains(text(), 'Modules')]")));
                Actions action = new Actions(driver);
                action.MoveToElement(modulesMenu).Perform();

                IWebElement repertoireModule = wait.Until(e => e.FindElement(By.XPath("//a[contains(text(), 'Repertoire Management Module')]")));
                repertoireModule.Click();

                IWebElement additionalFeaturesSection = wait.Until(e => e.FindElement(By.XPath("//h2[contains(text(), 'Additional Features')]")));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", additionalFeaturesSection);

                IWebElement productsSupportedButton = wait.Until(e => e.FindElement(By.XPath("//span[text()='Products Supported']")));
                productsSupportedButton.Click();

                Thread.Sleep(1000);
                IWebElement supportedProductsHeader = wait.Until(e => e.FindElement(By.XPath("//h3[contains(text(), 'There are several types of Product Supported:')]")));
                Assert.IsTrue(supportedProductsHeader.Displayed, "The Supported Products section is not visible.");

            }
            catch (Exception e)
            {
                Assert.Fail("Test failed due to exception: " + e.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }

    }
}
