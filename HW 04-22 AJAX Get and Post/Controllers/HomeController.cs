using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HW_04_22_AJAX_Get_and_Post.Models;
using _4_22AjaxGetPost.data;

namespace HW_04_22_AJAX_Get_and_Post.Controllers
{
    public class HomeController : Controller
    {

        private String _conn = @"Data Source =.\sqlExpress;Initial Catalog = PeopleDB; Integrated Security = True;";

        public IActionResult Index()
        {
            var db = new PersonDB(_conn);

            return View(db.GetPeople());
        }

        public IActionResult GetPeople()
        {
            var db = new PersonDB(_conn);
            IEnumerable<Person> ppl = db.GetPeople();
            return Json(db.GetPeople());
        }


        [HttpPost]
        public IActionResult AddPerson(Person p)
        {
            var db = new PersonDB(_conn);
            int id = db.AddPerson(p);         

            return Json(p);
        }

        [HttpPost]
        public void DeletePerson(int personId)
        {
            var db = new PersonDB(_conn);
            db.DeletePerson(personId);
        }

        [HttpPost]
        public void UpdatePerson(Person p)
        {
            var db = new PersonDB(_conn);
            db.UpdatePerson(p);
            //return Redirect("/");

        }


    }

}

                           