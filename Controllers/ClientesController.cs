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
    public class ClientesController : Controller
    {
        private readonly DB _context;

        public ClientesController(DB context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }
        public async Task<IActionResult> Activate(int? id)
        {
            if (id != null)
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(w => w.ID == id);
                if (cliente != null)
                {
                    switch (cliente.Ativo)
                    {
                        case true:
                            cliente.Ativo = false;
                            TempData["msg"] = "<script>alert('Cliente inativado!');</script>";
                            break;
                        case false:
                            cliente.Ativo = true;
                            TempData["msg"] = "<script>alert('Cliente ativado!');</script>";
                            break;
                    }
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,CNPJ")] Cliente cliente)
        {
            if (BaseModel.IsValid(cliente.CNPJ))
            {
                if (ModelState.IsValid)
                {
                    if (!_context.Clientes.Any(w => w.CNPJ == cliente.CNPJ))
                    {
                        cliente.Ativo = true;
                        _context.Add(cliente);
                        await _context.SaveChangesAsync();
                        TempData["msg"] = "<script>alert('Cliente criado!');</script>";

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Já possui um cliente com este CNPJ!');</script>";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('CNPJ inválido!');</script>";
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,CNPJ,Ativo")] Cliente cliente)
        {
            if (id != cliente.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "<script>alert('Cadastro alterado!');</script>";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ID))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ID == id);
        }
    }
}
