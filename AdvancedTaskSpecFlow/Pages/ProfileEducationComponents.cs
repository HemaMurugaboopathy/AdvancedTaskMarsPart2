using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTaskSpecFlow.Pages
{
    public class ProfileEducationComponents: CommonDriver
    {
        private IWebElement AddNewButton;
        private IWebElement DeleteButton;

        public void renderAddComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath","//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div", 8);
                AddNewButton = driver.FindElement(By.XPath("//div[@class='four wide column' and h3='Education']/following-sibling::div[@class='twelve wide column scrollTable']//th[@class='right aligned']/div"));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
        public void renderDeleteComponents(string CollegeName)
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", $"//div[@data-tab='third']//tr[td[2]='{CollegeName}']//td[last()]/span[2]", 9);
                DeleteButton = driver.FindElement(By.XPath($"//div[@data-tab='third']//tr[td[2]='{CollegeName}']//td[last()]/span[2]"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Click_AddEducation()
        {
            renderAddComponents();
            //Click add new button
            AddNewButton.Click();
        }
        public void Click_DeleteEducation(EducationData educationData)
        {
            string collegeName = educationData.CollegeName;
            Thread.Sleep(4000);
            renderDeleteComponents(collegeName);
            DeleteButton.Click();
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
        }
    }
}
