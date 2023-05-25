using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectDeleteTests:TestBase
    {
        [Test]

        public void DeleteProject()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "secret",
            };

            app.Login.Login(account);
            app.Management.GoToManagementPage();
            app.Management.GoToProjectManagementPage(); 

            app.ProjectManagement.CheckProjectAnotherCreate();
            
            List<ProjectData> oldProjects = app.ProjectManagement.GetProjectList();
            ProjectData toBeRemoved = oldProjects[0];

            app.ProjectManagement.DeleteProject(0);

            List<ProjectData> newProjects = app.ProjectManagement.GetProjectList();

            Assert.AreEqual(newProjects.Count + 1, oldProjects.Count);

            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();


            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
