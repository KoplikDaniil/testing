﻿using System;
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
            try
            {
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.LinkText("Завести почту")),"Завести почту"));
                driver.FindElement(By.LinkText("Завести почту")).Click();
                driver.FindElement(By.Name("firstname")).SendKeys("Test12345");
                driver.FindElement(By.Name("lastname")).SendKeys("Test12345");
                driver.FindElement(By.Name("login")).SendKeys("Test");
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("form__login-suggest")), "К сожалению, логин занят"));
                driver.FindElement(By.ClassName("login__suggest-button")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("suggest__logins")), "Свободные логины"));
                IList<IWebElement> select = driver.FindElements(By.ClassName("registration__pseudo-link"));
                select.ElementAtOrDefault(rand.Next(0, select.Count)).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("reg-field__popup")), "Логин свободен"));
               // System.Threading.Thread.Sleep(10000);
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail("[Selenium] Объект не найден!"+ ex.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[Selenium] WTF exception: " + ex.ToString());
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }

}
