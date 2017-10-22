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

        // GET: WebDevelopers/Edit/5
        public ActionResult Edit(int id)
        {
            WebDeveloperViewModel wdViewModel;
            if (createWebDeveloperViewModel(id, out wdViewModel) == HttpNotFound())
                return HttpNotFound();

            //Since we are in edit mode I pass a variable through ViewBag to the view to advise it that the fields are
            //enabled
            ViewBag.ReadOnly = false;

            return View("WebDeveloperForm", wdViewModel);
        }

        // GET: WebDevelopers/Details/5
        public ActionResult Details(int id)
        {
            WebDeveloperViewModel wdViewModel;
            if (createWebDeveloperViewModel(id, out wdViewModel) == HttpNotFound())
                return HttpNotFound();

            //Since we are in edit mode I pass a variable through ViewBag to the view to advise it that the fields are
            //disabled
            ViewBag.ReadOnly = true;

            return View("WebDeveloperForm", wdViewModel);
        }

        // GET: WebDevelopers/Create
        public ActionResult Create()
        {
            List<WebTechnology> wtList;
            List<Stack> stackList;
            using (var context = new Context())
            {
                wtList = context.WebTechnology.ToList();
                stackList = context.Stack.ToList();
            }

            //Create a new web developer view model to bind the inserted data into the view
            WebDeveloperViewModel wdViewModel = new WebDeveloperViewModel();
            wdViewModel.DayOfBirth = new DateTime(1990, 1, 1);
            wdViewModel.WebTechnologies = new List<WebTechnologyViewModel>();
            foreach (var wt in wtList)
            {
                var wtViewModel = new WebTechnologyViewModel();
                wtViewModel.Id = wt.Id;
                wtViewModel.Name = wt.Name;
                wdViewModel.WebTechnologies.Add(wtViewModel);
            }

            wdViewModel.Stacks = new List<StackViewModel>();
            foreach (var stack in stackList)
            {
                var stackViewModel = new StackViewModel();
                stackViewModel.Id = stack.Id;
                stackViewModel.Name = stack.Name;
                wdViewModel.Stacks.Add(stackViewModel);
            }


            ViewBag.ReadOnly = false;

            return View("WebDeveloperForm", wdViewModel);
        }
        

        private ActionResult createWebDeveloperViewModel(int id, out WebDeveloperViewModel wdViewModel)
        {
            //Create the web developer view model
            wdViewModel = new WebDeveloperViewModel();

            WebDeveloper wd;
            List<WebTechnology> wtList;
            List<Stack> stackList;

            //Extract the web developer from the DB
            using (var context = new Context())
            {
                wd = context.WebDeveloper.Include(x => x.Stacks).Include(x => x.WebTechnologies).SingleOrDefault(x => x.Id == id);
                wtList = context.WebTechnology.ToList();
                stackList = context.Stack.ToList();
            }

            if (wd == null)
                return HttpNotFound();

            //Assign the properties of the extracted web developer to the viewmodel, here we could use a mapper utility
            //like AutoMapper which semplify out work
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
            wdViewModel.Stacks = new List<StackViewModel>();

            //Assign the web technologies to the view model
            foreach (var wt in wtList)
            {
                var wtViewModel = new WebTechnologyViewModel();
                wtViewModel.Id = wt.Id;
                wtViewModel.Name = wt.Name;
                wtViewModel.IsSelected = wd.WebTechnologies.Any(x => x.Id == wt.Id);
                wdViewModel.WebTechnologies.Add(wtViewModel);
            }

            //Assign the stack to the view model
            foreach (var stack in stackList)
            {
                var stackViewModel = new StackViewModel();
                stackViewModel.Id = stack.Id;
                stackViewModel.Name = stack.Name;
                stackViewModel.IsSelected = wd.Stacks.Any(x => x.Id == stack.Id);
                wdViewModel.Stacks.Add(stackViewModel);
            }

            return Content("");
        }
    }
}
