using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtomicSeller.Helpers;
using AtomicSeller.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using AtomicSeller.Helpers.eCommerceConnectors;
using GLSAPI.Models;

namespace AtomicSeller.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult SendParcelData()
        {
            ShipmentResponse _Result = new GLS().GLS_SendParcelData();


            if (string.IsNullOrEmpty(_Result.responseHeader.ReturnMessage))
                FlashMessage.Flash(TempData, new FlashMessage("", FlashMessageType.Success, "Ok", true));
            else
                FlashMessage.Flash(TempData, new FlashMessage(_Result.responseHeader.ReturnMessage, FlashMessageType.Warning, "Error", true));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult GetShipmentLabel()
        {
            List<string> _ResponseHeader = new GLS().GLS_GetShipmentLabel();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult GetTrackingNumber()
        {
            List<string> _ResponseHeader = new GLS().GLS_GetTrackingNumber();

            return RedirectToAction("Index", "Home");
        }

        
    }
}