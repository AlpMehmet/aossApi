using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
             /// <summary>
        /// Online Sınav  Sorular Tablosu: Bu tabloya ui tarafında ekleme, güncelleme, silme yapılır. Bu tablonun amacı hocanın kendi alanına ait soruları girmesidir.
        /// </summary>
    public class AossSorular
    {
         /// <summary>
        /// Sorunun idsi boş bırakılmalı
        /// </summary>
        
        public long Id { get; set; }
                 /// <summary>
        ///Soru
        /// </summary>
        [Required]
        public string Soru { get; set; }
                 /// <summary>
        /// Sorunun doğru cevabı.
        /// </summary>
         [Required]
        public string DogruCevap { get; set; }
                 /// <summary>
        /// Soru yanlış cevaplarından
        /// </summary>
        public string Cevap1 { get; set; }
                 /// <summary>
        /// Soru yanlış cevaplarından
        /// </summary>
        public string Cevap2 { get; set; }
                 /// <summary>
        /// Soru yanlış cevaplarından
        /// </summary>
        public string Cevap3 { get; set; }
                 /// <summary>
        /// Soru yanlış cevaplarından
        /// </summary>
        public string Cevap4 { get; set; }
                 /// <summary>
        /// Soruyu ekleyen hocanın alanı
        /// </summary>
        public string SoruAlani { get; set; }
                 /// <summary>
        ///sorunun zorluk derecesi
        /// </summary>
        public string Zorluk { get; set; }

    }   
//dotnet aspnet-codegenerator controller -name AossSorularController -async -api -m AossSorular -dc AossContext -outDir Controllers
}