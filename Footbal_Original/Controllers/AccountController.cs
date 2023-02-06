using DataAccessLayer;
using Entities.Model;
using Entities.ViewModel.User;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Footbal_Original.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVm registerVm)
        {
            var context = new DataContext();
            var checkUser = context.Users.FirstOrDefault(x => x.Email == registerVm.Email);
            if (checkUser == null)
            {
                User user = new User
                {
                    Name = registerVm.Name,
                    Email = registerVm.Email,
                    Password = registerVm.Password
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("/Account/Login");  
        }

        [HttpPost]
        public ActionResult Login(LoginVm loginVm)
        {
            var context = new DataContext();
            var user = context.Users.FirstOrDefault(x => x.Email == loginVm.Email && x.Password==loginVm.Password);
            if (user != null)
            {
                return Redirect($"/Team/Index/");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}