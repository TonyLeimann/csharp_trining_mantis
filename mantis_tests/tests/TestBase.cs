using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mantis_tests
{
    public class TestBase
    {
        public static bool NEED_LONG_UI_CHECK = true;
        protected ApplicationManager app;
        

        [OneTimeSetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
            
        }
        public static Random rand = new Random();
        public static string GenerateRandomsString(int max)
        {
           int number = Convert.ToInt32(rand.NextDouble() * max);
           StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < number; i++)
            {
              stringBuilder.Append(Convert.ToChar(97 + Convert.ToInt32(rand.NextDouble() * 25)));
            }
            return stringBuilder.ToString();
        }

    }
}
