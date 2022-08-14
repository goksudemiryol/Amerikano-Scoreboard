using Xamarin.Forms;

namespace Amerikano
{
    public partial class App : Application
    {
        public static TabbedPage1 tabbedPage1;

        public App()
        {
            tabbedPage1 = new TabbedPage1();
            MainPage = tabbedPage1;
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
