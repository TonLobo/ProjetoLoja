using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loja.Data;
using Loja.Models;

namespace Loja.Controllers
{
    public class DescontosController : Controller
    {
        private readonly LojaContext _context;

        public DescontosController(LojaContext context)
        {
            _context = context;

        }
        [AuthorizeRole("Gerente")]
        public IActionResult Desconto()
        {
            return View();
        }

        // GET: Descontos
        /*public async Task<IActionResult> Index()
        {
            var lojaContext = _context.Desconto.Include(d => d.Venda);
            return View(await lojaContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Desconto == null)
            {
                return Problem("Tabela inexistente");
            }
            var descontos = from m in _context.Desconto select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                descontos = descontos.Where(s => s.Venda.BancoFinanciamento!.Contains(searchString));
            }

            return View(await descontos.ToListAsync());
        }

        // GET: Descontos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto
                .Include(d => d.Venda)
                .FirstOrDefaultAsync(m => m.DescontoId == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // GET: Descontos/Create
        public IActionResult Create()
        {
            ViewData["VendaId"] = new SelectList(_context.Set<Venda>(), "VendaId", "VendaId");
            return View();
        }

        // POST: Descontos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DescontoId,VendaId,ValorDesconto,Aprovado")] Desconto desconto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desconto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendaId"] = new SelectList(_context.Set<Venda>(), "VendaId", "VendaId", desconto.VendaId);
            return View(desconto);
        }

        // GET: Descontos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto.FindAsync(id);
            if (desconto == null)
            {
                return NotFound();
            }
            ViewData["VendaId"] = new SelectList(_context.Set<Venda>(), "VendaId", "VendaId", desconto.VendaId);
            return View(desconto);
        }

        // POST: Descontos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DescontoId,VendaId,ValorDesconto,Aprovado")] Desconto desconto)
        {
            if (id != desconto.DescontoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desconto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescontoExists(desconto.DescontoId))
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
            ViewData["VendaId"] = new SelectList(_context.Set<Venda>(), "VendaId", "VendaId", desconto.VendaId);
            return View(desconto);
        }

        // GET: Descontos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto
                .Include(d => d.Venda)
                .FirstOrDefaultAsync(m => m.DescontoId == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // POST: Descontos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desconto = await _context.Desconto.FindAsync(id);
            if (desconto != null)
            {
                _context.Desconto.Remove(desconto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescontoExists(int id)
        {
            return _context.Desconto.Any(e => e.DescontoId == id);
        }
    }
}
