using AdvancedTaskSpecFlow.Pages.ManageMenu.ManageListings;
using AdvancedTaskSpecFlow.Pages.ManageMenu;
using AdvancedTaskSpecFlow.Pages;
using System;
using TechTalk.SpecFlow;
using AdvancedTaskSpecFlow.Pages.ManageMenu.MangeRequest;
using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Utilities;
using AdvancedTaskSpecFlow.AssertHelpers;

namespace AdvancedTaskSpecFlow.StepDefinitions
{
    [Binding]
    public class ManageRequestStepDefinitions
    {
        ProfileLoginPageComponent profileLoginPageComponentObj;
        ManageOverviewComponent manageOverviewComponentObj;
        ManageRequestOverviewComponent manageRequestOverviewComponentObj;
        ReceivedRequestComponent receivedRequestComponentObj;
        SentRequestComponent sentRequestComponentObj;

        public ManageRequestStepDefinitions()
        {
            profileLoginPageComponentObj = new ProfileLoginPageComponent();
            manageOverviewComponentObj = new ManageOverviewComponent();
            manageRequestOverviewComponentObj = new ManageRequestOverviewComponent();
            receivedRequestComponentObj = new ReceivedRequestComponent();
            sentRequestComponentObj     = new SentRequestComponent();
        }
        [Given(@"User logged into Mars URL and navigates to Manage Request tab")]
        public void GivenUserLoggedIntoMarsURLAndNavigatesToManageRequestTab()
        {
            profileLoginPageComponentObj.SigninActions();
            profileLoginPageComponentObj.LoginActions();
            manageOverviewComponentObj.clickManageRequests();
        }

        [When(@"I click recieved requests and I accepts the received requests with ID '([^']*)'")]
        public void WhenIClickRecievedRequestsAndIAcceptsTheReceivedRequestsWithID(int id)
        {

            manageRequestOverviewComponentObj.clickReceivedRequest();
            ReceivedRequestData receivedRequestData = JsonReader.loadData<ReceivedRequestData>(@"ReceivedRequestData.json").FirstOrDefault(x => x.Id == id);
            receivedRequestComponentObj.clickAcceptButton(receivedRequestData);
        }

        [Then(@"the manage request with ID (.*) should be accepted successfully")]
        public void ThenTheManageRequestWithIDShouldBeAcceptedSuccessfully(int id)
        {
            ReceivedRequestData receivedRequestData = JsonReader.loadData<ReceivedRequestData>(@"ReceivedRequestData.json").FirstOrDefault(x => x.Id == id);
            string actualMessage = receivedRequestComponentObj.getMessage();
            ManageRequestAssertHelper.assertAcceptSuccessMessage(receivedRequestData.ExpectedMessage, actualMessage);
        }

        [When(@"I click recieved requests and I declines the received requestswith ID '([^']*)'")]
        public void WhenIClickRecievedRequestsAndIDeclinesTheReceivedRequestswithID(int id)
        {
            manageRequestOverviewComponentObj.clickReceivedRequest();
            ReceivedRequestData receivedRequestData = JsonReader.loadData<ReceivedRequestData>(@"ReceivedRequestData.json").FirstOrDefault(x => x.Id == id);
            receivedRequestComponentObj.clickDeclineButton(receivedRequestData);
        }

        [Then(@"the manage request with ID (.*) should be declined successfully")]
        public void ThenTheManageRequestWithIDShouldBeDeclinedSuccessfully(int id)
        {
            ReceivedRequestData receivedRequestData = JsonReader.loadData<ReceivedRequestData>(@"ReceivedRequestData.json").FirstOrDefault(x => x.Id == id);
            string actualMessage = receivedRequestComponentObj.getMessage();
            ManageRequestAssertHelper.assertDeclineSuccessMessage(receivedRequestData.ExpectedMessage, actualMessage);
        }

        [When(@"I click recieved requests and I completes the received requestswith ID '([^']*)'")]
        public void WhenIClickRecievedRequestsAndICompletesTheReceivedRequestswithID(int id)
        {
            manageRequestOverviewComponentObj.clickReceivedRequest();
            ReceivedRequestData receivedRequestData = JsonReader.loadData<ReceivedRequestData>(@"ReceivedRequestData.json").FirstOrDefault(x => x.Id == id);
            //receivedRequestComponentObj.clickAcceptButton(receivedRequestData);
            receivedRequestComponentObj.clickCompleteButton(receivedRequestData);
        }

        [Then(@"the received request with ID (.*) should be completed successfully")]
        public void ThenTheReceivedRequestWithIDShouldBeCompletedSuccessfully(int id)
        {
            ReceivedRequestData receivedRequestData = JsonReader.loadData<ReceivedRequestData>(@"ReceivedRequestData.json").FirstOrDefault(x => x.Id == id);
            string actualMessage = receivedRequestComponentObj.getMessage();
            ManageRequestAssertHelper.assertCompleteSuccessMessage(receivedRequestData.ExpectedMessage, actualMessage);
        }
        [When(@"I click sent requests")]
        public void WhenIClickSentRequests()
        {
            manageRequestOverviewComponentObj.clickSentRequest();
        }

        [Then(@"I withdraw the sent requestswith ID '([^']*)'")]
        public void ThenIWithdrawTheSentRequestswithID(int id)
        {
            SentRequestData sentRequestData = JsonReader.loadData<SentRequestData>(@"SentRequestData.json").FirstOrDefault(x => x.Id == id);
            sentRequestComponentObj.clickWithdrawButton(sentRequestData);
        }

        [Then(@"the withdraw request with ID (.*) should be withdrawn successfully")]
        public void ThenTheWithdrawRequestWithIDShouldBeWithdrawnSuccessfully(int id)
        {
            SentRequestData sentRequestData = JsonReader.loadData<SentRequestData>(@"SentRequestData.json").FirstOrDefault(x => x.Id == id);
            string actualMessage = sentRequestComponentObj.getMessage();
            ManageRequestAssertHelper.assertWithdrawSuccessMessage(sentRequestData.ExpectedMessage, actualMessage);

        }
    }
}
