namespace NSIDictionaryService.Share.DTOs.V021DTOs
{
    public class V021ResponseDTO
    {
        public int Id { get; set; }
        public int Code { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;

        public string PostName { get; set; } = String.Empty;

        public int PostId { get; set; }

        public int DictVersionId { get; set; }

        public V021ResponseDTO()
        {
        }
    }
}
