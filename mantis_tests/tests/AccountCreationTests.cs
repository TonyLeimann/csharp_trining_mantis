using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mantis_tests
{

    [TestFixture]
    public class AccountCreationTests:TestBase
    {
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
           
        }
        
        [Test]
        public void TestAccountRegistration()
        {

            AccountData account = new AccountData()
            {
                Name = "testuser_95",
                Password = "password",
                Email = "testuser_95@localhost.localdomain"

            };

            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount =  accounts.Find(name => name.Name == account.Name);
            if(existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }
           

            app.James.Remove(account);
            app.James.Add(account);


            app.Registration.RegisterAccount(account);// помощник по созданию
        }


        [OneTimeTearDown] 
        public void restoreConfig() 
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }

}
