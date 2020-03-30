using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using BDD.Util;

namespace BDD.Base
{
    public class BasePage
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IWebDriver driver;
        WebDriverWait wait;
        IReadOnlyList<IWebElement> result;
        IJavaScriptExecutor scriptExecutor;

        public BasePage(IWebDriver driver)
        {


            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public IWebElement FindElement(By by)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            HighLightElement(by);
            return driver.FindElement(by);
            //HightLightElement(by);

        }
        public void HighLightElement(By by)
        {
            scriptExecutor = (IJavaScriptExecutor)driver;
            scriptExecutor.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", driver.FindElement(by));
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }
        public void ClickElement(By by)
        {
            
            FindElement(by).Click();
         
        }
        public void SendKeys(By by, string str)//str = istanbul
        {
            FindElement(by).SendKeys(str);
            FindElement(by).SendKeys(Keys.Enter);
        }


        //TAKVİM İÇİN GEREKLİ ALANLAR
        public void CalenderSelect(string str, string[] arr)
        {
            if (str.Contains("Gidiş Bilet"))
            {
                //Gidiş Bilet, 2023,  Takvimdeki Yıl'ın xpath'i
                YearSelect(str, arr[2], "//*[@id='search-flight-datepicker-departure']/div/div[1]/div/div/span[2]");
                //Gidiş Bilet, Temmuz,  Takvimdeki Ay'ın xpath'i
                MonthSelect(str, arr[1], "//*[@id='search-flight-datepicker-departure']/div/div[1]/div/div/span[1]");
                //Gidiş Bilet, 05,  Takvimdeki Gün'ün xpath'i
                DaySelect(str, arr[0], "//*[@id='search-flight-datepicker-departure']/div/div[1]//tbody//a");
            }
            else if (str.Contains("Dönüş Bileti"))
            {
                YearSelect(str, arr[2], "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/div/span[2]");
                MonthSelect(str, arr[1], "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/div/span[1]");
                DaySelect(str, arr[0], "//*[@id='search-flight-datepicker-arrival']/div/div[2]//tbody//a");
            }

        }
        public void YearSelect(string rightclick, string year, string xpath)
        {
            if (rightclick.Contains("Gidiş Bilet"))
                SelectYear(By.XPath(xpath), year, "//*[@id='search-flight-datepicker-departure']/div/div[2]/div/a");
            else if (rightclick.Contains("Dönüş Bileti"))
                SelectYear(By.XPath(xpath), year, "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/a");
        }

        public void MonthSelect(string rightclick, string mounth, string xpath)
        {
            if (rightclick.Contains("Gidiş Bilet"))
                SelectMouth(By.XPath(xpath), mounth, "//*[@id='search-flight-datepicker-departure']/div/div[2]/div/a");
            else if (rightclick.Contains("Dönüş Bileti"))
                SelectMouth(By.XPath(xpath), mounth, "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/a");

        }

        public void DaySelect(string clickday, string day, string xpath)
        {
            SelectDay(xpath, day);
        }

        public void SelectYear(By by, string yearStr, string RightClick)
        {
            string str;
            while (true)
            {
                str = FindElement(by).Text;
                if (str.Equals(yearStr))
                    break;
                ClickElement(By.XPath(RightClick));
            }
        }
        public void SelectMouth(By by, string mounthStr, string RightClick)
        {
            string str;
            while (true)
            {
                str = FindElement(by).Text;
                if (str.Equals(mounthStr))
                    break;
                ClickElement(By.XPath(RightClick));
         
            }
        }

        public void SelectDay(string links, string day)
        {
            result = driver.FindElements(By.XPath(links));

            foreach (var res in result)
            {
                if (res.Text.Equals(day))
                {
                    res.Click();
                    break;
                }
            }
        }


    }
}
