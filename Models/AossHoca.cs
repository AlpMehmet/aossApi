using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossHoca
    {
        public long Id { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Sifre { get; set; }
        
        public string AdSoyad { get; set; }
        [Required]
        public string Alani { get; set; }
        
    }   
}