﻿using System;
using System.Drawing;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    class YandexCreateAccount
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private string RandomString(string Alphabet, int Length)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;
            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }
            return sb.ToString();
        }

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestYandexCreateAccount()
        {
            driver.Navigate().GoToUrl("http://www.yandex.ru/");
            try
            {
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.LinkText("Завести почту")), "Завести почту"));
                driver.FindElement(By.LinkText("Завести почту")).Click();
                driver.FindElement(By.Name("firstname")).SendKeys("Test12345");
                driver.FindElement(By.Name("lastname")).SendKeys("Test12345");
                driver.FindElement(By.Name("login")).SendKeys("Test.Create");
                wait.Until(ExpectedConditions.ElementIsVisible((By.ClassName("reg-field__popup"))));
                driver.FindElement(By.Name("password")).SendKeys("TestCreate12345");
                driver.FindElement(By.Name("password_confirm")).SendKeys("TestCreate12345");
                driver.FindElement(By.ClassName("registration__pseudo-link")).Click();
                driver.FindElement(By.ClassName("button2")).Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div")));
                driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[5]/span")).Click();
                driver.FindElement(By.Name("hint_answer")).SendKeys("143130");
                driver.FindElement(By.Name("captcha")).SendKeys(RandomString("абвгдеёжзийклмнопрстуфхйчшщъыьэюя", 5));
                driver.FindElement(By.ClassName("form__submit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("reg-field__popup")), "Вы неверно ввели символы. Попробуйте еще раз"));
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

    

