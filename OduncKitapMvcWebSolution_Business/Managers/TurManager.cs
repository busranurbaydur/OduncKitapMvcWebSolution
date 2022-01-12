using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapMvcWebSolution_Business.Managers
{
    public class TurManager
    {
        OduncKitapDBEntities dbContext = new OduncKitapDBEntities();
        public List<Turler> AktifTumTurleriGetir()
        {
            try
            {
                List<Turler> turler = new List<Turler>();
                turler = dbContext.Turler.Where(x => x.SilindiMi == false).ToList();

                return turler;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
