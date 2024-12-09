using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSIDictionaryService.Data.Models
{
    public class UploadResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        // Значения для базы (код/название)
        // 1 Успешно загружен
        // 2 Ошибка
    }
}
