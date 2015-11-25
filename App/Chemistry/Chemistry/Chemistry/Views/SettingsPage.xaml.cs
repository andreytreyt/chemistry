using System.Windows.Navigation;
using Chemistry.Common;
using Yandex.Metrica;

namespace Chemistry.Views
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            DataContext = App.Settings;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Counter.ReportEvent(App.Settings.Language.LanguagesForDisplay[App.Settings.Language.SelectingIndex].NativeName);
        }
    }
}