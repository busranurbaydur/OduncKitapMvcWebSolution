using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OduncKitapMvcWebSolution_UI.Models
{
    public class UyeViewModel
    {
        public int Id { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string UyeAdi { get; set; }
        public string UyeSoyadi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string TelefonNumarasi { get; set; }
        public string Email { get; set; }
        public bool SilindiMi { get; set; }
    }
}