using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;

namespace SEI.Desktop.SeleniumUtils
{
    public static class WebDriverExtensions
    {
        public static void LoadPage(this IWebDriver webDriver, TimeSpan timeToWait, string url)
        {
            webDriver.Manage().Timeouts().PageLoad = timeToWait;
            webDriver.Navigate().GoToUrl(url);
        }

        public static string GetText(this IWebDriver webDriver, By by)
        {
            IWebElement webElement = webDriver.FindElement(by);
            return webElement.Text;
        }

        public static void SetText(this IWebDriver webDriver, By by, string text)
        {
            IWebElement webElement = webDriver.FindElement(by);
            webElement.SendKeys(text);
        }

        public static void SetValueSelectElement(this IWebDriver webDriver, By by, string value)
        {
            var element = webDriver.FindElement(by);
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);
        }

        public static void Submit(this IWebDriver webDriver, By by)
        {
            IWebElement webElement = webDriver.FindElement(by);
            if (!(webDriver is InternetExplorerDriver))
            {
                webElement.Submit();
            }
            else
            {
                webElement.SendKeys(Keys.Enter);
            }
        }

        public static void ClosePopUp(this IWebDriver webDriver)
        {
            var popUp = webDriver.SwitchTo().Window(webDriver.WindowHandles[1]);

            if (popUp != null)
            {
                popUp.Close();
            }

            webDriver.SwitchTo().Window(webDriver.WindowHandles[0]);
        }

        public static void ClickElement(this IWebDriver webDriver, By by)
        {
            IWebElement webElement = webDriver.FindElement(by);
            webElement.Click();
        }

        public static void SetValueInputHidden(this IWebDriver webDriver, By by, string value)
        {
            IWebElement webElement = webDriver.FindElement(by);

            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript($"return document.getElementById('{webElement.GetAttribute("id")}').value = '{value}';");
        }

    }
}