using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    /// <summary>
    /// Hatalı Sorular Tablosu: Bu tabloya ui tarafında ekleme, silme, güncelleme yapılmaz. Bu tablonun amacı hatalı olan soruları tutmak ve oylamasının yapılmasıdır.
    /// </summary>
    public class AossHataliSorular
    {
        /// <summary>
        /// Hatalı Soru Id  bu veri eklenememeli çünkü kendisi otomatik oluşur.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Hatalı sorunun hangi soru olduğu verisi tutulur. Hatalı soru listelemek istendiğine bu tabloya erişim sağlanır ve sorular bu girdi sayesinde listelenir.
        /// </summary>
        [Required]
        public long SoruId { get; set; }
        /// <summary>
        /// Hatalı olarak işaretlenen sorunun alanı burada tutulur. Hocaya mail yollama ve ekranda gösterilecek alana ait hatalı sorular için.
        /// </summary>
        [Required]
        public string Alani { get; set; }
        /// <summary>
        /// Soruyu hatalı olarak işaretletleyen hocaların sayısı tutulur.
        /// </summary>
        public int oylamaHatali { get; set; }
        /// <summary>
        /// Soruyu hatasız olarak işaretletleyen hocaların sayısı tutulur. 
        /// </summary>
        public int oylamaHatasiz { get; set; }
        /// <summary>
        /// Kontrol işleminden dönen sonuç tutulur soru hatalı mı hatasız mı.
        /// </summary>
        public bool hataliMi {  get;    set;    }
    }   
}
