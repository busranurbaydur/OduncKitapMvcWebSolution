
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
    public class OduncIslemController : Controller
    {

        KitapManager myKitapManager = new KitapManager();
        UyeManager myUyeManager = new UyeManager();
        OduncIslemManager myOduncIslemManager = new OduncIslemManager();
       // private const int pageSize = 10;
        // GET: OduncIslem
        public ActionResult Index()
        {
            List<OduncIslemler> tumIslemleriGetir = myOduncIslemManager.TumIslemleriGetir();
            return View(tumIslemleriGetir);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> kitapListesi = new List<SelectListItem>();
            myKitapManager.TumAktifKitaplariGetir().ForEach(
                x =>
                kitapListesi.Add(new SelectListItem()
                {
                    Text = x.KitapAdi ,
                    Value = x.Id.ToString()
                }));

            ViewBag.KitapListesi = kitapListesi;

            

            List<SelectListItem> uyeListesi = new List<SelectListItem>();
            myUyeManager.TumAktifUyeleriGetir().ForEach(
                x =>
                uyeListesi.Add(new SelectListItem()
                {
                    Text = x.UyeAdi +" "+x.UyeSoyadi,
                    Value = x.Id.ToString()
                }));

            ViewBag.UyeListesi = uyeListesi;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(OduncIslemViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Lütfen Tüm Alanları Doğru Giriniz.");
                    return View(model);
                }
                OduncIslemler yeniOduncIslem = new OduncIslemler()
                {
                    KayitTarihi = DateTime.Now,
                    KitapId = model.KitapId,
                    UyeId = model.UyeId,
                    OduncAlinmaTarihi = model.OduncAlinmaTarihi,
                    PersonelId = 1
                };
                yeniOduncIslem.OduncBitisTarihi = model.OduncAlinmaTarihi.AddDays(15);
                yeniOduncIslem.TeslimEttiMi = false;
                //bll kayıt etsin
                if (myOduncIslemManager.OduncIslemEkle(yeniOduncIslem))
                {
                    return RedirectToAction("Index", "OduncIslem");
                }
                else
                {
                    ModelState.AddModelError("", "Ödünç verme işleminde beklenmedik bir hata oluştu... İşleminizi tekrar deneyiniz..");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                return View(model);
            }
        }

        public ActionResult Sil(int id)
        {
            try
            {
                if (myOduncIslemManager.IslemSil(id))
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
    }
}