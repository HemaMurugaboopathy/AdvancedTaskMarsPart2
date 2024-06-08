using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages.ManageMenu.MangeRequest
{
    public class ManageRequestOverviewComponent: CommonDriver
    {
        private IWebElement ReceivedRequestDropdown;
        private IWebElement SentRequestDropdown;

        public void renderReceivedRequestComponent()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Received Requests']", 8);
                ReceivedRequestDropdown = driver.FindElement(By.XPath("//a[text()='Received Requests']"));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
        public void renderSentRequestComponent()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Sent Requests']", 8);
                SentRequestDropdown = driver.FindElement(By.XPath("//a[text()='Sent Requests']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void clickReceivedRequest()
        {
            renderReceivedRequestComponent();
            ReceivedRequestDropdown.Click();
        }

        public void clickSentRequest()
        {
            renderSentRequestComponent();
            SentRequestDropdown.Click();
        }
    }
}
