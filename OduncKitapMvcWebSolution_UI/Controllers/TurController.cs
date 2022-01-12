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

                throw;
            }

            return View();
        }
    }
}