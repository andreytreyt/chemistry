using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Resources;
using Microsoft.Phone.Shell;
using Yandex.Metrica;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Chemistry.Views
{
    public partial class PeriodicPage : PhoneApplicationPage
    {
        public PeriodicPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back) return;

            foreach (var element in Database.GetElements())
            {
                var elementTile = new ElementTile
                {
                    Number = element.Number,
                    Alias = element.Alias,
                    Name = element.Name,
                    Type = element.Type,
                    IsRadiation = element.IsRadiation
                };
                elementTile.Tap += ElementTile_Tap;

                (TableGrid.FindName(element.Alias) as StackPanel).Children.Add(elementTile);
            }
            base.OnNavigatedTo(e);
        }

        private void ElementTile_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ElementPage.xaml?alias=" + (sender as ElementTile).Alias, UriKind.Relative));
        }

        #region AppBar
        
        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar.IsVisible = true;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            var menuItem = new ApplicationBarMenuItem(AppResources.Periodic);
            ApplicationBar.MenuItems.Add(menuItem);

            menuItem = new ApplicationBarMenuItem(AppResources.Solubility);
            menuItem.Click += Soluble_OnClick;
            ApplicationBar.MenuItems.Add(menuItem);
        }

        private void Soluble_OnClick(object sender, EventArgs e)
        {
            if (!IsPurchased())
            {
                if (MessageBox.Show(
                        String.Format("{0}\n\n{1}", AppResources.MessagePurchaseForTable, AppResources.MessagePurchasePack), 
                        AppResources.Chemistry, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Buy_OnClick(null, new EventArgs());
                }
                return;
            }

            NavigationService.Navigate(new Uri("/Views/SolubilityPage.xaml", UriKind.Relative));
        }

        #endregion
    }
}