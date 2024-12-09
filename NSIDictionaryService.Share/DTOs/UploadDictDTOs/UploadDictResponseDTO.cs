using NSIDictionaryService.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSIDictionaryService.Share.DTOs.UploadDictDTOs
{
    public class UploadDictResponseDTO
    {
        public int UploadingUserId { get; set; }

        public DateTime UploadDate { get; set; }

        public string DictCode { get; set; } = string.Empty;

        public int DictVersionId { get; set; }

        public int UploadMethodId { get; set; }

        public int UploadResultId { get; set; }

        public string ErrorDescription { get; set; } = String.Empty;

        public DateTime CreateDate { get; set; }
        public int EditUserId { get; set; }
        public DateTime EditDate { get; set; }
        public int DeletedUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

        public UploadDictResponseDTO(UploadDict uploadDict)
        {
            this.UploadingUserId = uploadDict.UploadingUserId;
            this.UploadDate = uploadDict.UploadDate;
            this.DictCode = uploadDict.DictCode;
            this.DictVersionId = uploadDict.DictVersionId;
            this.UploadMethodId = uploadDict.UploadMethodId;
            this.UploadResultId = uploadDict.UploadResultId;
            this.ErrorDescription = uploadDict.ErrorDescription;

            this.CreateDate = uploadDict.CreateDate;
            this.EditUserId = uploadDict.EditUserId;
            this.EditDate = uploadDict.EditDate;
            this.DeletedUserId = uploadDict.DeletedUserId;
            this.DeletedDate = uploadDict.DeletedDate;
            this.IsDeleted = uploadDict.IsDeleted;
        }

    }
}
