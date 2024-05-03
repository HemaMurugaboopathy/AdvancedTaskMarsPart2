using AdvancedTaskSpecFlow.JSON_Data;
using OpenQA.Selenium.Chrome;
using AdvancedTaskSpecFlow.Pages;
using AdvancedTaskSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;
using AdvancedTaskSpecFlow.AssertHelpers;


namespace AdvancedTaskSpecFlow.StepDefinitions
{
    [Binding]
    public class EducationFeatureStepDefinitions: CommonDriver
    {
        ProfileLoginPageComponent profileLoginPageComponentObj;
        ProfileMenuTabComponents profileMenuTabComponentsObj;
        ProfileEducationComponents profileEducationComponentsObj;
        AddEditEducationComponents addEditEducationComponentsObj;

        public EducationFeatureStepDefinitions()
        {
            profileEducationComponentsObj = new ProfileEducationComponents();
            profileLoginPageComponentObj = new ProfileLoginPageComponent();
            profileMenuTabComponentsObj = new ProfileMenuTabComponents();
            addEditEducationComponentsObj = new AddEditEducationComponents();
        }
       
        [Given(@"I logged in Mars portal successfully")]
        public void GivenILoggedInMarsPortalSuccessfully()
        {
            profileLoginPageComponentObj.SigninActions();
            profileLoginPageComponentObj.LoginActions();
            profileMenuTabComponentsObj.GoToEducationPage();
        }

        [When(@"I delete all records in the education page")]
        public void WhenIDeleteAllRecordsInTheEducationPage()
        {
            addEditEducationComponentsObj.Delete_All_Records();
        }

        [When(@"I create a new education with ID (.*)")]
        public void WhenICreateANewEducationWithID(int id)
        {
            List<EducationData> educationDataList = JsonReader.loadData<EducationData>("addEducationData.json");
            EducationData educationdata = educationDataList.FirstOrDefault(x => x.Id == id);
            profileEducationComponentsObj.Click_AddEducation();
            addEditEducationComponentsObj.Add_Education(educationdata);
        }

        [Then(@"the education with ID (.*) should be created successfully")]
        public void ThenTheEducationWithIDShouldBeCreatedSuccessfully(int id)
        {
            String acutalSuccessMessage = addEditEducationComponentsObj.getMessage();
            string expected = "Education has been added";
            EducationAssertHelper.assertAddEducationSuccessMessage(expected, acutalSuccessMessage);
        }

        [When(@"I leave the college textbox with ID (.*)")]
        public void WhenILeaveTheCollegeTextboxWithID(int id)
        {
            List<EducationData> educationDataList = JsonReader.loadData<EducationData>("addEducationData.json");
            EducationData educationdata = educationDataList.FirstOrDefault(x => x.Id == id);
            profileEducationComponentsObj.Click_AddEducation();
            addEditEducationComponentsObj.Add_Education(educationdata);
        }

        [Then(@"the education with ID (.*) shouldnot be created successfully")]
        public void ThenTheEducationWithIDShouldnotBeCreatedSuccessfully(int p0)
        {
            String acutalSuccessMessage = addEditEducationComponentsObj.getMessage();
            string expected = "Please enter all the fields";
            EducationAssertHelper.assertAddEducationSuccessMessage(expected, acutalSuccessMessage);
        }
        [When(@"I delete an existing education with ID (.*)")]
        public void WhenIDeleteAnExistingEducationWithID(int id)
        {
            List<EducationData> educationDataList = JsonReader.loadData<EducationData>("deleteEducationData.json");
            EducationData educationdata = educationDataList.FirstOrDefault(x => x.Id == id);
            profileEducationComponentsObj.Click_DeleteEducation(educationdata);
        }

        [Then(@"the education with ID (.*) should be deleted successfully")]
        public void ThenTheEducationWithIDShouldBeDeletedSuccessfully(int p0)
        {
            String acutalSuccessMessage = addEditEducationComponentsObj.getMessage();
            string expected = "Education entry successfully removed";
            EducationAssertHelper.assertAddEducationSuccessMessage(expected, acutalSuccessMessage);
        }


    }
}
