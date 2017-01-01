using Bio.io.DAL;
using Bio.io.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: Active/Details/5
        public void AddDatapoint(DataPoint datapoint)
        {
            //DataPoint new_datapoint = (new JavaScriptSerializer()).Deserialize<DataPoint>(datapoint);
            BioioRepository repo = new BioioRepository();
            repo.AddDataPoint(datapoint);
        }

        // GET: Active/Create
        public void CreateNewRoute(List<string> datapoints)
        {
            //List<DataPoint> datapoints = new List<DataPoint>();

            List<DataPoint> cleaned_datapoints = new List<DataPoint>();
            Route route = new Route();
            for (int k = 0; k < datapoints.Count; k++)
            {
                cleaned_datapoints.Add((new JavaScriptSerializer()).Deserialize<DataPoint>(datapoints[k]));
            }
            route.Coordinates = cleaned_datapoints;
            route.Created = DateTime.Now;
            BioioRepository repo = new BioioRepository();
            Route test_route = route;
            repo.AddNewRoute(route);
            int i = 0;
        }

        // POST: Active/Create
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
