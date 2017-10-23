using Backend_Amaris_Test.Models;
using Backend_Amaris_Test.ViewModel;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend_Amaris_Test.Controllers.Api
{
    public class WebDevelopersController : ApiController
    {
        // GET api/webdevelopers
        [HttpGet]
        public IHttpActionResult WebDevelopers()
        {
            //Extract developers from DB, including the two navigation properties stacks and web technologies
            List<WebDeveloper> wdList;
            using (var context = new Context())
            {
                wdList = context.WebDeveloper.Include(x => x.Stacks).Include(x => x.WebTechnologies).ToList();
            }

            //Create the viewmodel
            var wdViewModelList = new List<WebDeveloperViewModel>();
            foreach (var wd in wdList)
            {
                //Here I could use some mapping tool instead of assign any single properties, for example AutoMapper
                var wdViewModel = new WebDeveloperViewModel();
                wdViewModel.Address = wd.Address;
                wdViewModel.Comments = wd.Comments;
                wdViewModel.ContactPhone = wd.ContactPhone;
                wdViewModel.DayOfBirth = wd.DayOfBirth;
                wdViewModel.Email = wd.Email;
                wdViewModel.FirstName = wd.FirstName;
                wdViewModel.Id = wd.Id;
                wdViewModel.LastName = wd.LastName;
                wdViewModel.YearsOfExperience = wd.YearsOfExperience;
                wdViewModel.Stacks = new List<StackViewModel>();
                foreach (var stack in wd.Stacks)
                {
                    wdViewModel.Stacks.Add(new StackViewModel { Id = stack.Id, IsSelected = true, Name = stack.Name });
                }

                
                wdViewModel.WebTechnologies = new List<WebTechnologyViewModel>();
                foreach (var wt in wd.WebTechnologies)
                {
                    wdViewModel.WebTechnologies.Add(new WebTechnologyViewModel { Id = wt.Id, IsSelected = true, Name = wt.Name });
                }
                wdViewModelList.Add(wdViewModel);
            }

            return Ok(wdViewModelList);
        }

        // GET api/webdevelopers/1
        [HttpGet]
        public IHttpActionResult WebDeveloper(int id)
        {
            //Extract developer from DB, including the two navigation properties stacks and web technologies
            WebDeveloper wd;
            using (var context = new Context())
            {
                wd = context.WebDeveloper.Include(x => x.Stacks).Include(x => x.WebTechnologies).SingleOrDefault(x => x.Id == id);
            }

            //Create the viewmodel
            var wdViewModel = new WebDeveloperViewModel();

            //Here I could use some mapping tool instead of assign any single properties, for example AutoMapper
            wdViewModel.Address = wd.Address;
            wdViewModel.Comments = wd.Comments;
            wdViewModel.ContactPhone = wd.ContactPhone;
            wdViewModel.DayOfBirth = wd.DayOfBirth;
            wdViewModel.Email = wd.Email;
            wdViewModel.FirstName = wd.FirstName;
            wdViewModel.Id = wd.Id;
            wdViewModel.LastName = wd.LastName;
            wdViewModel.YearsOfExperience = wd.YearsOfExperience;
            wdViewModel.Stacks = new List<StackViewModel>();
            foreach (var stack in wd.Stacks)
            {
                wdViewModel.Stacks.Add(new StackViewModel { Id = stack.Id, IsSelected = true, Name = stack.Name });
            }


            wdViewModel.WebTechnologies = new List<WebTechnologyViewModel>();
            foreach (var wt in wd.WebTechnologies)
            {
                wdViewModel.WebTechnologies.Add(new WebTechnologyViewModel { Id = wt.Id, IsSelected = true, Name = wt.Name });
            }
            
            return Ok(wdViewModel);
        }

        // POST api/webdevelopers
        [HttpPost]
        public IHttpActionResult CreateWebDeveloper(WebDeveloperViewModel viewModel)
        {
            //Check model validity
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Create the new web developer and assign the content of the view model
            var wd = new WebDeveloper();

            using (var context = new Context())
            {
                wd.Address = viewModel.Address;
                wd.Comments = viewModel.Comments;
                wd.ContactPhone = viewModel.ContactPhone;
                wd.DayOfBirth = viewModel.DayOfBirth;
                wd.Email = viewModel.Email;
                wd.FirstName = viewModel.FirstName;
                wd.LastName = viewModel.LastName;
                wd.YearsOfExperience = viewModel.YearsOfExperience;

                //If a web technology has been selected I add it to the web technologies of the new webdeveloper
                wd.WebTechnologies = new List<WebTechnology>();
                foreach (var wt in viewModel.WebTechnologies)
                {
                    if (wt.IsSelected)
                    {
                        var wtInDB = context.WebTechnology.Single(x => x.Id == wt.Id);
                        wd.WebTechnologies.Add(wtInDB);
                    }
                }

                //If a stack has been selected I add it to the stacks of the new webdeveloper
                wd.Stacks = new List<Stack>();
                foreach (var stack in viewModel.Stacks)
                {
                    if (stack.IsSelected)
                    {
                        var stackInDB = context.Stack.Single(x => x.Id == stack.Id);
                        wd.Stacks.Add(stackInDB);
                    }
                }

                context.WebDeveloper.Add(wd);

                context.SaveChanges();
            }

            //Since the two navigation are not deserialized from the datatble I null them, thery are not necessary
            wd.WebTechnologies = null;
            wd.Stacks = null;
            return Created(new Uri(Request.RequestUri + "/" + wd.Id), wd);
        }

        // PUT api/webdevelopers/1
        [HttpPut]
        public IHttpActionResult EditWebDeveloper(int id, WebDeveloperViewModel viewModel)
        {
            //Check model validity
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var context = new Context())
            {
                //Retrieve the web developer from the db and assign the new properties taken from the view model
                var wdInDB = context.WebDeveloper.Include(x => x.Stacks).Include(x => x.WebTechnologies).SingleOrDefault(x => x.Id == id);

                if (wdInDB == null)
                    return NotFound();

                wdInDB.Address = viewModel.Address;
                wdInDB.Comments = viewModel.Comments;
                wdInDB.ContactPhone = viewModel.ContactPhone;
                wdInDB.DayOfBirth = viewModel.DayOfBirth;
                wdInDB.Email = viewModel.Email;
                wdInDB.FirstName = viewModel.FirstName;
                wdInDB.LastName = viewModel.LastName;
                wdInDB.YearsOfExperience = viewModel.YearsOfExperience;

                //If web technology is selected and it wasn't in the list of the current web technology of
                // the web developer I add it, otherwise if web technology isn't selected and it was in the 
                // list of the current web technology of the web developer I remove it
                foreach (var wt in viewModel.WebTechnologies)
                {
                    if (wt.IsSelected && !wdInDB.WebTechnologies.Any(x => x.Id == wt.Id))
                    {
                        var wtInDB = context.WebTechnology.Single(x => x.Id == wt.Id);
                        wdInDB.WebTechnologies.Add(wtInDB);
                    }
                    else if (!wt.IsSelected && wdInDB.WebTechnologies.Any(x => x.Id == wt.Id))
                    {
                        var wtInDB = context.WebTechnology.Single(x => x.Id == wt.Id);
                        wdInDB.WebTechnologies.Remove(wtInDB);
                    }
                }

                //If stack is selected and it wasn't in the list of the current stacky of
                // the web developer I add it, otherwise if stack isn't selected and it was in the 
                // list of the current stack of the web developer I remove it
                foreach (var stack in viewModel.Stacks)
                {
                    if (stack.IsSelected && !wdInDB.Stacks.Any(x => x.Id == stack.Id))
                    {
                        var stackInDB = context.Stack.Single(x => x.Id == stack.Id);
                        wdInDB.Stacks.Add(stackInDB);
                    }
                    else if (!stack.IsSelected && wdInDB.Stacks.Any(x => x.Id == stack.Id))
                    {
                        var stackInDB = context.Stack.Single(x => x.Id == stack.Id);
                        wdInDB.Stacks.Remove(stackInDB);
                    }
                }

                context.SaveChanges();
            }

            return Ok();
        }

        //DELETE api/webdevelopers/1
        [HttpDelete]
        public IHttpActionResult DeleteWebDeveloper(int id)
        {
            using(var context = new Context())
            {
                //Select the web developer in order to remove it from the DB
                var wdInDB = context.WebDeveloper.Include(x => x.Stacks).Include(x => x.WebTechnologies).SingleOrDefault(x => x.Id == id);

                if (wdInDB == null)
                    return NotFound();

                context.WebDeveloper.Remove(wdInDB);

                context.SaveChanges();
            }

            return Ok();
        }
    }
}
