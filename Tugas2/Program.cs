using System;
using System.Collections.Generic;
using System.Linq;

namespace Tugas2;
class Program
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();
        UserInterface userInterface = new UserInterface(taskManager);

        while (true)
        {
            Console.Clear();
            userInterface.DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    userInterface.AddTask();
                    break;
                case "2":
                    userInterface.EditTask();
                    break;
                case "3":
                    userInterface.DeleteTask();
                    break;
                case "4":
                    userInterface.DisplayTasks();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid. Tekan Enter untuk melanjutkan.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}

class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

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

class UserInterface
{
    private TaskManager taskManager;

    public UserInterface(TaskManager manager)
    {
        taskManager = manager;
    }

    public void DisplayMenu()
    {
        Console.WriteLine("==== Aplikasi Manajemen Tugas ====");
        Console.WriteLine("1. Tambah Tugas");
        Console.WriteLine("2. Edit Tugas");
        Console.WriteLine("3. Hapus Tugas");
        Console.WriteLine("4. Tampilkan Tugas");
        Console.WriteLine("5. Keluar");
        Console.Write("Pilihan Anda: ");
    }

    public void AddTask()
    {
        Console.Clear();
        Console.Write("Judul Tugas: ");
        string title = Console.ReadLine();
        Console.Write("Deskripsi Tugas: ");
        string description = Console.ReadLine();

        Task task = new Task { Title = title, Description = description };
        taskManager.AddTask(task);

        Console.WriteLine("Tugas berhasil ditambahkan. Tekan Enter untuk melanjutkan.");
        Console.ReadLine();
    }

    public void EditTask()
    {
        Console.Clear();
        Console.Write("Masukkan ID Tugas yang akan diedit: ");
        if (int.TryParse(Console.ReadLine(), out int taskId))
        {
            Task task = taskManager.GetTasks().FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                Console.Write("Judul Baru: ");
                string newTitle = Console.ReadLine();
                Console.Write("Deskripsi Baru: ");
                string newDescription = Console.ReadLine();

                taskManager.EditTask(taskId, newTitle, newDescription);
                Console.WriteLine("Tugas berhasil diedit. Tekan Enter untuk melanjutkan.");
            }
            else
            {
                Console.WriteLine("Tugas tidak ditemukan. Tekan Enter untuk melanjutkan.");
            }
        }
        else
        {
            Console.WriteLine("ID Tugas tidak valid. Tekan Enter untuk melanjutkan.");
        }
        Console.ReadLine();
    }

    public void DeleteTask()
    {
        Console.Clear();
        Console.Write("Masukkan ID Tugas yang akan dihapus: ");
        if (int.TryParse(Console.ReadLine(), out int taskId))
        {
            Task task = taskManager.GetTasks().FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                taskManager.DeleteTask(taskId);
                Console.WriteLine("Tugas berhasil dihapus. Tekan Enter untuk melanjutkan.");
            }
            else
            {
                Console.WriteLine("Tugas tidak ditemukan. Tekan Enter untuk melanjutkan.");
            }
        }
        else
        {
            Console.WriteLine("ID Tugas tidak valid. Tekan Enter untuk melanjutkan.");
        }
        Console.ReadLine();
    }

    public void DisplayTasks()
    {
        Console.Clear();
        List<Task> tasks = taskManager.GetTasks();
        Console.WriteLine("==== Daftar Tugas ====");
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Judul: {task.Title}, Deskripsi: {task.Description}");
        }
        Console.WriteLine("Tekan Enter untuk melanjutkan.");
        Console.ReadLine();
    }
}
