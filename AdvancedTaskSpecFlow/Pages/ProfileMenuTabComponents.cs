using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;

namespace AdvancedTaskSpecFlow.Pages
{
    public class ProfileMenuTabComponents: CommonDriver
    {
        private IWebElement educationTab;
        private IWebElement certificationTab;
        private IWebElement descriptionTab;
        
        public void renderEducationComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Education']", 8);
                educationTab = driver.FindElement(By.XPath("//a[text()='Education']"));
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Element not found: " + ex.Message);
                // Optionally, you can rethrow the exception if needed:
                // throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void renderCertificationComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Certifications']", 4);
                certificationTab = driver.FindElement(By.XPath("//a[text()='Certifications']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderDescriptionComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//h3[text()='Description']/span/i[@class='outline write icon']", 15);
                descriptionTab = driver.FindElement(By.XPath("//h3[text()='Description']/span/i[@class='outline write icon']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void GoToEducationPage()
        {
            renderEducationComponents();
            Thread.Sleep(6000);
            //Navigate to skills page          
            educationTab.Click();
            Thread.Sleep(4000);
        }

        public void GoToCertificationPage()
        {
            renderCertificationComponents();
            Thread.Sleep(6000);
            //Navigate to share skill page           
            certificationTab.Click();
            Thread.Sleep(4000);
        }

        public void GoToDescriptionPage()
        {
            renderDescriptionComponents();
            Thread.Sleep(6000);
            //Navigate to share skill page           
            descriptionTab.Click();
            Thread.Sleep(4000);
        }
    }
}

