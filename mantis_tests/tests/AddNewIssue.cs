using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests

{
    [TestFixture]
    public class AddNewIssue:TestBase
    {
        [Test]

        public void AddNewIssues()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "secret"
            };

            ProjectData project = new ProjectData()
            {
                Id = app.API.ProjectIdReturn(account)//универсальный метод по поиску ID, так как может и не быть проекта с id=1
            };



            IssueData issueData = new IssueData()
             {
                 Summary = "Some short text",
                 Description = "Some long text",
                 Category = "General"

             };
          
            app.API.CreateNewIssue(account,project,issueData);
        }
    }
}
