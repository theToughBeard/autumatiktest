using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static lab_2.Browser;

namespace lab_2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            InitWebDriver();
        }

        [TearDown]
        public void Cleanup()
        {
            Dispose();
        }

        [Test]
        public void GoogleHomeTest()
        {
            WebDriver.Navigate().GoToUrl("https://www.google.com");

            Assert.AreEqual("Google", WebDriver.Title);

            var logo = FindElement("hplogo", FindBy.Id);
            Assert.NotNull(logo);

            var searchInput = FindElement("input[name='q']");
            Assert.NotNull(searchInput);

            var searchButton = FindElement("FPdoLc", FindBy.ClassName)
                .FindElement("input[name='btnK']");
            Assert.NotNull(searchButton);

            var href = FindElement("a[href ^= 'https://mail.google.com/mail']");
            Assert.NotNull(href);
        }

        [Test]
        public void CheckKyivInWikipedia()
        {
            WebDriver.Navigate().GoToUrl("https://uk.wikipedia.org");

            var searchInput = FindElement("input[name='search']");
            searchInput.SendKeys("Київ" + Keys.Return);

            Thread.Sleep(2000);
            Assert.AreEqual("https://uk.wikipedia.org/wiki/%D0%9A%D0%B8%D1%97%D0%B2", WebDriver.Url);

            var emblem = FindElement("Герб Києва", FindBy.LinkText);
            Assert.NotNull(emblem);

            var population = FindElements(".infobox tr")
                .FirstOrDefault(el => el.FindElements("Населення", FindBy.LinkText).Any())?
                .FindElements("td", FindBy.TagName)
                .LastOrDefault()?.Text;
            Assert.NotNull(population);

            var covid = FindElement("Епідемія_коронавірусу", FindBy.Id);
            Assert.NotNull(covid);

            var t = FindElement("collapsibleTable0", FindBy.Id)
                .FindElements("tr", FindBy.TagName).Skip(4).FirstOrDefault()?
                .FindElements("th", FindBy.TagName).Skip(4).FirstOrDefault()?.Text;
            Assert.NotNull(t);

            var populationDensity = FindElements(".infobox tr")
                .FirstOrDefault(el => el.FindElements("Густота населення", FindBy.LinkText).Any())?
                .FindElements("td", FindBy.TagName)
                .LastOrDefault()?.Text;
            Assert.NotNull(populationDensity);

           var architecturalMonuments = FindElement("mw-parser-output", FindBy.ClassName)?
                .FindElements("ul", FindBy.TagName)?
                .FirstOrDefault(el => el.FindElements("Золоті ворота", FindBy.LinkText).Any())?
                .FindElements("li", FindBy.TagName)?.Count;

            Assert.IsTrue(architecturalMonuments > 20);
        }
    }
}