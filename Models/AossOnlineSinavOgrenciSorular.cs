using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    public class AossOnlineSinavOgrenciSorular

    {
        public long Id { get; set; }
         [Required]
        public long OnlineSinavOgrenciId { get; set; }
         [Required]
        public long SoruId { get; set; }

        public string Soru { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }

        public string DogruCevap{ get; set; }
        public string IsaretlenenCevap{ get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossOnlineSinavOgrenciSorularController -async -api -m AossOnlineSinavOgrenciSorular -dc AossContext -outDir Controllers
}