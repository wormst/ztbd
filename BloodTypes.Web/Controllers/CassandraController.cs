using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypes.Web.Controllers
{
    public class CassandraController : Controller
    {
        public IActionResult Index()
        {


            return View();
        }
    }
}
