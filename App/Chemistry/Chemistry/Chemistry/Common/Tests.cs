using System.Collections.Generic;
using Chemistry.Data;
using Chemistry.Models;
using Chemistry.Resources;

namespace Chemistry.Common
{
    public static class Tests
    {
        //Получение 10 случайных компонентов для теста в зависимости от номера теста
        public static IEnumerable<IComponents> GetTenRandomComponents(int testNumber)
        {
            switch (testNumber)
            {
                case 1:
                    return Database.GetTenRandomElements();
                case 2:
                    return Database.GetTenRandomElements();
                case 3:
                    return Database.GetTenRandomSubstances();
                case 4:
                    return Database.GetTenRandomReaction();
                default:
                    return null;
            }
        }

        //Получение конкретного вопроса (тип вопроса, содержимое вопроса, содержимое верного ответа) по номеру теста
        public static void GetTest(int testNumber, IComponents component, ref string typeQuestion, ref string nameQuestion, ref string nameAnswer)
        {
            switch (testNumber)
            {
                case 1:
                    typeQuestion = AppResources.Designation;
                    nameQuestion = component.Alias;
                    nameAnswer = component.Name;
                    break;
                case 2:
                    typeQuestion = AppResources.Name;
                    nameQuestion = component.Name;
                    nameAnswer = component.Alias;
                    break;
                case 3:
                    typeQuestion = AppResources.Formula;
                    nameQuestion = component.Alias;
                    nameAnswer = component.Name;
                    break;
                case 4:
                    typeQuestion = AppResources.Formula;
                    nameQuestion = component.Alias; // реакция с пропуском вещества
                    nameAnswer = component.Name; // недостающее вещество
                    break;
            }
        }
    }
}
