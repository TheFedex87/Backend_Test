using Backend_Amaris_Test.Models;
using Backend_Amaris_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Backend_Amaris_Test.Controllers
{
    public class WebDevelopersController : Controller
    {
        // GET: WebDevelopers
        public ActionResult List()
        {
            return View();
        }

        // GET: WebDevelopers/Details/5
        public ActionResult Edit(int id)
        {
            WebDeveloper wd;
            List<WebTechnology> wtList;
            
            using (var context = new Context())
            {
                wd = context.WebDeveloper.Include(x => x.WebTechnologies).SingleOrDefault(x => x.Id == id);
                wtList = context.WebTechnology.ToList();
            }

            if (wd == null)
                return HttpNotFound();

            WebDeveloperViewModel wdViewModel = new WebDeveloperViewModel();
            wdViewModel.FirstName = wd.FirstName;
            wdViewModel.LastName = wd.LastName;
            wdViewModel.Address = wd.Address;
            wdViewModel.Comments = wd.Comments;
            wdViewModel.ContactPhone = wd.ContactPhone;
            wdViewModel.DayOfBirth = wd.DayOfBirth;
            wdViewModel.Email = wd.Email;
            wdViewModel.Id = wd.Id;
            wdViewModel.YearsOfExperience = wd.YearsOfExperience;
            wdViewModel.WebTechnologies = new List<WebTechnologyViewModel>();

            
            foreach(var wt in wtList)
            {
                if (wd.WebTechnologies.Any(x => x.Id == wt.Id))
                {
                    wdViewModel.WebTechnologies.Add(new WebTechnologyViewModel { WebTechnology = wt, IsSelected = true });
                }
                else
                {
                    wdViewModel.WebTechnologies.Add(new WebTechnologyViewModel { WebTechnology = wt, IsSelected = false });
                }
            }

            return View(wdViewModel);
        }

        // GET: WebDevelopers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebDevelopers/Create
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

        // POST: WebDevelopers/Edit/5
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

        // GET: WebDevelopers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebDevelopers/Delete/5
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
