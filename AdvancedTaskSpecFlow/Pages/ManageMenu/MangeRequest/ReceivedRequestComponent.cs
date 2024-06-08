using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages.ManageMenu.MangeRequest
{
    public class ReceivedRequestComponent: CommonDriver
    {
        private IWebElement AcceptButton;
        private IWebElement successMessage;
        private IWebElement closeMessage;
        private IWebElement DeclineButton;
        private IWebElement CompleteButton;

        public void renderAcceptComponent(string title)
        {
            try
            {
                AcceptButton = driver.FindElement(By.XPath($"//table[@class='ui single line sortable striped table sortableHeader']//a[text()='{title}']/../following-sibling::td[@class='two wide']/button[text()='Accept'][1]"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderSuccessComponent() 
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

        public void renderDeclineComponent(string title)
        {
            try
            {
                DeclineButton = driver.FindElement(By.XPath($"//table[@class='ui single line sortable striped table sortableHeader']//a[text()='{title}']/../following-sibling::td[@class='two wide']/button[text()='Decline']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void renderCompleteComponent(string title)
        {
            try
            {
                CompleteButton = driver.FindElement(By.XPath($"//table[@class='ui single line sortable striped table sortableHeader']//a[text()='{title}']/../following-sibling::td[@class='two wide']/button[text()='Complete']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void clickAcceptButton(ReceivedRequestData receivedRequestData)
        {
            Thread.Sleep(3000);
            string title = receivedRequestData.Title;
            renderAcceptComponent(title);
            AcceptButton.Click();
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
        }

        public string getMessage()
        {
            renderSuccessComponent();
            string message = successMessage.Text;
            closeMessage.Click();
            Thread.Sleep(5000);
            return message;
        }
        public void clickDeclineButton(ReceivedRequestData receivedRequestData)
        {
            Thread.Sleep(25000);
            string title = receivedRequestData.Title;
            renderDeclineComponent(title);
            DeclineButton.Click();
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
        }

        public void clickCompleteButton(ReceivedRequestData receivedRequestData)
        {
            Thread.Sleep(15000);
            string title = receivedRequestData.Title;
            renderCompleteComponent(title);
            CompleteButton.Click();
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
        }
    }
}
