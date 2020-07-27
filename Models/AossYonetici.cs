using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossYonetici
    {
        public long Id { get; set; }
        [Required]
        public string Mail { get; set; }
         [Required]
        public string Sifre { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossHocaController -async -api -m AossHoca -dc AossContext -outDir Controllers
}