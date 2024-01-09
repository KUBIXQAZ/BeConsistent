using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.Shapes;

namespace BeConsistent;

public class Task
{
	public static StackLayout returnGUI(string title, DateTime startDate)
	{
		StackLayout mainStackLayout = new StackLayout
		{
			Padding = new Thickness(0,0,0,8)
		};

		Border border = new Border
		{
			Stroke = Color.FromHex("#424242"),
			HeightRequest = 80,
			VerticalOptions = LayoutOptions.Center,
			Padding = new Thickness (10,10,0,0),
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
}

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void AddTask(object sender, EventArgs args)
	{
		string text = "The term originally referred to messages sent using the Short Message Service. It has grown beyond alphanumeric";
		string[] words = text.Split(' ');
		foreach (string word in words)
		{
			Random random = new Random();
			int num = random.Next(3, 20);
			Console.WriteLine(num);
			DateTime date = DateTime.Now.AddDays(-num);

            taskStackLayout.Add(Task.returnGUI(word, date));
		}
    }
}

