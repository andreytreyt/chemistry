using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Models;
using Microsoft.Xna.Framework.Content;
using Yandex.Metrica;

namespace Chemistry.Views
{
    public partial class SubstancePage : PhoneApplicationPage
    {
        private string _alias = string.Empty;

        public SubstancePage()
        {
            InitializeComponent();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.TryGetValue("alias", out _alias))
            {
                Substance substance = Database.GetSubstanceByAlias(_alias);
                StackPanel aliasPanel = Helper.SubstanceFormula(substance.Alias, 22);
                aliasPanel.Margin = new Thickness(12, 0, 0, 0);
                foreach (TextBlock child in aliasPanel.Children)
                {
                    child.Foreground = (Brush)Application.Current.Resources["PhoneAccentBrush"];
                    child.FontWeight = FontWeights.Bold;
                }
                formula.Children.Clear();
                formula.Children.Add(aliasPanel);
                DataContext = substance;
            }
        }
    }
}