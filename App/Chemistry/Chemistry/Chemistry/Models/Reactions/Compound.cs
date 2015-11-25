using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Chemistry.Models.Reactions
{
    //Соединение
    [Table]
    public class Compound : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("Id");
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        //Коэффициент для первого исходного вещества
        private string _k1;
        [Column]
        public string K1
        {
            get
            {
                return _k1;
            }
            set
            {
                if (_k1 != value)
                {
                    NotifyPropertyChanging("K1");
                    _k1 = value;
                    NotifyPropertyChanged("K1");
                }
            }
        }

        //Первое исходное вещество
        private string _subs1;
        [Column]
        public string Subs1
        {
            get
            {
                return _subs1;
            }
            set
            {
                if (_subs1 != value)
                {
                    NotifyPropertyChanging("Subs1");
                    _subs1 = value;
                    NotifyPropertyChanged("Subs1");
                }
            }
        }

        //Индекс для первого исходного вещества
        private string _k12;
        [Column]
        public string K12
        {
            get
            {
                return _k12;
            }
            set
            {
                if (_k12 != value)
                {
                    NotifyPropertyChanging("K12");
                    _k12 = value;
                    NotifyPropertyChanged("K12");
                }
            }
        }

        //Коэффициент для второго исходного вещества
        private string _k2;
        [Column]
        public string K2
        {
            get
            {
                return _k2;
            }
            set
            {
                if (_k2 != value)
                {
                    NotifyPropertyChanging("K2");
                    _k2 = value;
                    NotifyPropertyChanged("K2");
                }
            }
        }

        //Второе исходное вещество
        private string _subs2;
        [Column]
        public string Subs2
        {
            get
            {
                return _subs2;
            }
            set
            {
                if (_subs2 != value)
                {
                    NotifyPropertyChanging("Subs2");
                    _subs2 = value;
                    NotifyPropertyChanged("Subs2");
                }
            }
        }

        //Индекс для второго исходного вещества
        private string _k22;
        [Column]
        public string K22
        {
            get
            {
                return _k22;
            }
            set
            {
                if (_k22 != value)
                {
                    NotifyPropertyChanging("K22");
                    _k22 = value;
                    NotifyPropertyChanged("K22");
                }
            }
        }

        //Катализатор
        private string _katalizator;
        [Column]
        public string Katalizator
        {
            get
            {
                return _katalizator;
            }
            set
            {
                if (_katalizator != value)
                {
                    NotifyPropertyChanging("Katalizator");
                    _katalizator = value;
                    NotifyPropertyChanged("Katalizator");
                }
            }
        }

        //Коэффициент для результирующего вещества
        private string _k;
        [Column]
        public string K
        {
            get
            {
                return _k;
            }
            set
            {
                if (_k != value)
                {
                    NotifyPropertyChanging("K");
                    _k = value;
                    NotifyPropertyChanged("K");
                }
            }
        }

        //Результирующее вещество
        private string _substance;
        [Column]
        public string Substance
        {
            get
            {
                return _substance;
            }
            set
            {
                if (_substance != value)
                {
                    NotifyPropertyChanging("Substance");
                    _substance = value;
                    NotifyPropertyChanged("Substance");
                }
            }
        }

        //Обозначение, показывающее выпадает ли результирующее вещество в осадок или выделяется в виде газа
        private string _substanceAd;
        [Column]
        public string SubstanceAd
        {
            get
            {
                return _substanceAd;
            }
            set
            {
                if (_substanceAd != value)
                {
                    NotifyPropertyChanging("SubstanceAd");
                    _substanceAd = value;
                    NotifyPropertyChanged("SubstanceAd");
                }
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        #endregion
    }
}