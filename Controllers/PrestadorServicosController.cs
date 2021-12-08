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
    public class PrestadorServicosController : Controller
    {
        private readonly DB _context;

        public PrestadorServicosController(DB context)
        {
            _context = context;
        }

        // GET: PrestadorServicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrestadoresServico.ToListAsync());
        }
        public async Task<IActionResult> Activate(int? id)
        {
            if (id != null)
            {
                var prestadorServico = await _context.PrestadoresServico.FirstOrDefaultAsync(w => w.ID == id);
                if (prestadorServico != null)
                {
                    switch (prestadorServico.Ativo)
                    {
                        case true:
                            prestadorServico.Ativo = false;
                            TempData["msg"] = "<script>alert('Prestador inativado!');</script>";
                            break;
                        case false:
                            prestadorServico.Ativo = true;
                            TempData["msg"] = "<script>alert('Prestador ativado!');</script>";
                            break;
                    }
                    _context.Update(prestadorServico);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["msg"] = BaseModel.RetornaErro();
                    return NotFound();
                }
            }
            else
            {
                TempData["msg"] = BaseModel.RetornaErro();
                return NotFound();
            }
        }

        // GET: PrestadorServicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrestadorServicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,CPF")] PrestadorServico prestadorServico)
        {
            if (BaseModel.IsValid(prestadorServico.CPF))
            {
                if (!_context.PrestadoresServico.Any(w => w.CPF == prestadorServico.CPF))
                {
                    if (ModelState.IsValid)
                    {
                        prestadorServico.Ativo = true;
                        _context.Add(prestadorServico);
                        await _context.SaveChangesAsync();
                        TempData["msg"] = "<script>alert('Prestador criado!');</script>";

                        return RedirectToAction(nameof(Index));
                    }
                    return View(prestadorServico);
                }
                else
                {
                    TempData["msg"] = "<script>alert('Já possui um Prestador com este CPF!');</script>";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));

            }
        }

        // GET: PrestadorServicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["msg"] = BaseModel.RetornaErro();
                return NotFound();
            }

            var prestadorServico = await _context.PrestadoresServico.FindAsync(id);
            if (prestadorServico == null)
            {
                TempData["msg"] = BaseModel.RetornaErro();
                return NotFound();
            }
            return View(prestadorServico);
        }

        // POST: PrestadorServicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,CPF,Ativo")] PrestadorServico prestadorServico)
        {
            if (id != prestadorServico.ID)
            {
                return NotFound();
            }
            if (BaseModel.IsValid(prestadorServico.CPF))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(prestadorServico);
                        await _context.SaveChangesAsync();
                        TempData["msg"] = "<script>alert('Cadastro alterado!');</script>";

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        TempData["msg"] = BaseModel.RetornaErro();
                        if (!PrestadorServicoExists(prestadorServico.ID))
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
            }
            else
            {
                TempData["msg"] = "<script>alert('CPF inválido!');</script>";

            }
            return View(prestadorServico);
        }

        // GET: PrestadorServicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["msg"] = BaseModel.RetornaErro();
                return NotFound();
            }

            var prestadorServico = await _context.PrestadoresServico
                .FirstOrDefaultAsync(m => m.ID == id);
            if (prestadorServico == null)
            {
                TempData["msg"] = BaseModel.RetornaErro();
                return NotFound();
            }

            return View(prestadorServico);
        }

        // POST: PrestadorServicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestadorServico = await _context.PrestadoresServico.FindAsync(id);
            _context.PrestadoresServico.Remove(prestadorServico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestadorServicoExists(int id)
        {
            return _context.PrestadoresServico.Any(e => e.ID == id);
        }
    }
}
