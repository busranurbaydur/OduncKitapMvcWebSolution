using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapMvcWebSolution_Business.Managers
{
    public class OduncIslemManager
    {
        OduncKitapDBEntities dbContext = new OduncKitapDBEntities();

        public bool OduncIslemEkle(OduncIslemler yeniOduncIslem)
        {
            try
            {
                bool sonuc = false;
                dbContext.OduncIslemler.Add(yeniOduncIslem);
                dbContext.SaveChanges();
                sonuc = true;
                return sonuc;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<OduncIslemler> TumIslemleriGetir()
        {
            try
            {
                List<OduncIslemler> oduncIslemList = new List<OduncIslemler>();
                oduncIslemList = dbContext.OduncIslemler.ToList();
                return oduncIslemList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool IslemSil(int id)
        {

            var silinecekIslem = dbContext.OduncIslemler.FirstOrDefault(x => x.Id == id);

            dbContext.OduncIslemler.Remove(silinecekIslem);
            dbContext.SaveChanges();

            return true;
        }

        
    }
}
