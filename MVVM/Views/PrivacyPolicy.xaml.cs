namespace TaskNoter.MVVM.Views
{
    public partial class PrivacyPolicy : ContentPage
    {
        public PrivacyPolicy()
        {
            InitializeComponent();
            AgreementCheckBox.CheckedChanged += AgreementCheckBox_CheckedChanged;
        }

        private void AgreementCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                DisplayAlert("Agreement Accepted!", "Thank you for agreeing to the PRIVACY POLICY", "Yiippeee");
            }
            else
            {
                DisplayAlert("Agreement Required!", "Please kindly agree to the PRIVACY POLICY before proceeding", "OK");
            }
        }

        private async void mainPageBTn_Clicked(object sender, EventArgs e)
        {
            // Ensure the user has agreed to the privacy policy before proceeding
            if (AgreementCheckBox.IsChecked)
            {
                await DisplayAlert("WELCOME!", "Enjoy being organized with Task Noter", "Yiippeee");
                await Navigation.PushModalAsync(new MainView());
            }
            else
            {
                await DisplayAlert("Agreement Required!", "Please kindly agree to the PRIVACY POLICY before proceeding", "OK");
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
}
