using Bio.io.DAL;
using Bio.io.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bio.io.Controllers
{
    public class RoutesController : Controller
    {
        // GET: AllRoutes
        public ActionResult All()
        {
            return View();
        }

        // GET: AllRoutes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AllRoutes/Create
        public String GetUserRoutes()
        {
            List<string> all_routes_as_strings = new List<string>();
            BioioRepository repo = new BioioRepository();
            List<Route> users_routes = repo.GetAllRoutes();
            
            string output = JsonConvert.SerializeObject(users_routes);

            return output;
        }

        // POST: AllRoutes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AllRoutes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AllRoutes/Edit/5
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

        // GET: AllRoutes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AllRoutes/Delete/5
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
