namespace NSIDictionaryService.Share.DTOs.V025DTOs
{
    public class V025DTO
    {
        public decimal Code { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;

        public int DictVersionId { get; set; }
    }
}
