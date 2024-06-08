using AdvancedTaskSpecFlow.AssertHelpers;
using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Pages;
using AdvancedTaskSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;

namespace AdvancedTaskSpecFlow.StepDefinitions
{
    [Binding]
    public class DescriptionFeatureStepDefinitions
    {
        ProfileLoginPageComponent profileLoginPageComponentObj;
        ProfileMenuTabComponents profileMenuTabComponentsObj;
        ProfileDescriptionComponent profileDescriptionComponentObj;
        public DescriptionFeatureStepDefinitions()
        {
            profileLoginPageComponentObj = new ProfileLoginPageComponent();
            profileMenuTabComponentsObj = new ProfileMenuTabComponents();
            profileDescriptionComponentObj = new ProfileDescriptionComponent();
        }


        [Given(@"User logged into Mars URL and navigates to profile page")]
        public void GivenUserLoggedIntoMarsURLAndNavigatesToProfilePage()
        {
            profileLoginPageComponentObj.SigninActions();
            profileLoginPageComponentObj.LoginActions();
            profileMenuTabComponentsObj.GoToDescriptionPage();
        }

        [When(@"I add my description with ID (.*)")]
        public void WhenIAddMyDescriptionWithID(int id)
        {
            List<DescriptionData> descriptionDataList = JsonReader.loadData<DescriptionData>("addDescriptionData.json");
            DescriptionData descriptionData = descriptionDataList.FirstOrDefault(x => x.Id == id);
            profileDescriptionComponentObj.enterDescription(descriptionData);
           

        }

        [Then(@"the description with ID (.*) should be updated successfully")]
        public void ThenTheDescriptionWithIDShouldBeUpdatedSuccessfully(int id)
        {
            DescriptionData descriptionData = JsonReader.loadData<DescriptionData>(@"addDescriptionData.json").FirstOrDefault(x => x.Id == id);
            string actualMessage = profileDescriptionComponentObj.getMessage();
            DescriptionAssertHelper.assertAddDescriptionSuccessMessage(descriptionData.ExpectedMessage, actualMessage);
        }
    }
}
