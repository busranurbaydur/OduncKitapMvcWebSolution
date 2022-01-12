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

        // GET: Kitap
        public ActionResult Index()
        {
            try
            {
                List<Kitaplar> kitapList = myKitapManager.TumAktifKitaplariGetir();
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