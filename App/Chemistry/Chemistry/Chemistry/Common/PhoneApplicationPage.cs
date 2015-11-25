using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Windows.ApplicationModel.Store;
using Chemistry.Resources;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Chemistry.Common
{
    //Базовый класс для страниц приложения
    public class PhoneApplicationPage : Microsoft.Phone.Controls.PhoneApplicationPage
    {
        public PhoneApplicationPage()
        {
            ApplicationBar = CreateApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            UpdateSystemTray();
        }

        //Изменнеие системного трея
        protected void UpdateSystemTray()
        {
            SystemTray.ForegroundColor = (App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color;
            SystemTray.Opacity = 0;
            SystemTray.IsVisible = true;
        }

        //Проверка приобретения безлимитного пакета
        protected bool IsPurchased()
        {
            return CurrentApp.LicenseInformation.ProductLicenses["6fce68c2f96147c294bd59b53adbaba7"].IsActive;
        }

        //Создание панели приложения
        protected ApplicationBar CreateApplicationBar()
        {
            var appbar = new ApplicationBar
            {
                IsVisible = false,
                BackgroundColor = Color.FromArgb(255, 20, 20, 20),
                ForegroundColor = Colors.White,
                Opacity = 1,
                Mode = ApplicationBarMode.Default
            };

            return appbar;
        }

        //Оценивание приложения в магазине
        protected void Rate_OnClick(object sender, EventArgs e)
        {
            new MarketplaceReviewTask().Show();
        }

        //Покупка безлимитного пакета
        protected async void Buy_OnClick(object sender, EventArgs e)
        {
            try
            {
                await CurrentApp.RequestProductPurchaseAsync("6fce68c2f96147c294bd59b53adbaba7", false);
                ProductLicense productLicense;
                if (CurrentApp.LicenseInformation.ProductLicenses.TryGetValue("6fce68c2f96147c294bd59b53adbaba7", out productLicense))
                {
                    if (productLicense.IsActive)
                    {
                        MessageBox.Show(AppResources.MessageThanksForPurchase, AppResources.Chemistry, MessageBoxButton.OK);
                        CurrentApp.ReportProductFulfillment("6fce68c2f96147c294bd59b53adbaba7");
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
