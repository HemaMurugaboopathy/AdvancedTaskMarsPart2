using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages.CertificationComponent
{
    public class AddCertificationComponent : CommonDriver
    {
        private IWebElement certificateTextbox;
        private IWebElement certifiedFromTextbox;
        private IWebElement certifiedyearDropdown;
        private IWebElement AddCertificationButton;
        private IReadOnlyCollection<IWebElement> deleteButtons;
        private IWebElement successMessage;
        private IWebElement closeMessageIcon;

        public void renderAddComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//input[@placeholder='Certificate or Award']", 15);
                certificateTextbox = driver.FindElement(By.XPath("//input[@placeholder='Certificate or Award']"));
                certifiedFromTextbox = driver.FindElement(By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']"));
                certifiedyearDropdown = driver.FindElement(By.XPath("//select[@name='certificationYear']"));
                AddCertificationButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//input[@value='Add']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderDeleteAllComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//div[@data-tab='fourth']//span[@class='button']/i[@class='remove icon']", 20);
                deleteButtons = driver.FindElements(By.XPath("//div[@data-tab='fourth']//span[@class='button']/i[@class='remove icon']"));
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Delete_All_Records()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//div[@data-tab='fourth']//span[@class='button']/i[@class='remove icon']", 30);
            }
            catch (WebDriverTimeoutException e)
            {
                return;
            }

            renderDeleteAllComponents();
            //Delete all records in the list

            foreach (IWebElement deleteButton in deleteButtons)
            {
                deleteButton.Click();

            }
        }
        public void Add_Certification(CertificationData certificationData)
        {
            renderAddComponents();
            Thread.Sleep(6000);   
            //Enter Certificate or Award
             certificateTextbox.SendKeys(certificationData.Certificate);

            //Enter Certified from
            certifiedFromTextbox.SendKeys(certificationData.CertifiedFrom);

            //Select Year
            SelectElement selectCertifiedYearOption = new SelectElement(certifiedyearDropdown);
            selectCertifiedYearOption.SelectByValue(certificationData.CertifiedYear);

            //Click Add button 
            AddCertificationButton.Click();
        }
        public void renderAddMessage()
        {
            try
            {
                Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 6);
                successMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
                closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public string getMessage()
        {
            renderAddMessage();
            string message = successMessage.Text;
            closeMessageIcon.Click();
            Thread.Sleep(6000);
            return message;
        }



    }



}

