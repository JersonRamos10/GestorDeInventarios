// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InventarioProductos.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Metodo para cerrar sesión
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // Cerrar sesion
            await _signInManager.SignOutAsync();
            // Redirigir al login o a la página de inicio (si no se proporciona un returnUrl)
            return Redirect(returnUrl ?? "/Identity/Account/Login");
        }
    }
}
