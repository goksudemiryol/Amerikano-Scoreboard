using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amerikano
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            this.Title = "Amerikano";
            Children.Add(new MainPage());
        }
    }
}