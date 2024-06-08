using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
