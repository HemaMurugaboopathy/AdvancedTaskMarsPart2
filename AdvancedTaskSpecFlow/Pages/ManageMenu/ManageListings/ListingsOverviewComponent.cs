using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;

namespace AdvancedTaskSpecFlow.Pages.ManageMenu.ManageListings
{
    public class ListingsOverviewComponent: CommonDriver
    {
        private IWebElement viewIcon;
        private IWebElement ViewTitle;
        private IWebElement titleTextBox;
        private IWebElement descriptionTextBox;
        private IWebElement saveButton;
        private IWebElement successMessage;
        private IWebElement EditButton;
        private IWebElement DeleteButton;
        private IWebElement YesButton;
        private IWebElement ToggleButton;

        public void renderEditButton(string existingTitle)
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", $"//td[text()='{existingTitle}']/following-sibling::td/div/button[@class='ui button']/i[@class='outline write icon']", 20);
                EditButton = driver.FindElement(By.XPath($"//td[text()='{existingTitle}']/following-sibling::td/div/button[@class='ui button']/i[@class='outline write icon']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void clickEditButton(ShareSkillData shareSkillData)
        {
            string existingTitle = shareSkillData.ExistingTitle;
            Thread.Sleep(6000);
            renderEditButton(existingTitle);
            EditButton.Click();
        }
        public void renderUpdateComponents()
        {
            Wait.WaitToExist(driver, "XPath", "//input[@name=\"title\"]", 30);
            titleTextBox = driver.FindElement(By.XPath("//input[@name=\"title\"]"));
            descriptionTextBox = driver.FindElement(By.XPath("//textarea[@name=\"description\"]"));
            saveButton = driver.FindElement(By.XPath("//input[@value='Save']"));

        }
       
        public void Edit_Listings(ShareSkillData newshareSkillData)
        {
            renderUpdateComponents();
            Thread.Sleep(8000);
            titleTextBox.Clear();
            titleTextBox.SendKeys(newshareSkillData.Title);

            descriptionTextBox.Clear();
            descriptionTextBox.SendKeys(newshareSkillData.Description);
            saveButton.Click();
        }

        private void renderViewComponents(string title)
        {
            try
            {
                viewIcon = driver.FindElement(By.XPath($"//td[text()='{title}']/following-sibling::td/div/button[@class='ui button']/i[@class='eye icon']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

   

        public void clickViewButton(ShareSkillData shareSkillData)
        {
            String title = shareSkillData.Title;
            Thread.Sleep(6000);
            renderViewComponents(title);
            viewIcon.Click();
        }
        public void renderViewTitle(string title)
        {
            try
            {
                ViewTitle = driver.FindElement(By.XPath($"//span[text()='{title}']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string getViewTitle(string title)
        {
            Thread.Sleep(4000);
            renderViewTitle(title);
            return ViewTitle.Text;
        }

        public void renderDeleteButton(string title)
        {
            try
            {
                DeleteButton = driver.FindElement(By.XPath($"//td[text()='{title}']/following-sibling::td/div/button[@class='ui button']/i[@class='remove icon']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderYesButton()
        {
            try
            {
                YesButton = driver.FindElement(By.XPath("//button[@class='ui icon positive right labeled button']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void clickDeleteButton(ShareSkillData shareSkillData)
        {
            String title = shareSkillData.Title;
            Thread.Sleep(5000);
            renderDeleteButton(title);
            DeleteButton.Click();
            renderYesButton();
            YesButton.Click();
        }
        public void renderToggleButton()
        {
            try
            {
                ToggleButton = driver.FindElement(By.XPath("//input[@name=\"isActive\"]"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        public void clickToggleButton()
        {
            renderToggleButton();
            ToggleButton.Click();
        }

        public void renderMessage()
        {
            try
            {
                Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                successMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

       
        public string getMessage()
        {
            renderMessage();
            return successMessage.Text;
        }
    }
}
