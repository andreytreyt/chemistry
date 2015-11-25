using System;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Resources;
using Microsoft.Phone.Shell;
using Yandex.Metrica;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Chemistry.Views
{
    public partial class ElementsPage : PhoneApplicationPage
    {
        public ElementsPage()
        {
            InitializeComponent();
            App.ChemistryDb = new ChemistryDataContext(ChemistryDataContext.DbConnectionString);
            DataContext = this;
            BuildLocalizedApplicationBar();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }

            if (NonMetals.Items.Count > 0) return;

            NonMetals.ItemsSource = Database.GetElements(AppResources.NonMetal);
            Halogens.ItemsSource = Database.GetElements(AppResources.Halogen);
            InertGases.ItemsSource = Database.GetElements(AppResources.InertGas);
            SemiMetals.ItemsSource = Database.GetElements(AppResources.SemiMetal);
            Alkalines.ItemsSource = Database.GetElements(AppResources.Alkaline);
            AlkalinesEarth.ItemsSource = Database.GetElements(AppResources.AlkalineEarth);
            Lanthanides.ItemsSource = Database.GetElements(AppResources.Lanthanide);
            Actinides.ItemsSource = Database.GetElements(AppResources.Actinide);
            Transitionals.ItemsSource = Database.GetElements(AppResources.Transitional);
            PostTransitionals.ItemsSource = Database.GetElements(AppResources.PostTransitional);
        }

        private void ElementTile_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ElementPage.xaml?alias=" + (sender as ElementTile).Alias, UriKind.Relative));
        }

        #region AppBar

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar.IsVisible = true;

            var iconButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/table.png", UriKind.Relative)) { Text = AppResources.Tables };
            iconButton.Click += Table_OnClick;
            ApplicationBar.Buttons.Add(iconButton);

            iconButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/substance.png", UriKind.Relative)) { Text = AppResources.Substances };
            iconButton.Click += Substances_OnClick;
            ApplicationBar.Buttons.Add(iconButton);

            iconButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/reaction.png", UriKind.Relative)) { Text = AppResources.Reactions };
            iconButton.Click += Reaction_OnClick;
            ApplicationBar.Buttons.Add(iconButton);

            iconButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/test.png", UriKind.Relative)) { Text = AppResources.Tests };
            iconButton.Click += Tests_OnClick;
            ApplicationBar.Buttons.Add(iconButton);

            var menuItem = new ApplicationBarMenuItem(AppResources.Rate);
            menuItem.Click += Rate_OnClick;
            ApplicationBar.MenuItems.Add(menuItem);

            if (!IsPurchased())
            {
                menuItem = new ApplicationBarMenuItem(AppResources.UnlimitedPack);
                menuItem.Click += Buy_OnClick;
                ApplicationBar.MenuItems.Add(menuItem);
            }

            menuItem = new ApplicationBarMenuItem(AppResources.Settings);
            menuItem.Click += Settings_OnClick;
            ApplicationBar.MenuItems.Add(menuItem);
        }

        private void Table_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PeriodicPage.xaml", UriKind.Relative));
        }

        private void Substances_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SubstancesPage.xaml", UriKind.Relative));
        }

        private void Reaction_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ReactionsPage.xaml", UriKind.Relative));
        }

        private void Tests_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/TestsPage.xaml", UriKind.Relative));
        }

        private void Settings_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SettingsPage.xaml", UriKind.Relative));
        }

        #endregion
    }
}