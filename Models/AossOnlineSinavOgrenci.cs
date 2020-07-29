using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossOnlineSinavOgrenci
    {
        public long Id { get; set; }
         [Required]
        public int OgId { get; set; }
         [Required]
        public int OnlineSinavId { get; set; }
        public int DogruS { get; set; }
        public int YanlisS { get; set; }
        public int BosS { get; set; }
        public int Puan { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossHocaController -async -api -m AossHoca -dc AossContext -outDir Controllers
}