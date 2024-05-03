using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages.CertificationComponent
{
    public class ProfileCertification_Component: CommonDriver
    {
        private IWebElement AddNewButton;
        private IWebElement DeleteButton;

        public void renderAddComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div", 8);
                AddNewButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Certification']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderDeleteComponents(string Certificate)
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", $"//div[@data-tab='fourth']//tr[td[1]='{Certificate}']//td[last()]/span[2]", 15);
                DeleteButton = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//tr[td[1]='{Certificate}']//td[last()]/span[2]"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Click_AddCertification()
        {
            renderAddComponents();
            //Click add new button
            AddNewButton.Click();
        }
        public void Click_DeleteCertification(CertificationData certificationData)
        {
            string certificate = certificationData.Certificate;
            Thread.Sleep(4000);
            renderDeleteComponents(certificate);
            DeleteButton.Click();
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
        }
    }
}
