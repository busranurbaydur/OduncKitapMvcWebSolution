using OduncKitapMvcWebSolution_Business;
using OduncKitapMvcWebSolution_Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OduncKitapMvcWebSolution_UI.Controllers
{
    public class KitapController : Controller
    {
        KitapManager myKitapManager = new KitapManager();
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
    }
}