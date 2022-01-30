using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OduncKitapMvcWebSolution_UI.Models
{
    public class PersonelViewModel
    {
        public int Id { get; set; }
        public string PersonelAdi { get; set; }
        public string PersonelSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public bool SilindiMi { get; set; }
    }
}
