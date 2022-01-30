using OduncKitapMvcWebSolution_Business;
using OduncKitapMvcWebSolution_Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OduncKitapMvcWebSolution_UI.Controllers
{
    public class TurController : Controller
    {
        TurManager myTurManager = new TurManager();

        // GET: Tur
        public ActionResult Index()
        {

            //tüm türler gelecek bu yüzden bll gidip metot oluşturduk
            try
            {
                List<Turler> turList = myTurManager.AktifTumTurleriGetir();
                ViewBag.TurListCount = 0;
                if (turList.Count>0)
                {
                    ViewBag.TurListCount = turList.Count;
                }
                return View(turList);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public ActionResult Sil(int id)
        {
            try
            {

                var silinecekTur = myTurManager.TurSil(id);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }

           
        }

        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            if (id > 0)
            {
                Turler bulunanTur = myTurManager.AktifTumTurleriGetir().FirstOrDefault(x => x.Id == id);
            }

            return View();
        }


       // [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Duzenle(Turler tur)
        {
            Turler guncellenecekTur = myTurManager.AktifTumTurleriGetir().FirstOrDefault(x => x.Id == tur.Id);
            if (guncellenecekTur.TurAdi!=null)
            {
                
                guncellenecekTur.TurAdi = tur.TurAdi;
                guncellenecekTur.EklenmeTarihi = DateTime.Now;
                guncellenecekTur.SilindiMi = false;
                myTurManager.TurGuncelle(tur);
            }
           

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(Turler yenitur)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yenitur);
                }
                Turler eklenecekTur = new Turler()
                {
                    TurAdi = yenitur.TurAdi,
                    EklenmeTarihi = DateTime.Now,
                   SilindiMi=false

                };

                if (myTurManager.YeniTurEkle(eklenecekTur))
                {
                    return RedirectToAction("Index", "Tur");
                }
                else
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yenitur);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return View(yenitur);
            }
           
        }


    }
}