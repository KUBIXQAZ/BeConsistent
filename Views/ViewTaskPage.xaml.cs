using BeConsistent.Models;

namespace BeConsistent.Views;

public partial class ViewTaskPage : ContentPage
{
    TaskModel currentTask = null;
	public ViewTaskPage(TaskModel task)
	{
		InitializeComponent();

        currentTask = task;

        titleLabel.Text = task.title;
        DateLabel.Text = task.startDate.ToString();

        TimeSpan days = DateTime.Now - task.startDate;
        DaysLabel.Text = days.Days.ToString();
    }

    private void RemoveTaskImageButton_Clicked(object sender, EventArgs e)
    {
        App.tasks.Remove(currentTask);
        currentTask = null;

        TaskModel.SaveTasks();
        App.Current.MainPage.Navigation.PopToRootAsync();
    }
}