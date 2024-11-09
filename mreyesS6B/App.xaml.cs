namespace mreyesS6B
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.vEstudiante());
        }
    }
}
