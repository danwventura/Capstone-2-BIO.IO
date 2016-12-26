using Bio.io.DAL;
using Bio.io.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Bio.io.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public string GetCurrentUserId()
        {
            //BioioRepository Repo = new BioioRepository();
            //return Repo.GetUserByID(UserID);

            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null) {

                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);


                if (userIdClaim != null)
                {

                    var userIdValue = userIdClaim.Value;
                    return userIdValue;
                }
                
                
           }
                return null;

        }
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
