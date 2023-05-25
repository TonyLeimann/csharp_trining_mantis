using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalisticTelnet;

namespace mantis_tests
{
    public class JamesHelper:HelperBase
    {
        public JamesHelper(ApplicationManager manager) : base(manager)
        {

        }

        public void Add(AccountData account)
        {
            if (Verify(account))
            {
                return;
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("adduser " + account.Name + " " + account.Password);
            Console.WriteLine(telnet.Read());
        }
        public void Remove(AccountData account)
        {
            if (!Verify(account))
            {
                return;
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("deluser " + account.Name);
            Console.WriteLine(telnet.Read());
        }


        public bool Verify(AccountData account) 
        {
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("verify " + account.Name);
            string info = telnet.Read();
            Console.WriteLine(info);
            return ! info.Contains("does not exist");
        }


        private TelnetConnection LoginToJames()
        {
            TelnetConnection telnet = new TelnetConnection("localhost", 4555);
            Console.WriteLine(telnet.Read());// считываем то, что написал James ("Ввести логин")
            telnet.WriteLine("root");
            Console.WriteLine(telnet.Read());// считываем то, что написал James ("Ввести password")
            telnet.WriteLine("root");
            Console.WriteLine(telnet.Read());// считываем то, что написал James 
            return telnet;
        }
    }
}
