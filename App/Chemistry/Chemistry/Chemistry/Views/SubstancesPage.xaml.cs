using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Models;
using Chemistry.Resources;
using Microsoft.Phone.Shell;
using Yandex.Metrica;
using PhoneApplicationPage = Chemistry.Common.PhoneApplicationPage;

namespace Chemistry.Views
{
    public partial class SubstancesPage : PhoneApplicationPage
    {
        public SubstancesPage()
        {
            InitializeComponent();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (SubstancesPivot.Items.Count > 0) return;

            CreatePivotForAllSubstances();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar.IsVisible = true;
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            var menuItem = new ApplicationBarMenuItem(AppResources.All);
            menuItem.Click += MenuItem_Click;
            ApplicationBar.MenuItems.Add(menuItem);

            foreach (var type in Database.GetSubstanceTypes())
            {
                menuItem = new ApplicationBarMenuItem(type);
                menuItem.Click += MenuItem_Click;
                ApplicationBar.MenuItems.Add(menuItem);
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = (ApplicationBarMenuItem)sender;
            var type = menuItem.Text;
            if (String.CompareOrdinal(type, AppResources.All) == 0)
            {
                CreatePivotForAllSubstances();
                return;
            }

            PageTextBlock.Text = type.ToUpper();
            SubstancesPivot.SelectedIndex = 0;

            var substabces = new Dictionary<string, ListBox>();

            var listBox = new ListBox
            {
                Width = 468,
                Margin = new Thickness(12, 0, -12, 0)
            };
            foreach (var substance in Database.GetSubstances(type))
            {
                listBox.Items.Add(SubstancePanel(substance));
            }

            substabces.Add(AppResources.All, listBox);
            
            foreach (var subType in Database.GetSubstanceSubTypes(type))
            {
                listBox = new ListBox
                {
                    Width = 468,
                    Margin = new Thickness(12, 0, -12, 0)
                };
                foreach (var substance in Database.GetSubstances(type, subType))
                {
                    listBox.Items.Add(SubstancePanel(substance));
                }

                substabces.Add(subType, listBox);
            }

            SubstancesPivot.ItemsSource = substabces;
        }

        private void CreatePivotForAllSubstances()
        {
            PageTextBlock.Text = AppResources.Substances;

            var stackPanel = new StackPanel
            {
                Width = 468,
                Margin = new Thickness(0, -24, -12, 0)
            };
            var searchTextBox = new TextBox
            {
                Margin = new Thickness(0, 0, 12, 0)
            };
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            stackPanel.Children.Add(searchTextBox);

            var listBox = new ListBox
            {
                Height = 600,
                Margin = new Thickness(12, 0, 0, 0)
            };
            foreach (var substance in Database.GetSubstances())
            {
                listBox.Items.Add(SubstancePanel(substance));
            }
            stackPanel.Children.Add(listBox);

            var substabces = new Dictionary<string, StackPanel>();
            substabces.Add(AppResources.All, stackPanel);
            SubstancesPivot.ItemsSource = substabces;
        }

        private StackPanel SubstancePanel(Substance substance)
        {
            var substancePanel = new StackPanel { Tag = substance.Alias, Margin = new Thickness(0, 0, 0, 17) };
            substancePanel.Tap += Substance_Tap;

            var aliasPanel = Helper.SubstanceFormula(substance.Alias, 54);
            substancePanel.Children.Add(aliasPanel);
            substancePanel.Children.Add(new TextBlock
            {
                Text = substance.Name,
                Margin = new Thickness(0, -6, 12, 0),
                Style = (Style)Application.Current.Resources["PhoneTextSubtleStyle"]
            });
            return substancePanel;
        }

        private void Substance_Tap(object sender, EventArgs e)
        {
            var stackPanel = (StackPanel)sender;
            string alias = stackPanel.Tag.ToString();
            NavigationService.Navigate(new Uri("/Views/SubstancePage.xaml?alias=" + alias, UriKind.Relative));

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = Helper.ClearText((sender as TextBox).Text);
            var listBox = ((sender as TextBox).Parent as StackPanel).Children[1] as ListBox;
            listBox.Items.Clear();
            foreach (var substance in Database.GetSearchingSubstances(query))
            {
                listBox.Items.Add(SubstancePanel(substance));
            }
        }
    }
}