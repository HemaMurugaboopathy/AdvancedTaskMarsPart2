using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages
{
    public class ProfileLoginPageComponent: CommonDriver
    {
        private IWebElement signInbutton;
        private IWebElement emailTextbox;
        private IWebElement passwordTextbox;
        private IWebElement loginButton;

        public void renderComponents()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                Wait.WaitToBeClickable(driver, "XPath", "//a[@class='item' and text()='Sign In']", 10);
                signInbutton = driver.FindElement(By.XPath("//a[@class='item' and text()='Sign In']"));
                //Wait.WaitToExist(driver, "XPath", "//input[@name='email']", 10);
                emailTextbox = wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@name='email']")));

                passwordTextbox = driver.FindElement(By.XPath("//input[@name='password']"));
                loginButton = driver.FindElement(By.XPath("//button[@class='fluid ui teal button']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void SigninActions()
        {
            renderComponents();
            signInbutton.Click();
        }
        public void LoginActions()
        {
            renderComponents();
            //Enter email         
            emailTextbox.SendKeys("h.prabhaharan@gmail.com");

            //Enter password
            passwordTextbox.SendKeys("654321");

            //Click login to enter
            loginButton.Click();
        }
    }
}
