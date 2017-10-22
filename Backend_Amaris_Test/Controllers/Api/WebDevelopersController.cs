using Backend_Amaris_Test.Models;
using Backend_Amaris_Test.ViewModel;
using System;
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
            List<WebDeveloper> wdList;
            using (var context = new Context())
            {
                wdList = context.WebDeveloper.ToList();
            }

            return Ok(wdList);
        }

        // GET api/webdevelopers
        [HttpGet]
        public IHttpActionResult WebDeveloper(int id)
        {
            WebDeveloper wd;
            using (var context = new Context())
            {
                wd = context.WebDeveloper.SingleOrDefault(x => x.Id == id);
            }

            return Ok(wd);
        }

        [HttpPut]
        public IHttpActionResult EditWebDeveloper(int id, WebDeveloperViewModel viewModel)
        {
            return Ok();
        }
    }
}
