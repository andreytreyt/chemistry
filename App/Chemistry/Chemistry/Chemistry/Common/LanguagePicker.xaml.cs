using System.Windows;
using System.Windows.Controls;

namespace Chemistry.Common
{
    //Выподающий список с языками
    //При изменении языка появляется соответствующее сообщение
    public partial class LanguagePicker : UserControl
    {
        public LanguagePicker()
        {
            InitializeComponent();
            LangListPicker.ItemsSource = App.Settings.Language.LanguagesForDisplay;
            LangListPicker.SelectedIndex = App.Settings.Language.SelectedIndex;
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        private void LangListPicker_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.Settings.Language.SelectedIndex != LangListPicker.SelectedIndex)
            {
                RestartTextBlock.Visibility = Visibility.Visible;
                App.Settings.Language.IsChanged = true;
            }
            else
            {
                RestartTextBlock.Visibility = Visibility.Collapsed;
                App.Settings.Language.IsChanged = false;
            }
            App.Settings.Language.SelectingIndex = LangListPicker.SelectedIndex;
        }

        public static readonly DependencyProperty HeaderProperty =
          DependencyProperty.Register("Header", typeof(string), typeof(LanguagePicker), null);
    }
}
