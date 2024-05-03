using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages.AccountMenu
{
    public class ProfileUserComponent: CommonDriver
    {
        private IWebElement userTab;

        public void renderComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//*[starts-with(text(),'Hi')]", 4);
                userTab = driver.FindElement(By.XPath("//*[starts-with(text(),'Hi')]"));
            }
 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void GoToUserTab()
        {
            renderComponents();
            //Navigate to change password page  
            userTab.Click();
        }

    }
}
