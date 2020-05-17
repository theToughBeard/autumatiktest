using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace lab_2
{
    public static class Browser
    {
        private static IWebDriver _webDriver;

        public static IWebDriver WebDriver => _webDriver ?? InitWebDriver();

        public static IWebDriver InitWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("--remote-debugging-port=9222");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalCapability("useAutomationExtension", false);

            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            _webDriver = new ChromeDriver(driverService, options);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);

            return _webDriver;
        }

        public static IWebElement ScrollToElement(IWebElement element, int tiks = 1200)
        {
            ((IJavaScriptExecutor) WebDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(tiks);
            return element;
        }

        private static By ByType(string name, FindBy findBy)
            => findBy switch
            {
                FindBy.CssSelector => By.CssSelector(name),
                FindBy.TagName => By.TagName(name),
                FindBy.ClassName => By.ClassName(name),
                FindBy.Id => By.Id(name),
                FindBy.LinkText => By.LinkText(name),
                FindBy.Name => By.Name(name),
                FindBy.PartialLinkText => By.PartialLinkText(name),
                FindBy.XPath => By.XPath(name),
                _ => throw new ArgumentOutOfRangeException(nameof(findBy), findBy, null)
            };

        public static IWebElement FindElement(string name, FindBy findBy = FindBy.CssSelector, int seconds = 5)
        {
            try
            {
                var findByType = ByType(name, findBy);
                WebDriverWait webDriverWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));

                return webDriverWait.Until(ExpectedConditions.ElementIsVisible(findByType));
            }
            catch
            {
                return null;
            }
        }

        public static ReadOnlyCollection<IWebElement> FindElements(string name, FindBy findBy = FindBy.CssSelector,
            int seconds = 5)
        {
            try
            {
                var findByType = ByType(name, findBy);
                WebDriverWait webDriverWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));

                return webDriverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(findByType));
            }
            catch
            {
                return new List<IWebElement>().AsReadOnly();
            }
        }

        public static IWebElement FindElement(this IWebElement element, string name, FindBy findBy = FindBy.CssSelector)
        {
            try
            {
                var findByType = ByType(name, findBy);
                return element.FindElement(findByType);
            }
            catch
            {
                return null;
            }
        }

        public static IReadOnlyCollection<IWebElement> FindElements(this IWebElement element, string name,
            FindBy findBy = FindBy.CssSelector)
        {
            try
            {
                var findByType = ByType(name, findBy);
                return element.FindElements(findByType);
            }
            catch
            {
                return new List<IWebElement>().AsReadOnly();
            }
        }

        public static void Dispose()
        {
            _webDriver?.Close();
            _webDriver?.Dispose();
        }
    }

    public enum FindBy : byte
    {
        CssSelector,
        TagName,
        ClassName,
        Id,
        LinkText,
        Name,
        PartialLinkText,
        XPath
    }
}