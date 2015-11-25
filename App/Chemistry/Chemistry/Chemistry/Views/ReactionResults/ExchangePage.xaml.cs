using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Models;
using Chemistry.Models.Reactions;
using Chemistry.Resources;
using Yandex.Metrica;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using PhoneApplicationPage = Chemistry.Common.PhoneApplicationPage;

namespace Chemistry.Views.ReactionResults
{
    public partial class ExchangePage : PhoneApplicationPage
    {
        public ExchangePage()
        {
            InitializeComponent();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ExchangePivot.Items.Count > 0) return;

            ObservableCollection<Exchange> exchanges =
                Database.GetExchanges(NavigationContext.QueryString["alias1"], NavigationContext.QueryString["alias2"]);

            var results = new List<StackPanel>();
            StackPanel stackPanel;
            foreach (Exchange exchange in exchanges)
            {
                IComponents component3 = Database.GetComponent(exchange.Subs3);
                IComponents component4 = Database.GetComponent(exchange.Subs4);
                if (component3 == null || component4 == null) continue;

                stackPanel = new StackPanel { Margin = new Thickness(0, -30, 0, 0), };
                stackPanel.Children.Add(new TextBlock
                {
                    Text = String.Format("{0} {1} {2}",
                        Database.GetComponent(exchange.Subs3).Name, AppResources.And, Database.GetComponent(exchange.Subs4).Name),
                    TextWrapping = TextWrapping.Wrap,
                    Style = (Style)Application.Current.Resources["PhoneTextGroupHeaderStyle"]
                });
                stackPanel.Children.Add(new ScrollViewer 
                {
                    Margin = new Thickness(12, 0, 12, 0),
                    VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    Content = ReactionFormula(exchange)
                });

                results.Add(stackPanel);
            }

            ExchangePivot.ItemsSource = results;
        }

        private StackPanel ReactionFormula(Exchange exchange)
        {
            StackPanel stackPanel = new StackPanel
            {
                Margin = new Thickness(0, 50, 0, 50),
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            int formulaSize = 44;
            int indexSize = formulaSize / 2;
            int katalizatorSize = formulaSize / 3;
            stackPanel.Children.Add(Helper.FormulaText(exchange.K1, formulaSize));
            StackPanel subsPanel = Helper.SubstanceFormula(exchange.Subs1, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(exchange.K12, indexSize));
            stackPanel.Children.Add(Helper.FormulaText("+", formulaSize));
            stackPanel.Children.Add(Helper.FormulaText(exchange.K2, formulaSize));
            subsPanel = Helper.SubstanceFormula(exchange.Subs2, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(exchange.K22, indexSize));
            stackPanel.Children.Add(Helper.FormulaText(" → ", formulaSize));
            stackPanel.Children.Add(Helper.KatalizatorText(exchange.Katalizator, katalizatorSize));
            stackPanel.Children.Add(Helper.FormulaText(exchange.K3, formulaSize));
            subsPanel = Helper.SubstanceFormula(exchange.Subs3, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(exchange.K32, indexSize));
            stackPanel.Children.Add(Helper.FormulaText(exchange.Subs3Ad, formulaSize));
            stackPanel.Children.Add(Helper.FormulaText("+", formulaSize));
            stackPanel.Children.Add(Helper.FormulaText(exchange.K4, formulaSize));
            subsPanel = Helper.SubstanceFormula(exchange.Subs4, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(exchange.K42, indexSize));
            stackPanel.Children.Add(Helper.FormulaText(exchange.Subs4Ad, formulaSize));

            return stackPanel;
        }

        private void Substance_Tap(object sender, GestureEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            string alias = string.Empty;
            foreach (TextBlock child in stackPanel.Children)
                alias += child.Text;
            NavigationService.Navigate(new Uri(Helper.GetNavigationString(alias) + alias, UriKind.Relative));
        }
    }
}