using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Chemistry.Models
{
    //Элемент. Здесь все свойства должны быть понятны
    [Table]
    public class Element : INotifyPropertyChanged, INotifyPropertyChanging, IComponents
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

        private string _type;
        [Column]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    NotifyPropertyChanging("Type");
                    _type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        #region АТОМНЫЕ СВОЙСТВА

        //номер в таблице
        private int _number;
        [Column(CanBeNull = false)]
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number != value)
                {
                    NotifyPropertyChanging("Number");
                    _number = value;
                    NotifyPropertyChanged("Number");
                }
            }
        }

        //название
        private string _name;
        [Column(CanBeNull = false)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        //латинское название
        private string _latName;
        [Column(CanBeNull = false)]
        public string LatName
        {
            get
            {
                return _latName;
            }
            set
            {
                if (_latName != value)
                {
                    NotifyPropertyChanging("LatName");
                    _latName = value;
                    NotifyPropertyChanged("LatName");
                }
            }
        }

        //обозначение
        private string _alias;
        [Column(CanBeNull = false)]
        public string Alias
        {
            get
            {
                return _alias;
            }
            set
            {
                if (_alias != value)
                {
                    NotifyPropertyChanging("Alias");
                    _alias = value;
                    NotifyPropertyChanged("Alias");
                }
            }
        }

        //молярная масса
        private string _m;
        [Column(CanBeNull = false)]
        public string M
        {
            get
            {
                return _m;
            }
            set
            {
                if (_m != value)
                {
                    NotifyPropertyChanging("M");
                    _m = value;
                    NotifyPropertyChanged("M");
                }
            }
        }

        //радиус атома
        private string _radius;
        [Column]
        public string Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (_radius != value)
                {
                    NotifyPropertyChanging("Radius");
                    _radius = value;
                    NotifyPropertyChanged("Radius");
                }
            }
        }
        
        #endregion

        #region ХИМИЧЕСКИЕ СВОЙСТВА

        //ковалентный радиус
        private string _kovRadius;
        [Column]
        public string KovRadius
        {
            get
            {
                return _kovRadius;
            }
            set
            {
                if (_kovRadius != value)
                {
                    NotifyPropertyChanging("KovRadius");
                    _kovRadius = value;
                    NotifyPropertyChanged("KovRadius");
                }
            }
        }

        //радиус иона
        private string _ionRadius;
        [Column]
        public string IonRadius
        {
            get
            {
                return _ionRadius;
            }
            set
            {
                if (_ionRadius != value)
                {
                    NotifyPropertyChanging("IonRadius");
                    _ionRadius = value;
                    NotifyPropertyChanged("IonRadius");
                }
            }
        }

        //электроотрицательность
        private string _electronegativity;
        [Column]
        public string Electronegativity
        {
            get
            {
                return _electronegativity;
            }
            set
            {
                if (_electronegativity != value)
                {
                    NotifyPropertyChanging("Electronegativity");
                    _electronegativity = value;
                    NotifyPropertyChanged("Electronegativity");
                }
            }
        }

        //степени окисления
        private string _oxidation;
        [Column]
        public string Oxidation
        {
            get
            {
                return _oxidation;
            }
            set
            {
                if (_oxidation != value)
                {
                    NotifyPropertyChanging("Oxidation");
                    _oxidation = value;
                    NotifyPropertyChanged("Oxidation");
                }
            }
        }

        //энергия ионизации
        private string _energy;
        [Column]
        public string Energy
        {
            get
            {
                return _energy;
            }
            set
            {
                if (_energy != value)
                {
                    NotifyPropertyChanging("Energy");
                    _energy = value;
                    NotifyPropertyChanged("Energy");
                }
            }
        }

        #endregion

        #region ТЕРМОДИНАМИЧЕСКИЕ СВОЙСТВА

        //плотность
        private string _density;
        [Column]
        public string Density
        {
            get
            {
                return _density;
            }
            set
            {
                if (_density != value)
                {
                    NotifyPropertyChanging("Density");
                    _density = value;
                    NotifyPropertyChanged("Density");
                }
            }
        }

        //температура плавления
        private string _temperatureMelting;
        [Column]
        public string TemperatureMelting
        {
            get
            {
                return _temperatureMelting;
            }
            set
            {
                if (_temperatureMelting != value)
                {
                    NotifyPropertyChanging("TemperatureMelting");
                    _temperatureMelting = value;
                    NotifyPropertyChanged("TemperatureMelting");
                }
            }
        }

        //температура кипения
        private string _temperatureBoiling;
        [Column]
        public string TemperatureBoiling
        {
            get
            {
                return _temperatureBoiling;
            }
            set
            {
                if (_temperatureBoiling != value)
                {
                    NotifyPropertyChanging("TemperatureBoiling");
                    _temperatureBoiling = value;
                    NotifyPropertyChanged("TemperatureBoiling");
                }
            }
        }

        //теплота плавления
        private string _heatMelting;
        [Column]
        public string HeatMelting
        {
            get
            {
                return _heatMelting;
            }
            set
            {
                if (_heatMelting != value)
                {
                    NotifyPropertyChanging("HeatMelting");
                    _heatMelting = value;
                    NotifyPropertyChanged("HeatMelting");
                }
            }
        }

        //теплота испарения
        private string _heatEvaporation;
        [Column]
        public string HeatEvaporation
        {
            get
            {
                return _heatEvaporation;
            }
            set
            {
                if (_heatEvaporation != value)
                {
                    NotifyPropertyChanging("HeatEvaporation");
                    _heatEvaporation = value;
                    NotifyPropertyChanged("HeatEvaporation");
                }
            }
        }

        //теплоемкость
        private string _heatCapacity;
        [Column]
        public string HeatCapacity
        {
            get
            {
                return _heatCapacity;
            }
            set
            {
                if (_heatCapacity != value)
                {
                    NotifyPropertyChanging("HeatCapacity");
                    _heatCapacity = value;
                    NotifyPropertyChanged("HeatCapacity");
                }
            }
        }

        //емкость
        private string _volume;
        [Column]
        public string Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (_volume != value)
                {
                    NotifyPropertyChanging("Volume");
                    _volume = value;
                    NotifyPropertyChanged("Volume");
                }
            }
        }

        //теплопроводность
        private string _thermalConductivity;
        [Column]
        public string ThermalConductivity
        {
            get
            {
                return _thermalConductivity;
            }
            set
            {
                if (_thermalConductivity != value)
                {
                    NotifyPropertyChanging("ThermalConductivity");
                    _thermalConductivity = value;
                    NotifyPropertyChanged("ThermalConductivity");
                }
            }
        }

        #endregion

        private string _description;
        [Column]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    NotifyPropertyChanging("Description");
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        private bool _isRadiation;
        [Column]
        public bool IsRadiation
        {
            get
            {
                return _isRadiation;
            }
            set
            {
                if (_isRadiation != value)
                {
                    NotifyPropertyChanging("IsRadiation");
                    _isRadiation = value;
                    NotifyPropertyChanged("IsRadiation");
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