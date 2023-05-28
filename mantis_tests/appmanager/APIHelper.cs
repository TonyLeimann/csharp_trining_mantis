using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper:HelperBase
    {
        

        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account,ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();// объект для обращения к операциям
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category =issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name,account.Password,issue);//нужный метод
        }

        public List<ProjectData> GetProjectList(AccountData account)
        {

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Name, account.Password);
            List < ProjectData > projectList = new List<ProjectData >();
            ProjectData project = new ProjectData();
        
            for (int i = 0; i < projects.Length; i++)
            {
               
                project.NameProject = projects[i].name;
                project.Id = projects[i].id;
                projectList.Add(new ProjectData { NameProject = projects[i].name,Id = projects[i].id });

            }


            return projectList;
            
        }

        public void CreateProject(AccountData account,ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project1 = new Mantis.ProjectData();

            project1.name = project.NameProject;
            client.mc_project_add(account.Name, account.Password, project1);

        }

        public void DeleteProject(AccountData account,ProjectData toBeRemoved)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            client.mc_project_delete(account.Name, account.Password, toBeRemoved.Id);




        }

        public void CheckProjectAnotherCreate(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Name, account.Password);
             
            if (projects.Length == 0) 
            {
                ProjectData project = new ProjectData()
                {
                    NameProject = "New"
                };
                CreateProject(account,project);
            }

            

        }

        public string ProjectIdReturn(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Name, account.Password);

            if (projects.Length == 0)
            {
                ProjectData project = new ProjectData()
                {
                    NameProject = "New"
                };
                CreateProject(account, project);
            }

            Mantis.ProjectData[] projects1 = client.mc_projects_get_user_accessible(account.Name, account.Password);
                           

            return projects1[0].id;

        }
    }
}
