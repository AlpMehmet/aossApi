using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossOnlineSinavSorular

    {
        public long Id { get; set; }
         [Required]
        public string OturumKodu { get; set; }
         [Required]
        public string SoruId { get; set; }

    }   
//dotnet aspnet-codegenerator controller -name AossSorularController -async -api -m AossSorular -dc AossContext -outDir Controllers
}