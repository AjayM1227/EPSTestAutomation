using EPS.Automation;
using EPS.Automation.DataTransferObjects;
using EPS.Tests.UIPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace EPS.Tests.Tests.TestDefinitions
{
    [Binding]
    public sealed class UsersManagementDefinition :BasePage
    {
        /// <summary>
        /// The static instance of the logger for the class.
        /// </summary>
        private static readonly Logger Logger =
        Logger.GetInstance(typeof(UsersManagementDefinition));

        /// <summary>
        /// This method is to click on LSB option.
        /// </summary>
        /// <param name="options">This is options Type .</param>
        ///  <param name="settingtype">This is  Type of settings .</param>
        [When(@"I Click on the ""([^""]*)"" option in ""([^""]*)""")]
        public void clickOnTheOption(string options, string settingtype)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            new UsersPage().ClickOnTheOption(options,settingtype);
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        /// This method is to click on buttons of the User Access Management.
        /// </summary>
        /// <param name="buttonType">This is button Type .</param>
        [When(@"I click on ""([^""]*)"" button in User Access Management")]
        public void clickOnButtonInUserAccessManagement(string buttonType)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            new UsersPage().ClickOnButtonInUserAccessManagement(buttonType);
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }


        /// <summary>
        /// This method is enter the user details.
        /// </summary>
        /// <param name="userType">This is user Type present in enum.</param>
        /// <param name="groupName">This is group name of the user.</param>
        /// <param name="role">This is role user.</param>

        [When(@"I enter user details of ""([^""]*)"" in ""([^""]*)"" group with ""([^""]*)""")]
        public void EnterUserDetailsOfInGroupWith(User.UserTypeEnum userType, string groupName, string role)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);          
            new UsersPage().FillUserDetails(userType, groupName, role);
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }



    }
}
