using System.ComponentModel.DataAnnotations;

namespace AossAPI.Models{
    /// <summary>
    /// Zorluk Puanlama Tablosu: Bu tabloya ui tarafında ekleme, silme, güncelleme yapılır. Sınav oluşturulurken ikinci olarak doldurulması gereken tablodur. Amaç oluşturulacak sınavın zorluklarının puanlarının tutulmasıdır.
    /// </summary>
    public class AossZorlukPuanlama
    {
            /// <summary>
            /// Boş brakılmalı
            /// </summary>
        public long Id { get; set; }
                    /// <summary>
            /// Kolay sorular kaç puan
            /// </summary>
         [Required]
        public float KolayPuan { get; set; }
                    /// <summary>
            /// Orta sorular kaç puan
            /// </summary>
         [Required]
        public float OrtaPuan { get; set; }
                    /// <summary>
            /// Zor sorular kaç puan
            /// </summary>
         [Required]
        public float ZorPuan { get; set; }
                    /// <summary>
            /// Online sınavın idsi
            /// </summary>
         [Required]
         
         public long OnlineSinavId { get; set; }
    }   
//dotnet aspnet-codegenerator controller -name AossZorlukPuanlamaController -async -api -m AossZorlukPuanlama -dc AossContext -outDir Controllers
}