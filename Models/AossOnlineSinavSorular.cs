using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
     /// <summary>
    /// Online Sınav  Sorular Tablosu: Bu tabloya ui tarafında ekleme, güncelleme, silme yapılır.Üççüncü olarak bu tablonun doldurulması gerekli. Bu tablonun amacı hocanın oluşturduğu sınava ait soruları tutması içindir.
    /// </summary>
    public class AossOnlineSinavSorular

    {
     /// <summary>
    /// Ekleme Yaplırken boş bırakılmalı
    /// </summary>
        public long Id { get; set; }
             /// <summary>
    ///Sorunun ekleneceği sınavvın idsi
    /// </summary>
         [Required]
        public long OnlineSinavId { get; set; }
                     /// <summary>
    ///Eklenen Sorunun idsi
    /// </summary>
         [Required]
        public long SoruId { get; set; }

    }   
//dotnet aspnet-codegenerator controller -name AossOnlineSinavSorularController -async -api -m AossOnlineSinavSorular -dc AossContext -outDir Controllers
}