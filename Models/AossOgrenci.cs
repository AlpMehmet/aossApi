using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossOgrenci
    {
        public long Id { get; set; }
        [Required]
        public int OgrenciNo { get; set; }
        [Required]
        public string Sifre { get; set; }
        [Required]
        public string AdSoyad { get; set; }

        public string Alani { get; set; }
    }   
}