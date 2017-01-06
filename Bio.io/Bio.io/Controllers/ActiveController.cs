using Bio.io.DAL;
using Bio.io.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Bio.io.Controllers
{
    public class ActiveController : Controller
    {
        // GET: Active
        public ActionResult Index()
        {
            return View();
        }

        public string GetCurrentUser()
        {
    
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {

                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    BioioRepository repo = new BioioRepository();
                    //string userUserName = User.Identity.Name;
                    string userIdValue = userIdClaim.Value;
                    User this_user = repo.GetUserById(userIdValue);
                    var my_user = JsonConvert.SerializeObject(this_user);
                    return my_user;
                }
            }
            return null;
        }



        // GET: Active/Create
        public void CreateNewRoute(List<string> datapoints)
        {

            List<DataPoint> cleaned_datapoints = new List<DataPoint>();
            Route route = new Route();

            
            for (int k = 0; k < datapoints.Count; k++)
            {
                cleaned_datapoints.Add((new JavaScriptSerializer()).Deserialize<DataPoint>(datapoints[k]));
            }


            route.Coordinates = cleaned_datapoints;
            route.Created = DateTime.Now;
            BioioRepository repo = new BioioRepository();
            repo.AddNewRoute(route);
            int i = 0;
        }

        

        // GET: Active/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Active/Edit/5
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

        // GET: Active/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Active/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
