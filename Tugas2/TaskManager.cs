/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugas2;
using System;
using System.Collections.Generic;
using System.Linq;

class TaskManager
{
    private List<Task> tasks;
    private int nextId = 1;

    public TaskManager()
    {
        tasks = new List<Task>();
    }

    public void AddTask(Task task)
    {
        task.Id = nextId++;
        tasks.Add(task);
    }

    public void EditTask(int taskId, string newTitle, string newDescription)
    {
        Task task = tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            task.Title = newTitle;
            task.Description = newDescription;
        }
    }

    public void DeleteTask(int taskId)
    {
        Task task = tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            tasks.Remove(task);
        }
    }

    public List<Task> GetTasks()
    {
        return tasks;
    }
}
*/