using BeConsistent.Models;
using Newtonsoft.Json;

namespace BeConsistent;

public partial class App : Application
{
	public static List<TaskModel> tasks = new List<TaskModel>();
    public static string filePath = Path.Combine(FileSystem.AppDataDirectory, "tasks.json");

    public App()
	{
		InitializeComponent();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            tasks = JsonConvert.DeserializeObject<List<TaskModel>>(json);
        }

        MainPage = new AppShell();
	}
}
