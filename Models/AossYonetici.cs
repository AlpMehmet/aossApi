using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
         /// <summary>
        /// Yonetici Tablosu: Bu tabloya ui tarafında ekleme, güncelleme, silme yapılır. 
        /// </summary>
    public class AossYonetici
    {
                 /// <summary>
        /// Boş bırakılmalı
        /// </summary>
        public long Id { get; set; }
                 /// <summary>
        /// Mail giriş için
        /// </summary>
        [Required]
        public string Mail { get; set; }
                 /// <summary>
        /// Sifre  giriş için. 
        /// </summary>
         [Required]
        public string Sifre { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossHocaController -async -api -m AossHoca -dc AossContext -outDir Controllers
}