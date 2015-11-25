using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Chemistry.Common;
using Chemistry.Models;
using Chemistry.Models.Reactions;

namespace Chemistry.Data
{
    class Database
    {
        //Подключение к базе данных
        private readonly ChemistryDataContext _chemistryDb = new ChemistryDataContext(ChemistryDataContext.DbConnectionString);

        //Текущая локаль
        private readonly string _culture = App.Settings.Language.LanguagesForDisplay[App.Settings.Language.SelectedIndex].Name;

        internal void Load()
        {
            LoadElements();
            LoadSubstances();
            LoadReactions();
            LoadControls();
            _chemistryDb.SubmitChanges();
        }

        //Загрузка элементов
        private void LoadElements()
        {
            //Чтение их xml-файла, соответствующей локали
            var xElement = XElement.Load(String.Format("Data/Xml/Elements/Elements_{0}.xml", _culture));
            var xmlData =
                from element in xElement.Descendants("Element")
                select new Element
                    {
                        Type = element.Attribute("Type").Value,
                        Number = Int32.Parse(element.Attribute("Number").Value),
                        Name = element.Attribute("Name").Value,
                        LatName = element.Attribute("LatName").Value,
                        Alias = element.Attribute("Alias").Value,
                        M = element.Attribute("M").Value,
                        Radius = element.Attribute("Radius").Value,
                        KovRadius = element.Attribute("KovRadius").Value,
                        IonRadius = element.Attribute("IonRadius").Value,
                        Electronegativity = element.Attribute("Electronegativity").Value,
                        Oxidation = element.Attribute("Oxidation").Value,
                        Energy = element.Attribute("Energy").Value,
                        Density = element.Attribute("Density").Value,
                        TemperatureMelting = element.Attribute("TemperatureMelting").Value,
                        TemperatureBoiling = element.Attribute("TemperatureBoiling").Value,
                        HeatMelting = element.Attribute("HeatMelting").Value,
                        HeatEvaporation = element.Attribute("HeatEvaporation").Value,
                        HeatCapacity = element.Attribute("HeatCapacity").Value,
                        Volume = element.Attribute("Volume").Value,
                        ThermalConductivity = element.Attribute("ThermalConductivity").Value,
                        Description = element.Attribute("Description").Value,
                        IsRadiation = Helper.RadiationElements.Contains(element.Attribute("Alias").Value)
                    };
            var elements = new ObservableCollection<Element>(xmlData);

            //Добавление данных в базу
            _chemistryDb.Elements.InsertAllOnSubmit(elements);
        }

        //Загрузка веществ
        private void LoadSubstances()
        {
            //Чтение их xml-файла, соответствующей локали
            var xElement = XElement.Load(String.Format("Data/Xml/Substances/Substances_{0}.xml", _culture));
            var xmlData =
                from element in xElement.Descendants("Substance")
                select new Substance
                {
                    Type = element.Attribute("Type").Value,
                    SubType = element.Attribute("SubType").Value,
                    Name = element.Attribute("Name").Value,
                    Alias = element.Attribute("Alias").Value,
                    Description = element.Attribute("Description").Value
                };
            var substances = new ObservableCollection<Substance>(xmlData);

            //Добавление данных в базу
            _chemistryDb.Substances.InsertAllOnSubmit(substances);
        }

        //Загрузка реакций
        private void LoadReactions()
        {
            var xElement = XElement.Load("Data/Xml/Reactions/Reactions.xml");

            // СОЕДИНЕНИЕ
            var xmlDataCompound =
                from element in xElement.Descendants("Compound")
                select new Compound
                {
                    K1 = element.Attribute("K1").Value,
                    Subs1 = element.Attribute("Subs1").Value,
                    K12 = element.Attribute("K12").Value,
                    K2 = element.Attribute("K2").Value,
                    Subs2 = element.Attribute("Subs2").Value,
                    K22 = element.Attribute("K22").Value,
                    Katalizator = element.Attribute("Katalizator").Value,
                    K = element.Attribute("K").Value,
                    Substance = element.Attribute("Substance").Value,
                    SubstanceAd = element.Attribute("SubstanceAd").Value
                };
            var connections = new ObservableCollection<Compound>(xmlDataCompound);

            //Добавление данных в базу
            _chemistryDb.Compounds.InsertAllOnSubmit(connections);

            // РАЗЛОЖЕНИЕ
            var xmlDataDecomposition =
                from element in xElement.Descendants("Decomposition")
                select new Decomposition
                {
                    K = element.Attribute("K").Value,
                    Substance = element.Attribute("Substance").Value,
                    Katalizator = element.Attribute("Katalizator").Value,
                    K1 = element.Attribute("K1").Value,
                    Subs1 = element.Attribute("Subs1").Value,
                    Subs1Ad = element.Attribute("Subs1Ad").Value,
                    K12 = element.Attribute("K12").Value,
                    K2 = element.Attribute("K2").Value,
                    Subs2 = element.Attribute("Subs2").Value,
                    K22 = element.Attribute("K22").Value,
                    Subs2Ad = element.Attribute("Subs2Ad").Value
                };
            var decompositions = new ObservableCollection<Decomposition>(xmlDataDecomposition);

            //Добавление данных в базу
            _chemistryDb.Decompositions.InsertAllOnSubmit(decompositions);

            // ОБМЕН
            var xmlDataExchange =
                from element in xElement.Descendants("Exchange")
                select new Exchange
                {
                    K1 = element.Attribute("K1").Value,
                    Subs1 = element.Attribute("Subs1").Value,
                    K12 = element.Attribute("K12").Value,
                    K2 = element.Attribute("K2").Value,
                    Subs2 = element.Attribute("Subs2").Value,
                    K22 = element.Attribute("K22").Value,
                    Katalizator = element.Attribute("Katalizator").Value,
                    K3 = element.Attribute("K3").Value,
                    Subs3 = element.Attribute("Subs3").Value,
                    Subs3Ad = element.Attribute("Subs3Ad").Value,
                    K32 = element.Attribute("K32").Value,
                    K4 = element.Attribute("K4").Value,
                    Subs4 = element.Attribute("Subs4").Value,
                    K42 = element.Attribute("K42").Value,
                    Subs4Ad = element.Attribute("Subs4Ad").Value,
                };
            var exchanges = new ObservableCollection<Exchange>(xmlDataExchange);

            //Добавление данных в базу
            _chemistryDb.Exchanges.InsertAllOnSubmit(exchanges);

            // ЗАМЕЩЕНИЕ
            var xmlDataReplace =
                from element in xElement.Descendants("Substitution")
                select new Substitution
                {
                    K1 = element.Attribute("K1").Value,
                    Subs1 = element.Attribute("Subs1").Value,
                    K12 = element.Attribute("K12").Value,
                    K2 = element.Attribute("K2").Value,
                    Subs2 = element.Attribute("Subs2").Value,
                    K22 = element.Attribute("K22").Value,
                    Katalizator = element.Attribute("Katalizator").Value,
                    K3 = element.Attribute("K3").Value,
                    Subs3 = element.Attribute("Subs3").Value,
                    Subs3Ad = element.Attribute("Subs3Ad").Value,
                    K32 = element.Attribute("K32").Value,
                    K4 = element.Attribute("K4").Value,
                    Subs4 = element.Attribute("Subs4").Value,
                    K42 = element.Attribute("K42").Value,
                    Subs4Ad = element.Attribute("Subs4Ad").Value
                };
            var replaces = new ObservableCollection<Substitution>(xmlDataReplace);

            //Добавление данных в базу
            _chemistryDb.Substitutions.InsertAllOnSubmit(replaces);
        }

        //Загрузка тестов
        private void LoadControls()
        {
            //Чтение их xml-файла, соответствующей локали
            var number = 0;
            var xElement = XElement.Load(String.Format("Data/Xml/Tests/Tests_{0}.xml", _culture));
            var xmlData =
                from element in xElement.Descendants("Test")
                select new Test
                {
                    Number = ++number,
                    Name = element.Value
                };
            var tests = new ObservableCollection<Test>(xmlData);

            //Добавление данных в базу
            _chemistryDb.Tests.InsertAllOnSubmit(tests);
        }

        #region ЕЛЕМЕНТЫ

        private static ObservableCollection<Element> _elements;

        //Получение всех элементов
        public static ObservableCollection<Element> GetElements()
        {
            if (_elements == null)
            {
                var elementsInDb = from Element element in App.ChemistryDb.Elements
                    orderby element.Number
                    select element;
                _elements = new ObservableCollection<Element>(elementsInDb);
            }

            return _elements;
        }

        //Получение всех элементов конкретного типа
        public static ObservableCollection<Element> GetElements(string type)
        {
            return new ObservableCollection<Element>(GetElements().Where(e => e.Type == type));
        }

        //Получение элемента по обозначению
        public static Element GetElementByAlias(string alias)
        {
            return GetElements().FirstOrDefault(e => e.Alias == alias);
        }

        //Получение элемента по названию
        public static Element GetElementByName(string name)
        {
            return GetElements().FirstOrDefault(e => e.Name == name);
        }

        //Получение элемента по номеру
        public static Element GetElementByNumber(int number)
        {
            return GetElements().FirstOrDefault(e => e.Number == number);
        }

        //Получение всех типов элементов        
        public static IEnumerable<string> GetElementTypes()
        {
            var typesInDB = from Element elem in App.ChemistryDb.Elements
                            orderby elem.Type
                            select elem.Type;
            return new List<string>(typesInDB).Distinct();
        }

        //Получение 10 случайных элементов для теста
        public static IEnumerable<IComponents> GetTenRandomElements()
        {
            var elementsList = new List<IComponents>();
            foreach (var randomNumber in Helper.GetRandomNumbers(App.ChemistryDb.Elements.Count()))
            {
                var elementInDB = from element in App.ChemistryDb.Elements
                                  where element.Id == randomNumber
                                  select element;
                elementsList.Add(elementInDB.First());
            }
            return elementsList;
        }

        //Получение обозначений всех элементов
        public static List<string> GetElementAlias()
        {
            return GetElements().Select(e => e.Alias).ToList();
        }

        #endregion

        #region ВЕЩЕСТВА

        private static ObservableCollection<Substance> _substances;

        //Получение всех веществ
        public static ObservableCollection<Substance> GetSubstances()
        {
            if (_substances == null)
            {
                var substancesInDb = from Substance substance in App.ChemistryDb.Substances
                    orderby substance.Alias
                    select substance;
                _substances = new ObservableCollection<Substance>(substancesInDb);
            }

            return _substances;
        }

        //Получение всех веществ конкретной группы
        public static ObservableCollection<Substance> GetSubstances(string type)
        {
            return new ObservableCollection<Substance>(GetSubstances().Where(s => s.Type == type));
        }

        //Получение всех веществ конкретной группы и подгруппы
        public static ObservableCollection<Substance> GetSubstances(string type, string subType)
        {
            return new ObservableCollection<Substance>(GetSubstances(type).Where(s => s.SubType == subType));
        }

        //Получение всех веществ по запрашиваемому названию или формуле
        public static ObservableCollection<Substance> GetSearchingSubstances(string query)
        {
            return new ObservableCollection<Substance>(
                GetSubstances()
                .Where(s => s.Name.ToUpper().IndexOf(query) > -1 || s.Alias.ToUpper().IndexOf(query) > -1)
                .OrderBy(s => s.Alias));
        }

        //Получение вещества по формуле
        public static Substance GetSubstanceByAlias(string alias)
        {
            return GetSubstances().FirstOrDefault(s => s.Alias == alias);
        }

        //Получение вещества по названию
        public static Substance GetSubstanceByName(string name)
        {
            return GetSubstances().FirstOrDefault(s => s.Alias == name);
        }

        //Получение всех групп веществ  
        public static IEnumerable<string> GetSubstanceTypes()
        {
            return GetSubstances().Select(s => s.Type).Distinct();
        }

        //Получение всех подгрупп веществ конкретной группы
        public static IEnumerable<string> GetSubstanceSubTypes(string type)
        {
            return GetSubstances().Where(s => s.Type == type && !String.IsNullOrEmpty(s.SubType)).Select(s => s.SubType).Distinct();
        }

        //Получение 10 случайных веществ для теста
        public static IEnumerable<IComponents> GetTenRandomSubstances()
        {
            var substancesList = new List<IComponents>();
            foreach (var randomNumber in Helper.GetRandomNumbers(App.ChemistryDb.Substances.Count()))
            {
                var substanceInDB = from substance in App.ChemistryDb.Substances
                                    where substance.Id == randomNumber
                                    select substance;
                substancesList.Add(substanceInDB.First());
            }
            return substancesList;
        }

        //Получение формул всех веществ
        public static List<string> GetSubstanceAlias()
        {
            return GetSubstances().Select(s => s.Alias).ToList();
        }

        #endregion

        #region ЭЛЕМЕНТЫ + ВЕЩЕСТВА

        //Получение списка всех элементов и веществ
        public static ObservableCollection<IComponents> GetComponents()
        {
            var components = new ObservableCollection<IComponents>();
            foreach (var element in GetElements())
                components.Add(element);
            foreach (var substance in GetSubstances())
                components.Add(substance);
            return components;
        }

        //Получение элемента или вещества по обозначению или формуле
        public static IComponents GetComponent(string alias)
        {
            if (GetElementAlias().IndexOf(alias) > -1) return GetElementByAlias(alias);
            if (GetSubstanceAlias().IndexOf(alias) > -1) return GetSubstanceByAlias(alias);
            return null;
        }

        #endregion

        #region РЕАКЦИИ

        // СОЕДИНЕНИЕ
        private static ObservableCollection<Compound> _compounds;

        //Получение списка всех реакций
        public static ObservableCollection<Compound> GetCompounds()
        {
            if (_compounds == null)
            {
                var compoundsInDb = from Compound compound in App.ChemistryDb.Compounds
                    orderby compound.Substance
                    select compound;
                _compounds = new ObservableCollection<Compound>(compoundsInDb);
            }

            return _compounds;
        }

        //Получение реакций по исходным веществам
        public static ObservableCollection<Compound> GetCompounds(string element1, string element2)
        {
            return new ObservableCollection<Compound>(GetCompounds()
                .Where(c => (c.Subs1 == element1 && c.Subs2 == element2) || (c.Subs1 == element2 && c.Subs2 == element1)));
        }

        //Получение реакции по результирующему веществу
        public static Compound GetCompound(string alias)
        {
            return GetCompounds().FirstOrDefault(c => c.Substance == alias);
        }

        // РАЗЛОЖЕНИЕ
        private static ObservableCollection<Decomposition> _decompositions;

        //Получение списка всех реакций
        public static ObservableCollection<Decomposition> GetDecompositions()
        {
            if (_decompositions == null)
            {
                var decompositionsInDb = from Decomposition decomposition in App.ChemistryDb.Decompositions
                    orderby decomposition.Substance
                    select decomposition;
                _decompositions = new ObservableCollection<Decomposition>(decompositionsInDb);
            }

            return _decompositions;
        }

        //Получение реакций по исходному веществу
        public static ObservableCollection<Decomposition> GetDecompositions(string alias)
        {
            return new ObservableCollection<Decomposition>(GetDecompositions().Where(d => d.Substance == alias));
        }

        // ОБМЕН
        private static ObservableCollection<Exchange> _exchanges;

        //Получение списка всех реакций
        public static ObservableCollection<Exchange> GetExchanges()
        {
            if (_exchanges == null)
            {
                var exchangesInDb = from Exchange exchange in App.ChemistryDb.Exchanges
                    select exchange;
                _exchanges = new ObservableCollection<Exchange>(exchangesInDb);
            }

            return _exchanges;
        }

        //Получение реакций по исходным веществам
        public static ObservableCollection<Exchange> GetExchanges(string element1, string element2)
        {
            return new ObservableCollection<Exchange>(GetExchanges()
                .Where(e => (e.Subs1 == element1 && e.Subs2 == element2) || (e.Subs1 == element2 && e.Subs2 == element1)));
        }

        // ЗАМЕЩЕНИЕ
        private static ObservableCollection<Substitution> _substitutions;

        //Получение списка всех реакций
        public static ObservableCollection<Substitution> GetSubstitutions()
        {
            if (_substitutions == null)
            {
                var replacesInDb = from Substitution substitution in App.ChemistryDb.Substitutions
                    select substitution;
                _substitutions = new ObservableCollection<Substitution>(replacesInDb);
            }

            return _substitutions;
        }

        //Получение реакций по исходным веществам
        public static ObservableCollection<Substitution> GetSubstitutions(string element1, string element2)
        {
            return new ObservableCollection<Substitution>(GetSubstitutions()
                .Where(s => (s.Subs1 == element1 && s.Subs2 == element2) || (s.Subs1 == element2 && s.Subs2 == element1)));
        }

        //Получение 10 случайных реакций для теста
        public static IEnumerable<IComponents> GetTenRandomReaction()
        {
            var reactionsList = new List<IComponents>();

            int coutNumbers = App.ChemistryDb.Compounds.Count();
            if (coutNumbers > App.ChemistryDb.Decompositions.Count())
                coutNumbers = App.ChemistryDb.Decompositions.Count();
            if (coutNumbers > App.ChemistryDb.Exchanges.Count())
                coutNumbers = App.ChemistryDb.Exchanges.Count();
            if (coutNumbers > App.ChemistryDb.Substitutions.Count())
                coutNumbers = App.ChemistryDb.Substitutions.Count();

            foreach (var randomNumber in Helper.GetRandomNumbers(coutNumbers))
            {
                var random = new Random();
                var reactionType = random.Next(0, 4);

                switch (reactionType)
                {
                    // соединение
                    case 0:
                        {
                            var reactionInDb = from compound in App.ChemistryDb.Compounds
                                               where compound.Id == randomNumber
                                               select compound;
                            Compound reaction = reactionInDb.First();
                            IComponents component = new Substance();
                            component.Alias = string.Concat(reaction.K1, reaction.Subs1, reaction.K12, "+", reaction.K2, reaction.Subs2, reaction.K22, "→?");
                            component.Name = string.Concat(reaction.K, reaction.Substance);
                            reactionsList.Add(component);
                        }
                        break;
                    // разложение
                    case 1:
                        {
                            var reactionInDb = from decomposition in App.ChemistryDb.Decompositions
                                               where decomposition.Id == randomNumber
                                               select decomposition;
                            Decomposition reaction = reactionInDb.First();
                            IComponents component = new Substance();
                            component.Alias = string.Concat("?→", reaction.K1, reaction.Subs1, reaction.K12, "+", reaction.K2, reaction.Subs2, reaction.K22);
                            component.Name = string.Concat(reaction.K, reaction.Substance);
                            reactionsList.Add(component);
                        }
                        break;
                    // обмен
                    case 2:
                        {
                            var reactionInDb = from exchange in App.ChemistryDb.Exchanges
                                               where exchange.Id == randomNumber
                                               select exchange;
                            Exchange reaction = reactionInDb.First();
                            IComponents component = new Substance();
                            component.Alias = string.Concat(reaction.K1, reaction.Subs1, reaction.K12, "+", reaction.K2, reaction.Subs2, reaction.K22, "→?+", reaction.K4, reaction.Subs4, reaction.K42);
                            component.Name = string.Concat(reaction.K3, reaction.Subs3, reaction.K32);
                            reactionsList.Add(component);
                        }
                        break;
                    // замещение
                    case 3:
                        {
                            var reactionInDb = from substitution in App.ChemistryDb.Substitutions
                                               where substitution.Id == randomNumber
                                               select substitution;
                            Substitution reaction = reactionInDb.First();
                            IComponents component = new Substance();
                            component.Alias = string.Concat(reaction.K1, reaction.Subs1, reaction.K12, "+", reaction.K2, reaction.Subs2, reaction.K22, "→", reaction.K3, reaction.Subs3, reaction.K32, "+?");
                            component.Name = string.Concat(reaction.K4, reaction.Subs4, reaction.K42);
                            reactionsList.Add(component);
                        }
                        break;
                }
            }

            return reactionsList;
        }

        #endregion

        #region КОНТРОЛЬ

        private static ObservableCollection<Test> _tests;

        //Получение списка всех тестов
        public static ObservableCollection<Test> GetTests()
        {
            if (_tests == null)
            {
                var testsInDb = from Test test in App.ChemistryDb.Tests
                                select test;
                _tests = new ObservableCollection<Test>(testsInDb);
            }

            return _tests;
        }

        //Получение теста по номеру
        public static Test GetTest(int number)
        {
            return GetTests().FirstOrDefault(t => t.Number == number);
        }

        #endregion
    }
}
