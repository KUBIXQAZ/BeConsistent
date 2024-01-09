using BeConsistent.Models;

namespace BeConsistent;

public partial class App : Application
{
	public static List<TaskModel> tasks = new List<TaskModel>();

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
