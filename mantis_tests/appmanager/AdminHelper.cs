using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper:HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, string baseUrl) : base(manager) 
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();

            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.XPath("//table/tbody/tr/td/a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match match = Regex.Match(href, @"\d+$");
                string id = match.Value;

                accounts.Add(new AccountData()
                {
                    Id = id,
                    Name = name

                });
            }


            return accounts;

        }

        public void DeleteAccount(AccountData account) 
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElements(By.CssSelector("input[type='submit']"))[2].Click();
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();//инициализация SimpleBrowser
            driver.Url = baseUrl + "/login_page.php";
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            driver.FindElement(By.Name("password")).SendKeys("secret");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            return driver;
        }
    }
}
