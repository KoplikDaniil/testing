using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace csharp_example
{
    [TestFixture]
    public class TestingLogin
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void testingLogin()
        {
            var rand = new Random();

            driver.Navigate().GoToUrl("https://www.yandex.ru/");
            driver.FindElement(By.LinkText("Завести почту")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys("Test12345");
            driver.FindElement(By.Name("lastname")).SendKeys("Test12345");
            driver.FindElement(By.Name("login")).SendKeys("Test");
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("form__login-suggest")), "К сожалению, логин занят"));
            driver.FindElement(By.ClassName("login__suggest-button")).Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[2]/div/div/div/form/div[1]/div[3]/div/div/div/div/div/strong")), "Свободные логины"));
            IList <IWebElement> select = driver.FindElements(By.XPath( "//*[@id='root']/div/div[2]/div/div[2]/div/div/div/form/div[1]/div[3]/div/div/div/div/div/ul/li"));
            //System.Threading.Thread.Sleep(10000);
            select.ElementAtOrDefault(rand.Next(0, 10)).Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[2]/div/div/div/form/div[1]/div[3]/div/div")), "Логин свободен"));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }

}
