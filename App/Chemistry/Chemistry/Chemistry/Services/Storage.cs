using System.Collections;
using System.IO.IsolatedStorage;

namespace Chemistry.Services
{
    //Класс для работы с изолированным хранилищем
    //Сейчас он используется для сохранения выбранного пользователем языка приложения
    public class Storage
    {
        private static readonly IDictionary Settings = IsolatedStorageSettings.ApplicationSettings;

        //Количество элементов, хранящихся в изолированном хранилище
        public int Count()
        {
            return Settings.Count;
        }

        //Очистка изолированного хванилища
        public void Clear()
        {
            Settings.Clear();
        }

        //Существует ли элемент с таким ключем
        public bool Exists(string key)
        {
            return Settings.Contains(key);
        }

        //Сохранение элемента по ключу
        //Если елемент с таким ключем уже есть, то он сохраняется, а если нет, то он добавляется
        public void Save(string key, object value)
        {
            if (Settings.Contains(key)) Settings[key] = value;
            else Settings.Add(key, value);
        }

        //Удаление элемента по ключу
        public void Remove(string key)
        {
            if (Settings.Contains(key)) Settings.Remove(key);
        }

        //Загрузка элемента по ключу
        //Если элемент с таким ключем есть, то возвращается его значение, а если нет, то возвращается null
        public object Load(string key)
        {
            return Settings.Contains(key) ? Settings[key] : null;
        }
    }
}
