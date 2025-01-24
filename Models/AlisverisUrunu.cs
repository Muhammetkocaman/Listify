using System.ComponentModel.DataAnnotations;

namespace web_uyg.Models
{
    public class AlisverisUrunu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir")]
        public string UrunAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(1, 1000, ErrorMessage = "Miktar 1-1000 arasında olmalıdır")]
        public int Miktar { get; set; }

        [Required(ErrorMessage = "Miktar birimi zorunludur")]
        public string MiktarBirimi { get; set; } = "Adet";

        public bool AlındiMi { get; set; }

        public int? KategoriId { get; set; }
        public Kategori? Kategori { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
    }
}
