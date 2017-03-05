using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioDotnet.Controllers;
using PortfolioDotnet.Models;


namespace PortfolioDotnet.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProjects()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetProjects(Projects name)
        {
            List<Projects> projectList = Projects.GetProjects();

            return View(projectList);
        }
    }
}
