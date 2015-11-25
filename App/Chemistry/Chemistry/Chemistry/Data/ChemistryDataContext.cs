using System.Data.Linq;
using Chemistry.Models;
using Chemistry.Models.Reactions;

namespace Chemistry.Data
{
    //Класс, в котором описывается база данных и ее таблицы
    public class ChemistryDataContext : DataContext
    {
        public static string DbConnectionString = "Data Source=isostore:/Chemistry.sdf";

        public ChemistryDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Element> Elements;
        public Table<Substance> Substances;
        public Table<Test> Tests;
        public Table<Compound> Compounds;
        public Table<Decomposition> Decompositions;
        public Table<Exchange> Exchanges;
        public Table<Substitution> Substitutions;
    }
}
