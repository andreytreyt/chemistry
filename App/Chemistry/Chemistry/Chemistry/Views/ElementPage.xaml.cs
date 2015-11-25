using System;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Models;
using Chemistry.Resources;
using Microsoft.Phone.Shell;
using Yandex.Metrica;

namespace Chemistry.Views
{
    public partial class ElementPage : PhoneApplicationPage
    {
        private string _alias = string.Empty;

        public ElementPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Back) return;

            if (NavigationContext.QueryString.TryGetValue("alias", out _alias))
            {
                var element = Database.GetElementByAlias(_alias);

                double tm;
                if (double.TryParse(element.TemperatureMelting.Substring(0, element.TemperatureMelting.Length - 2), out tm))
                    element.TemperatureMelting = string.Concat(tm - 273, " ⁰C (", tm, " K)");
                double tb;
                if (double.TryParse(element.TemperatureBoiling.Substring(0, element.TemperatureBoiling.Length - 2), out tb))
                    element.TemperatureBoiling = string.Concat(tb - 273, " ⁰C (", tb, " K)");

                DataContext = element;
                ElementNameTextBlock.Text = element.Name.ToUpper();
                if (String.CompareOrdinal(element.LatName.ToUpper(), "н/д".ToUpper()) != 0)
                    ElementNameTextBlock.Text += string.Concat(" / ", element.LatName.ToUpper());
                ElementImage.Source = new BitmapImage { UriSource = new Uri(string.Concat("/Data/Images/Elements/", element.Alias, ".jpg"), UriKind.Relative) };

                string refreshPivot;
                if (NavigationContext.QueryString.TryGetValue("refreshPivot", out refreshPivot))
                {
                    ElementPivot.SelectedIndex = int.Parse(refreshPivot);
                    NavigationService.RemoveBackEntry();
                }
            }
        }

        #region AppBar

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar.IsVisible = true;

            var iconButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/previous.png", UriKind.Relative)) { Text = AppResources.Previous };
            iconButton.Click += Previous_OnClick;
            ApplicationBar.Buttons.Add(iconButton);

            iconButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/next.png", UriKind.Relative)) { Text = AppResources.Next };
            iconButton.Click += Next_OnClick;
            ApplicationBar.Buttons.Add(iconButton);
        }

        private void Previous_OnClick(object sender, EventArgs e)
        {
            int numberPivot = ElementPivot.SelectedIndex;
            int oldNumber = Database.GetElementByAlias(_alias).Number;
            int newNumber = oldNumber - 1;
            if (newNumber < 1) newNumber = Database.GetElements().Count;
            Element element = Database.GetElementByNumber(newNumber);
            NavigationService.Navigate(new Uri("/Views/ElementPage.xaml?refreshPivot=" + numberPivot + "&alias=" + element.Alias, UriKind.Relative));
        }

        private void Next_OnClick(object sender, EventArgs e)
        {
            int numberPivot = ElementPivot.SelectedIndex;
            int oldNumber = Database.GetElementByAlias(_alias).Number;
            int newNumber = oldNumber + 1;
            if (newNumber > Database.GetElements().Count) newNumber = 1;
            Element element = Database.GetElementByNumber(newNumber);
            NavigationService.Navigate(new Uri("/Views/ElementPage.xaml?refreshPivot=" + numberPivot + "&alias=" + element.Alias, UriKind.Relative));
        }

        #endregion
    }
}