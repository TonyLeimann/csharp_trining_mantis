using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationSoapTests:TestBase
    {
        [Test]

        public void ProjectCreationSoapTest()
        {
            AccountData accountData = new AccountData()
            {
                Name = "administrator",
                Password = "secret"
            };
            ProjectData projectName = new ProjectData()
            {
                NameProject = GenerateRandomsString(10)
            };


            List<ProjectData> oldProjects = app.API.GetProjectList(accountData);

            app.API.CreateProject(accountData, projectName);

            List<ProjectData> newProjects = app.API.GetProjectList(accountData);

            oldProjects.Add(projectName);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);  


        }
    }
}
