using BDD.Base;
using BDD.Util;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace BDD
{
    [Binding]
    public sealed class Steps // steps kısmında bdd adımları tutulur yani senaryolar
    {

        public IWebDriver Driver { get; set; }
        private readonly ScenarioContext context;
        public BasePage basePage;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Steps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        //Browser ayaga kalkar
        [BeforeScenario]//her senaryonun oncesınde calış.
        public void SetUp()
        {
            Logging.Logger();
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("disable-popup-blocking");
            options.AddArgument("disable-notifications");
            options.AddArgument("test-type");
            Driver = new ChromeDriver(options);

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            Driver.Navigate().GoToUrl("https://www.flypgs.com/");
            log.Info("Driver ayağa kalktı...");
            basePage = new BasePage(Driver); //burdakı amac basepage deki metodları çağırmak ıcın olusturuldu.
            
        }


        [BeforeStep] //her adım öncesi çalışır.
        public void BeforeStep()
        {
            log.Info("Step :" + context.StepContext.StepInfo.Text);

        }
        [Given("'(.*)' objesine tıklanır.")]
        public void LoginSteps(String obje)
        {
            log.Info("Parametre   :" + obje);
            basePage.ClickElement(By.CssSelector(obje));
        }
        [Given("'(.*)' alanına tıklanır.")]
        public void LoginSteps2(String obje)
        {
            log.Info("Parametre   :" + obje);
            basePage.ClickElement(By.XPath(obje));
        }

        [Given("'(.*)' seçimi '(.*)' yapılır.")]
        public void CalenderSelect(string str, string calender)//Dönüş Bilet, 20 temmuz 2050
        {
            string[] arr = calender.Split("."); //20 temmuz 2050

            basePage.CalenderSelect(str, arr);
        }

        [Given("'(.*)' objesine '(.*)' yazılır.")]
        public void Send(string obje, string value)
        {
            basePage.SendKeys(By.CssSelector(obje), value);
        }

        [Given("'(.*)' alanına '(.*)' yazılır.")]
        public void Send2(string obje, string value)
        {
            basePage.SendKeys(By.XPath(obje), value);
        }

        [Given("'(.*)' saniye beklenir.")]
        public void TimeWait(int second)
        {

            log.Info(second+ "saniye bekleniyor...");
            Thread.Sleep(TimeSpan.FromSeconds(second));

        }


        //browser kapatılır
        [AfterScenario]
        public void TearDown()
        {
           // Driver.Quit();
        }

    }
}
