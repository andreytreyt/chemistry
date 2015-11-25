namespace Chemistry.Models
{
    //Общий интерфецс для элементов и веществ. Используется в реакциях обмена (ExchangePage) и замещения (SubstitutionPage).
    public interface IComponents
    {
        //обозначение
        string Alias { get; set; }
        //название
        string Name { get; set; }
    }
}
