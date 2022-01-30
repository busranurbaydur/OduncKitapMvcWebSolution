using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OduncKitapMvcWebSolution_UI.Models
{
    public class TurViewModel
    {
        public byte Id { get; set; }
        public string TurAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool SilindiMi { get; set; }

    }
}