using TaskNoter.MVVM.Views;
using TaskNoter.Service;

namespace TaskNoter
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new LandingPage());
        }
    }
}
