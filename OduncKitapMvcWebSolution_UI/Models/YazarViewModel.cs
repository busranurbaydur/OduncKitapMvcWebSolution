using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OduncKitapMvcWebSolution_UI.Models
{
    public class YazarViewModel
    {
        public int Id { get; set; }
        public string YazarAdi { get; set; }
        public string YazarSoyadi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool SilindiMi { get; set; }
    }
}