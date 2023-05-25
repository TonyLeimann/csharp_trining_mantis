using OpenQA.Selenium;
using System;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class RegistrationHelper:HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void RegisterAccount(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            String url = GetConfirmationUrl(account);
            FillPasswordForm(url,account);
            SubmitPasswordForm();

        }

        private string GetConfirmationUrl(AccountData account)
        {
            String message = manager.Mail.GetLastMail(account);// получили письмо
            Match match =  Regex.Match(message, @"http://\S*");    // извлечения текста
            return match.Value;//возвращаем фрагмент, который подошел под регулярное значение
        }

        private void FillPasswordForm(string url,AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.XPath("//input[@id='realname']")).Click();
            driver.FindElement(By.XPath("//input[@id='realname']")).Clear();
            driver.FindElement(By.XPath("//input[@id='realname']")).SendKeys(account.Name);
            driver.FindElement(By.XPath("//input[@id='password']")).Click();
            driver.FindElement(By.XPath("//input[@id='password']")).Clear();
            driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@id='password-confirm']")).Click();
            driver.FindElement(By.XPath("//input[@id='password-confirm']")).Clear();
            driver.FindElement(By.XPath("//input[@id='password-confirm']")).SendKeys(account.Password);

        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void OpenRegistrationForm()
        {
            driver.FindElement(By.CssSelector("a[href ='signup_page.php']")).Click();
            
        }
        private void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).Click();
            driver.FindElement(By.Name("username")).Clear();
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(account.Email);

        }

        private void SubmitRegistration()
        {
            //driver.FindElement(By.TagName("input")).FindElement(By.LinkText("Зарегистрироваться")).Click();
            driver.FindElement(By.XPath("//input [@value = 'Зарегистрироваться']")).Click();
        }

        private void OpenMainPage()
        {
            if(driver.Url == "http://localhost/mantisbt-2.25.7/login_page.php")
            {
                return;
            }
            manager.Driver.Url = "http://localhost/mantisbt-2.25.7/login_page.php";
        }
    }

}
