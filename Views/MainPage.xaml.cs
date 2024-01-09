using BeConsistent.Models;
using CommunityToolkit.Maui.Views;

namespace BeConsistent.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        taskStackLayout.Children.Clear();

        foreach (TaskModel task in App.tasks)
        {
            taskStackLayout.Children.Add(TaskModel.CreateGUI(task.title, task.startDate, task));
        }
    }

    private async void AddTask(object sender, EventArgs args)
	{
        AddTaskPopup addTaskPopup = new AddTaskPopup();
		var answer = await this.ShowPopupAsync(addTaskPopup);

		if ((bool)answer == true)
		{
			DateTime currDate = DateTime.Now;

			StackLayout stackLayout = TaskModel.CreateGUIAndTask(addTaskPopup.title, currDate);

            taskStackLayout.Add(stackLayout);
		}
    }
}

