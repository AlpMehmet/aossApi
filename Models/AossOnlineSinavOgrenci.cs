using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossOnlineSinavOgrenci
    {
        public long Id { get; set; }
         [Required]
        public string OgId { get; set; }
         [Required]
        public string OnlineSinavId { get; set; }
        public string DogruS { get; set; }
        public string YanlisS { get; set; }
        public string BosS { get; set; }
        public string Puan { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossHocaController -async -api -m AossHoca -dc AossContext -outDir Controllers
}