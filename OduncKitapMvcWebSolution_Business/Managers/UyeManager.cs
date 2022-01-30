using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapMvcWebSolution_Business.Managers
{
    public class UyeManager
    {
        OduncKitapDBEntities dbContext = new OduncKitapDBEntities();


        public List<Uyeler> TumAktifUyeleriGetir()
        {
            try
            {
                List<Uyeler> uyeList = new List<Uyeler>();
                uyeList = dbContext.Uyeler.Where(x => !x.SilindiMi).ToList();
                return uyeList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool YeniUyeEkle(Uyeler yeniUye)
        {
            try
            {
                dbContext.Uyeler.Add(yeniUye);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UyeSil(int id)
        {
            var silinecekUye = dbContext.Uyeler.FirstOrDefault(x => x.Id == id);
            dbContext.Uyeler.Remove(silinecekUye);
            dbContext.SaveChanges();

            return true;
        }

        public bool UyeGuncelle(Uyeler uye)
        {
            var guncellenecekUye = dbContext.Uyeler.FirstOrDefault(x => x.Id == uye.Id);
            dbContext.SaveChanges();
            return true;
        }

    }
}
