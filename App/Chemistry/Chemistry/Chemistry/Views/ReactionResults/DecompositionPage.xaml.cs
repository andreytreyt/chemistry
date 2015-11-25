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
    public partial class DecompositionPage : PhoneApplicationPage
    {
        public DecompositionPage()
        {
            InitializeComponent();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DecompositionPivot.Items.Count > 0) return;

            List<string> elementsAlias = new List<string>();
            List<string> substancesAlias = new List<string>();
            foreach (Element element in Database.GetElements())
                elementsAlias.Add(element.Alias);
            foreach (Substance substance in Database.GetSubstances())
                substancesAlias.Add(substance.Alias);

            ObservableCollection<Decomposition> decompositions = new ObservableCollection<Decomposition>();
            foreach (Decomposition decomposition in Database.GetDecompositions(NavigationContext.QueryString["alias"]))
            {
                if (elementsAlias.IndexOf(decomposition.Subs1) > -1 || elementsAlias.IndexOf(decomposition.Subs2) > -1 ||
                    substancesAlias.IndexOf(decomposition.Subs1) > -1 && substancesAlias.IndexOf(decomposition.Subs2) > -1)
                {
                    decompositions.Add(decomposition);
                }
            }

            var results = new List<StackPanel>();
            StackPanel stackPanel;
            foreach (Decomposition decomposition in decompositions)
            {
                Substance substance = Database.GetSubstanceByAlias(decomposition.Substance);
                if (substance == null) continue;

                stackPanel = new StackPanel { Margin = new Thickness(0, -30, 0, 0), };
                stackPanel.Children.Add(new TextBlock
                {
                    Text = String.Format("{0} {1} {2}",
                        Database.GetComponent(decomposition.Subs1).Name, AppResources.And, Database.GetComponent(decomposition.Subs2).Name),
                    TextWrapping = TextWrapping.Wrap,
                    Style = (Style)Application.Current.Resources["PhoneTextGroupHeaderStyle"]
                });
                stackPanel.Children.Add(new ScrollViewer
                {
                    Margin = new Thickness(12, 0, 12, 0),
                    VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    Content = ReactionFormula(decomposition)
                });

                results.Add(stackPanel);
            }

            DecompositionPivot.ItemsSource = results;
        }

        private StackPanel ReactionFormula(Decomposition decomposition)
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
            stackPanel.Children.Add(Helper.FormulaText(decomposition.K, formulaSize));
            StackPanel subsPanel = Helper.SubstanceFormula(decomposition.Substance, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(" → ", formulaSize));
            stackPanel.Children.Add(Helper.KatalizatorText(decomposition.Katalizator, katalizatorSize));
            stackPanel.Children.Add(Helper.FormulaText(decomposition.K1, formulaSize));
            subsPanel = Helper.SubstanceFormula(decomposition.Subs1, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(decomposition.K12, indexSize));
            stackPanel.Children.Add(Helper.FormulaText(decomposition.Subs1Ad, formulaSize));
            stackPanel.Children.Add(Helper.FormulaText("+", formulaSize));
            stackPanel.Children.Add(Helper.FormulaText(decomposition.K2, formulaSize));
            subsPanel = Helper.SubstanceFormula(decomposition.Subs2, formulaSize);
            subsPanel.Tap += Substance_Tap;
            stackPanel.Children.Add(subsPanel);
            stackPanel.Children.Add(Helper.FormulaText(decomposition.K22, indexSize));
            stackPanel.Children.Add(Helper.FormulaText(decomposition.Subs2Ad, formulaSize));

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