using BusinessLayer.Repositories;
using BusinessLayer.Repositories.Model;
using DataAccessLayer;
using DataAccessLayer.EntityFramework;
using Entities.ViewModel.Player;
using Entities.ViewModel.Team;
using Footbal_Original.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Footbal_Original.Controllers
{
    public class PlayerController : Controller
    {
        TeamRepository teamRepository = new TeamRepository(new EfTeamRepository());
        PlayerRepository playerRepository = new PlayerRepository(new EfPlayerDal());


        // GET: Player
        public ActionResult Index()
        {
            DataContext dataContext = new DataContext();
            var values = dataContext.Players.Include(x => x.Team).ToList();
            return View(values);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.Team = teamRepository.GetList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(AddPlayerModel model)
        {

            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension=Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmdd") + extension;
            model.Image = "~/Content/image/"+fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/image"), fileName);
            model.ImageFile.SaveAs(fileName);
            using (DataContext context=new DataContext())
            {
                context.Players.Add(new Entities.Model.Player { Name = model.Name, PlayerNumber = model.PlayerNumber, TeamId = model.TeamId, Image = model.Image });
                context.SaveChanges();
            }
            ModelState.Clear();
            //playerRepository.TAdd(new Entities.Model.Player { Name = model.Name, PlayerNumber = model.PlayerNumber, TeamId = model.TeamId, Image = model.Image });

            return RedirectToAction("/Index");
        }
        public async Task<ActionResult> Delete(int id)
        {
            var value = playerRepository.GetById(id);
            if (value != null)
            {
                var datacontext = new DataContext();
                datacontext.Players.Attach(value);
                datacontext.Entry(value).State = EntityState.Deleted;
                await datacontext.SaveChangesAsync();
               // playerRepository.TDelete(value);
            }
            return Redirect("/Player/Index");
        }
        public async Task<ActionResult> Details(int id)
        {
            var value = playerRepository.GetById(id);
            return View(value);
        }
        public async Task<ActionResult> Edit(int id)
        {
            DataContext dataContext = new DataContext();
            ViewBag.Team = dataContext.Teams.ToList();
            if (id == 0)
            {
                return HttpNotFound();
            }
            var value = dataContext.Players.Include(x => x.Team).FirstOrDefault(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(UpdatePlayerModel model)
        {
            var value = playerRepository.GetById(model.Id);
            if (value != null)
            {
                if (model.Name==null)
                {
                    value.Name = value.Name;
                }
                else
                {
                    value.Name = model.Name;
                }
                if (model.PlayerNumber==0)
                {
                    value.PlayerNumber = value.PlayerNumber;
                }
                else
                {
                    value.PlayerNumber = model.PlayerNumber;
                }
                if (model.TeamId==0)
                {
                    value.TeamId = value.TeamId;
                }
                value.TeamId = model.TeamId;
                try
                {
                    playerRepository.TUpdate(value);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Unexpected error while save img");
                    return View();
                }
            }
            return RedirectToAction("/Index");

        }
    }
}