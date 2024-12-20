namespace NSIDictionaryService.Share.DTOs.V006DTOs
{
    public class V006ResponseDTO
    {
        public int Code { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;

        public V006ResponseDTO()
        {
        }
    }
}
