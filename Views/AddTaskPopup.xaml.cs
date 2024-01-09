using CommunityToolkit.Maui.Views;

namespace BeConsistent.Views;

public partial class AddTaskPopup : Popup
{
	public string title = null;

	public AddTaskPopup()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		title = titleEntry.Text;
		Close(true);
    }

	private void CancelButton_Clicked(object sender, EventArgs args)
	{
		Close(false);
	}
}