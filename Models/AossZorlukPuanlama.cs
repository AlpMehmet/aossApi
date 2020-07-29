using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossZorlukPuanlama
    {
        public long Id { get; set; }
         [Required]
        public float KolayPuan { get; set; }
         [Required]
        public float OrtaPuan { get; set; }
         [Required]
        public float ZorPuan { get; set; }
         [Required]
         public long OnlineSinavId { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossZorlukPuanlamaController -async -api -m AossZorlukPuanlama -dc AossContext -outDir Controllers
}