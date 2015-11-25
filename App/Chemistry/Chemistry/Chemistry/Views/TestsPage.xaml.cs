
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Resources;
using Yandex.Metrica;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Chemistry.Views
{
    public partial class TestsPage : PhoneApplicationPage
    {
        public TestsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (Tests.Items.Count > 0) return;

            Tests.ItemsSource = Database.GetTests();

            Counter.ReportEvent(GetType().Name);
        }

        private void Tile_Tap(object sender, GestureEventArgs e)
        {
            var number = int.Parse((sender as StackPanel).Tag.ToString());

            if (number != 1 && !IsPurchased())
            {
                if (MessageBox.Show(
                        String.Format("{0}\n\n{1}", AppResources.MessagePurchaseForTest, AppResources.MessagePurchasePack), 
                        AppResources.Chemistry, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Buy_OnClick(null, new EventArgs());
                }
                return;
            }

            NavigationService.Navigate(new Uri("/Views/TestPage.xaml?number=" + number, UriKind.Relative));
        }
    }
}