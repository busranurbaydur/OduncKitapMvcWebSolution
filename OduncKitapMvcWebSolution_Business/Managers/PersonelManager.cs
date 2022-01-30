using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapMvcWebSolution_Business.Managers
{
    public class PersonelManager
    {
        OduncKitapDBEntities dbContext = new OduncKitapDBEntities();
        public List<Personeller> TumAktifPersonelleriGetir()
        {
            try
            {
                List<Personeller> personelList = new List<Personeller>();
                personelList = dbContext.Personeller.Where(x => !x.SilindiMi).ToList();
                return personelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool YeniPersonelEkle(Personeller yeniPersonel)
        {
            try
            {
                dbContext.Personeller.Add(yeniPersonel);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool PersonelSil(int id)
        {
            var silinecekPersonel = dbContext.Personeller.FirstOrDefault(x => x.Id == id);
            dbContext.Personeller.Remove(silinecekPersonel);
            dbContext.SaveChanges();

            return true;
        }

        public bool PersonelGuncelle(Personeller personel)
        {
            var guncellenecekPersonel = dbContext.Personeller.FirstOrDefault(x => x.Id == personel.Id);
            dbContext.SaveChanges();
            return true;
        }

    }
}
