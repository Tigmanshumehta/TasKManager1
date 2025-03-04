using System;
using System.Collections.Generic;

namespace TaskManager
{
    public enum UserRole
    {
        SiteAdmin,
        ProjectManager,
        Developer,
        QAAnalyst
    }

    public enum TaskStatus
    {
        Open,
        Development,
        QA,
        Closed
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User ProjectManager { get; set; }
        public List<User> Developers { get; set; } = new List<User>();
        public List<User> QAAnalysts { get; set; } = new List<User>();
    }

    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public User AssignedDeveloper { get; set; }
        public List<string> Comments { get; set; } = new List<string>();
    }
}
