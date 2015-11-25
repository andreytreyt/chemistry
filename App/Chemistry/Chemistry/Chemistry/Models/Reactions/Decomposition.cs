using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Chemistry.Models.Reactions
{
    //Разложение
    [Table]
    public class Decomposition : INotifyPropertyChanged, INotifyPropertyChanging
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

        //Коэффициент исходного вещества
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

        //Исходное вещество
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

        //Катадизатор
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

        //Коэффициент для первого результирующего вещества
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
        
        //Первое результирующее вещество
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

        //Индекс для первого результирующего вещества
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

        //Обозначение, показывающее выпадает ли первое результирующее вещество в осадок или выделяется в виде газа
        private string _subs1Ad;
        [Column]
        public string Subs1Ad
        {
            get
            {
                return _subs1Ad;
            }
            set
            {
                if (_subs1Ad != value)
                {
                    NotifyPropertyChanging("Subs1Ad");
                    _subs1Ad = value;
                    NotifyPropertyChanged("Subs1Ad");
                }
            }
        }

        //Коэффициент для второго результирующего вещества
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

        //Второе результирующее вещество
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

        //Индекс для второго результирующего вещества
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

        //Обозначение, показывающее выпадает ли второе результирующее вещество в осадок или выделяется в виде газа
        private string _subs2Ad;
        [Column]
        public string Subs2Ad
        {
            get
            {
                return _subs2Ad;
            }
            set
            {
                if (_subs2Ad != value)
                {
                    NotifyPropertyChanging("Subs2Ad");
                    _subs2Ad = value;
                    NotifyPropertyChanged("Subs2Ad");
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