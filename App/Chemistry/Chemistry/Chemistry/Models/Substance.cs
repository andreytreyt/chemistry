using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace Chemistry.Models
{
    //Вещество. Здесь все свойства должны быть понятны
    [Table]
    public class Substance : INotifyPropertyChanged, INotifyPropertyChanging, IComponents
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

        private string _subType;
        [Column]
        public string SubType
        {
            get
            {
                return _subType;
            }
            set
            {
                if (_subType != value)
                {
                    NotifyPropertyChanging("SubType");
                    _subType = value;
                    NotifyPropertyChanged("SubType");
                }
            }
        }

        private string _name;
        [Column]
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

        private string _alias;
        [Column]
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