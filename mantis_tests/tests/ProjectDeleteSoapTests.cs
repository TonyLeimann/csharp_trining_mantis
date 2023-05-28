using mantis_tests.Mantis;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectDeleteSoapTests:TestBase
    {
        [Test]

        public void ProjectDeleteSoapTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "secret",
            };

            app.API.CheckProjectAnotherCreate(account);

            List<ProjectData> oldProjects = app.API.GetProjectList(account);
            ProjectData toBeRemoved = oldProjects[0];

            app.API.DeleteProject(account,toBeRemoved);

            List<ProjectData> newProjects = app.API.GetProjectList(account);

            Assert.AreEqual(newProjects.Count + 1, oldProjects.Count);

            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();


            Assert.AreEqual(oldProjects, newProjects);

        }

    }
}
