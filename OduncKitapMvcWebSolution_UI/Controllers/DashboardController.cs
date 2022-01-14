using OduncKitapMvcWebSolution_Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OduncKitapMvcWebSolution_UI.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        KitapManager myKitapManager = new KitapManager();
        public ActionResult Index()
        {
            ViewBag.BugunEklenenKitapSayisi = myKitapManager.TumAktifKitaplariGetir().Where(x => x.KayitTarihi > DateTime.Now.AddDays(-1) && x.KayitTarihi < DateTime.Now.AddDays(1))
                .Count();
            

            return View();
        }
    }
}