using EPS.Automation;
using EPS.Automation.DataTransferObjects;
using EPS.Automation.Exceptions;
using EPS.Tests.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPS.Tests.UIPages
{
    class BrowseURL : BasePage
    {

        /// <summary>
        /// The static instance of the logger for the class.
        /// </summary>
        private static readonly Logger Logger =
            Logger.GetInstance(typeof(BrowseURL));

        //Test

        /// <summary>
        /// This is the login retry count.
        /// </summary>
        private static readonly int LoginAttemptRetryCount = Convert.ToInt32(ConfigurationManager.
            AppSettings[BrowseURLResource.Login_Page_AppSetting_RetryCount_Key]);

        /// <summary>
        /// Get Wait Limit Time From Config.
        /// </summary>
        private readonly int _getWaitTimeOut = Convert.ToInt32(
            ConfigurationManager.AppSettings[BrowseURLResource.ElementFindTimeOutInSeconds_Key]);

        /// <summary>
        /// Initialize base MMSG login Url.
        /// </summary>
        private readonly string _baseLoginUrl = null;

        /// <summary>
        ///  Get Title of the Page.
        /// </summary>
        public new string GetPageTitle()
        {
            //Get Page Title
            { return base.GetPageTitle; }
        }

        // Property of URL
        public string GetURL
        {
            get { return this._baseLoginUrl; }
        }

        /// <summary>
        /// Gets the base login Url from configuration.
        /// </summary>
        /// <param name="userTypeEnum">This is User by Type Enum.</param>
        public BrowseURL(User.UserTypeEnum userTypeEnum)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            try
            {
                switch (userTypeEnum)
                {
                    // Get URL of EPS
                    case User.UserTypeEnum.EPSAdminUser01:
                    case User.UserTypeEnum.EPSSuperAdminUser01:
                    

                        _baseLoginUrl = string.Format(URLConfigurationManager.EPSUserApplicationUrlRoot);
                        // Delete all browser cookies
                        DeleteAllBrowserCookies();
                        break;

                   
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
            finally
            {
                // Write value to extent report
                new CustomReport().AddStatusToReport(CustomReport.ReportStatus.Info,
                    string.Format("Application URL: {0}", _baseLoginUrl));
            }
            Logger.LogMethodExit(IsTakeScreenShotDuringEntryExit);
        }

        /// <summary>
        ///  Navigates from base Url in browser through WebDriver.
        /// </summary>
        public void GoToLoginUrl(User.UserTypeEnum userTypeEnum)
        {
            Logger.LogMethodEntry(IsTakeScreenShotDuringEntryExit);
            try
            {
                //Get Url Successfully Browsed
                if (!IsUrlBrowsedSuccessful(userTypeEnum))
                {
                    //Open Url in Browser
                    DeleteAllBrowserCookies();
                    NavigateToBrowseUrl(this._baseLoginUrl);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, false);
            }
            finally
            {
                FinallyMessageForURLAccessFailure();
            }
        }

        /// <summary>
        /// Conditional check andthrow message if MOL url launch has failed
        /// </summary>
        private void FinallyMessageForURLAccessFailure()
        {
            string title = base.GetPageTitle;
            if (title != BrowseURLResource.ApplicationLoginPage_Title &&
                !IsElementPresent(By.XPath(LoginPageResource.LoginPage_Login_Button_XPath), 10))
            {
                WebDriverSingleton.GetInstance().Dispose();
                throw new GenericException(string.Format
                       (BrowseURLResource.Login_FinallyMessageForURLAccessFailure_Text, Environment.UserName));
            }
        }

        /// <summary>
        /// Check thr Url Browsed Successfully.
        /// </summary>
        /// <returns>True if Url browsed successfully else false.</returns>
        /// <remarks>Slow web page loading or page not found then this
        /// method open the Url in the address bar and wait till specified time
        ///  to page get successfully browse.</remarks>
        public Boolean IsUrlBrowsedSuccessful(User.UserTypeEnum userTypeEnum)
        {
            //Start Stop Watch
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            bool isUrlBrowsedSuccessful = false;

            if (User.UserTypeEnum.EPSAdminUser01 == userTypeEnum||
                User.UserTypeEnum.EPSSuperAdminUser01 == userTypeEnum
                )
            {
                // Check if URL is browsed sucessfully
                isUrlBrowsedSuccessful = this.IsApplicationUrlBrowsedSuccessful();
            }
            return isUrlBrowsedSuccessful;
        }

        /// <summary>
        /// Get MOL URL browse status
        /// </summary>
        /// <returns>URl access status</returns>
        private bool IsApplicationUrlBrowsedSuccessful()
        {
            bool status = false;
            //Get Image Present On The Page
            String getCurrentPageTitle = (base.GetPageTitle).ToString();
            if (!GetCurrentUrl.Contains(_baseLoginUrl))
            { //Navigate Base Url
                NavigateToBrowseUrl(this._baseLoginUrl);
                Thread.Sleep(2000);
                WaitUntilWindowLoads(base.GetPageTitle);
            }

            if (!getCurrentPageTitle.Equals(BrowseURLResource.ApplicationLoginPage_Title) &&
                !IsElementPresent(By.XPath(LoginPageResource.LoginPage_Login_Button_XPath), 10))
            {
                //Navigate Base Url
                NavigateToBrowseUrl(this._baseLoginUrl);
                Thread.Sleep(2000);
                WaitUntilWindowLoads(base.GetPageTitle);
                getCurrentPageTitle = (base.GetPageTitle).ToString();
                if (getCurrentPageTitle.Equals(BrowseURLResource.ApplicationLoginPage_Title) &&
                IsElementPresent(By.XPath(LoginPageResource.LoginPage_Login_Button_XPath), 10))
                {
                    status = true;
                }
            }
            else
            {
                if (getCurrentPageTitle.Equals(BrowseURLResource.ApplicationLoginPage_Title))
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }
    }
}
