using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossZorlukPuanlama
    {
        public long Id { get; set; }
         [Required]
        public string KolayPuan { get; set; }
         [Required]
        public string OrtaPuan { get; set; }
         [Required]
        public string ZorPuan { get; set; }
         [Required]
         public string OnlineSinavId { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossHocaController -async -api -m AossHoca -dc AossContext -outDir Controllers
}