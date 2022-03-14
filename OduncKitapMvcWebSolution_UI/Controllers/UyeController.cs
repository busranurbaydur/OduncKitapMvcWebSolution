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
    public class UyeController : Controller
    {
        UyeManager myUyeManager = new UyeManager();
        // GET: Uye
        public ActionResult Index()
        {
            try
            {
                List<Uyeler> uyeList = myUyeManager.TumAktifUyeleriGetir();
                ViewBag.UyeListCount = 0;
                if (uyeList.Count > 0)
                {
                    ViewBag.UyeListCount = uyeList.Count;
                }
                return View(uyeList);

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
        public ActionResult Ekle(Uyeler yeniUye)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniUye);
                }
                Uyeler eklenecekUye = new Uyeler()
                {
                    UyeAdi = yeniUye.UyeAdi,
                    UyeSoyadi = yeniUye.UyeSoyadi,
                    KayitTarihi = DateTime.Now,
                    SilindiMi = false,
                    DogumTarihi=yeniUye.DogumTarihi,                 
                    TelefonNumarasi=yeniUye.TelefonNumarasi,
                    Email = yeniUye.Email

                };

                if (myUyeManager.YeniUyeEkle(eklenecekUye))
                {
                    return RedirectToAction("Index", "Uye");
                }
                else
                {
                    ModelState.AddModelError("", "Üye işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniUye);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return View(yeniUye);
            }

        }

        public ActionResult Sil(int id)
        {
            try
            {
                if (myUyeManager.UyeSil(id))
                {
                    return RedirectToAction("Index", "Uye");
                }
                return RedirectToAction("Index","Uye");

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return RedirectToAction("Index", "Uye");

            }
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {

            if (id > 0)
            {
                var uye = myUyeManager.TumAktifUyeleriGetir().FirstOrDefault(x => x.Id == id);
                //return View("Index",yazar);
            }

            if (id>0)
            {
                Uyeler uye = myUyeManager.TumAktifUyeleriGetir().FirstOrDefault(x=>x.Id==id);
                UyeViewModel model = new UyeViewModel()
                {
                    Id=uye.Id,
                    UyeAdi = uye.UyeAdi,
                    UyeSoyadi = uye.UyeSoyadi,
                    DogumTarihi=DateTime.Now,
                    TelefonNumarasi = uye.TelefonNumarasi,
                    Email=uye.Email

                };
                return View(model);
            }

            return View(new UyeViewModel());
        }


        // [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Guncelle(Uyeler uye)
        {
            var guncellenecekUye = myUyeManager.TumAktifUyeleriGetir().Find(x => x.Id == uye.Id);

            if (guncellenecekUye.UyeAdi != null)
            {
                guncellenecekUye.UyeAdi = uye.UyeAdi;
                guncellenecekUye.UyeSoyadi = uye.UyeSoyadi;
                guncellenecekUye.DogumTarihi = uye.DogumTarihi;
                guncellenecekUye.TelefonNumarasi = uye.TelefonNumarasi;
                guncellenecekUye.Email = uye.Email;
                guncellenecekUye.KayitTarihi = DateTime.Now;
                guncellenecekUye.SilindiMi = false;
                myUyeManager.UyeGuncelle(uye);
            }
            return RedirectToAction("Index", "Uye");
        }
    }
}