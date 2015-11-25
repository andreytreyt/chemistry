using Chemistry.Services;

namespace Chemistry.ViewModels
{
    //Класс настроек, который используется на странице "SettingsPage"
    public sealed class Settings
    {
        //Язык приложения
        private Language _language;
        public Language Language
        {
            get
            {
                return _language ?? (_language = new Language());
            }
        }
    }
}
