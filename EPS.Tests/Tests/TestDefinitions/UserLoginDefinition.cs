using EPS.Automation;
using EPS.Automation.DataTransferObjects;
using EPS.Automation.Exceptions;
using EPS.Tests.UIPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace EPS.Tests.Tests.TestDefinitions
{
    [Binding]
    public sealed class UserLoginDefinition : BasePage
    {

        /// <summary>
        /// The static instance of the logger for the class.
        /// </summary>
        private static readonly Logger Logger =
        Logger.GetInstance(typeof(UserLoginDefinition));

        /// <summary>
        /// The instance for Login Page
        /// </summary>
         /// <param name="userType">This is user Type present in enum.</param>
        private BrowseURL loginPage;
        [Given(@"I access the URL as ""(.*)""")]
        public void AccessApplicationURL(string userType)
        {
            try
            {
                UserType = userType;
                CurrentBrowserName = ConfigurationManager.AppSettings.Get("Browser");
                int loginRetry = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Retry_Count"));
                TransactionTimings = DateTime.Now.ToString();
                // Pick Url based on user type enum
                loginPage = new BrowseURL((User.UserTypeEnum)
                    Enum.Parse(typeof(User.UserTypeEnum), userType));
                //Login  the type of the user
                bool isBaseMMSGUrlBrowsedSuccessful =
                        loginPage.IsUrlBrowsedSuccessful((User.UserTypeEnum)Enum.Parse(typeof(User.UserTypeEnum), userType));
                //Check Is Url Browsed Successfully
                for (int i = 1; i <= loginRetry; i++)
                {
                    if (!isBaseMMSGUrlBrowsedSuccessful)
                    {
                        loginPage.GoToLoginUrl((User.UserTypeEnum)Enum.Parse(typeof(User.UserTypeEnum), userType));

                        isBaseMMSGUrlBrowsedSuccessful =
                           loginPage.IsUrlBrowsedSuccessful((User.UserTypeEnum)Enum.Parse(typeof(User.UserTypeEnum), userType));
                    }
                    else
                    {
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                new BaseTestScript().WebDriverCleanUp();
                ExceptionHandler.HandleException(e, true);
            }
        }

        [Given(@"I access the URL as ""(.*)"" test")]
        public void GivenIAccessTheURLAsTest(string userType)
        {
            try
            {

                CurrentBrowserName = ConfigurationManager.AppSettings.Get("Browser");
                int loginRetry = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Retry_Count"));
                TransactionTimings = DateTime.Now.ToString();
                //Login  the type of the user
                bool isBaseMMSGUrlBrowsedSuccessful =
                        loginPage.IsUrlBrowsedSuccessful((User.UserTypeEnum)Enum.Parse(typeof(User.UserTypeEnum), userType));

            }
            catch (Exception e)
            {
                new BaseTestScript().WebDriverCleanUp();
                ExceptionHandler.HandleException(e, true);
            }
        }


        /// <summary>
        /// Verify the login page title
        /// </summary>
        /// <param name="loginPageName">This is login page name.</param>
        [Then(@"I should be displayed with ""(.*)"" login page")]
        public void DisplayOfLoginPage(string loginPageName)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            // Assert the expected value with the actual value
            Logger.LogAssertion("CommonAcceptanceTestDefinition", ScenarioContext.Current.ScenarioInfo.Title, () =>
            Assert.AreEqual(loginPageName, new LoginPage().GetLoginPageTitle()));
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        /// Login as user based on the user type
        /// </summary>
        /// <param name="userType">This is user type enum</param>
        /// <param name="userNameType">This is user name.</param>
        /// <param name="passwordType">This is password</param>
        [When(@"I login as ""(.*)"" with ""(.*)"" and ""(.*)""")]
        public void LoginAsEPSUser(User.UserTypeEnum userType, string userNameType, string passwordType)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            new LoginPage().LoginAsEPSUser(userType, userNameType, passwordType);
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        /// Option In User Profile.
        /// </summary>
        /// <param name="options">This is options in user profile.</param>
        [When(@"I click on ""([^""]*)"" option in User Profile")]
        public void clickOnOptionInUserProfile(string options)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            new UsersPage().OptionInUserProfile(options);
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }


    }
}
