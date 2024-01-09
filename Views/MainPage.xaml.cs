using BeConsistent.Views;
using CommunityToolkit.Maui.Views;
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

    private async void AddTask(object sender, EventArgs args)
	{
        AddTaskPopup addTaskPopup = new AddTaskPopup();
		var answer = await this.ShowPopupAsync(addTaskPopup);

		if ((bool)answer == true) taskStackLayout.Add(Task.returnGUI(addTaskPopup.title, DateTime.Now));
    }
}

