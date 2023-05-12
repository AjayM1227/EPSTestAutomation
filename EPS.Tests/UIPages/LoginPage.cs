using EPS.Automation;
using EPS.Automation.DataTransferObjects;
using EPS.Automation.Exceptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPS.Tests.UIPages
{
    class LoginPage : BasePage
    {
        /// The static instance of the logger for the class.
        /// </summary>
        private static readonly Logger Logger =
            Logger.GetInstance(typeof(LoginPage));

        /// <summary>
        /// This is the login retry count.
        /// </summary>
        private static readonly int LoginAttemptRetryCount = Convert.ToInt32(ConfigurationManager.
            AppSettings[LoginPageResource.Login_Page_AppSetting_RetryCount_Key]);

        private static readonly int WaitTimeCount = Convert.ToInt32(ConfigurationManager.
            AppSettings[LoginPageResource.Login_Page_AppSetting_WaitTime_Key]);

        /// <summary>
        /// Get login page title
        /// </summary>
        /// <returns>loginPageTitle</returns>
        public string GetLoginPageTitle()
        {
            string loginPageTitle = string.Empty;
            //   IWebElement getButton = null;
            try
            {
                // Wait untill window load and select window
                Thread.Sleep(3000);             
                // Get the login page title
                loginPageTitle = GetInnerTextAttributeValueByXPath(LoginPageResource.LoginPage_GetLoginPageTitle_Xpath);
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }

            return loginPageTitle;
        }

        /// <summary>
        /// Login as user based on the user type
        /// </summary>
        /// <param name="userType">This is user type enum.</param>
        ///  <param name="userNameType">This is user Name Type.</param>
        ///  <param name="passwordType">This is user Password Type.</param>
        public void LoginAsEPSUser(User.UserTypeEnum userTypeEnum, string userNameType, string passwordType)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            try
            {
                Thread.Sleep(7000);
                int count = 0;
                //Initialize Variable
                bool isLoginSuccessful = false;            
                while (count <= LoginAttemptRetryCount && !isLoginSuccessful)
                {
                    // Get the username and password based on the user type
                    User user = User.Get(userTypeEnum);

                    // Execute based on the user type
                    switch (userTypeEnum)
                    {
                        case User.UserTypeEnum.EPSAdminUser01:
                        case User.UserTypeEnum.EPSSuperAdminUser01:
                      

                            UserName = user.Name.ToString();
                            Password = user.Password.ToString();
                            // Enter user name and password
                            isLoginSuccessful = EPSUserAppAuthenticate(userTypeEnum, UserName, Password, userNameType, passwordType, count);
                           
                            break;
                    }

                    // Store data in memory
                    user.Name = UserName;
                    user.Password = Password;
                    user.UpdateUserInMemory(user);
                    user.WriteUserInMemory(user, "Name", UserName);
                    user.WriteUserInMemory(user, "Password", Password);
                    // Assign user details to global variable
                    UserName = user.Name;
                    Password = user.Password;
                    UserType = userTypeEnum.ToString();
                    count++;
                }
            }
            catch (Exception e)
            {
                UserName = "";
                UserType = "";
                Password = "";
                ExceptionHandler.HandleException(e);
                throw new LoginFailedException(String.Format
                (LoginPageResource.
                Login_Page_DefaultUserLoginAuthentication_ExceptionMessage_Text, UserName));
            }
            finally
            {
                new CustomReport().AddStatusToReport(CustomReport.ReportStatus.Info, string.Format("User Name: {0}", UserName));
            }
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        /// The function locates the credentials locators and
        /// submits the data to login into the LMSAdmin app.
        /// </summary>
        /// <param name="userName">Username of the user for login.</param>
        /// <param name="password">Password for the user to login.</param>
        /// <param>Number of times the retry needs to be performed for login.
        /// <name>retryCount</name> </param>
        /// <param name="userType"> This is User Type.</param>
        /// <returns>User Authentication Result</returns>
        private Boolean EPSUserAppAuthenticate(User.UserTypeEnum userTypeEnum, string userName,
            string password, string userNameType, string passwordType, int count)
        {
            //Authenticate User Login
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            //Initialize Variable
            bool isLoginSuccessful = false;
            //If the user name and password is for invalid attempt try then do not enter into retry mechanism
            if (userNameType == "Invalid username" || passwordType == "Invalid password"
                || userNameType == "Blank username" || passwordType == "Blank password")
            {
                // Enter user name
                EnterUserName(userName, userNameType);
                // Enter Password
                EnterPassword(password, passwordType);
                if (userNameType == "Blank username" || passwordType == "Blank password")
                {
                   
                    isLoginSuccessful = true;
                }
                else
                {
                    // Click login button
                    this.ClickLogInButton();
                    isLoginSuccessful = true;
                }
            }
            else
            {
                // Enter user name
                EnterUserName(userName, userNameType);
                // Enter Password
                EnterPassword(password, passwordType);
                 isLoginSuccessful = DefaultUserLoginAuthentication(userName, password,
                    userTypeEnum, count, isLoginSuccessful);
            }
            Logger.LogMethodExit(base.IsTakeScreenShotDuringEntryExit);
            return isLoginSuccessful;
        }

        /// <summary>
        /// Enter User Name.
        /// </summary>
        /// <param name="userName">This is User Name.</param>
        public void EnterUserName(string userName, string userNameType)
        {
            //Enter User Name
            Logger.LogMethodEntry(base.IsTakeScreenShotDuringEntryExit);
            //Enter UserName
            WaitForElement(By.XPath(LoginPageResource.LoginPage_UserName_TextBox_Xpath));
            ClearTextByXPath(LoginPageResource.LoginPage_UserName_TextBox_Xpath);
            // Enter user name based on the user name type
            switch (userNameType)
            {
                case "Valid username":
                    FillTextBoxByXPath(LoginPageResource.LoginPage_UserName_TextBox_Xpath, userName);
                    break;

                case "Invalid username":
                    FillTextBoxByXPath(LoginPageResource.LoginPage_UserName_TextBox_Xpath, LoginPageResource.LoginPage_UserNameTextBox_InvalidUser_Value);
                    break;

                case "Blank username":
                    FillTextBoxByXPath(LoginPageResource.LoginPage_UserName_TextBox_Xpath, "");
                    break;
            }
            Logger.LogMethodExit(base.IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        /// Enter  User Password.
        /// </summary>
        /// <param name="password">This is password.</param>
        public void EnterPassword(string password, string passwordType)
        {
            //Enter User Name
            Logger.LogMethodEntry(base.IsTakeScreenShotDuringEntryExit);
            //Enter Password
            WaitForElement(By.XPath(LoginPageResource.LoginPage_Password_TextBox_Xpath));
            ClearTextByXPath(LoginPageResource.LoginPage_Password_TextBox_Xpath);

            switch (passwordType)
            {
                case "Valid password":
                    FillTextBoxByXPath(LoginPageResource.LoginPage_Password_TextBox_Xpath, password);
                    break;

                case "Invalid password":
                    FillTextBoxByXPath(LoginPageResource.LoginPage_Password_TextBox_Xpath,
                        LoginPageResource.LoginPage_Password_InvalidPassword_Text);
                    break;

                case "Blank password":
                    FillTextBoxByXPath(LoginPageResource.LoginPage_Password_TextBox_Xpath, "");
                    break;
            }
            Logger.LogMethodExit(base.IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        /// Click  User Sign In Button.
        /// </summary>
        public void ClickLogInButton()
        {
            //Click SignIn Button
            Logger.LogMethodEntry(base.IsTakeScreenShotDuringEntryExit);
            // Wait untill Click button load
            WaitForElement(By.XPath(LoginPageResource.LoginPage_Login_Button_XPath));
            IWebElement loginButton = GetWebElementPropertiesByXPath(LoginPageResource.
                LoginPage_Login_Button_XPath);
            ClickByJavaScriptExecutor(loginButton);
            Thread.Sleep(10000);
            
            Logger.LogMethodExit(base.IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        /// Authenticate Login by Default Pegasus User.
        /// </summary>
        /// <param name="userName">This is username.</param>
        /// <param name="password">This is user password.</param>
        /// <param name="userTypeEnum">This user by type enum.</param>
        /// <param name="count">This is the number of login attempt.</param>
        /// <param name="isLoginSuccessful">This is Login Successfull Status.</param>
        /// <returns>Returns true if login successfull otherwise false.</returns>
        private bool DefaultUserLoginAuthentication(String userName, String password,
            User.UserTypeEnum userTypeEnum, int count, bool isLoginSuccessful)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            //Click on Login Submit Button based on User by Type
            if (User.UserTypeEnum.EPSAdminUser01 == userTypeEnum
                || User.UserTypeEnum.EPSSuperAdminUser01 == userTypeEnum )
            {
                this.ClickLogInButton();
            }
            // is login success
            isLoginSuccessful = ValidateLogin(userTypeEnum);
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
            return isLoginSuccessful;
        }

        /// <summary>
        /// This method is used to validate the login of usertypes.
        /// </summary>
        /// <param name="userTypeEnum">This is the user by type.</param>
        /// <returns>Login Attempt Status Result.</returns>
        private Boolean ValidateLogin(User.UserTypeEnum userTypeEnum)
        {
            //Validate Login Success
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            Boolean isLoginSuccessful = false;
            switch (userTypeEnum)
            {
                //Logged in by user
                case User.UserTypeEnum.EPSSuperAdminUser01:              
                case User.UserTypeEnum.EPSAdminUser01:
                    //Validate MOL User Login
                    isLoginSuccessful = this.IsUserLoggedIn(false);
                    break;
            }
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
            return isLoginSuccessful;
        }

        /// <summary>
        /// Validate  User Login.
        /// </summary>
        /// <param name="isLoginSuccessful">This is Login Success Initialization value.</param>
        /// <returns>Login Success Status.</returns>
        private Boolean IsUserLoggedIn(Boolean isLoginSuccessful)
        {
            //Checks After LoggedIn Window Present
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);

            Thread.Sleep(10000);

            if (GetPageTitle.Contains(LoginPageResource.LoginPage_DashboardPageTitle_Title))
            {
                isLoginSuccessful = true;
            }

            Logger.LogMethodExit(base.IsTakeScreenShotDuringEntryExit);
            return isLoginSuccessful;
        }

    }
}
