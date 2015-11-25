using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Chemistry.Resources;

namespace Chemistry.Common
{
    //Плитка элемента, которая отображается на страницах "Элементы" и "Периодическая таблица"
    public partial class ElementTile : UserControl
    {
        public ElementTile()
        {
            InitializeComponent();
        }

        private void ElementTile_OnLoaded(object sender, RoutedEventArgs e)
        {
            RadiationImage.Visibility = IsRadiation ? Visibility.Visible : Visibility.Collapsed;
            TilePanel.Background = TileColor;
        }

        //Определение цвета плитки по типу элемента
        public Brush TileColor
        {
            get
            {
                if (Type == AppResources.NonMetal)
                    return new SolidColorBrush(Color.FromArgb(200, 0, 238, 0));

                if (Type == AppResources.Halogen)
                    return new SolidColorBrush(Color.FromArgb(200, 0, 221, 187));

                if (Type == AppResources.InertGas)
                    return new SolidColorBrush(Color.FromArgb(200, 102, 170, 255));

                if (Type == AppResources.SemiMetal)
                    return new SolidColorBrush(Color.FromArgb(200, 85, 204, 136));

                if (Type == AppResources.Alkaline)
                    return new SolidColorBrush(Color.FromArgb(200, 255, 170, 0));

                if (Type == AppResources.AlkalineEarth)
                    return new SolidColorBrush(Color.FromArgb(200, 243, 243, 0));

                if (Type == AppResources.Lanthanide)
                    return new SolidColorBrush(Color.FromArgb(200, 255, 170, 136));

                if (Type == AppResources.Actinide)
                    return new SolidColorBrush(Color.FromArgb(200, 221, 170, 204));

                if (Type == AppResources.Transitional)
                    return new SolidColorBrush(Color.FromArgb(200, 221, 153, 153));

                if (Type == AppResources.PostTransitional)
                    return new SolidColorBrush(Color.FromArgb(200, 153, 187, 170));
                       
                return new SolidColorBrush(Colors.White);
            }
        }

        //порядковый номер
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        //обозначение
        public string Alias
        {
            get { return (string)GetValue(AliasProperty); }
            set { SetValue(AliasProperty, value); }
        }
        //название
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        //тип
        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        //является ли радиокнивным
        public bool IsRadiation
        {
            get { return (bool)GetValue(IsRadiationProperty); }
            set { SetValue(IsRadiationProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
          DependencyProperty.Register("Number", typeof(int), typeof(ElementTile), null);

        public static readonly DependencyProperty AliasProperty =
          DependencyProperty.Register("Alias", typeof(string), typeof(ElementTile), null);

        public static readonly DependencyProperty NameProperty =
          DependencyProperty.Register("Name", typeof(string), typeof(ElementTile), null);

        public static readonly DependencyProperty TypeProperty =
          DependencyProperty.Register("Type", typeof(string), typeof(ElementTile), null);

        public static readonly DependencyProperty IsRadiationProperty =
          DependencyProperty.Register("IsRadiation", typeof(bool), typeof(ElementTile), null);
    }
}
