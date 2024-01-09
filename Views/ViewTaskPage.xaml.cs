using BeConsistent.Models;
using System.Threading.Tasks;

namespace BeConsistent.Views;

public partial class ViewTaskPage : ContentPage
{
	public ViewTaskPage(TaskModel task)
	{
		InitializeComponent();

        titleLabel.Text = task.title;
        DateLabel.Text = task.startDate.ToString();

        TimeSpan days = DateTime.Now - task.startDate;
        DaysLabel.Text = days.Days.ToString();
    }
}