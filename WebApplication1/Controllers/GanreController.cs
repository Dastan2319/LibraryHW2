using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GanreController : Controller
    {
        IGanreService ganreService;

        public GanreController(IGanreService serv)
        {
            ganreService = serv;
        }

        public ActionResult Index()
        {
            IEnumerable<GanreDTO> ganreDtos = ganreService.GetBook();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GanreDTO, GanreViewModel>()).CreateMapper();
            var ganre = mapper.Map<IEnumerable<GanreDTO>, List<GanreViewModel>>(ganreDtos);
            return View(ganre);
        }


        public ActionResult EditOrCreate(int? id)
        {
            GanreViewModel ganre = new GanreViewModel();

            if (id != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GanreDTO, GanreViewModel>()).CreateMapper();
                ganre = mapper.Map<GanreDTO, GanreViewModel>(ganreService.GetGanre(id));
            }
            return View(ganre);

        }
        [HttpPost]
        public ActionResult EditOrCreate(Ganre ganre)
        {

            if (ganre.Id != 0)
            {
                ganreService.SaveUpdate(ganre);
            }
            else
            {
                var ganreDto = new GanreDTO {FirstName= ganre.FirstName };
                ganreService.MakeBook(ganreDto);
            }
            return RedirectToActionPermanent("Index", "Ganre");
        }
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Ganre");
        }
    }
}
