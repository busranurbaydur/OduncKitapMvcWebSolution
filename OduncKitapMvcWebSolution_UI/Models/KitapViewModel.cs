using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OduncKitapMvcWebSolution_UI.Models
{
    public class KitapViewModel
    {
        public int Id { get; set; }
        public DateTime KayitTarihi { get; set; }
        [Required(ErrorMessage ="Kitap Adı Girişi Gereklidir.")]
        [StringLength(maximumLength:50,ErrorMessage ="Kitap Adı en az 1, en fazla 50 karakter olmalıdır.")]
        public string KitapAdi { get; set; }
        public byte TurId { get; set; }
        public int YazarId { get; set; }
        [Required(ErrorMessage = "Sayfa Sayısı Gereklidir.")]
        public int SayfaSayisi { get; set; }
        [Required(ErrorMessage = "Stok Adedi Gereklidir.")]
        public int StokAdedi { get; set; }
        public bool SilindiMi { get; set; }
        public string ResimLink { get; set; }
        public HttpPostedFileBase Resim { get; set; }
    }
}