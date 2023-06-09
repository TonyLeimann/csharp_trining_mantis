﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        private string baseUrl;

        public RegistrationHelper Registration { get;  set; }
        public FtpHelper Ftp { get;  set; }
        public JamesHelper James { get;  set; }
        public MailHelper Mail { get; set; }
        public LoginHelper Login { get;  set; }
        public ProjectManagementHelper ProjectManagement { get;  set; }
        public ManagementMenuHelper Management { get;  set; }
        public AdminHelper Admin { get; set; }
        public APIHelper API { get; set; }

        private static ThreadLocal <  ApplicationManager > app = new ThreadLocal<ApplicationManager>();

        public IWebDriver Driver { get { return driver; } } 


        public ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseUrl = "http://localhost/mantisbt-2.25.7";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            Login = new LoginHelper(this);
            ProjectManagement = new ProjectManagementHelper(this);
            Management = new ManagementMenuHelper(this);
            Admin = new AdminHelper(this,baseUrl);
            API = new APIHelper(this);
        }

         ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {

            }
        }

        public static ApplicationManager GetInstance()// global
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseUrl + "/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }

    }
}
