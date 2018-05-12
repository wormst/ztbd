using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodTypes.Core.Models;
using BloodTypes.Infrastructure;
using System.Linq;

namespace BloodTypes.Web.Controllers
{
    public class PeopleController : Controller
    {
        private readonly CassandraDbContext dbContext;

        public PeopleController(CassandraDbContext context)
        {
            this.dbContext = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var data = this.dbContext.People.GetAll();
            ViewData["PeopleCount"] = data.Count();

            return View(await Task.Run(() => this.dbContext.People.GetAll().Take(20)));
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await Task.Run(() => this.dbContext.People.Get(id));
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gender,Name,Surname,City,Country,Birthdate,Telephone,BloodType,Weight,Height")] Person person)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.People.Add(person);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var person = await Task.Run(() => this.dbContext.People.Get(id));
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Gender,Name,Surname,City,Country,Birthdate,Telephone,BloodType,Weight,Height")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.dbContext.People.Update(person);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var person = await Task.Run(() => this.dbContext.People.Get(id));
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await Task.Run(() => this.dbContext.People.Get(id));
            this.dbContext.People.Remove(person);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return this.dbContext.People.Get(id) != null;
        }
    }
}
