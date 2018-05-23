using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodTypes.Core.Models;
using BloodTypes.Infrastructure;

namespace BloodTypes.Web.Views
{
    public class BloodAmountsController : Controller
    {
        private readonly CassandraDbContext _context;

        public BloodAmountsController(CassandraDbContext context)
        {
            _context = context;
        }

        // GET: BloodAmounts
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => _context.BloodAmounts.GetAll()));
        }

        // GET: BloodAmounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodAmount = await Task.Run(() => _context.BloodAmounts.Get(id));
            if (bloodAmount == null)
            {
                return NotFound();
            }

            return View(bloodAmount);
        }

        // GET: BloodAmounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BloodAmounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Quantity")] BloodAmount bloodAmount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloodAmount);
                return RedirectToAction(nameof(Index));
            }
            return View(bloodAmount);
        }

        // GET: BloodAmounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodAmount = await Task.Run(() => _context.BloodAmounts.Get(id));
            if (bloodAmount == null)
            {
                return NotFound();
            }
            return View(bloodAmount);
        }

        // POST: BloodAmounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Type,Quantity")] BloodAmount bloodAmount)
        {
            if (id != bloodAmount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodAmount);
                }
                catch
                {
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bloodAmount);
        }

        // GET: BloodAmounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodAmount = await Task.Run(() => _context.BloodAmounts.Get(id));
            if (bloodAmount == null)
            {
                return NotFound();
            }

            return View(bloodAmount);
        }

        // POST: BloodAmounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bloodAmount = await Task.Run(() => _context.BloodAmounts.Get(id));
            _context.BloodAmounts.Remove(bloodAmount);

            return RedirectToAction(nameof(Index));
        }
    }
}
