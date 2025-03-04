using System;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManagerApp app = TaskManagerApp.LoadData();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Task Manager Application");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Project");
                Console.WriteLine("3. Assign User to Project");
                Console.WriteLine("4. Create Task");
                Console.WriteLine("5. List Tasks");
                Console.WriteLine("6. Add Comment to Task");
                Console.WriteLine("7. Change Task Status");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateUser(app);
                        break;
                    case "2":
                        CreateProject(app);
                        break;
                    case "3":
                        AssignUserToProject(app);
                        break;
                    case "4":
                        CreateTask(app);
                        break;
                    case "5":
                        ListTasks(app);
                        break;
                    case "6":
                        AddCommentToTask(app);
                        break;
                    case "7":
                        ChangeTaskStatus(app);
                        break;
                    case "8":
                        exit = true;
                        app.SaveData();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateUser(TaskManagerApp app)
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Select role: 1. SiteAdmin, 2. ProjectManager, 3. Developer, 4. QAAnalyst");
            int roleOption = int.Parse(Console.ReadLine());
            UserRole role = (UserRole)(roleOption - 1);
            app.CreateUser(name, role);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void CreateProject(TaskManagerApp app)
        {
            Console.Write("Enter project name: ");
            string projectName = Console.ReadLine();
            Console.Write("Enter Project Manager ID: ");
            int projectManagerId = int.Parse(Console.ReadLine());
            app.CreateProject(projectName, projectManagerId);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void AssignUserToProject(TaskManagerApp app)
        {
            Console.Write("Enter project ID: ");
            int projectId = int.Parse(Console.ReadLine());
            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            app.AssignUserToProject(projectId, userId);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void CreateTask(TaskManagerApp app)
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();
            Console.Write("Enter project ID: ");
            int projectId = int.Parse(Console.ReadLine());
            Console.Write("Enter developer ID: ");
            int developerId = int.Parse(Console.ReadLine());
            app.CreateTask(title, description, projectId, developerId);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ListTasks(TaskManagerApp app)
        {
            app.ListTasks();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void AddCommentToTask(TaskManagerApp app)
        {
            Console.Write("Enter task ID: ");
            int taskId = int.Parse(Console.ReadLine());
            Console.Write("Enter comment: ");
            string comment = Console.ReadLine();
            app.AddCommentToTask(taskId, comment);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ChangeTaskStatus(TaskManagerApp app)
        {
            Console.Write("Enter task ID: ");
            int taskId = int.Parse(Console.ReadLine());
            Console.WriteLine("Select status: 1. Open, 2. Development, 3. QA, 4. Closed");
            int statusOption = int.Parse(Console.ReadLine());
            TaskStatus status = (TaskStatus)(statusOption - 1);
            app.ChangeTaskStatus(taskId, status);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
