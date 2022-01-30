using OduncKitapMvcWebSolution_Business;
using OduncKitapMvcWebSolution_Business.Managers;
using OduncKitapMvcWebSolution_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OduncKitapMvcWebSolution_UI.Controllers
{
    public class YazarController : Controller
    {
        YazarManager myYazarManager = new YazarManager();

        // GET: Yazar
        public ActionResult Index()
        {
            try
            {
                List<Yazarlar> yazarList = myYazarManager.TumAktifYazarlariGetir();
                ViewBag.YazarListCount = 0;
                if (yazarList.Count > 0)
                {
                    ViewBag.YazarListCount = yazarList.Count;
                }
                return View(yazarList);

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
        public ActionResult Ekle(Yazarlar yeniyazar)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniyazar);
                }
                Yazarlar eklenecekYazar = new Yazarlar()
                {
                    YazarAdi = yeniyazar.YazarAdi,
                    YazarSoyadi = yeniyazar.YazarSoyadi,
                    EklenmeTarihi = DateTime.Now,
                    SilindiMi = false
                };

                if (myYazarManager.YeniYazarEkle(eklenecekYazar))
                {
                    return RedirectToAction("Index", "Yazar");
                }
                else
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniyazar);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return View(yeniyazar);
            }

        }

        public ActionResult Sil(int id)
        {
            try
            {
                if (myYazarManager.YazarSil(id))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return RedirectToAction("Index", "Yazar");

            }
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {

            if (id>0)
            {
               var yazar= myYazarManager.TumAktifYazarlariGetir().FirstOrDefault(x=>x.Id==id);
                YazarViewModel model = new YazarViewModel()
                {
                    Id = yazar.Id,
                    YazarAdi = yazar.YazarAdi,
                    YazarSoyadi = yazar.YazarSoyadi,
                    SilindiMi = false,
                    EklenmeTarihi = yazar.EklenmeTarihi
                };
                return View(model);
            }

           
            return View(new YazarViewModel());
        }


       // [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Guncelle(Yazarlar yazar)
        {
            var guncellenecekYazar = myYazarManager.TumAktifYazarlariGetir().Find(x=>x.Id==yazar.Id);

            if (guncellenecekYazar.YazarAdi!=null)
            {
                guncellenecekYazar.YazarAdi = yazar.YazarAdi;
                guncellenecekYazar.YazarSoyadi = yazar.YazarSoyadi;
                guncellenecekYazar.EklenmeTarihi = DateTime.Now;
                guncellenecekYazar.SilindiMi = false;
                myYazarManager.YazarGuncelle(yazar);
            }
            return RedirectToAction("Index");
        }

    }
}