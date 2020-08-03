using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    /// <summary>
    /// Online Sınav Ogrenci Tablosu: Bu tabloya ui tarafında ekleme, silme, güncelleme yapılmaz. Bu tablonun amacı öğrencilerin hangi sınavlara gireceği ve girdikleri sınavlardaki doğru yanlış ve boş sayılarının tutulması ve puanlamanın hesaplanması
    /// </summary>
    public class AossOnlineSinavOgrenci
    {
        public long Id { get; set; }
         [Required]
        public int OgId { get; set; }
         [Required]
        public int OnlineSinavId { get; set; }
        public int DogruS { get; set; }
        public int YanlisS { get; set; }
        public int BosS { get; set; }

        public int Puan { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossOnlineSinavOgrenciController -async -api -m AossOnlineSinavOgrenci -dc AossContext -outDir Controllers
}