using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    /// <summary>
    /// Öğrenci Tablosu: Ui tarafında ekleme, silme, güncelleme işlemleri bu tablo için yapılabilir.
    /// </summary>
    public class AossOgrenci
    {
         /// <summary>
        /// Öğrenci id bu veri eklenememeli çünkü kensidi otomatik oluşur.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Öğrencinin numarası giriş için
        /// </summary>
        [Required]
        public int OgrenciNo { get; set; }
        /// <summary>
        /// Öğrencinin şifresi giriş için
        /// </summary>
        [Required]
        public string Sifre { get; set; }
        /// <summary>
        /// Öğrencinin ad soyadı
        /// </summary>
        [Required]
        public string AdSoyad { get; set; }
        /// <summary>
        /// Öğrencinin alanı gireceği sınavlar ve bunun gibi alan gerektiren işilemler için.
        /// </summary>        
        public string Alani { get; set; }
    }   
}