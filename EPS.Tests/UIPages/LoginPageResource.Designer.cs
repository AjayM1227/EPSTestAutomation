﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPS.Tests.UIPages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LoginPageResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LoginPageResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EPS.Tests.UIPages.LoginPageResource", typeof(LoginPageResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Retry_Count.
        /// </summary>
        internal static string Login_Page_AppSetting_RetryCount_Key {
            get {
                return ResourceManager.GetString("Login_Page_AppSetting_RetryCount_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WaitTime.
        /// </summary>
        internal static string Login_Page_AppSetting_WaitTime_Key {
            get {
                return ResourceManager.GetString("Login_Page_AppSetting_WaitTime_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to login using username &quot;{0}&quot;.
        /// </summary>
        internal static string Login_Page_DefaultUserLoginAuthentication_ExceptionMessage_Text {
            get {
                return ResourceManager.GetString("Login_Page_DefaultUserLoginAuthentication_ExceptionMessage_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Home.
        /// </summary>
        internal static string LoginPage_DashboardPageTitle_Title {
            get {
                return ResourceManager.GetString("LoginPage_DashboardPageTitle_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //div/h1/span.
        /// </summary>
        internal static string LoginPage_GetLoginPageTitle_Xpath {
            get {
                return ResourceManager.GetString("LoginPage_GetLoginPageTitle_Xpath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //button[.=&apos;Login&apos;].
        /// </summary>
        internal static string LoginPage_Login_Button_XPath {
            get {
                return ResourceManager.GetString("LoginPage_Login_Button_XPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Password.
        /// </summary>
        internal static string LoginPage_Password_InvalidPassword_Text {
            get {
                return ResourceManager.GetString("LoginPage_Password_InvalidPassword_Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //span/input[@id=&apos;Input_PasswordVal&apos;].
        /// </summary>
        internal static string LoginPage_Password_TextBox_Xpath {
            get {
                return ResourceManager.GetString("LoginPage_Password_TextBox_Xpath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //span/input[@id=&apos;Input_UsernameVal&apos;].
        /// </summary>
        internal static string LoginPage_UserName_TextBox_Xpath {
            get {
                return ResourceManager.GetString("LoginPage_UserName_TextBox_Xpath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid User.
        /// </summary>
        internal static string LoginPage_UserNameTextBox_InvalidUser_Value {
            get {
                return ResourceManager.GetString("LoginPage_UserNameTextBox_InvalidUser_Value", resourceCulture);
            }
        }
    }
}
