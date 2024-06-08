using AdvancedTaskSpecFlow.AssertHelpers;
using AdvancedTaskSpecFlow.JSON_Data;
using AdvancedTaskSpecFlow.Pages;
using AdvancedTaskSpecFlow.Pages.CertificationComponent;
using AdvancedTaskSpecFlow.Utilities;
using System;
using TechTalk.SpecFlow;

namespace AdvancedTaskSpecFlow.StepDefinitions
{
    [Binding]
    public class CertificationFeatureStepDefinitions: CommonDriver
    {
        ProfileLoginPageComponent profileLoginPageComponentObj;
        ProfileMenuTabComponents profileMenuTabComponentsObj;
        ProfileCertification_Component profileCertificationComponentObj;
        AddCertificationComponent addCertificationComponentObj;

        public CertificationFeatureStepDefinitions()
        {
            profileCertificationComponentObj = new ProfileCertification_Component();
            profileLoginPageComponentObj = new ProfileLoginPageComponent();
            profileMenuTabComponentsObj = new ProfileMenuTabComponents();
            addCertificationComponentObj = new AddCertificationComponent();
        }

        [Given(@"User logged into Mars URL and navigates to Certifications tab")]
        public void GivenUserLoggedIntoMarsURLAndNavigatesToCertificationsTab()
        {
            profileLoginPageComponentObj.SigninActions();
            profileLoginPageComponentObj.LoginActions();
            profileMenuTabComponentsObj.GoToCertificationPage();
        }

     
        [When(@"I delete all records in the certification page")]
        public void WhenIDeleteAllRecordsInTheCertificationPage()
        {
            addCertificationComponentObj.Delete_All_Records();
        }

        [When(@"I create a new certification with ID (.*)")]
        public void WhenICreateANewCertificationWithID(int id)
        {
            List<CertificationData> certificationDataList = JsonReader.loadData<CertificationData>("addCertificationData.json");
            CertificationData certificationdata = certificationDataList.FirstOrDefault(x => x.Id == id);
            profileCertificationComponentObj.Click_AddCertification();
            addCertificationComponentObj.Add_Certification(certificationdata);
        }

        [Then(@"the certification with ID (.*) should be created successfully")]
        public void ThenTheCertificationWithIDShouldBeCreatedSuccessfully(int id)
        {
            String acutalSuccessMessage = addCertificationComponentObj.getMessage();
            string expected = @".* has been added to your certification.*";
            CertificationAssertHelper.assertAddCertificationSuccessMessage(expected, acutalSuccessMessage);
        }

        [When(@"I leave the certificate textbox with ID (.*)")]
        public void WhenILeaveTheCertificateTextboxWithID(int id)
        {
            List<CertificationData> certificationDataList = JsonReader.loadData<CertificationData>("addCertificationData.json");
            CertificationData certificationdata = certificationDataList.FirstOrDefault(x => x.Id == id);
            profileCertificationComponentObj.Click_AddCertification();
            addCertificationComponentObj.Add_Certification(certificationdata);
        }

        [Then(@"the certification with ID (.*) shouldnot be created successfully")]
        public void ThenTheCertificationWithIDShouldnotBeCreatedSuccessfully(int id)
        {
            String acutalSuccessMessage = addCertificationComponentObj.getMessage();
            string expected = "Please enter Certification Name, Certification From and Certification Year";
            CertificationAssertHelper.assertAddCertificationSuccessMessage(expected, acutalSuccessMessage);
        }

        [When(@"I delete an existing certification with ID (.*)")]
        public void WhenIDeleteAnExistingCertificationWithID(int id)
        {
            List<CertificationData> certificationDataList = JsonReader.loadData<CertificationData>("deleteCertificationData.json");
            CertificationData certificationdata = certificationDataList.FirstOrDefault(x => x.Id == id);
            profileCertificationComponentObj.Click_DeleteCertification(certificationdata);
        }

        [Then(@"the certification with ID (.*) should be deleted successfully")]
        public void ThenTheCertificationWithIDShouldBeDeletedSuccessfully(int id)
        {
            String acutalSuccessMessage = addCertificationComponentObj.getMessage();
            string expected = @".* has been deleted from your certification.*";
            CertificationAssertHelper.assertAddCertificationSuccessMessage(expected, acutalSuccessMessage);
        }
    }
}
