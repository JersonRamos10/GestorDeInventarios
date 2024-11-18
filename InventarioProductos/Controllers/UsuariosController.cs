using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioProductos.Data;
using InventarioProductos.Models;
using Microsoft.AspNetCore.Identity;

namespace InventarioProductos.Controllers
{
    [Authorize(Roles = "Admin")]  // Esto asegura que solo los administradores puedan acceder a esta acción
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuariosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Usuarios/Index
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Users.ToListAsync();

            // Crear una lista de usuarios con sus roles
            var usuariosConRoles = new List<UsuariosRolesModel>();

            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                usuariosConRoles.Add(new UsuariosRolesModel
                {
                    User = usuario,
                    Roles = roles
                });
            }

            return View(usuariosConRoles);
        }

        // GET: Usuarios/Editar/5
        public async Task<IActionResult> Editar(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }


            return View(user);
        }

        // POST: Usuarios/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(string id, IdentityUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await _userManager.FindByIdAsync(id);
                    if (currentUser == null)  // Verificar si el usuario existe
                    {
                        return NotFound();
                    }

                    currentUser.UserName = user.UserName;
                    currentUser.Email = user.Email;

                    var result = await _userManager.UpdateAsync(currentUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Eliminar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // ID no proporcionado
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Usuario no encontrado
            }

            var model = new EliminarUsuarioModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }



        // POST: Usuarios/EliminarConfirmado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID no proporcionado.");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Usuario no encontrado
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index"); // Redirige al index
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Eliminar", new EliminarUsuarioModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            });
          
        }

    }
}