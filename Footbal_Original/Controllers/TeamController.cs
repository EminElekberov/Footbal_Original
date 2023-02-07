using BusinessLayer.Repositories;
using DataAccessLayer;
using DataAccessLayer.EntityFramework;
using Entities.ViewModel.Team;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var values = dataContext.Teams.Include(x => x.Coach).ToList();
            return View(values);
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddTeamModel model)
        {
            teamRepository.TAdd(new Entities.Model.Team { Title = model.Title, CoachId = model.CoachId, Status = true, CreatedDateOnUTC = DateTime.Now });
            return RedirectToAction("/Index");
        }
        public async Task<ActionResult> Delete(int id)
        {
            var value = teamRepository.GetById(id);
            if (value != null)
            {
                var datacontext = new DataContext();
                datacontext.Teams.Attach(value);
                datacontext.Entry(value).State = EntityState.Deleted;
                await datacontext.SaveChangesAsync();
            }
            else
            {
                return HttpNotFound();
            }
            return View();
        }
        public async Task<ActionResult> Details(int id)
        {
            var value = teamRepository.GetById(id);
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
            var value = teamRepository.GetById(id);
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateTeamModel model)
        {
            var value = teamRepository.GetById(model.Id);
            if (value != null)
            {
                value.CoachId = model.CoachId;
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