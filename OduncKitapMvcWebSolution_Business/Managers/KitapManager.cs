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
        public bool YeniKitapEkle(Kitaplar yeniKitap)
        {
            try
            {
                dbContext.Kitaplar.Add(yeniKitap);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool KitapSil(int id)
        {
            var kit=dbContext.Kitaplar.FirstOrDefault(x => x.Id == id);
            dbContext.Kitaplar.Remove(kit);
            dbContext.SaveChanges();
            return true;
        }


        public bool KitapGuncelle(Kitaplar kitap)
        {
            var guncellenecekKitap = dbContext.Kitaplar.FirstOrDefault(x=>x.Id==kitap.Id);
           // dbContext.SaveChanges();

            return true;
        }
    }
}
