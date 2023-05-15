using EPS.Automation;
using EPS.Automation.DataTransferObjects;
using EPS.Automation.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
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
                        Thread.Sleep(2000);
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
            string referenceNumber = string.Empty;


            try
            {
                Guid userInformation = Guid.NewGuid();

                String date = DateTime.Now.ToString("yyyy/MM/dd HH-mm-ss");

                // Generate Customized GUID for text fill
                fullName = "AutoUser" + Guid.NewGuid().ToString().Substring(0, 4);            
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
                // Getting Reference Number from the Archive list and updating same to test data
                ClickOptionsInLSB("Home");
                ClickOnTab("Archive");
                WaitForElement(By.XPath(UsersPageResource.FillUserDetails_calander_icon_Xpath));
                ClickButtonByXPath(UsersPageResource.FillUserDetails_calander_icon_Xpath);
                WaitForElement(By.XPath(UsersPageResource.FillUserDetails_Todays_Date_Xpath));
                ClickButtonByXPath(UsersPageResource.FillUserDetails_Todays_Date_Xpath);
                Thread.Sleep(3000);
                referenceNumber= GetElementInnerTextByXPath(UsersPageResource.FillUserDetails_First_Reference_Number_Xpath);
                Console.WriteLine(referenceNumber);



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
                user.WriteUserInMemory(user, "ReferenceNumber", referenceNumber);
                user.UpdateEmail(emailAddress);
                user.UpdateMobileNo(mobileNo);
                user.UpdateName(fullName);
                user.UpdateDesignation(designation);
                user.UpdateGroup(groupName);
                user.UpdateRole(role);
                user.UpdateReferenceNumber(referenceNumber);
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

        /// <summary>
        /// Click option in LSB.
        /// </summary>
        /// <param name="options">This is options in LSB.</param>
        public void ClickOptionsInLSB(string options)
        {
            try
            {
                switch (options)
                {
                    case "Home":
                        Thread.Sleep(3000);
                        WaitForElement(By.XPath(UsersPageResource.ClickOptionsInLSB_Home_Xpath));
                        IWebElement home = GetWebElementPropertiesByXPath(UsersPageResource.ClickOptionsInLSB_Home_Xpath);
                        ClickByJavaScriptExecutor(home);

                        break;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }

        /// <summary>
        /// Tab Navigation.
        /// </summary>
        /// <param name="tabName">This is tab Names present in the page.</param>
        public void ClickOnTab(string tabName)
        {
            try
            {
                switch (tabName)
                {
                    case "Archive":
                        Thread.Sleep(3000);
                        WaitForElement(By.XPath(UsersPageResource.ClickOnTab_Archive_Xpath));
                        IWebElement archiveTab = GetWebElementPropertiesByXPath(UsersPageResource.ClickOnTab_Archive_Xpath);
                        ClickByJavaScriptExecutor(archiveTab);

                        break;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }

        /// <summary>
        /// This method is used to approve the newly created user.
        /// </summary>
        /// <param name="userType">This is user Type present in enum.</param>
        /// <param name="actionType">This is action Type .</param>
        public void ApproveOrRejectTheNewlyCreatedUser(string actionType, User.UserTypeEnum userType)
        {
            
            try
            {
                User user = User.Get(userType);
                bool status = false;
                string expectedreferenceNumber = user.ReferenceNumber.ToString();
                WaitForElement(By.XPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_Reference_Number_Xpath));
                IList<IWebElement> ActualReferenceNumber = GetWebElementsProperties(By.XPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_Reference_Number_Xpath));
                for (int i = 0; i < ActualReferenceNumber.Count; i++)
                {
                    if(expectedreferenceNumber == ActualReferenceNumber[i].Text)
                    {
                        ActualReferenceNumber[i].Click();
                        status = true;
                        break;
                    }
                }
                Thread.Sleep(3000);
                if(status)
                {
                    switch (actionType)
                    {
                        case "APPROVE":
                            WaitForElement(By.XPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_UploadFile_Xpath));

                            // Get the current directory path
                            string currentDirectory = Directory.GetCurrentDirectory();

                            // Get the project directory path by traversing up two levels
                            string projectDirectory = Directory.GetParent(currentDirectory).Parent.FullName;
                            // Concatenate the project directory path with the file name or relative file path
                            string filePath = Path.Combine(projectDirectory, "TestData\\Files\\Approve.png");
                            Thread.Sleep(3000);
                           //   UploadFile(filePath, By.XPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_UploadFile_Xpath));                  
                            Thread.Sleep(20000);
                            
                            WaitForElement(By.XPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_Approve_Button_Xpath));
                            ClickButtonByXPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_Approve_Button_Xpath);
                            break;
                        case "REJECT":
                            WaitForElement(By.XPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_LTA_Remarks_Xpath));
                            FillTextBoxByXPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_LTA_Remarks_Xpath, "Rejected");
                            WaitForElement(By.XPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_REJECT_Button_Xpath));
                            ClickButtonByXPath(UsersPageResource.UserPage_ApproveOrRejectTheNewlyCreatedUser_REJECT_Button_Xpath);
                            break;
                    }
                }
               

               
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }
    }
}
