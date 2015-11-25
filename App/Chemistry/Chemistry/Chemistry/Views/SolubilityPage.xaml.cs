using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Models;
using Chemistry.Resources;
using Microsoft.Phone.Shell;
using Yandex.Metrica;

namespace Chemistry.Views
{
    public partial class SolubilityPage : PhoneApplicationPage
    {
        private bool removeBackEntry = true;

        public SolubilityPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back && removeBackEntry)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back) return;
            List<string> solubleList = new List<string> { "Р", "М", "Н", "Х" };

            foreach (var child in SolubleGrid.Children)
            {
                if (child is ListBoxItem)
                {
                    ListBoxItem listBoxItem = (ListBoxItem)child;
                    string tag = listBoxItem.Tag.ToString();
                    int index = tag.IndexOf("|");
                    string alias = string.Empty, ad = string.Empty;
                    if (index > -1)
                    {
                        alias = tag.Remove(index);
                        ad = tag.Remove(0, index + 1);
                    }
                    else
                    {
                        ad = tag;
                    }

                    if (solubleList.IndexOf(ad) < 0)
                    {
                        StackPanel formulaPanel = Helper.SubstanceFormula(alias, 30);
                        formulaPanel.Children.Add(new TextBlock
                            {
                                Text = ad,
                                FontSize = 16,
                                Margin = new Thickness(0, 0, 0, 0),
                                Style = (Style)Application.Current.Resources["PhoneTextNormalStyle"]
                            });
                        listBoxItem.Content = formulaPanel;
                    }
                    else
                    {
                        StackPanel solublePanel = new StackPanel
                            {
                                Width = 70,
                                Height = 70,
                                Margin = new Thickness(5),
                                VerticalAlignment = VerticalAlignment.Center
                            };
                        solublePanel.Children.Add(new TextBlock
                            {
                                Text = String.CompareOrdinal(ad, "Х") == 0 ? "-" : ad,
                                Foreground = new SolidColorBrush(Colors.Black),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Style = (Style)Application.Current.Resources["PhoneTextNormalStyle"],
                                FontSize = 50,
                                FontWeight = FontWeights.Bold
                            });

                        #region SwitchAd
                        switch (ad)
                        {
                            case "Р":
                                solublePanel.Background = new SolidColorBrush(Color.FromArgb(200, 123, 162, 255));
                                break;
                            case "М":
                                solublePanel.Background = new SolidColorBrush(Color.FromArgb(200, 254, 253, 2));
                                break;
                            case "Н":
                                solublePanel.Background = new SolidColorBrush(Color.FromArgb(200, 254, 101, 155));
                                break;
                            case "Х":
                                solublePanel.Background = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255));
                                break;
                        }
                        #endregion

                        listBoxItem.Content = solublePanel;
                        listBoxItem.Tap += Tile_Tap;
                    }
                }
            }
        }

        private void Tile_Tap(object sender, GestureEventArgs e)
        {
            ListBoxItem tile = (ListBoxItem)sender;
            string tag = tile.Tag.ToString();
            string alias = string.Empty;
            int index = tag.IndexOf("|");
            if (index > -1)
            {
                alias = tag.Remove(index);
                Substance substance = Database.GetSubstanceByAlias(alias);
                if (substance != null)
                    NavigationService.Navigate(new Uri("/Views/SubstancePage.xaml?alias=" + substance.Alias, UriKind.Relative));
            }
        }

        #region AppBar

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar.IsVisible = true;
            ApplicationBar. Mode = ApplicationBarMode.Minimized;

            var menuItem = new ApplicationBarMenuItem(AppResources.Periodic);
            menuItem.Click += Periodic_OnClick;
            ApplicationBar.MenuItems.Add(menuItem);

            menuItem = new ApplicationBarMenuItem(AppResources.Solubility);
            ApplicationBar.MenuItems.Add(menuItem);
        }

        private void Periodic_OnClick(object sender, EventArgs e)
        {
            removeBackEntry = false;
            NavigationService.GoBack();
        }

        #endregion
    }
}