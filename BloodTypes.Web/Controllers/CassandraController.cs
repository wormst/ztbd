using BloodTypes.Infrastructure;
using BloodTypes.Infrastructure.Repositories;
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
        private readonly CassandraDbContext dbContext;

        public CassandraController(CassandraDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => this.dbContext.Clusters.GetAll().Take(20)));
        }
    }
}
