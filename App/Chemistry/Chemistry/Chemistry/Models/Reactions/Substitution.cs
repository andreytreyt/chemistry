using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Chemistry.Models.Reactions
{
    //Замещение
    [Table]
    public class Substitution : INotifyPropertyChanged, INotifyPropertyChanging
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

        //Коэффициент для первого результирующего вещества
        private string _k3;
        [Column]
        public string K3
        {
            get
            {
                return _k3;
            }
            set
            {
                if (_k3 != value)
                {
                    NotifyPropertyChanging("K3");
                    _k3 = value;
                    NotifyPropertyChanged("K3");
                }
            }
        }

        //Первое результирующее вещество
        private string _subs3;
        [Column]
        public string Subs3
        {
            get
            {
                return _subs3;
            }
            set
            {
                if (_subs3 != value)
                {
                    NotifyPropertyChanging("Subs3");
                    _subs3 = value;
                    NotifyPropertyChanged("Subs3");
                }
            }
        }

        //Индекс для первого результирующего вещества
        private string _k32;
        [Column]
        public string K32
        {
            get
            {
                return _k32;
            }
            set
            {
                if (_k32 != value)
                {
                    NotifyPropertyChanging("K32");
                    _k32 = value;
                    NotifyPropertyChanged("K32");
                }
            }
        }

        //Обозначение, показывающее выпадает ли первое результирующее вещество в осадок или выделяется в виде газа
        private string _subs3Ad;
        [Column]
        public string Subs3Ad
        {
            get
            {
                return _subs3Ad;
            }
            set
            {
                if (_subs3Ad != value)
                {
                    NotifyPropertyChanging("Subs3Ad");
                    _subs3Ad = value;
                    NotifyPropertyChanged("Subs3Ad");
                }
            }
        }

        //Коэффициент для второго результирующего вещества
        private string _k4;
        [Column]
        public string K4
        {
            get
            {
                return _k2;
            }
            set
            {
                if (_k4 != value)
                {
                    NotifyPropertyChanging("K4");
                    _k4 = value;
                    NotifyPropertyChanged("K4");
                }
            }
        }

        //Второе результирующее вещество
        private string _subs4;
        [Column]
        public string Subs4
        {
            get
            {
                return _subs4;
            }
            set
            {
                if (_subs4 != value)
                {
                    NotifyPropertyChanging("Subs4");
                    _subs4 = value;
                    NotifyPropertyChanged("Subs4");
                }
            }
        }

        //Индекс для второго результирующего вещества
        private string _k42;
        [Column]
        public string K42
        {
            get
            {
                return _k42;
            }
            set
            {
                if (_k42 != value)
                {
                    NotifyPropertyChanging("K42");
                    _k42 = value;
                    NotifyPropertyChanged("K42");
                }
            }
        }

        //Обозначение, показывающее выпадает ли второе результирующее вещество в осадок или выделяется в виде газа
        private string _subs4Ad;
        [Column]
        public string Subs4Ad
        {
            get
            {
                return _subs4Ad;
            }
            set
            {
                if (_subs4Ad != value)
                {
                    NotifyPropertyChanging("Subs4Ad");
                    _subs4Ad = value;
                    NotifyPropertyChanged("Subs4Ad");
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