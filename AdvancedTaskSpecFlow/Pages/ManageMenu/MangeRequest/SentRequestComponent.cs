using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages.ManageMenu.MangeRequest
{
    public class SentRequestComponent: CommonDriver
    {
        private IWebElement WithdrawButton;
        private IWebElement successMessage;
        private IWebElement closeMessage;

        public void renderWithdrawComponents(string title)
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", $"//table[@class='ui single line sortable striped table sortableHeader']//a[text()='{title}']/../following-sibling::td[@class='two wide']/button[text()='Withdraw']   ", 15);
                WithdrawButton = driver.FindElement(By.XPath($"//table[@class='ui single line sortable striped table sortableHeader']//a[text()='{title}']/../following-sibling::td[@class='two wide']/button[text()='Withdraw']   "));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
        public void renderSuccessComponents()
        {
            try
            {
                successMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
                closeMessage = driver.FindElement(By.XPath("//*[@class='ns-close']"));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
        public void clickWithdrawButton(SentRequestData sentRequestData)
        {
            Thread.Sleep(15000);
            string title = sentRequestData.Title;
            renderWithdrawComponents(title);
            WithdrawButton.Click();
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
        }
        public string getMessage()
        {
            renderSuccessComponents();
            string message = successMessage.Text;
            closeMessage.Click();
            Thread.Sleep(6000);
            return message;
        }
    }
}
