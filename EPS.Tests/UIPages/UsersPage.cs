using EPS.Automation;
using EPS.Automation.DataTransferObjects;
using EPS.Automation.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPS.Tests.UIPages
{
    class UsersPage : BasePage
    {
        /// <summary>
        /// This method is to click on LSB option.
        /// </summary>
        /// <param name="options">This is options Type .</param>
        ///  <param name="settingtype">This is  Type of settings .</param>
        public void  ClickOnTheOption(string options, string settingtype)
        {
            try { 
                switch (settingtype)
                {
                    case "Settings":
                        switch(options)
                        {
                            case "User Access Management":
                                WaitForElement(By.XPath(UsersPageResource.UserPage_clickOnTheOption_Settings_UserAccessManagement_Xpath));
                                ClickButtonByXPath(UsersPageResource.UserPage_clickOnTheOption_Settings_UserAccessManagement_Xpath);
                                break;
                        }
                        break;
                }
            
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }

        /// <summary>
        /// This method is to click on buttons of the User Access Management.
        /// </summary>
        /// <param name="buttonType">This is button Type .</param>
        public void ClickOnButtonInUserAccessManagement(string buttonType)
        {
            try
            {
                switch(buttonType)
                {
                    case "ADD USER":
                        WaitForElement(By.XPath(UsersPageResource.UserPage_clickOnButtonInUserAccessManagement_AddUser_Xpath));
                        ClickButtonByXPath(UsersPageResource.UserPage_clickOnButtonInUserAccessManagement_AddUser_Xpath);
                        Thread.Sleep(3000);
                        break;
                }

            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }

        /// <summary>
        /// This method is enter the user details.
        /// </summary>
        /// <param name="userType">This is user Type present in enum.</param>
        /// <param name="groupName">This is group name of the user.</param>
        /// <param name="role">This is role user.</param>
        public void FillUserDetails(User.UserTypeEnum userType, string groupName, string role)
        {
            User user = User.Get(userType);
            string fullName = string.Empty;
            string emailAddress = string.Empty;
            string mobileNo = RandomDigits(8);
            string designation = string.Empty;
            
            try
            {
                Guid userInformation = Guid.NewGuid();

                String date = DateTime.Now.ToString("yyyy/MM/dd HH-mm-ss");

                // Generate Customized GUID for text fill
                fullName = "AutoUser" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Guid.NewGuid().ToString().Substring(0, 4);            
                emailAddress = string.Format(fullName + "@gmail.com");
                designation = "Test01";

                switch (userType)
                {
                    case User.UserTypeEnum.NewEPSUser01:
                        // Fill Full Name textbox
                        WaitForElement(By.XPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_FullName_Xpath));
                        ClearTextByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_FullName_Xpath);
                        FillTextBoxByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_FullName_Xpath, fullName);

                        // Fill Email Address textbox
                        WaitForElement(By.XPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_EmailAddress_Xpath));
                        ClearTextByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_EmailAddress_Xpath);
                        FillTextBoxByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_EmailAddress_Xpath, emailAddress);

                        // Fill Mobile number textbox
                        WaitForElement(By.XPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_mobileNo_Xpath));
                        ClearTextByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_mobileNo_Xpath);
                        FillTextBoxByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_mobileNo_Xpath, mobileNo);

                        // Fill Designation textbox
                        WaitForElement(By.XPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_Designation_Xpath));
                        ClearTextByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_Designation_Xpath);
                        FillTextBoxByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_Designation_Xpath, designation);

                        // Selecting group name from the dropdown
                        WaitForElement(By.Id(UsersPageResource.FillUserDetails_CreateUser_PopUp_Dropdown_Xpath));                    
                        SelectDropDownValueThroughTextById(UsersPageResource.FillUserDetails_CreateUser_PopUp_Dropdown_Xpath, groupName);

                        // Fill Role textbox
                        WaitForElement(By.XPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_Role_Xpath));
                        ClearTextByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_Role_Xpath);
                        FillTextBoxByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_Role_Xpath, role);
                        PressEnterKeyByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_Role_Xpath);

                        

                        break;
                }
                IWebElement submitButton = GetWebElementPropertiesByXPath(UsersPageResource.FillUserDetails_CreateUser_PopUp_submitbutton_Xpath);
                PerformMouseClickAction(submitButton);
                Thread.Sleep(3000);






            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                // Store data in memory
                user.Name = fullName;
                user.UpdateUserInMemory(user);
                user.WriteUserInMemory(user, "Email", emailAddress);
                user.WriteUserInMemory(user, "MobileNo", mobileNo);
                user.WriteUserInMemory(user, "Name", fullName);
                user.WriteUserInMemory(user, "Designation", designation);
                user.WriteUserInMemory(user, "Group", groupName);
                user.WriteUserInMemory(user, "Role", role);
                user.UpdateEmail(emailAddress);
                user.UpdateMobileNo(mobileNo);
                user.UpdateName(fullName);
                user.UpdateDesignation(designation);
                user.UpdateGroup(groupName);
                user.UpdateRole(role);
            }
        }


        /// <summary>
        /// This method is used to generate the mobile number.
        /// </summary>
        /// <param name="length">This is length of the mobile number.</param>
        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        /// <summary>
        /// Option In User Profile.
        /// </summary>
        /// <param name="options">This is options in user profile.</param>
        public void OptionInUserProfile(string options)
        {
            try
            {
                switch(options)
                {
                    case "LogOut":
                        Thread.Sleep(3000);
                        WaitForElement(By.XPath(UsersPageResource.OptionInUserProfile_UserProfile_Xpath));
                        IWebElement userProfile = GetWebElementPropertiesByXPath(UsersPageResource.OptionInUserProfile_UserProfile_Xpath);
                        ClickByJavaScriptExecutor(userProfile);
                      
                        Thread.Sleep(2000);
                        IWebElement logout = GetWebElementPropertiesByXPath(UsersPageResource.OptionInUserProfile_UserProfile_Logout_Xpath);
                        ClickByJavaScriptExecutor(logout);
                     
                        break;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }
        
    }
}
