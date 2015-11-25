using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Chemistry.Common;
using Chemistry.Data;
using Chemistry.Resources;
using Microsoft.Phone.Shell;
using Yandex.Metrica;
using PhoneApplicationPage = Chemistry.Common.PhoneApplicationPage;

namespace Chemistry.Views
{
    public partial class TestPage : PhoneApplicationPage
    {
        private readonly Dictionary<int, ScrollViewer> _questions = new Dictionary<int, ScrollViewer>();
        private readonly Dictionary<int, string> _questionNames = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _realAnswers = new Dictionary<int, string>();
        private readonly DateTime _dtStart = DateTime.Now;

        public TestPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            Counter.ReportEvent(GetType().Name);
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar.IsVisible = true;

            var iconButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/ok.png", UriKind.Relative)) { Text = AppResources.Complete };
            iconButton.Click += Complete_OnClick;
            ApplicationBar.Buttons.Add(iconButton);
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            e.Cancel = MessageBox.Show(AppResources.ExitFromTest, AppResources.Chemistry, MessageBoxButton.OKCancel) != MessageBoxResult.OK;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (TestPivot.Items.Count > 0) return;

            string number;
            if (NavigationContext.QueryString.TryGetValue("number", out number))
            {
                var test = Database.GetTest(int.Parse(number));
                var componentsList = Tests.GetTenRandomComponents(test.Number).ToList();

                ScrollViewer scrollViewer;
                StackPanel stackPanel;
                TextBlock textBlock;
                TextBox textBox;
                var j = 0;
                string typeQuestion = string.Empty,
                       nameQuestion = string.Empty,
                       nameAnswer = string.Empty;

                foreach (var component in componentsList)
                {
                    Tests.GetTest(test.Number, component, ref typeQuestion, ref nameQuestion, ref nameAnswer);

                    ++j;

                    scrollViewer = new ScrollViewer
                    {
                        Width = 468,
                        Height = 600,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
                    };
                    stackPanel = new StackPanel();
                    textBlock = new TextBlock
                        {
                            Text = test.Name,
                            Style = (Style)Application.Current.Resources["PhoneTextNormalStyle"]
                        };
                    stackPanel.Children.Add(textBlock);
                    textBlock = new TextBlock
                        {
                            Text = typeQuestion,
                            Style = (Style)Application.Current.Resources["PhoneTextGroupHeaderStyle"]
                        };
                    stackPanel.Children.Add(textBlock);
                    StackPanel questionPanel;
                    questionPanel = nameQuestion.IndexOf('+') > -1
                        ? Helper.ReactionFormula(nameQuestion, 48)
                        : Helper.SubstanceFormula(nameQuestion, 72);
                    questionPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Children.Add(new ScrollViewer
                    {
                        Margin = new Thickness(12, 0, 24, 0),
                        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                        Content = questionPanel
                    });
                    textBlock = new TextBlock
                        {
                            Text = AppResources.Answer,
                            Style = (Style)Application.Current.Resources["PhoneTextGroupHeaderStyle"]
                        };
                    stackPanel.Children.Add(textBlock);
                    textBox = new TextBox
                        {
                            Name = string.Concat("answerUser", j)
                        };
                    stackPanel.Children.Add(textBox);
                    scrollViewer.Content = stackPanel;

                    _questions.Add(j, scrollViewer);
                    _questionNames.Add(j, nameQuestion);
                    _realAnswers.Add(j, nameAnswer);
                }

                TestPivot.ItemsSource = _questions;
            }
        }

        private void Complete_OnClick(object sender, EventArgs e)
        {
            LoadTestResult();
            TestPivot.Visibility = Visibility.Collapsed;
            ApplicationBar.IsVisible = false;
            TestResultPivot.Visibility = Visibility.Visible;
        }

        private void LoadTestResult()
        {
            var answerTrue = 0;
            TimeTextBlock.Text = (DateTime.Now - _dtStart).ToString();

            foreach (var question in _questions)
            {
                var answerUserControl = question.Value.FindName(string.Concat("answerUser", question.Key)) as TextBox;

                string answerUser = answerUserControl != null ? answerUserControl.Text : String.Empty,
                       answerReal = _realAnswers[question.Key];

                var verifyPanel = new StackPanel
                {
                    Margin = new Thickness(-12, 0, -12, 0),
                    Orientation = System.Windows.Controls.Orientation.Horizontal
                };
                verifyPanel.Children.Add(new TextBlock { Text = string.Concat(question.Key, "."), Style = (Style)App.Current.Resources["PhoneTextLargeStyle"] });
                verifyPanel.Children.Add(_questionNames[question.Key].IndexOf('+') > -1
                                             ? Helper.ReactionFormula(_questionNames[question.Key], 32)
                                             : Helper.SubstanceFormula(_questionNames[question.Key], 32));
                var truePanel = new StackPanel { Orientation = System.Windows.Controls.Orientation.Horizontal };
                var verifyBlock = new TextBlock { Style = (Style)App.Current.Resources["PhoneTextLargeStyle"] };
                var realBlock = new StackPanel();
                if (String.CompareOrdinal(Helper.ClearText(answerUser), Helper.ClearText(answerReal)) == 0)
                {
                    verifyBlock.Text = AppResources.True;
                    verifyBlock.Foreground = new SolidColorBrush(Colors.Green);
                    verifyPanel.Children.Add(new TextBlock { Text = string.Concat("- ", answerReal), Style = (Style)App.Current.Resources["PhoneTextLargeStyle"] });
                    answerTrue++;
                }
                else
                {
                    verifyBlock.Text = AppResources.False;
                    verifyBlock.Foreground = new SolidColorBrush(Colors.Red);
                    verifyPanel.Children.Add(new TextBlock { Text = string.Concat("- ", answerUser), Style = (Style)App.Current.Resources["PhoneTextLargeStyle"] });
                    realBlock = Helper.SubstanceFormula(answerReal, 32);
                    foreach (TextBlock child in realBlock.Children)
                        child.Foreground = (Brush)App.Current.Resources["PhoneAccentBrush"];
                }
                ResultPanel.Children.Add(new ScrollViewer
                {
                    Margin = new Thickness(12, 0, 0, 0),
                    VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    Content = verifyPanel
                });
                truePanel.Children.Add(verifyBlock);
                truePanel.Children.Add(realBlock);
                ResultPanel.Children.Add(truePanel);
            }

            ResultTextBlock.Text += string.Concat(" ", 10 * answerTrue, "%");
        }
    }
}