namespace ToDoListSqlite.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool isComplete { get; set; }

    }
}
