using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Models;
using Chemistry.Models.Reactions;
using Chemistry.Resources;
using Yandex.Metrica;

namespace Chemistry.Views
{
    public partial class ReactionsPage : PhoneApplicationPage
    {
        private ObservableCollection<Compound> compounds = new ObservableCollection<Compound>();
        private ObservableCollection<Decomposition> decompositions = new ObservableCollection<Decomposition>();
        private ObservableCollection<Exchange> exchanges = new ObservableCollection<Exchange>();
        private ObservableCollection<Substitution> substitutions = new ObservableCollection<Substitution>();

        private ObservableCollection<IComponents> components = new ObservableCollection<IComponents>();
        private List<string> componentsAlias = new List<string>();

        public ReactionsPage()
        {
            InitializeComponent();

            Counter.ReportEvent(GetType().Name);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (CompoundFirstSubstance.Items.Count > 0 && CompoundSecondSubstance.Items.Count > 0 &&
                DecompositionSubstance.Items.Count > 0 &&
                ExchangeFirstSubstance.Items.Count > 0 && ExchangeSecondSubstance.Items.Count > 0 &&
                SubstitutionFirstSubstance.Items.Count > 0 && SubstitutionSecondSubstance.Items.Count > 0) return;

            compounds = Database.GetCompounds();
            decompositions = Database.GetDecompositions();
            exchanges = Database.GetExchanges();
            substitutions = Database.GetSubstitutions();

            components = Database.GetComponents();
            foreach (IComponents component in Database.GetComponents())
                componentsAlias.Add(component.Alias);

            CompoundFirstSubstance.ItemsSource = LoadCompound();
            DecompositionSubstance.ItemsSource = LoadDecomposition();
            ExchangeFirstSubstance.ItemsSource = LoadExchange();
            SubstitutionFirstSubstance.ItemsSource = LoadSubstitution(); 
        }
   
        #region LoadReaction
        private List<IComponents> LoadCompound()
        {
            List<IComponents> componentsCompound = new List<IComponents>();
            foreach (Compound compound in compounds.ToList())
            {
                if (componentsAlias.IndexOf(compound.Substance) > -1)
                {
                    if (componentsAlias.IndexOf(compound.Subs1) > -1 && componentsAlias.IndexOf(compound.Subs2) > -1)
                    {
                        componentsCompound.Add(components.First(component => component.Alias == compound.Subs1));
                        componentsCompound.Add(components.First(component => component.Alias == compound.Subs2));
                    }
                }
            }
            return componentsCompound.Distinct().OrderBy(component => component.Name).ToList();
        }
        private List<IComponents> LoadDecomposition()
        {
            List<IComponents> componentsDecomposition = new List<IComponents>();
            foreach (Decomposition decomposition in decompositions.ToList())
            {
                if (componentsAlias.IndexOf(decomposition.Substance) > -1)
                {
                    if (componentsAlias.IndexOf(decomposition.Subs1) > -1 && componentsAlias.IndexOf(decomposition.Subs2) > -1)
                    {
                        componentsDecomposition.Add(components.First(component => component.Alias == decomposition.Substance));
                    }
                }
            }
            return componentsDecomposition.Distinct().OrderBy(component => component.Name).ToList();
        }
        private List<IComponents> LoadExchange()
        {
            List<IComponents> componentsExchange = new List<IComponents>();
            foreach (Exchange exchange in exchanges.ToList())
            {
                if (componentsAlias.IndexOf(exchange.Subs1) > -1 && componentsAlias.IndexOf(exchange.Subs2) > -1 &&
                        componentsAlias.IndexOf(exchange.Subs3) > -1 && componentsAlias.IndexOf(exchange.Subs4) > -1)
                {
                    componentsExchange.Add(components.First(component => component.Alias == exchange.Subs1));
                    componentsExchange.Add(components.First(component => component.Alias == exchange.Subs2));
                }
            }
            return componentsExchange.Distinct().OrderBy(component => component.Name).ToList();
        }
        private List<IComponents> LoadSubstitution()
        {
            List<IComponents> componentsSubstitution = new List<IComponents>();
            foreach (Substitution replace in substitutions.ToList())
            {
                if (componentsAlias.IndexOf(replace.Subs1) > -1 && componentsAlias.IndexOf(replace.Subs2) > -1 &&
                        componentsAlias.IndexOf(replace.Subs3) > -1 && componentsAlias.IndexOf(replace.Subs4) > -1)
                {
                    componentsSubstitution.Add(components.First(component => component.Alias == replace.Subs1));
                    componentsSubstitution.Add(components.First(component => component.Alias == replace.Subs2));
                }
            }
            return componentsSubstitution.Distinct().OrderBy(component => component.Name).ToList();
        }
        #endregion

        #region OnSelectionChanged
        private void CompoundFirstSubstance_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<IComponents> componentsConnection = new List<IComponents>();
            foreach (Compound connection in compounds.ToList())
            {
                if (componentsAlias.IndexOf(connection.Substance) < 0) continue;

                if (componentsAlias.IndexOf(connection.Subs1) > -1 && componentsAlias.IndexOf(connection.Subs2) > -1)
                {
                    IComponents component1 = components.First(component => component.Alias == connection.Subs1);
                    IComponents component2 = components.First(component => component.Alias == connection.Subs2);
                    if (component1 == CompoundFirstSubstance.SelectedItem)
                        componentsConnection.Add(component2);
                    if (component2 == CompoundFirstSubstance.SelectedItem)
                        componentsConnection.Add(component1);
                }
            }
            CompoundSecondSubstance.ItemsSource = componentsConnection.Distinct().OrderBy(component => component.Name).ToList();
        }
        private void ExchangeFirstSubstance_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<IComponents> componentsExchange = new List<IComponents>();
            foreach (Exchange exchange in exchanges.ToList())
            {
                if (componentsAlias.IndexOf(exchange.Subs3) < 0 || componentsAlias.IndexOf(exchange.Subs4) < 0) continue;

                if (componentsAlias.IndexOf(exchange.Subs1) > -1 && componentsAlias.IndexOf(exchange.Subs2) > -1)
                {
                    IComponents component1 = components.First(component => component.Alias == exchange.Subs1);
                    IComponents component2 = components.First(component => component.Alias == exchange.Subs2);
                    if (component1 == ExchangeFirstSubstance.SelectedItem)
                        componentsExchange.Add(component2);
                    if (component2 == ExchangeFirstSubstance.SelectedItem)
                        componentsExchange.Add(component1);
                }
            }
            ExchangeSecondSubstance.ItemsSource = componentsExchange.Distinct().OrderBy(component => component.Name).ToList();
        }
        private void SubstitutionFirstSubstance_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<IComponents> componentsSubstitution = new List<IComponents>();
            foreach (Substitution substitution in substitutions.ToList())
            {
                if (componentsAlias.IndexOf(substitution.Subs3) < 0 || componentsAlias.IndexOf(substitution.Subs4) < 0) continue;

                if (componentsAlias.IndexOf(substitution.Subs1) > -1 && componentsAlias.IndexOf(substitution.Subs2) > -1)
                {
                    IComponents component1 = components.First(component => component.Alias == substitution.Subs1);
                    IComponents component2 = components.First(component => component.Alias == substitution.Subs2);
                    if (component1 == SubstitutionFirstSubstance.SelectedItem)
                        componentsSubstitution.Add(component2);
                    if (component2 == SubstitutionFirstSubstance.SelectedItem)
                        componentsSubstitution.Add(component1);
                }
            }
            SubstitutionSecondSubstance.ItemsSource = componentsSubstitution.Distinct().OrderBy(component => component.Name).ToList();
        }
        #endregion

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            var reactionType = (sender as Button).Name.ToLower();

            if (reactionType != "combine" && !IsPurchased())
            {
                if (MessageBox.Show(
                        String.Format("{0}\n\n{1}", AppResources.MessagePurchaseForReaction, AppResources.MessagePurchasePack), 
                        AppResources.Chemistry, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Buy_OnClick(null, new EventArgs());
                }
                return;
            }

            string uri = "/Views/ReactionResults/";
            switch (reactionType)
            {
                case "combine":
                    uri += string.Concat("CompoundPage.xaml?alias1=", (CompoundFirstSubstance.SelectedItem as IComponents).Alias,
                                                            "&alias2=", (CompoundSecondSubstance.SelectedItem as IComponents).Alias);
                    NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                    break;
                case "decompose":
                    uri += string.Concat("DecompositionPage.xaml?alias=", (DecompositionSubstance.SelectedItem as IComponents).Alias);
                    NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                    break;
                case "exchange":
                    uri += string.Concat("ExchangePage.xaml?alias1=", (ExchangeFirstSubstance.SelectedItem as IComponents).Alias,
                                                          "&alias2=", (ExchangeSecondSubstance.SelectedItem as IComponents).Alias);
                    NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                    break;
                case "substitute":
                    uri += string.Concat("SubstitutionPage.xaml?alias1=", (SubstitutionFirstSubstance.SelectedItem as IComponents).Alias,
                                                         "&alias2=", (SubstitutionSecondSubstance.SelectedItem as IComponents).Alias);
                    NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                    break;
            }
        }
    }
}