using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    /// <summary>
    /// Online Sınav Tablosu: Bu tabloya ui tarafında ekleme, silme, güncelleme yapılır. Sınav oluşturulurken ilk doldurulması gereken tablodur.
    /// </summary>
    public class AossOnlineSinav
    {

        /// <summary>
        /// Online sınavın idsi   bu veri eklenememeli çünkü kensidi otomatik oluşur.
        /// </summary>
        public long Id { get; set; }
         /// <summary>
        /// Sınavın başlama tarihi
        /// </summary>    
         [Required]
        public string Baslama { get; set; }
        /// <summary>
        /// Sınavın bitiş tarihi
        /// </summary>
         [Required]
        public string Bitis { get; set; }
        /// <summary>
        /// Sınav süresi
        /// </summary>
         [Required]
        public string Sure { get; set; }
        /// <summary>
        /// Soınavı oluşturan hocanın alanı
        /// </summary>
         [Required]
        public string Alan { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossOnlineSinavController -async -api -m AossOnlineSinav -dc AossContext -outDir Controllers
}