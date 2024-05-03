using AdvancedTaskSpecFlow.AssertHelpers;
using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Pages;
using AdvancedTaskSpecFlow.Pages.ManageMenu;
using AdvancedTaskSpecFlow.Pages.ManageMenu.ManageListings;
using AdvancedTaskSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;

namespace AdvancedTaskSpecFlow.StepDefinitions
{
    [Binding]
    public class ManageListingsFeatureStepDefinitions: CommonDriver
    {

        ProfileLoginPageComponent profileLoginPageComponentObj;
        ManageOverviewComponent manageOverviewComponentObj;
        ListingsOverviewComponent listingsOverviewComponentObj;
        public ManageListingsFeatureStepDefinitions()
        {
            profileLoginPageComponentObj = new ProfileLoginPageComponent();
            manageOverviewComponentObj = new ManageOverviewComponent();
            listingsOverviewComponentObj= new ListingsOverviewComponent();
        }

        [Given(@"User logged into Mars URL and navigates to Manage Listings tab")]
        public void GivenUserLoggedIntoMarsURLAndNavigatesToManageListingsTab()
        {
            profileLoginPageComponentObj.SigninActions();
            profileLoginPageComponentObj.LoginActions();
            manageOverviewComponentObj.clickManageListings();
        }

        [When(@"I update my skills in my listings with ID (.*)")]
        public void WhenIUpdateMySkillsInMyListingsWithID(int id)
        {
            ShareSkillData shareSkillData = JsonReader.loadData<ShareSkillData>(@"ShareSkillData.json").FirstOrDefault(x => x.Id == id);
            listingsOverviewComponentObj.clickEditButton(shareSkillData);
            listingsOverviewComponentObj.Edit_Listings(shareSkillData);
            manageOverviewComponentObj.clickManageListings();
        }

        [Then(@"the listings with ID (.*) should be updated successfully")]
        public void ThenTheListingsWithIDShouldBeUpdatedSuccessfully(int id)
        {
            manageOverviewComponentObj.clickManageListings();
        }

        [When(@"I want to view my skills with ID (.*)")]
        public void WhenIWantToViewMySkillsWithID(int id)
        {
            ShareSkillData shareSkillData = JsonReader.loadData<ShareSkillData>(@"ShareSkillData.json").FirstOrDefault(x => x.Id == id);
            listingsOverviewComponentObj.clickViewButton(shareSkillData);
        }

        [Then(@"I can able to view my skills with ID (.*) successfully")]
        public void ThenICanAbleToViewMySkillsWithIDSuccessfully(int id)
        {
            ShareSkillData shareSkillData = JsonReader.loadData<ShareSkillData>(@"ShareSkillData.json").FirstOrDefault(x => x.Id == id);
            string viewTitle = listingsOverviewComponentObj.getViewTitle(shareSkillData.Title);
            ManageListingsAssertHelper.assertViewManageListingsSuccessMessage(shareSkillData.Title, viewTitle);
        }

        [When(@"I want to click the toggle button to active")]
        public void WhenIWantToClickTheToggleButtonToActive()
        {
            listingsOverviewComponentObj.clickToggleButton();
        }

        [Then(@"the listings with ID (.*) should be activated successfully")]
        public void ThenTheListingsWithIDShouldBeActivatedSuccessfully(int id)
        {
            string actualMessage = listingsOverviewComponentObj.getMessage();
            ManageListingsAssertHelper.assertEnableManageListingsSuccessMessage("Service has been activated", actualMessage);
        }

        [When(@"I want to click the toggle button to deactivate")]
        public void WhenIWantToClickTheToggleButtonToDeactivate()
        {
            listingsOverviewComponentObj.clickToggleButton();
        }

        [Then(@"the listings with ID (.*) should be deactivated successfully")]
        public void ThenTheListingsWithIDShouldBeDeactivatedSuccessfully(int id)
        {
            string actualMessage = listingsOverviewComponentObj.getMessage();
            ManageListingsAssertHelper.assertDisableManageListingsSuccessMessage("Service has been deactivated", actualMessage);
        }


        [When(@"I want to delete my skills with ID (.*)")]
        public void WhenIWantToDeleteMySkillsWithID(int id)
        {
            ShareSkillData shareSkillData = JsonReader.loadData<ShareSkillData>(@"ShareSkillData.json").FirstOrDefault(x => x.Id == id);
            listingsOverviewComponentObj.clickDeleteButton(shareSkillData);
        }

        [Then(@"the skills with ID (.*) should be created successfully")]
        public void ThenTheSkillsWithIDShouldBeCreatedSuccessfully(int id)
        {
            string actualMessage = listingsOverviewComponentObj.getMessage();
            ManageListingsAssertHelper.assertDeleteManageListingsSuccessMessage("Selenium has been deleted", actualMessage);
        }
    }
}
