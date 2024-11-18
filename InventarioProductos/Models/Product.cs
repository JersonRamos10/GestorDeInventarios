using Microsoft.EntityFrameworkCore;

namespace InventarioProductos.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [Precision(18,2)]
        public decimal Precio { get; set; }
      
        public int Cantidad { get; set; }
       
    }
}
