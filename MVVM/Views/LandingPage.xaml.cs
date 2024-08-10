namespace TaskNoter.MVVM.Views;

public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
		InitializeComponent();
	}

     private void newTaskBTN_Clicked(object sender, EventArgs e)
     {
        Navigation.PushModalAsync(new NewTaskView());
     }

    private void currentTaskBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new MainView());
    }

    private void privacyPrincipleBTn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new PrivacyPolicy());
    }
}