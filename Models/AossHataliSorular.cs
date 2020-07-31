using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossHataliSorular
    {
        public long Id { get; set; }
        [Required]
        public long SoruId { get; set; }
        [Required]
        public string Alani { get; set; }

        public int oylamaHatali { get; set; }
        public int oylamaHatasiz { get; set; }

        public bool hataliMi {  get;    set;    }
    }   
}
