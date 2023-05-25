using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests: TestBase
    {
        [Test]

        public void ProjectCreationTest()
        {
            AccountData accountData = new AccountData()
            {
                Name = "administrator",
                Password = "secret",
            };

            ProjectData projectName = new ProjectData()
            {
                NameProject = GenerateRandomsString(5)
            };

            app.Login.Login(accountData);
            app.Management.GoToManagementPage();
            app.Management.GoToProjectManagementPage();

            List<ProjectData> oldProjects = app.ProjectManagement.GetProjectList();


            app.ProjectManagement.CreateProject(projectName);

            List<ProjectData> newprojects = app.ProjectManagement.GetProjectList();

            Assert.AreEqual(oldProjects.Count + 1, newprojects.Count);

            oldProjects.Add(projectName);
            oldProjects.Sort();
            newprojects.Sort();

            Assert.AreEqual(oldProjects, newprojects);


        }


    }
}
