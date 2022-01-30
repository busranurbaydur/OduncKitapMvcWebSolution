using OduncKitapMvcWebSolution_Business;
using OduncKitapMvcWebSolution_Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OduncKitapMvcWebSolution_UI.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        PersonelManager myPersonelManager = new PersonelManager();
        public ActionResult Index()
        {
            try
            {
                List<Personeller> personelList = myPersonelManager.TumAktifPersonelleriGetir();
                ViewBag.PersonelListCount = 0;
                if (personelList.Count > 0)
                {
                    ViewBag.PersonelListCount = personelList.Count;
                }
                return View(personelList);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

       
        

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(Personeller yeniPersonel)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniPersonel);
                }
                Personeller eklenecekPersonel = new Personeller()
                {
                    PersonelAdi = yeniPersonel.PersonelAdi,
                    PersonelSoyadi = yeniPersonel.PersonelSoyadi,
                    Email = yeniPersonel.Email,
                    Telefon = yeniPersonel.Telefon,
                    SilindiMi = false,
                  
                };

                if (myPersonelManager.YeniPersonelEkle(eklenecekPersonel))
                {
                    return RedirectToAction("Index", "Personel");
                }
                else
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniPersonel);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return View(yeniPersonel);
            }

        }

        public ActionResult Sil(int id)
        {
            try
            {
                if (myPersonelManager.PersonelSil(id))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return RedirectToAction("Index", "Personel");

            }
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {

            if (id > 0)
            {
                var personel = myPersonelManager.TumAktifPersonelleriGetir().FirstOrDefault(x => x.Id == id);
                
            }
            return View();
        }


        // [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Guncelle(Personeller personel)
        {
            var guncellenecekPersonel = myPersonelManager.TumAktifPersonelleriGetir().Find(x => x.Id == personel.Id);

            if (guncellenecekPersonel.PersonelAdi != null)
            {
                guncellenecekPersonel.PersonelAdi = personel.PersonelAdi;
                guncellenecekPersonel.PersonelSoyadi = personel.PersonelSoyadi;
                guncellenecekPersonel.Telefon = personel.Telefon;
                guncellenecekPersonel.Email = personel.Email;
                guncellenecekPersonel.SilindiMi = personel.SilindiMi;
                myPersonelManager.PersonelGuncelle(personel);
            }
            return RedirectToAction("Index");
        }
    }
}