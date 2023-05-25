using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectManagementHelper:HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }

        


        public void CreateProject(ProjectData projectName)
        {
            CreateNewProject();
            EnterName(projectName);
            AddProject();
        }

        public void DeleteProject(int line)
        {
            SelectProject(line);
            SelectDeleteProject();
            SubmitDelete();
        }


        public void CreateNewProject()
        {
            driver.FindElement(By.XPath("//button[text()='Создать новый проект']")).Click();
        }

        public void EnterName(ProjectData projectName)
        {
            driver.FindElement(By.XPath("//input[@id='project-name']")).Click();
            driver.FindElement(By.XPath("//input[@id='project-name']")).Clear();
            driver.FindElement(By.XPath("//input[@id='project-name']")).SendKeys(projectName.NameProject);
        }

        public void AddProject()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
            System.Threading.Thread.Sleep(2000);
        }

        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();

            ICollection<IWebElement> webProjects = driver.FindElements(By.XPath("//a[contains(@href,'manage_proj_edit_page')]"));

            for (int i = 0; i < webProjects.Count; i++)
            {
                string nameOfProject = driver.FindElement(By.XPath("//tbody/tr[" + (i + 1) + "]/td/a[contains(@href,'manage_proj_edit_page')]")).Text;
                projects.Add(new ProjectData { NameProject = nameOfProject });
            }

            return projects;
        }

        public void SelectProject(int line)
        {
            driver.FindElement(By.XPath("//tbody/tr[" + (line + 1) + "]/td/a[contains(@href,'manage_proj_edit_page')]")).Click();
        }

        public void SelectDeleteProject()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public void SubmitDelete()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }


        public void CheckProjectAnotherCreate()
        {
            if (IsElementPresent(By.XPath("//a[contains(@href,'manage_proj_edit_page')]")))
            {
                return;
            }

            ProjectData project = new ProjectData()
            {
              NameProject = "new"
            };

            CreateProject(project);
        }



    }
}
