using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace TaskManager
{
    [Serializable]
    public class TaskManagerApp
    {
        private List<User> users = new List<User>();
        private List<Project> projects = new List<Project>();
        private List<Task> tasks = new List<Task>();

        private const string DataFilePath = "C:\\Users\\Asus\\OneDrive\\Desktop\\Storage\\TigmDB\\Text.txt";

        public void CreateUser(string name, UserRole role)
        {
            int newId = users.Count + 1;
            users.Add(new User { Id = newId, Name = name, Role = role });
            Console.WriteLine($"User {name} created with role {role}");
        }

        public void CreateProject(string projectName, int projectManagerId)
        {
            User projectManager = users.FirstOrDefault(u => u.Id == projectManagerId && u.Role == UserRole.ProjectManager);
            if (projectManager != null)
            {
                int newId = projects.Count + 1;
                projects.Add(new Project { Id = newId, Name = projectName, ProjectManager = projectManager });
                Console.WriteLine($"Project {projectName} created and assigned to {projectManager.Name}");
            }
            else
            {
                Console.WriteLine("Invalid Project Manager ID");
            }
        }

        public void AssignUserToProject(int projectId, int userId)
        {
            Project project = projects.FirstOrDefault(p => p.Id == projectId);
            User user = users.FirstOrDefault(u => u.Id == userId);

            if (project != null && user != null)
            {
                if (user.Role == UserRole.Developer)
                {
                    project.Developers.Add(user);
                    Console.WriteLine($"Developer {user.Name} assigned to project {project.Name}");
                }
                else if (user.Role == UserRole.QAAnalyst)
                {
                    project.QAAnalysts.Add(user);
                    Console.WriteLine($"QA Analyst {user.Name} assigned to project {project.Name}");
                }
                else
                {
                    Console.WriteLine("Only Developers and QA Analysts can be assigned to projects");
                }
            }
            else
            {
                Console.WriteLine("Invalid Project ID or User ID");
            }
        }

        public void CreateTask(string title, string description, int projectId, int developerId)
        {
            Project project = projects.FirstOrDefault(p => p.Id == projectId);
            User developer = users.FirstOrDefault(u => u.Id == developerId && u.Role == UserRole.Developer);

            if (project != null && developer != null)
            {
                int newId = tasks.Count + 1;
                tasks.Add(new Task { Id = newId, Title = title, Description = description, Status = TaskStatus.Open, AssignedDeveloper = developer });
                Console.WriteLine($"Task {title} created and assigned to {developer.Name}");
            }
            else
            {
                Console.WriteLine("Invalid Project ID or Developer ID");
            }
        }

        public void ListTasks()
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task ID: {task.Id}, Title: {task.Title}, Status: {task.Status}, Assigned Developer: {task.AssignedDeveloper?.Name}");
            }
        }

        public void AddCommentToTask(int taskId, string comment)
        {
            Task task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.Comments.Add(comment);
                Console.WriteLine($"Comment added to task {task.Title}");
            }
            else
            {
                Console.WriteLine("Invalid Task ID");
            }
        }

        public void ChangeTaskStatus(int taskId, TaskStatus status)
        {
            Task task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.Status = status;
                Console.WriteLine($"Task {task.Title} status changed to {status}");
            }
            else
            {
                Console.WriteLine("Invalid Task ID");
            }
        }

        public void SaveData()
        {
            using (FileStream fs = new FileStream(DataFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }
        }

        public static TaskManagerApp LoadData()
        {
            if (File.Exists(DataFilePath))
            {
                using (FileStream fs = new FileStream(DataFilePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (TaskManagerApp)formatter.Deserialize(fs);
                }
            }
            return new TaskManagerApp();
        }
    }
}
