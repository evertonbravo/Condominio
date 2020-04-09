using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoDeCondominio.Data;
using GestaoDeCondominio.Models;

namespace GestaoDeCondominio.Controllers
{
    public class ApartamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apartamentos.ToListAsync());
        }

        public async Task<IActionResult> ListMoradores(int? ApartamentoId)
        {
            return View(await _context.Moradores.Where(morador => morador.IdApartamento.Equals(ApartamentoId)).ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamentoRecuperado = await _context.Apartamentos
                .FirstOrDefaultAsync(apartamento => apartamento.Id == id);
            if (apartamentoRecuperado == null)
            {
                return NotFound();
            }

            return View(apartamentoRecuperado);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Bloco,MoradorId,Morador")] Apartamento apartamento)
        {
            if (ModelState.IsValid)
            {                
                _context.Add(apartamento);
                var moradordoApartamento = apartamento.Morador;
                await _context.SaveChangesAsync();
                moradordoApartamento.IdApartamento = apartamento.Id;
                apartamento.MoradorId = moradordoApartamento.Id;
                _context.Update(moradordoApartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartamento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamentoRecuperado = await _context.Apartamentos.FindAsync(id);
            if (apartamentoRecuperado == null)
            {
                return NotFound();
            }
            return View(apartamentoRecuperado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Bloco")] Apartamento apartamento)
        {
            if (id != apartamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartamentoExists(apartamento.Id))
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
            return View(apartamento);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _context.Apartamentos
                .FirstOrDefaultAsync(apartamento => apartamento.Id == id);
            if (apartamento == null)
            {
                return NotFound();
            }

            return View(apartamento);
        }

        // POST: Apartamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartamento = await _context.Apartamentos.FindAsync(id);
            _context.Apartamentos.Remove(apartamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartamentoExists(int id)
        {
            return _context.Apartamentos.Any(e => e.Id == id);
        }
    }
}
