using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapMvcWebSolution_Business.Managers
{
    public class KitapManager
    {

        OduncKitapDBEntities dbContext = new OduncKitapDBEntities();

        public List<Kitaplar> TumAktifKitaplariGetir()
        {
            try
            {
                List<Kitaplar> kitaplar = new List<Kitaplar>();
                //page işlemi eklenecek

                kitaplar = dbContext.Kitaplar.Where(x => x.SilindiMi == false && x.StokAdedi > 0).ToList();

                return kitaplar;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
