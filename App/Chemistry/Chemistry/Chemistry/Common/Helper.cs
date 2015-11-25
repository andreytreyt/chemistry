using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Chemistry.Data;

namespace Chemistry.Common
{
    static class Helper
    {
        //Список радиоактивных элементов
        public static readonly List<string> RadiationElements =
            new List<string>{"Tc", "Pm", "Bi", "Po", "At", "Rn", "Fr", "Ra", 
                "Ac", "Th", "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es", "Fm", "Md", "No", "Lr",
                "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Cn"};

        //Проверка и получение имени 3d-модели
        public static string CheckModel(string nameModel)
        {
            string name = string.Empty;
            name = nameModel.Substring(nameModel.IndexOf("/") + 1);

            // Элементы со схожей решеткой к H (водороду)
            var elementsModelH = new List<string> { "H", "Se", "N", "He", "Te", "Be", "Mg", "La", "Pr", "Nd", "Pm", "Gd", "Td", "Dy", "Ho", "Er", "Tm", "Lu", "Am", "Cm", "Bk", "Cf", "Sc", "Ti", "Co", "Zn", "Y", "Zr", "Tc", "Ru", "Cd", "Hf", "Re", "Os", "Tl" };
            // Элементы со схожей решеткой к O (кислороду)
            var elementsModelO = new List<string> { "O", "F", "Cl", "Br", "I", "Po" };
            // Элементы со схожей решеткой к S (сере)
            var elementsModelS = new List<string> { "S", "Pa", "Ga", "U" };
            // Элементы со схожей решеткой In  (индию)
            var elementsModelIn = new List<string> { "In", "Sn" };
            // Элементы со схожей решеткой к Ne (неону)
            var elementsModelNe = new List<string> { "Ne", "Ar", "Kr", "Xe", "Rn", "Ca", "Sr", "Ce", "Yb", "Ac", "Th", "Es", "Ni", "Cu" };
            // Элементы со схожей решеткой к C (углероду)
            var elementsModelC = new List<string> { "C", "Si", "Ge" };
            // Элементы со схожей решеткой к As (мышьяку)
            var elementsModelAs = new List<string> { "As", "Sb", "Sm", "Pu", "Hg", "Bi" };
            // Элементы со схожей решеткой к Li (литию)
            var elementsModelLi = new List<string> { "Li", "Na", "K", "Rb", "Cs", "Fr", "Ba", "Ra", "Eu", "Np", "Fm", "Md", "No", "Lr", "V", "Cr", "Mn", "Fe", "Nb", "Mo", "Rh", "Pd", "Ag", "Ta", "W", "Ir", "Pt", "Au", "Al", "Pb" };
            // Элементы со схожей решеткой к Rf (резерфордию) НА САМОМ ДЕЛЕ НЕИЗВЕСТНО КАКИЕ У НИХ КРИСТАЛЛИЧЕСКИЕ РЕШЕТКИ
            var elementsModelRf = new List<string> { "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Cn" };


            if (elementsModelH.IndexOf(name) > -1)
                nameModel = "ModelElement/H";
            else if (elementsModelIn.IndexOf(name) > -1)
                nameModel = "ModelElement/In";
            else if (elementsModelO.IndexOf(name) > -1)
                nameModel = "ModelElement/O";
            else if (elementsModelS.IndexOf(name) > -1)
                nameModel = "ModelElement/S";
            else if (elementsModelNe.IndexOf(name) > -1)
                nameModel = "ModelElement/Ne";
            else if (elementsModelC.IndexOf(name) > -1)
                nameModel = "ModelElement/C";
            else if (elementsModelAs.IndexOf(name) > -1)
                nameModel = "ModelElement/As";
            else if (elementsModelLi.IndexOf(name) > -1)
                nameModel = "ModelElement/Li";
            else if (elementsModelRf.IndexOf(name) > -1)
                nameModel = "ModelElement/Rf";

            return nameModel;
        }

        //Получение 10 случайных чисел для получения 10 случайных элементов, веществ или реакций
        public static int[] GetRandomNumbers(int max)
        {
            var randomNumbers = new int[10];
            var random = new Random();
            randomNumbers[0] = random.Next(1, max);
            int i = 1;
            while (i < randomNumbers.Count())
            {
                bool next = true;
                int r = random.Next(1, max);
                // Проверка на повтор элемента
                foreach (var randomNumber in randomNumbers)
                {
                    if (r == randomNumber)
                    {
                        next = false;
                        break;
                    }
                }
                if (next)
                {
                    randomNumbers[i] = r;
                    ++i;
                }
            }
            return randomNumbers;
        }

        #region Formula

        //Формирование формулы вещетсва
        public static StackPanel SubstanceFormula(string alias, int size)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

            if (String.CompareOrdinal(alias, string.Empty) == 0) return stackPanel;

            int k = 0;
            int j;
            while (Int32.TryParse(alias[k].ToString(), out j))
            {
                stackPanel.Children.Add(FormulaText(alias[k].ToString(), size));
                k++;
            }
            alias = alias.Remove(0, k);

            foreach (char symbol in alias)
            {
                int i;
                if (Int32.TryParse(symbol.ToString(), out i))
                    stackPanel.Children.Add(FormulaText(symbol.ToString(), size / 2));
                else
                    stackPanel.Children.Add(FormulaText(symbol.ToString(), size));
            }
            return stackPanel;
        }

        //Формирование формулы реакции
        public static StackPanel ReactionFormula(string alias, int size)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            var aliasList = new List<string>();

            int pos1 = alias.IndexOf('+'), 
                pos2 = alias.IndexOf('→');

            int pos;
            if (pos1 < pos2)
            {
                pos = alias.IndexOf('+') + 1;
                aliasList.Add(alias.Remove(pos)); // добавляем первое в-во
                alias = alias.Remove(0, pos);

                pos = alias.IndexOf('→') + 1;
                aliasList.Add(alias.Remove(pos)); // добавляем второе в-во
                alias = alias.Remove(0, pos);

                
                if (alias.IndexOf('+') > -1) // проверка на два в-ва
                {
                    pos = alias.IndexOf('+') + 1;
                    aliasList.Add(alias.Remove(pos)); // если два в-ва, то добавляем третье в-во
                    alias = alias.Remove(0, pos);
                }
                aliasList.Add(alias); // добавляем последнее в-во
            }
            else
            {
                pos = alias.IndexOf('→') + 1;
                aliasList.Add(alias.Remove(pos)); // добавляем первое в-во
                alias = alias.Remove(0, pos);

                pos = alias.IndexOf('+') + 1;
                aliasList.Add(alias.Remove(pos)); // добавляем второе в-во
                alias = alias.Remove(0, pos);

                aliasList.Add(alias); // добавляем третье в-во
            }

            foreach (var aliasItem in aliasList)
                stackPanel.Children.Add(SubstanceFormula(aliasItem, size));

            return stackPanel;
        }

        //Получение текста формулы
        public static TextBlock FormulaText(string text, int size)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = size,
                VerticalAlignment = VerticalAlignment.Bottom,
                Foreground = (Brush)Application.Current.Resources["PhoneContrastBackgroundBrush"]
            };
        }

        //Получение текста катализатора
        public static TextBlock KatalizatorText(string text, int size)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = size,
                Margin = new Thickness(-(size * 4.5), -10, 0, 0),
                TextAlignment = TextAlignment.Center,
                Foreground = (Brush)Application.Current.Resources["PhoneContrastBackgroundBrush"]
            };
        }

        #endregion

        //Определение и получение сроки навигации в зависимости от нажатия пользователя на часть формулы в реакциях (элемент или вещество)
        public static string GetNavigationString(string alias)
        {
            return Database.GetElementAlias().IndexOf(alias) > -1 ? "/Views/ElementPage.xaml?alias=" : "/Views/SubstancePage.xaml?alias=";
        }

        //Очистка текста от лишних знаков для проверки правильности ответа в тесте
        public static string ClearText(string text)
        {
            return text.Replace(" ", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty).ToUpper();
        }
    }
}
