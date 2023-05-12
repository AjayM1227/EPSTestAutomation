using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Tests.Utilities
{
    class URLConfigurationManager
    {
        /// <summary>
        /// Property URL root.
        /// </summary>
        public static string EPSUserApplicationUrlRoot
        {
            get { return GetEPSUserApplicationUrlRoot(); }
        }

        /// <summary>
        /// Find URL root based on application environment.
        /// </summary>
        /// <returns>Application cs url.</returns>
        public static string GetEPSUserApplicationUrlRoot()
        {
            string applicationUrl = String.Empty;
            string appConfigApplicationURL = String.Empty;
            string getApplicationUrl = ConfigurationManager.AppSettings["EPSUserApplicationURL"];
            // Get environment value from app config file
            string getEnvironment = ConfigurationManager.AppSettings["Environment"];
            if (getApplicationUrl != String.Empty)
            {
                appConfigApplicationURL = ConfigurationManager.AppSettings["EPSUserApplicationURL"];
                applicationUrl = string.Format(appConfigApplicationURL, getEnvironment.ToLower());
            }
           
            else
            {
                throw new ArgumentException("The suggested application environment was not found");
            }

            return applicationUrl;
        }
    }
}
