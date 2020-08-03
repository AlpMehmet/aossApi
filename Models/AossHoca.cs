using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    /// <summary>
    /// Hoca Tablosu: Ui tarafında ekleme, silme, güncelleme işlemleri bu tablo için yapılabilir.
    /// </summary>
    public class AossHoca
    {
        /// <summary>
        /// Hoca id  bu veri eklenememeli çünkü kensidi otomatik oluşur.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Hocanın mail adresi: giriş ve mail yollama işlemleri için
        /// </summary>
        [Required]
        public string Mail { get; set; }
        /// <summary>
        /// Hocanın Giriş Şifresi
        /// </summary>
        [Required]
        public string Sifre { get; set; }
        /// <summary>
        /// Hocanın Ad Ve Soyadı
        /// </summary>
        public string AdSoyad { get; set; }
        /// <summary>
        /// Hocanın alani veya dersi kriter olarak bir çok methodda gerekli 
        /// </summary>
        [Required]
        public string Alani { get; set; }
        
    }   
}