using BeConsistent.Models;
using System.Threading.Tasks;

namespace BeConsistent.Views;

public partial class ViewTaskPage : ContentPage
{
    TaskModel currentTask = null;
    bool isShowingBreaks = false;

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

    private void ResetDateButton_Clicked(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;

        currentTask.startDate = date;
        currentTask.breaks.Add(date);

        TimeSpan days = DateTime.Now - currentTask.startDate;
        DaysLabel.Text = days.Days.ToString();

        DateLabel.Text = currentTask.startDate.ToString();

        UpdateHistory();

        TaskModel.SaveTasks();
    }

    private async void UpdateHistory()
    {
        BreaksList.Children.Clear();
        if (isShowingBreaks == true)
        {
            if (currentTask.breaks != null)
            {
                bool color = false;
                foreach (DateTime date in currentTask.breaks)
                {
                    color = !color;
                    Color textColor = color ? Color.FromHex("#C3C3C3") : Color.FromHex("#a1a1a1");

                    Label label = new Label
                    {
                        TextColor = textColor,
                        HorizontalOptions = LayoutOptions.Center,
                        Text = date.ToString(),
                        Margin = new Thickness(0, 0, 0, 10)
                    };
                    BreaksList.Add(label);
                }

                double height = scrollView.Height;
                await scrollView.ScrollToAsync(0, height, true);
            }
        }
    }

    private void ShowBreaksButton_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        isShowingBreaks = !isShowingBreaks;
        if (isShowingBreaks == true) button.Text = "Hide breaks.";
        button.Text = "See breaks.";
        UpdateHistory();
    }
}