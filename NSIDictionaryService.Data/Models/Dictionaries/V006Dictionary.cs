namespace NSIDictionaryService.Data.Models.Dictionaries
{
    public class V006Dictionary:BaseDictionaryType
    {
        public override string ToString()
        {
            return $"Код: {this.Code}, Наименование: {this.Name}, Дата начала: {this.BeginDate}, Дата окончания: {this.EndDate}";
        }
    }
}
