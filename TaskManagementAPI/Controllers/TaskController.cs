using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<TaskItem> _taskRepo;

        // Constructor for dependency injection
        public TaskController(IRepository<TaskItem> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        // GET: api/task (Admin only)
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            // Simulated role check: assume middleware sets "UserRole"
            var role = HttpContext.Items["UserRole"]?.ToString();
            if (role != "Admin")
                return Forbid("Only Admin can view all tasks.");

            var tasks = await _taskRepo.GetAll();
            return Ok(tasks);
        }

        // GET: api/task/{id} (Admin or assigned user)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var role = HttpContext.Items["UserRole"]?.ToString();
            var userId = GetCurrentUserId();

            var task = await _taskRepo.GetById(id);
            if (task == null)
                return NotFound("Task not found.");

            if (role != "Admin" && task.AssignedToUserId != userId)
                return Forbid("You can only view tasks assigned to you.");

            return Ok(task);
        }

        // POST: api/task (Admin only)
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            var role = HttpContext.Items["UserRole"]?.ToString();
            if (role != "Admin")
                return Forbid("Only Admin can create tasks.");

            await _taskRepo.Add(task);
            return Ok(task);
        }

        // PUT: api/task/{id} (Admin can edit everything; Users can update only status)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var role = HttpContext.Items["UserRole"]?.ToString();
            var userId = GetCurrentUserId();

            var task = await _taskRepo.GetById(id);
            if (task == null)
                return NotFound("Task not found.");

            if (role != "Admin" && task.AssignedToUserId != userId)
                return Forbid("You can only update your assigned tasks.");

            // Admin can update all fields; User can update only status
            if (role != "Admin")
            {
                task.Status = updatedTask.Status;
            }
            else
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.Status = updatedTask.Status;
                task.AssignedToUserId = updatedTask.AssignedToUserId;
            }

            await _taskRepo.Update(task);
            return Ok(task);
        }

        // DELETE: api/task/{id} (Admin only)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var role = HttpContext.Items["UserRole"]?.ToString();
            if (role != "Admin")
                return Forbid("Only Admin can delete tasks.");

            var task = await _taskRepo.GetById(id);
            if (task == null)
                return NotFound("Task not found.");

            await _taskRepo.Delete(id);
            return Ok("Task deleted successfully.");
        }

        // Helper method to simulate retrieving the current user's ID
        private int GetCurrentUserId()
        {
            return int.TryParse(HttpContext.Items["UserId"]?.ToString(), out var userId) ? userId : 0;
        }
    }
}
