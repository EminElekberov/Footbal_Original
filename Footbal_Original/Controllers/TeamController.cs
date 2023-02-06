using BusinessLayer.Repositories;
using DataAccessLayer;
using DataAccessLayer.EntityFramework;
using Entities.ViewModel.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Footbal_Original.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        TeamRepository teamRepository = new TeamRepository(new EfTeamRepository());
        CoachRepository coachRepository = new CoachRepository(new EfCoachRepository());
        DataContext dataContext = new DataContext();
        public ActionResult Index()
        {
            ViewBag.Coach = coachRepository.GetList();
            var value = teamRepository.GetList();
            return View(value);
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddTeamModel model)
        {
            teamRepository.TAdd(new Entities.Model.Team {Title = model.Title,CoachId=model.CoachId, Status = true, CreatedDateOnUTC = DateTime.Now });
            return View("Team/Index");
        }
        public async Task<JsonResult> Delete(int id)
        {
            var value = teamRepository.GetById(id);
            if (value != null)
            {
                value.Status = false;
                teamRepository.TDelete(value);
                return Json(new
                {
                    status = 200
                });
            }
            else
            {
                return Json(new
                {
                    status = 400,
                    JsonRequestBehavior.AllowGet
                });
            }
         
        }
        public async Task<ActionResult> Details(int id)
        {
            var value = teamRepository.GetById(id);
            return View(value);
        }
        public async Task<JsonResult>  Edit(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 404
                });
            }
            var value = teamRepository.GetById(id);
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateTeamModel model)
        {
            var value = teamRepository.GetById(model.Id);
            if (value != null)
            {
                value.CoachId= model.CoachId; 
                value.Title = model.Title;
                value.Status = true;
                value.ModifiedDateOnUTC = DateTime.Now;
                value.CreatedDateOnUTC = DateTime.Now;
                teamRepository.TUpdate(value);
            }
            return Json(new
            {
                status = 200
            });
        }
    }
}