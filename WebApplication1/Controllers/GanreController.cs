using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class GanreController : Controller
    {
        IGanreService ganreService;

        public GanreController(IGanreService serv)
        {
            ganreService = serv;
        }
        
        public ActionResult Create()
        {
            GanreDTO ganre = new GanreDTO();
            return View(ganre);
        }
        
        [HttpPost]
        public ActionResult Create(GanreDTO collection)
        {
            var bookDto = new GanreDTO { FirstName=collection.FirstName };
            ganreService.MakeGanre(bookDto);
            return RedirectToActionPermanent("EditOrCreate", "Main");
        }


       
    }
}
