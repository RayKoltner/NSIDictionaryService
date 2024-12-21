using NSIDictionaryService.Data.Models;
using NSIDictionaryService.Data.Models.Dictionaries;
using NSIDictionaryService.Share.DTOs;
using NSIDictionaryService.Share.DTOs.V006DTOs;
using NSIDictionaryService.Share.DTOs.V012DTOs;
using NSIDictionaryService.Share.DTOs.V021DTOs;
using NSIDictionaryService.Share.DTOs.V025DTOs;

namespace NSIDictionaryService.Api.Services.Mappers
{
    public static class UniversalResponseMapper
    {
        public static UploadInfoResponseDTO ConvertToResponse(UploadInfo uploadInfo)
        {
            UploadInfoResponseDTO dto = new UploadInfoResponseDTO()
            {
                Id = uploadInfo.Id,
                UploadingUserId = uploadInfo.UploadingUserId,
                UploadDate = uploadInfo.UploadDate,
                DictCode = uploadInfo.DictCode,
                DictVersionId = uploadInfo.DictVersionId,
                UploadMethodId = uploadInfo.UploadMethodId,
                UploadResultId = uploadInfo.UploadResultId,
                ErrorDescription = uploadInfo.ErrorDescription
            };

            return dto;
        }

        public static V006ResponseDTO ConvertToResponse(V006Dictionary dictionary)
        {
            V006ResponseDTO dto = new V006ResponseDTO()
            {
                Code = dictionary.Code,
                BeginDate = dictionary.BeginDate,
                EndDate = dictionary.EndDate,
                Name = dictionary.Name,
                Comments = dictionary.Comments
            };
            return dto;
        }
        
        public static V012ResponseDTO ConvertToResponse(V012Dictionary dictionary)
        {
            V012ResponseDTO dto = new V012ResponseDTO()
            {
                Code = dictionary.Code,
                BeginDate = dictionary.BeginDate,
                EndDate = dictionary.EndDate,
                Name = dictionary.Name,
                Comments = dictionary.Comments,
                UmpId = dictionary.UMPId
            };
            return dto;
        }

        public static V021ResponseDTO ConvertToResponse(V021Dictionary dictionary)
        {
            V021ResponseDTO dto = new V021ResponseDTO()
            {
                Code = dictionary.Code,
                BeginDate = dictionary.BeginDate,
                EndDate = dictionary.EndDate,
                Name = dictionary.Name,
                Comments = dictionary.Comments,
                PostName = dictionary.PostName,
                PostId = dictionary.PostId
            };
            return dto;
        }

        public static V025ResponseDTO ConvertToResponse(V025Dictionary dictionary)
        {
            V025ResponseDTO dto = new V025ResponseDTO()
            {
                Code = dictionary.Code,
                BeginDate = dictionary.BeginDate,
                EndDate = dictionary.EndDate,
                Name = dictionary.Name,
                Comments = dictionary.Comments
            };
            return dto;
        }

        public static DictVersionResponseDTO ConvertToResponse(DictVersion version, DictCode code)
        {
            DictVersionResponseDTO dto = new DictVersionResponseDTO()
            {
                Id = version.Id,
                DictionaryCodeId = code.Id,
                DictionaryCodeName = code.Name,
                VersionCode = version.VersionCode,
                PublicationDate = version.PublicationDate.ToShortDateString()
            };
            return dto;
        }

        public static DictVersionResponseDTO ConvertToResponse(DictVersion version)
        {
            DictVersionResponseDTO dto = new DictVersionResponseDTO()
            {
                Id = version.Id,
                DictionaryCodeId = version.DictCodeId,
                VersionCode = version.VersionCode,
                PublicationDate = version.PublicationDate.ToShortDateString()
            };
            return dto;
        }
    }
}
