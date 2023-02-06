using BusinessLayer.Repositories;
using BusinessLayer.Repositories.Model;
using DataAccessLayer.EntityFramework;
using Entities.ViewModel.Player;
using Entities.ViewModel.Team;
using Footbal_Original.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
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
        IHostingEnvironment env;


        // GET: Player
        public ActionResult Index()
      {
            var value = playerRepository.GetList();
            return View(value);
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
            if (model.Photo != null)
            {
                try
                {
                    string folder = @"Content\assets\images\Player";
                    var newImg = await model.Photo.SaveAsync(env.WebRootPath, folder);
                    model.Image = newImg;
                    //
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Unexpected error while save img");
                    return View();
                }
            }
            playerRepository.TAdd(new Entities.Model.Player { Name = model.Name, PlayerNumber = model.PlayerNumber, TeamId = model.TeamId, Image = model.Image });

            return View("Player/Index");
        }
        public async Task<ActionResult> Delete(int id)
        {
            var value = playerRepository.GetById(id);
            if (value != null)
            {
                playerRepository.TDelete(value);
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
            if (id == null)
            {
                return HttpNotFound();
            }
            var value = playerRepository.GetById(id);
            return View(value);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(UpdatePlayerModel model)
        {
            var value = playerRepository.GetById(model.Id);
            if (value != null)
            {

                value.Name = model.Name;
                value.PlayerNumber = model.PlayerNumber;
                if (model.Photo != null)
                {
                    try
                    {
                        string folder = @"Content\assets\images\Player";
                        var newImg = await model.Photo.SaveAsync(env.WebRootPath, folder);
                        FileExtension.DeleteImage(env.WebRootPath, folder, value.Image);
                        value.Image = newImg;
                        //
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Unexpected error while save img");
                        return View();
                    }
                }
                playerRepository.TUpdate(value);
            }
            return Redirect("/Player/Index");
        }
    }
}