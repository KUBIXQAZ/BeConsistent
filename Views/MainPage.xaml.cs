using BeConsistent.Models;
using CommunityToolkit.Maui.Views;

namespace BeConsistent.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		foreach(TaskModel task in App.tasks)
		{
			taskStackLayout.Add(TaskModel.createGUI(task.title, task.startDate, task));
		}
	}

    private async void AddTask(object sender, EventArgs args)
	{
        AddTaskPopup addTaskPopup = new AddTaskPopup();
		var answer = await this.ShowPopupAsync(addTaskPopup);

		if ((bool)answer == true)
		{
			DateTime currDate = DateTime.Now;

			StackLayout stackLayout = TaskModel.createGUIAndTask(addTaskPopup.title, currDate);

            taskStackLayout.Add(stackLayout);
		}
    }
}

