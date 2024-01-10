using BeConsistent.Views;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;

namespace BeConsistent.Models
{
    [Serializable]
    public class TaskModel
    {
        public string title { get; set; }
        public DateTime startDate { get; set; }
        public List<DateTime> breaks = new List<DateTime>();

        public static TaskModel CreateTask(string title, DateTime startDate)
        {
            TaskModel task = new TaskModel
            {
                title = title,
                startDate = startDate,
            };

            App.tasks.Add(task);

            return task;
        }

        public static StackLayout CreateGUI(string title, DateTime startDate, TaskModel task)
        {
            StackLayout mainStackLayout = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 8)
            };

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) => ViewTask(sender, e, task);
            mainStackLayout.GestureRecognizers.Add(tapGestureRecognizer);

            Border border = new Border
            {
                Stroke = Color.FromHex("#424242"),
                HeightRequest = 80,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 10, 0, 0),
                StrokeThickness = 1,
                StrokeShape = new RoundRectangle()
                {
                    CornerRadius = 10
                },
                BackgroundColor = Color.FromHex("#2B2B2B")
            };

            Grid grid = new Grid();

            grid.AddRowDefinition(new RowDefinition { Height = GridLength.Star });
            grid.AddRowDefinition(new RowDefinition { Height = GridLength.Star });

            Label titleLabel = new Label
            {
                FontSize = 25,
                VerticalOptions = LayoutOptions.End,
                Text = title
            };

            TimeSpan days = DateTime.Now - startDate;

            Label daysLabel = new Label
            {
                FontSize = 15,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Colors.WhiteSmoke,
                Text = days.Days + " Days"
            };

            grid.Add(titleLabel, 0, 0);
            grid.Add(daysLabel, 0, 1);

            border.Content = grid;

            mainStackLayout.Add(border);

            return mainStackLayout;
        }

        public static void SaveTasks()
        {
            string json = JsonConvert.SerializeObject(App.tasks);
            File.WriteAllText(App.filePath, json);
        }

        public static StackLayout CreateGUIAndTask(string title, DateTime startDate)
        {
            TaskModel task = CreateTask(title, startDate);
            StackLayout mainStackLayout = CreateGUI(title, startDate, task);
            SaveTasks();

            return mainStackLayout;
        }

        public static void ViewTask(object sender, EventArgs args, TaskModel task)
        {
            App.Current.MainPage.Navigation.PushAsync(new ViewTaskPage(task));
        }
    }
}
