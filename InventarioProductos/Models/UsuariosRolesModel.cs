using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventarioProductos.Models
{
    public class UsuariosRolesModel : Controller
    {
        public IdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
