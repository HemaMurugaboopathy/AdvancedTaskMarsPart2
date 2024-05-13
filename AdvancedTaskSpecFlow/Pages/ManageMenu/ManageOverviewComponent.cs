using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages.ManageMenu
{
    public class ManageOverviewComponent: CommonDriver
    {
        private IWebElement ManageListingsTab;
        private IWebElement ManageRequestsTab;

        public void renderManageListings()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Manage Listings']", 8);
                ManageListingsTab = driver.FindElement(By.XPath("//a[text()='Manage Listings']")); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderManageRequests()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//div[text()='Manage Requests']", 8);
                ManageRequestsTab = driver.FindElement(By.XPath("//div[text()='Manage Requests']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void clickManageListings()
        {
            
            renderManageListings();
            ManageListingsTab.Click();
            Thread.Sleep(5000);
        }
        public void clickManageRequests()
        {
            Thread.Sleep(5000);
            renderManageRequests();
            ManageRequestsTab.Click();
        }
    }
}
