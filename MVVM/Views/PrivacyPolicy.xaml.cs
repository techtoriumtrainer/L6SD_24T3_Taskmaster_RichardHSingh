namespace TaskNoter.MVVM.Views;

public partial class PrivacyPolicy : ContentPage
{
	public PrivacyPolicy()
	{
		InitializeComponent();

        AgreementCheckBox.CheckedChanged += AgreementCheckBox_CheckedChanged;
    }

    private void AgreementCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(e.Value)
        {
            DisplayAlert("Agreement Accepted!!",
                             "Thank you for agreeing to the PRIVACY POLICY",
                             "Yiippeee");
            
        }
        else
        {
            DisplayAlert("Agreement Required!!",
                              "Please kindly agree to the PRIVACY POLICY before proceeding",
                              "OK");
        }
    }




    private async void mainPageBTn_Clicked(object sender, EventArgs e)
    {
        // =============================================================================
        // BRANCH FUNCTIONS TO CHECK CHECKBOX IS CHECKED OR NOT BEFORE GOING TO MAIN APP
        // =============================================================================

        //if (!AgreementCheckBox.IsChecked)
        //{
        //    await DisplayAlert("Agreement Required!!",
        //                      "Please kindly agree to the PRIVACY POLICY before proceeding",
        //                      "OK");
        //}
        //else
        //{
        //    await DisplayAlert("WELCOME!!",
        //                       "Enjoy being organised with Task Noter",
        //                       "Yiippeee");


        //    await Navigation.PushModalAsync(new MainView());    
        //}


        // =============================================================================
        //                    ENABLE BUTTON TO GO TO MAIN VIEW PAGE
        // =============================================================================

       
        await Navigation.PushModalAsync(new MainView());
    }

    private void RejectBTn_Clicked(object sender, EventArgs e)
    {
        #if ANDROID || IOS
                System.Environment.Exit(0);
        #elif WINDOWS
            Application.Current.Quit();
        #endif
    }
}