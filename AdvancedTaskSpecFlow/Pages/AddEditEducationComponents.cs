using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AdvancedTaskSpecFlow.Pages
{
    public class AddEditEducationComponents: CommonDriver
    {
        private IReadOnlyCollection<IWebElement> deleteButtons;
        private IWebElement collegeTextbox;
        private IWebElement countryDropdown;
        private IWebElement titleDropdown;
        private IWebElement degreeTextbox;
        private IWebElement yearDropdown;
        private IWebElement addButton;
        private IWebElement successMessage;
        private IWebElement closeMessageIcon;

        public void renderAddComponents()
        {
            try
            {
                collegeTextbox = driver.FindElement(By.XPath("//input[@placeholder='College/University Name']"));
                countryDropdown = driver.FindElement(By.XPath("//select[@name='country']"));
                titleDropdown = driver.FindElement(By.XPath("//select[@name='title']"));
                degreeTextbox = driver.FindElement(By.XPath("//input[@placeholder='Degree']"));
                yearDropdown = driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));
                addButton = driver.FindElement(By.XPath("//input[@value='Add']"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void renderDeleteAllComponents()
        {
            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//div[@data-tab='third']//span[@class='button']/i[@class='remove icon']", 20);
                deleteButtons = driver.FindElements(By.XPath("//div[@data-tab='third']//span[@class='button']/i[@class='remove icon']"));
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
                Wait.WaitToBeClickable(driver, "XPath", "//div[@data-tab='third']//span[@class='button']/i[@class='remove icon']", 30);
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
        public void Add_Education(EducationData educationData)
        {

            renderAddComponents();
            //Enter College/University name
            collegeTextbox.SendKeys(educationData.CollegeName);

            //Select Country of college
            SelectElement selectCountryOption = new SelectElement(countryDropdown);
            selectCountryOption.SelectByValue(educationData.Country);

            //Select Title
            SelectElement selectTitleOption = new SelectElement(titleDropdown);
            selectTitleOption.SelectByValue(educationData.Title);

            //Enter Degree
            degreeTextbox.SendKeys(educationData.Degree);

            //Select Year of graduation
            SelectElement selectYearOption = new SelectElement(yearDropdown);
            selectYearOption.SelectByValue(educationData.GraduationYear);

            Wait.WaitToBeClickable(driver, "XPath", "//input[@value='Add']", 15);
            //Click Add button 
            addButton.Click();
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
