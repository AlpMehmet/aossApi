using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossSorular
    {
        public long Id { get; set; }
        [Required]
        public string Soru { get; set; }
         [Required]
        public string DogruCevap { get; set; }
        public string Cevap1 { get; set; }
        public string Cevap2 { get; set; }
        public string Cevap3 { get; set; }
        public string Cevap4 { get; set; }
        public string SoruAlani { get; set; }
        public string Zorluk { get; set; }

    }   
//dotnet aspnet-codegenerator controller -name AossSorularController -async -api -m AossSorular -dc AossContext -outDir Controllers
}