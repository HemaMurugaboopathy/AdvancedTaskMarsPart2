using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;

namespace AdvancedTaskSpecFlow.Pages
{
    public class ProfileDescriptionComponent: CommonDriver
    {
        private IWebElement descriptionTextbox;
        private IWebElement SaveButton;
        private IWebElement successMessage;
        private IWebElement closeMessage;
        public void renderComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//textarea[@name='value']", 4);
                descriptionTextbox = driver.FindElement(By.XPath("//textarea[@name='value']"));
                SaveButton = driver.FindElement(By.XPath("//button[@type='button']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderSuccessMessage()
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
        public void enterDescription(DescriptionData descriptionData)
        {
            renderComponents();
            descriptionTextbox.Clear();
            descriptionTextbox.SendKeys(descriptionData.Description);
            SaveButton.Click();
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
        }
        public string getMessage()
        {
            renderSuccessMessage();
            string message = successMessage.Text;
            closeMessage.Click();
            Thread.Sleep(6000);
            return message;
        }
    }
}
