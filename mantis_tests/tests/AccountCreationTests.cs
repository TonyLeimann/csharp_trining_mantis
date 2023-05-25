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
                Name = "testuser_",
                Password = "password",
                Email ="testuser_@localhost.localdomain"
                
            };
           
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
