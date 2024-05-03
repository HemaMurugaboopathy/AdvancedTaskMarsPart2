using AdvancedTaskSpecFlow.AssertHelpers;
using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Pages;
using AdvancedTaskSpecFlow.Pages.AccountMenu;
using AdvancedTaskSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;

namespace AdvancedTaskSpecFlow.StepDefinitions
{
    [Binding]
    public class PasswordFeatureStepDefinitions: CommonDriver
    {
        ProfileLoginPageComponent profileLoginPageComponentObj;
        ProfileUserComponent profileUserComponentObj;
        PasswordComponent passwordComponentObj;

        public PasswordFeatureStepDefinitions()
        {
            profileLoginPageComponentObj = new ProfileLoginPageComponent();
            profileUserComponentObj = new ProfileUserComponent();
            passwordComponentObj = new PasswordComponent();
        }

        [Given(@"I logged in Mars portal successfully and navigate to user tab")]
        public void GivenILoggedInMarsPortalSuccessfullyAndNavigateToUserTab()
        {
            profileLoginPageComponentObj.SigninActions();
            profileLoginPageComponentObj.LoginActions();
            profileUserComponentObj.GoToUserTab();
        }


        [When(@"I change my password and update new password with ID (.*)")]
        public void WhenIChangeMyPasswordAndUpdateNewPasswordWithID(int id)
        {
            List<PasswordData> passwordDataList = JsonReader.loadData<PasswordData>("addPasswordData.json");
            PasswordData passwordData = passwordDataList.FirstOrDefault(x => x.Id == id);
            
            passwordComponentObj.Change_Password(passwordData);
        }

        [Then(@"the password with ID (.*) should be updated successfully")]
        public void ThenThePasswordWithIDShouldBeUpdatedSuccessfully(int p0)
        {
            String acutalSuccessMessage = passwordComponentObj.getMessage();
            string expected = "Password Changed Successfully";
            PasswordAssertHelper.assertUpdatePasswordSuccessMessage(expected, acutalSuccessMessage);
        }

    }
}
