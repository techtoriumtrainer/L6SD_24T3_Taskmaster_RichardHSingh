namespace TaskNoter.MVVM.Views;

public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
		InitializeComponent();
	}

    // ================== CODE FOR GOING TO NEWTASKVIEW PAGE ======================
    // ============================================================================
    private void newTaskBTN_Clicked(object sender, EventArgs e)
     {
        Navigation.PushModalAsync(new NewTaskView());
     }

    // ================== CODE FOR GOING TO MAINVIEW PAGE ======================
    // =========================================================================
    private void currentTaskBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new MainView());
    }

    // ================== CODE FOR GOING TO PRIVACY POLICY PAGE ======================
    // ===============================================================================
    private void privacyPrincipleBTn_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new PrivacyPolicy());
    }
}