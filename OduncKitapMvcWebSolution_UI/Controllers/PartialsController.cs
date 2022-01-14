using OduncKitapMvcWebSolution_Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OduncKitapMvcWebSolution_UI.Controllers
{
    public class PartialsController : Controller
    {
        KitapManager myKitapManager = new KitapManager();
        public PartialViewResult MenuPartialResult()
        {
            //TODO:
            int toplamKitapSayisi = myKitapManager.TumAktifKitaplariGetir().Count();
            TempData["ToplamKitapSayisi"] = toplamKitapSayisi;


            return PartialView("_PartialMenu");
        }
    }
}