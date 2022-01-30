using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapMvcWebSolution_Business.Managers
{
    public class YazarManager
    {
        OduncKitapDBEntities dbcontext = new OduncKitapDBEntities();

        public List<Yazarlar> TumAktifYazarlariGetir()
        {
            try
            {
                List<Yazarlar> yazarList = new List<Yazarlar>();
                yazarList = dbcontext.Yazarlar.Where(x => !x.SilindiMi).ToList();
                return yazarList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public bool YeniYazarEkle(Yazarlar yeniYazar)
        {
            try
            {
                dbcontext.Yazarlar.Add(yeniYazar);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool YazarSil(int id)
        {
            var silinecekYazar = dbcontext.Yazarlar.FirstOrDefault(x => x.Id == id);

            dbcontext.Yazarlar.Remove(silinecekYazar);
            dbcontext.SaveChanges();

            return true;
        }


        public bool YazarGuncelle(Yazarlar yazar)
        {
            var guncellenecek = dbcontext.Yazarlar.FirstOrDefault(x => x.Id == yazar.Id);
            
            dbcontext.SaveChanges();

            return true;
        }
    }


}
