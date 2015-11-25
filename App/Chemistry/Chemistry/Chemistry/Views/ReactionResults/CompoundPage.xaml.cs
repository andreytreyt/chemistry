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
using Yandex.Metrica;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using PhoneApplicationPage = Chemistry.Common.PhoneApplicationPage;

namespace Chemistry.Views.ReactionResults
{
    public partial class CompoundPage : PhoneApplicationPage
    {
        public CompoundPage()
        {
            InitializeComponent();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (CompoundPivot.Items.Count > 0) return;

            ObservableCollection<Compound> compounds =
                Database.GetCompounds(NavigationContext.QueryString["alias1"], NavigationContext.QueryString["alias2"]);

            var results = new List<StackPanel>();
            StackPanel stackPanel;
            foreach (Compound compound in compounds)
            {
                Substance substance = Database.GetSubstanceByAlias(compound.Substance);
                if (substance == null) continue;

                stackPanel = new StackPanel { Margin = new Thickness(0, -30, 0, 0), };
                stackPanel.Children.Add(new TextBlock
                    {
                        Text = substance.Name,
                        TextWrapping = TextWrapping.Wrap,
                        Style = (Style)Application.Current.Resources["PhoneTextGroupHeaderStyle"]
                    });
                stackPanel.Children.Add(new ScrollViewer
                {
                    Margin = new Thickness(12, 0, 12, 0),
                    VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    Content = ReactionFormula(compound)
                });

                results.Add(stackPanel);
            }

            CompoundPivot.ItemsSource = results;
        }

        private StackPanel ReactionFormula(Compound compound)
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
            StackPanel subsPanel = Helper.SubstanceFormula(compound.Subs1, formulaSize);
            stackPanel.Children.Add(Helper.FormulaText(compound.K1, formulaSize));
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(compound.K12, indexSize));
            stackPanel.Children.Add(Helper.FormulaText("+", formulaSize));
            stackPanel.Children.Add(Helper.FormulaText(compound.K2, formulaSize));
            subsPanel = Helper.SubstanceFormula(compound.Subs2, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(compound.K22, indexSize));
            stackPanel.Children.Add(Helper.FormulaText(" → ", formulaSize));
            stackPanel.Children.Add(Helper.KatalizatorText(compound.Katalizator, katalizatorSize));
            stackPanel.Children.Add(Helper.FormulaText(compound.K, formulaSize));
            subsPanel = Helper.SubstanceFormula(compound.Substance, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(compound.SubstanceAd, formulaSize));

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