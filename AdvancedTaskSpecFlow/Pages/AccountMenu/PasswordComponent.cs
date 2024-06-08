using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;

namespace AdvancedTaskSpecFlow.Pages.AccountMenu
{
    public class PasswordComponent: CommonDriver
    {
        private IWebElement ChangePasswordDropdown;
        private IWebElement CurrentPasswordTextbox;
        private IWebElement NewPasswordTextbox;
        private IWebElement ConfirmPasswordTextbox;
        private IWebElement SaveButton;
        private IWebElement successMessage;
        public void renderChangePasswordDropdown()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Change Password']", 4);
                ChangePasswordDropdown = driver.FindElement(By.XPath("//a[text()='Change Password']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void renderAddComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//input[@placeholder=\"Current Password\"]", 8);
                CurrentPasswordTextbox = driver.FindElement(By.XPath("//input[@placeholder=\"Current Password\"]"));
                NewPasswordTextbox = driver.FindElement(By.XPath("//input[@placeholder=\"New Password\"]"));
                ConfirmPasswordTextbox = driver.FindElement(By.XPath("//input[@placeholder=\"Confirm Password\"]"));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderSaveComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//button[@type='button']", 8);
                SaveButton = driver.FindElement(By.XPath("//button[@type='button']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Change_Password(PasswordData passwordData)
        {
            renderChangePasswordDropdown();
            ChangePasswordDropdown.Click();
            renderAddComponents();
            CurrentPasswordTextbox.SendKeys(passwordData.CurrentPassword);
            NewPasswordTextbox.SendKeys(passwordData.NewPassword);
            ConfirmPasswordTextbox.SendKeys(passwordData.ConfirmPassword);
            Thread.Sleep(5000);
            renderSaveComponents();
            SaveButton.Click();
        }
        public void renderAddMessage()
        {
            try
            {
                Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 6);
                successMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
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
            Thread.Sleep(6000);
            return message;
        }
    }
}
