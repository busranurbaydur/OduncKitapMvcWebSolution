using OduncKitapMvcWebSolution_Business;
using OduncKitapMvcWebSolution_Business.Managers;
using OduncKitapMvcWebSolution_UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OduncKitapMvcWebSolution_UI.Controllers
{
    public class KitapController : Controller
    {
        KitapManager myKitapManager = new KitapManager();
        YazarManager myYazarManager = new YazarManager();
        TurManager myTurManager = new TurManager();
        private const int pageSize= 10;
        // GET: Kitap
        public ActionResult Index(int? page=1)
        {
            try
            {
                //paging 1.yöntem: bu yöntem en klasik yöntemdir..
                List<Kitaplar> kitapList = myKitapManager.TumAktifKitaplariGetir()
                    .Skip((page.Value < 1 ? 1 : page.Value-1)*pageSize)
                    .Take(pageSize)
                    .ToList()
                    ;

                var total = myKitapManager.TumAktifKitaplariGetir().Count;
                ViewBag.ToplamSayfa = (int)Math.Ceiling(total / (double)pageSize);
                ViewBag.Suan = page;
                ViewBag.PageSize = pageSize;

                ViewBag.KitapListCount = 0;
                if (kitapList.Count>0)
                {
                    ViewBag.KitapListCount = kitapList.Count;
                }

                return View(kitapList);
            }
            catch (Exception ex)
            {

                ViewBag.HataMesaji = "Beklenmedik bir hata oluştu.." ;
                return View();
                //ex.message loglanabilir..
            }
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> turListesi = new List<SelectListItem>();
            myTurManager.AktifTumTurleriGetir().ForEach(
                x =>
                turListesi.Add(new SelectListItem()
                {
                    Text=x.TurAdi,
                    Value=x.Id.ToString()
                }));

            ViewBag.TurListesi = turListesi;


            List<SelectListItem> yazarListesi = new List<SelectListItem>();
            myYazarManager.TumAktifYazarlariGetir().ForEach(
                x =>
                yazarListesi.Add(new SelectListItem()
                {
                    Text = x.YazarAdi + " " + x.YazarSoyadi,
                    Value = x.Id.ToString()
                }));

            ViewBag.YazarListesi = yazarListesi;

            return View(new KitapViewModel());

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(KitapViewModel yeniKitap)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniKitap);
                }
                Kitaplar eklenecekKitap = new Kitaplar()
                {
                    KayitTarihi = DateTime.Now,
                    KitapAdi = yeniKitap.KitapAdi,
                    SayfaSayisi = yeniKitap.SayfaSayisi,
                    StokAdedi = yeniKitap.StokAdedi,
                    YazarId = yeniKitap.YazarId,
                    TurId = yeniKitap.TurId
                };
                //Resim null değilse sisteme kaydolacak
                if (yeniKitap.Resim != null
                    && yeniKitap.Resim.ContentType.Contains("image")
                    && yeniKitap.Resim.ContentLength > 0
                    )
                {
                    //string filename = Path
                    //    .GetFileNameWithoutExtension(yeniKitap.Resim.FileName);
                    string filename = SiteSettings.CharacterFormatConverter(yeniKitap.KitapAdi).ToLower();
                    string extName = Path
                        .GetExtension(yeniKitap.Resim.FileName);
                    //
                    filename += "-" + Guid.NewGuid()
                        .ToString().Replace("-", "");
                    var directoryPath =
                        Server.MapPath($"~/BookImages/");
                    var filePath = Server
                        .MapPath($"~/BookImages/")
                        + filename + extName;
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    yeniKitap.Resim.SaveAs(filePath);
                    eklenecekKitap.ResimLink =
                        @"/BookImages/" + filename + extName;
                }

                //managerı çağırabiliriz.
                if (myKitapManager.YeniKitapEkle(eklenecekKitap))
                {
                    return RedirectToAction("Index", "Kitap");
                }
                else
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniKitap);
                }
            }
            catch (Exception ex)
            {
                // UI'da ex.Message'lar gönderilmez. exception mesajları loglanır. Ama biz şimdilik geçici olarak bir hata olursa görmek amacıyla yazıverdik.

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return View(yeniKitap);
            }
        }


        public ActionResult Sil(int id)
        {
            
            try
            {
                if (myKitapManager.KitapSil(id)==true)
                {
                    return RedirectToAction("Index");
                }
               
               return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return RedirectToAction("Index", "Kitap");
            }

        }


        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            List<SelectListItem> turListesi = new List<SelectListItem>();
            myTurManager.AktifTumTurleriGetir().ForEach(
                x =>
                turListesi.Add(new SelectListItem()
                {
                    Text = x.TurAdi,
                    Value = x.Id.ToString()
                }));

            ViewBag.TurListesi = turListesi;


            List<SelectListItem> yazarListesi = new List<SelectListItem>();
            myYazarManager.TumAktifYazarlariGetir().ForEach(
                x =>
                yazarListesi.Add(new SelectListItem()
                {
                    Text = x.YazarAdi + " " + x.YazarSoyadi,
                    Value = x.Id.ToString()
                }));

            ViewBag.YazarListesi = yazarListesi;
            if (id>0)
            {
                Kitaplar kitap = myKitapManager.TumAktifKitaplariGetir().FirstOrDefault(x => x.Id == id);
                KitapViewModel model = new KitapViewModel()
                {
                    Id=kitap.Id,
                    KayitTarihi=kitap.KayitTarihi,
                    KitapAdi=kitap.KitapAdi,
                    SayfaSayisi=kitap.SayfaSayisi,
                    StokAdedi=kitap.StokAdedi,
                    SilindiMi=kitap.SilindiMi,
                    YazarId=kitap.YazarId,
                    TurId=kitap.TurId,
                    ResimLink=kitap.ResimLink
                };
                return View(model);
            }

            return View(new KitapViewModel());
        }


        [HttpPost]
        public ActionResult Guncelle(KitapViewModel kitap)
        {
            Kitaplar guncellenecekKitap = myKitapManager.TumAktifKitaplariGetir().FirstOrDefault(x=>x.Id==kitap.Id);

            if (guncellenecekKitap.KitapAdi!=null)
            {
                //guncellenecekKitap.KayitTarihi = kitap.KayitTarihi;            
                guncellenecekKitap.KayitTarihi =kitap.KayitTarihi;
                guncellenecekKitap.KitapAdi = kitap.KitapAdi;
                guncellenecekKitap.SayfaSayisi = kitap.SayfaSayisi;
              
                guncellenecekKitap.SilindiMi = kitap.SilindiMi;
                guncellenecekKitap.StokAdedi = kitap.StokAdedi;
                guncellenecekKitap.YazarId = kitap.YazarId;
                guncellenecekKitap.TurId = kitap.TurId;
                if (kitap.Resim != null
                   && kitap.Resim.ContentType.Contains("image")
                   && kitap.Resim.ContentLength > 0
                   )
                {
                    //string filename = Path
                    //    .GetFileNameWithoutExtension(yeniKitap.Resim.FileName);
                    string filename = SiteSettings.CharacterFormatConverter(kitap.KitapAdi).ToLower();
                    string extName = Path
                        .GetExtension(kitap.Resim.FileName);
                    //
                    filename += "-" + Guid.NewGuid()
                        .ToString().Replace("-", "");
                    var directoryPath =
                        Server.MapPath($"~/BookImages/");
                    var filePath = Server
                        .MapPath($"~/BookImages/")
                        + filename + extName;
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    kitap.Resim.SaveAs(filePath);
                    guncellenecekKitap.ResimLink =
                        @"/BookImages/" + filename + extName;
                }
                myKitapManager.KitapGuncelle(guncellenecekKitap);
                
                
            }

            return RedirectToAction("Index");
        }
    }
}