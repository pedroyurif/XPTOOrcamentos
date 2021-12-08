using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XPTOOrcamentos;
using XPTOOrcamentos.Models;

namespace XPTOOrcamentos.Controllers
{
    public class OrdemServicosController : Controller
    {
        private readonly DB _context;
        public string ValorString { get; set; }
        public OrdemServicosController(DB context)
        {
            _context = context;
        }

        // GET: OrdemServicos
        public async Task<IActionResult> Index()
        {
            var dB = _context.OrdensServico.Include(o => o.Cliente).Include(o => o.Prestador);
            return View(await dB.ToListAsync());
        }

        // GET: OrdemServicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordemServico = await _context.OrdensServico
                .Include(o => o.Cliente)
                .Include(o => o.Prestador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordemServico == null)
            {
                return NotFound();
            }

            return View(ordemServico);
        }

        // GET: OrdemServicos/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(w => w.Ativo), "ID", "Nome");
            ViewData["PrestadorID"] = new SelectList(_context.PrestadoresServico.Where(w => w.Ativo), "ID", "Nome");
            return View();
        }

        // POST: OrdemServicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TituloServico,ClienteID,PrestadorID,DataExecucao,Valor")] OrdemServico ordemServico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordemServico);
                await _context.SaveChangesAsync();
                TempData["msg"] = "<script>alert('Ordem de serviço criada!');</script>";

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(w => w.Ativo), "Nome", "ID", ordemServico.ClienteID);
            ViewData["PrestadorID"] = new SelectList(_context.PrestadoresServico.Where(w => w.Ativo), "ID", "Nome", ordemServico.PrestadorID);
            return View(ordemServico);
        }

        // GET: OrdemServicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordemServico = await _context.OrdensServico.FindAsync(id);
            if (ordemServico == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(w => w.Ativo), "ID", "Nome", ordemServico.ClienteID);
            ViewData["PrestadorID"] = new SelectList(_context.PrestadoresServico.Where(w => w.Ativo), "ID", "Nome", ordemServico.PrestadorID);
            return View(ordemServico);
        }

        // POST: OrdemServicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TituloServico,ClienteID,PrestadorID,DataExecucao,Valor")] OrdemServico ordemServico)
        {
            if (id != ordemServico.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordemServico);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "<script>alert('Ordem de serviço alterada!');</script>";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdemServicoExists(ordemServico.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(w => w.Ativo), "ID", "Nome", ordemServico.ClienteID);
            ViewData["PrestadorID"] = new SelectList(_context.PrestadoresServico.Where(w => w.Ativo), "ID", "Nome", ordemServico.PrestadorID);
            return View(ordemServico);
        }

        // GET: OrdemServicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordemServico = await _context.OrdensServico
                .Include(o => o.Cliente)
                .Include(o => o.Prestador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordemServico == null)
            {
                return NotFound();
            }

            return View(ordemServico);
        }

        // POST: OrdemServicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordemServico = await _context.OrdensServico.FindAsync(id);
            _context.OrdensServico.Remove(ordemServico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdemServicoExists(int id)
        {
            return _context.OrdensServico.Any(e => e.ID == id);
        }
    }
}
