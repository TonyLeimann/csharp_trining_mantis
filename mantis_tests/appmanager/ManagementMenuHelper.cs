using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ManagementMenuHelper:HelperBase
    {
        public ManagementMenuHelper(ApplicationManager manager) : base(manager) { }

        public void GoToManagementPage()
        {
            if(driver.Url == "http://localhost/mantisbt-2.25.7/manage_overview_page.php")
            {
                return;
            }
            driver.FindElement(By.CssSelector("a[href ='/mantisbt-2.25.7/manage_overview_page.php']")).Click();
        }

        public void GoToProjectManagementPage()
        {
            if(driver.Url == "http://localhost/mantisbt-2.25.7/manage_proj_page.php")
            { 
                return; 
            }
            driver.FindElement(By.CssSelector("a[href ='/mantisbt-2.25.7/manage_proj_page.php']")).Click();
        }


    }
}
