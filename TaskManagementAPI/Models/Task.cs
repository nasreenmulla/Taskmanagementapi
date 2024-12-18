namespace TaskManagementAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Completed"
        public int AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; }
    }
}
