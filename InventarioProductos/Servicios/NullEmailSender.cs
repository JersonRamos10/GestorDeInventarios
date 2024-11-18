using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace InventarioProductos.Services
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           // simula el envio de correo electrónico
            return Task.CompletedTask;
        }
    }
}
