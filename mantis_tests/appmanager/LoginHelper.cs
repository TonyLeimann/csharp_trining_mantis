using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class LoginHelper:HelperBase
    {
       

        public LoginHelper(ApplicationManager manager) : base(manager) { }

        public void Login(AccountData account)
        {

            if (IsLoggedIn())// метод где мы уже залогинились
            {
                if (IsLoggedIn(account))// проверяем залогинены ли мы под нужным пользователем
                {
                    return;
                }
                Logout();// если не та учетная, то разлогин

            }

            Type(By.Name("username"),account.Name);
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        private bool IsLoggedIn(AccountData account)
        {
            bool result = IsLoggedIn() && GetLoggetUserName() == account.Name;
            return result;
        }

        private void Logout()
        {
            driver.FindElement(By.CssSelector("span.user-info")).Click();
            driver.FindElement(By.CssSelector("a[href='/mantisbt-2.25.7/logout_page.php']")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("a[href='/mantisbt-2.25.7/logout_page.php'"));
        }

        public string GetLoggetUserName()
        {
            string user = driver.FindElement(By.CssSelector("span.user-info")).Text;

            return user.Trim();
        }

       

    }
}
