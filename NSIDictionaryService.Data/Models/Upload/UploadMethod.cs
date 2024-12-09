using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSIDictionaryService.Data.Models
{
    public class UploadMethod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        // Значения для базы (код\название):
        // 1 Ручная загрузка
        // 2 Загрузка из APi
        // 3 Загрузка из файла
    }
}
