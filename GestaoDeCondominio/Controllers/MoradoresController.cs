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
    public class MoradoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoradoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Moradores.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moradorRecuperadodoPeloID = await _context.Moradores
                .FirstOrDefaultAsync(morador => morador.Id == id);
            if (moradorRecuperadodoPeloID == null)
            {
                return NotFound();
            }

            return View(moradorRecuperadodoPeloID);
        }
        
        public IActionResult Create()
        {
            ViewBag.Apartamento = _context.Apartamentos.Select(c => new SelectListItem()
            { Text = c.Numero.ToString() +"-"+c.Bloco , Value = c.Id.ToString() }).ToList();
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataDeNascimento,Telefone,Cpf,Email,IdApartamento")] Morador moradorPreenchido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moradorPreenchido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Apartamento = _context.Apartamentos.Select(c => new SelectListItem()
            { Text = c.Numero.ToString() + "-" + c.Bloco, Value = c.Id.ToString() }).ToList();
            
            return View(moradorPreenchido);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moradorRecuperadodoPeloID = await _context.Moradores.FindAsync(id);
            if (moradorRecuperadodoPeloID == null)
            {
                return NotFound();
            }
            return View(moradorRecuperadodoPeloID);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataDeNascimento,Telefone,Cpf,Email,IdApartamento")] Morador moradorPreenchido)
        {
            if (id != moradorPreenchido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moradorPreenchido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoradorExists(moradorPreenchido.Id))
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
            return View(moradorPreenchido);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moradorRecuperadodoPeloID = await _context.Moradores
                .FirstOrDefaultAsync(morador => morador.Id == id);
            if (moradorRecuperadodoPeloID == null)
            {
                return NotFound();
            }

            return View(moradorRecuperadodoPeloID);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moradorRecuperadodoPeloID = await _context.Moradores.FindAsync(id);
            _context.Moradores.Remove(moradorRecuperadodoPeloID);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoradorExists(int id)
        {
            return _context.Moradores.Any(e => e.Id == id);
        }
        
    }
}
