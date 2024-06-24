using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loja.Data;
using Loja.Models;
using Microsoft.AspNetCore.Authorization;

namespace Loja.Controllers
{
    public class VendasController : Controller
    {
        private readonly LojaContext _context;

        public VendasController(LojaContext context)
        {
            _context = context;
        }

        // GET: Vendas

        /*public async Task<IActionResult> Index()
        {
            var lojaContext = _context.Venda.Include(v => v.Carro).Include(v => v.Cliente).Include(v => v.Vendedor);
            return View(await lojaContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var vendas = from m in _context.Venda select m; //select * from movie

            if (startDate.HasValue)
            {
                vendas = vendas.Where(s => s.DataEntrega >= startDate.Value); // where ReleaseDate >= startDate
            }
            if (endDate.HasValue)
            {
                vendas = vendas.Where(s => s.DataEntrega <= endDate.Value); // where ReleaseDate >= endDate
            }

            return View(await vendas.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Carro)
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .FirstOrDefaultAsync(m => m.VendaId == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["CarroId"] = new SelectList(_context.Carro, "CarroId", "CarroId");
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId");
            ViewData["VendedorId"] = new SelectList(_context.Usuario, "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendaId,CarroId,ClienteId,ValorVenda,ValorEntrada,IsAVista,Parcelas,ValorJuros,BancoFinanciamento,VendedorId,ComissaoVendedor,DataEntrega")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "CarroId", "CarroId", venda.CarroId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", venda.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Usuario, "UsuarioId", "UsuarioId", venda.VendedorId);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "CarroId", "CarroId", venda.CarroId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", venda.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Usuario, "UsuarioId", "UsuarioId", venda.VendedorId);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendaId,CarroId,ClienteId,ValorVenda,ValorEntrada,IsAVista,Parcelas,ValorJuros,BancoFinanciamento,VendedorId,ComissaoVendedor,DataEntrega")] Venda venda)
        {
            if (id != venda.VendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.VendaId))
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
            ViewData["CarroId"] = new SelectList(_context.Carro, "CarroId", "CarroId", venda.CarroId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", venda.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Usuario, "UsuarioId", "UsuarioId", venda.VendedorId);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Carro)
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .FirstOrDefaultAsync(m => m.VendaId == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Venda.FindAsync(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Venda.Any(e => e.VendaId == id);
        }
    }
}
