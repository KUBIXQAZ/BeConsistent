using BeConsistent.Models;

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

        ShowHideHistoryButton();
    }

    private bool isFirstClick2 = true;
    private DateTime firstClickTime2;
    private CancellationTokenSource cancellationTokenSource2;
    private int waitTime2 = 2000;

    private async void RemoveTaskImageButton_Clicked(object sender, EventArgs e)
    {
        var button = DeleteTaskButton;

        var oldBack = button.Background;

        if (isFirstClick2)
        {
            cancellationTokenSource2 = new CancellationTokenSource();

            isFirstClick2 = false;
            firstClickTime2 = DateTime.Now;

            button.Background = Colors.Red;

            try
            {
                await Task.Delay(waitTime2, cancellationTokenSource2.Token);
            }
            catch { }

            button.Background = oldBack;

            if ((DateTime.Now - firstClickTime2).TotalMilliseconds >= waitTime2)
            {
                isFirstClick2 = true;
            }
        }
        else
        {
            cancellationTokenSource2.Cancel();

            button.Background = oldBack;

            App.tasks.Remove(currentTask);
            currentTask = null;

            TaskModel.SaveTasks();
            App.Current.MainPage.Navigation.PopToRootAsync();

            isFirstClick2 = true;
        }
    }

    private bool isFirstClick = true;
    private DateTime firstClickTime;
    private CancellationTokenSource cancellationTokenSource;
    private int waitTime = 2000;

    private async void ResetDateButton_Clicked(object sender, EventArgs e)
    {
        var button = ResetButton;

        var oldBack = button.Background;
        var oldText = button.Text;

        if (isFirstClick)
        {
            cancellationTokenSource = new CancellationTokenSource();

            isFirstClick = false;
            firstClickTime = DateTime.Now;

            button.Background = Colors.Red;
            button.Text = "Confirm";

            try
            {
                await Task.Delay(waitTime, cancellationTokenSource.Token);
            }
            catch { }

            button.Background = oldBack;
            button.Text = oldText;

            if ((DateTime.Now - firstClickTime).TotalMilliseconds >= waitTime)
            {
                isFirstClick = true;
            }
        }
        else
        {
            cancellationTokenSource.Cancel();

            button.Background = oldBack;
            button.Text = oldText;

            DateTime date = DateTime.Now;

            currentTask.startDate = date;
            currentTask.breaks.Add(date);

            TimeSpan days = DateTime.Now - currentTask.startDate;
            DaysLabel.Text = days.Days.ToString();

            DateLabel.Text = currentTask.startDate.ToString();

            UpdateHistory();
            ShowHideHistoryButton();

            TaskModel.SaveTasks();

            isFirstClick = true;
        }
    }

    private void ShowHideHistoryButton()
    {
        if(currentTask.breaks.Count == 0) HistoryButton.IsVisible = false;
        else HistoryButton.IsVisible = true;
    }

    private async void UpdateHistory()
    {
        BreaksList.Children.Clear();

        var breaks = new List<DateTime>(currentTask.breaks);
        breaks.Reverse();

        if (isShowingBreaks == true)
        {
            if (breaks != null)
            {
                bool color = false;
                foreach (DateTime date in breaks)
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