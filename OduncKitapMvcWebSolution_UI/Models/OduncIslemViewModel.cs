using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OduncKitapMvcWebSolution_UI.Models
{
    public class OduncIslemViewModel
    {
        public int Id { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int UyeId { get; set; }
        public int KitapId { get; set; }
        public int PersonelId { get; set; }
        public DateTime OduncAlinmaTarihi { get; set; }
        public DateTime OduncBitisTarihi { get; set; }
        public bool TeslimEttiMi { get; set; }
        public Nullable<DateTime> TeslimEttigiTarih { get; set; }
    }
}