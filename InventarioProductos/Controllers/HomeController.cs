using InventarioProductos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using InventarioProductos.Data;
using Microsoft.EntityFrameworkCore;

namespace InventarioProductos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context; // Inyeccion del contexto de la base de datos

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context; // Asignar el contexto
        }

        // Método para mostrar el dashboard del administrador
        public async Task<IActionResult> AdminDashboard()
        {
            // Consulta la lista de productos
            var productos = await _context.Products.ToListAsync();
            return View(productos); // Pasar la lista de productos a la vista
        }

        // Método para la pagina de inicio
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return View("IndexAdmin"); // Redirige a la vista exclusiva para admin
                }
            }

            return View("Index"); // Vista para usuarios estandar
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
