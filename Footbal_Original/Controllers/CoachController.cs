using BusinessLayer.Repositories;
using BusinessLayer.Repositories.Model;
using DataAccessLayer;
using DataAccessLayer.EntityFramework;
using Entities.ViewModel.Coach;
using Entities.ViewModel.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Footbal_Original.Controllers
{
    public class CoachController : Controller
    {
        CoachRepository coachRepository= new CoachRepository(new EfCoachRepository());

        // GET: Coach
        public ActionResult Index()
        {
            var value = coachRepository.GetList();
            return View(value);
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddCoachModel model)
        {
            coachRepository.TAdd(new Entities.Model.Coach { Name=model.Name });
            return RedirectToAction("/Coach/Index");
        }
        public async Task<ActionResult> Delete(int id)
        {
            var value = coachRepository.GetById(id);
            if (value != null)
            {
                var context = new DataContext();
                context.Coaches.Remove(value);
                context.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            return View();
        }
        //public async Task<JsonResult> Delete(int id)
        //{
        //    var value = coachRepository.GetById(id);
        //    if (value != null)
        //    {
        //        var context = new DataContext();
        //        context.Coaches.Remove(value);
        //        context.SaveChanges();
        //        //coachRepository.TDelete(value);
        //        return Json(new
        //        {
        //            status = 200
        //        });
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            status = 400,
        //            JsonRequestBehavior.AllowGet
        //        });
        //    }

        //}
        public async Task<ActionResult> Details(int id)
        {
            var value = coachRepository.GetById(id);
            return View(value);
        }
        public async Task<JsonResult> Edit(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 404
                });
            }
            var value = coachRepository.GetById(id);
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateCoachModel model)
        {
            var value = coachRepository.GetById(model.Id);
            if (value != null)
            {
                value.Name = model.Name;
                coachRepository.TUpdate(value);
            }
            return Json(new
            {
                status = 200
            });
        }
    }
}