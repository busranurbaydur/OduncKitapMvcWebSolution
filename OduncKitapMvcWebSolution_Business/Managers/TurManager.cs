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

        public bool YeniTurEkle(Turler yeniTur)
        {
            try
            {
                dbContext.Turler.Add(yeniTur);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool TurSil(int id)
        {
            var silinecekTur = dbContext.Turler.FirstOrDefault(x=>x.Id==id);
            dbContext.Turler.Remove(silinecekTur);
            dbContext.SaveChanges();

            return true;
        }
      
        public bool TurGuncelle(Turler tur)
        {
            var guncellenecekTur = dbContext.Turler.FirstOrDefault(x => x.Id==tur.Id);
            dbContext.SaveChanges();
            return true;
        }
    }
}
