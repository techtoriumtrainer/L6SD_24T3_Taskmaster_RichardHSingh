namespace TaskNoter.MVVM.Views;

public partial class PrivacyPolicy : ContentPage
{
	public PrivacyPolicy()
	{
		InitializeComponent();
	}

    private async void mainPageBTn_Clicked(object sender, EventArgs e)
    {
        // using if else statement to ensure all boxes are checked
        if (!IntroCheckBox.IsChecked)
        {
           await DisplayAlert("Agreement Required!!",
                              "Please kindly agree to the privacy policy related to 'INTRODUCTION' section before proceeding",
                              "OK");
        }
        else if (!InfoCollectedCheckBox.IsChecked)
        {
            await DisplayAlert("Agreement Required!!",
                               "Please kindly agree to the privacy policy in related to 'INFORMATION WE COLLECT' section before proceeding",
                               "OK");
        }
        else if (!HowUsedCheckBox.IsChecked)
        {
            await DisplayAlert("Agreement Required!!",
                               "Please kindly agree to the privacy policy in related to 'HOW WE USE INFORMATION' section before proceeding",
                               "OK");
        }
        else if (!DisclosureCheckBox.IsChecked)
        {
            await DisplayAlert("Agreement Required!!",
                               "Please kindly agree to the privacy policy in related to 'DISCLOSURE OF INFORMATION' section before proceeding",
                               "OK");
        }
        else if (!SecurityCheckBox.IsChecked)
        {
            await DisplayAlert("Agreement Required!!",
                               "Please kindly agree to the privacy policy in related to 'SECURITY OF INFORMATION' section before proceeding",
                               "OK");
        }
        else if (!ProtectionCheckBox.IsChecked)
        {
            await DisplayAlert("Agreement Required!!",
                               "Please kindly agree to the privacy policy in related to 'PROTECTION OF DATA RIGHTS' section before proceeding",
                               "OK");
        }
        else if (!ChangesCheckBox.IsChecked)
        {
            await DisplayAlert("Agreement Required!!",
                               "Please kindly agree to the privacy policy in related to 'CHANGES OF PRIVACY POLICY' section before proceeding",
                               "OK");
        }
        else if (!ContactCheckBox.IsChecked)
        {
            await DisplayAlert("Agreement Required!!",
                               "Please kindly agree to the privacy policy in related to 'CONTACT US' section before proceeding",
                               "OK");
        }
        else
        {
            await DisplayAlert("WELCOME!!",
                               "Enjoy being organised with Task Noter",
                               "Yiippeee");

            await Navigation.PushModalAsync(new MainView());
          

        }
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