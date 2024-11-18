using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventarioProductos.Data;
using InventarioProductos.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventarioProductos.Controllers
{
     
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string terminoBusqueda, decimal? precioMinimo, decimal? precioMaximo, int? cantidadMinima, int? cantidadMaxima)
        {
            var productos = _context.Products.AsQueryable();

            // Busqueda por termino
            if (!string.IsNullOrEmpty(terminoBusqueda))
            {
                productos = productos.Where(p => p.Nombre.Contains(terminoBusqueda)|| p.Descripcion.Contains(terminoBusqueda));
            }
            //filtrar por precio minimo
            if(precioMinimo.HasValue)
            {
                productos = productos.Where(p => p.Precio >= precioMinimo.Value);
            }

            // Filtrar por precio máximo
            if (precioMaximo.HasValue)
            {
                productos = productos.Where(p => p.Precio <= precioMaximo.Value);
            }

            //Filtros por cantidad
            if (cantidadMinima.HasValue)
            {
                productos = productos.Where(p => p.Cantidad >= cantidadMinima.Value);
            }

            if (cantidadMaxima.HasValue)
            {
                productos = productos.Where(p => p.Cantidad <= cantidadMaxima.Value);
            
            }
            var listaProductos = await productos.ToListAsync();

            // Pasar los parametros y la lista de productos a la vista
            ViewData["terminoBusqueda"] = terminoBusqueda;
            ViewData["precioMinimo"] = precioMinimo;
            ViewData["precioMaximo"] = precioMaximo;
            ViewData["cantidadMinima"] = cantidadMinima;
            ViewData["cantidadMaxima"] = cantidadMaxima;

            return View(listaProductos);

           
        }

        //Metodo para mostrar detalles de un producto
        // GET: Products/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")] // solo admimistradores pueden acceder
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,Cantidad")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,Cantidad")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
