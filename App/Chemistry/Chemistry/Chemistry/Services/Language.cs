using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Chemistry.Services
{
    //Класс для работы с языками
    public class Language
    {
        //Свойтсво для отображения языков в выпадающем списке
        //Оно необходимо для того, что в списке, например, выпадало не "русский (Росиия)", а "русский"
        public ObservableCollection<CultureInfo> LanguagesForDisplay = new ObservableCollection<CultureInfo>
        {
            new CultureInfo("en"),
            new CultureInfo("ru"),
            new CultureInfo("es")
        };

        public ObservableCollection<CultureInfo> Languages = new ObservableCollection<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("ru-RU"),
            new CultureInfo("es-ES")
        };

        //По умолчанию в приложении установлен английский язык
        //Список языков, по которому проверяется язык системы на телефоне. Если он есть в этом списке, то язык приложения при первом запуске меняется на русский 
        public List<string> RussianUICulture = new List<string> { "ru", "ru-RU", "ru-TJ", "ru-TM", "uk-UA", "be-BY", "kk-KZ", "bg-BG", "uz-Latn-UZ", "lv-LV", "lt-LT" };
        //Список языков, по которому проверяется язык системы на телефоне. Если он есть в этом списке, то язык приложения при первом запуске меняется на испанский 
        public List<string> SpanishUICulture = new List<string> { "es", "es-ES", "es-US", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-EC", "es-SV", "es-GT", "es-HN", "es-MX", "es-NI", "es-PY", "es-PE", "es-DO", "es-VE", "eu-ES", "ca-ES", "gl-ES" };

        //Индекс элемента в списке языков, который выбрал пользователь, но не перезапустил приложение для его применения
        public int SelectingIndex { get; set; }
        //Индекс элемента в списке языков, который установлен в текущий момент
        public int SelectedIndex { get; set; }
        //Изменен ли язык
        public bool IsChanged { get; set; }
    }
}
