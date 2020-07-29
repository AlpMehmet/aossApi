using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossOnlineSinavSorular

    {
        public long Id { get; set; }
         [Required]
        public long OnlineSinavId { get; set; }
         [Required]
        public long SoruId { get; set; }

    }   
//dotnet aspnet-codegenerator controller -name AossOnlineSinavSorularController -async -api -m AossOnlineSinavSorular -dc AossContext -outDir Controllers
}