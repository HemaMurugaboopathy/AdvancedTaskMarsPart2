using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Utilities
{
    public class CommonDriver
    {
        private const string Url = "http://localhost:5000/";
        public static IWebDriver driver;

        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(Url);
        }
        public void Close()
        {
            driver.Quit();
        }
    }

}

