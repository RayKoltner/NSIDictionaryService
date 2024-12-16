﻿namespace NSIDictionaryService.Share.DTOs.V012DTOs
{
    public class V012PutDTO
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Comments { get; set; } = string.Empty;

        public int DictVersionId { get; set; }

        public int UmpId { get; set; }
    }
}
