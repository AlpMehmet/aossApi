using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossOnlineSinav
    {
        public long Id { get; set; }
         [Required]
        public string Baslama { get; set; }
         [Required]
        public string Bitis { get; set; }
         [Required]
        public string Sure { get; set; }
         [Required]
        public string Alan { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossOnlineSinavController -async -api -m AossOnlineSinav -dc AossContext -outDir Controllers
}