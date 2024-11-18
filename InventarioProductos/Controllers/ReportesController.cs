using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioProductos.Data;
using InventarioProductos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioProductos.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reportes
        public async Task<IActionResult> Index()
        {
           var model = new ReportesYEstadisticasViewModel
{
    Estadisticas = new EstadisticasViewModel
    {
        TotalInventario = await _context.Products.SumAsync(p => p.Precio * p.Cantidad),
        TotalProductos = await _context.Products.CountAsync()
    },
    Reportes = new ReportesViewModels
    {
        ProductosMasVendidos = await _context.Products
            .OrderByDescending(p => p.Cantidad)  // 'Cantidad' representa las ventas
            .Take(5)
            .ToListAsync(),
        ProductosConMenorStocks = await _context.Products
            .OrderBy(p => p.Cantidad)
            .Take(5)
            .ToListAsync(),
        ProductosMenosVendidos = await _context.Products
            .OrderBy(p => p.Cantidad)  
            .Take(5)
            .ToListAsync()
    }
};

return View(model);


        }

        // GET: Estadísticas (para usuarios no autenticados o vista general)
        public async Task<IActionResult> Estadisticas()
        {
            // Recupera los productos de la base de datos
            var productos = await _context.Products.ToListAsync();

            // Solo estadisticas sin los reportes
            var model = new EstadisticasViewModel
            {
                TotalInventario = await _context.Products.SumAsync(p => p.Precio * p.Cantidad),
                TotalProductos = await _context.Products.CountAsync(),
                Productos = productos // Pasamos los productos al modelo
            };

            return View(model); // Devuelve la vista Estadisticas con el modelo de estadísticas
        }
    }
}
