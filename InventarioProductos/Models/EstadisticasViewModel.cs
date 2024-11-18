namespace InventarioProductos.Models
{
    public class EstadisticasViewModel
    {

        public decimal TotalInventario { get; set; }
        public int TotalProductos { get; set; }
      
        public List<Product> Productos { get; set; }
        public List<Product> ProductosMasVendidos { get; set; }

        public List<Product> ProductosMenosVendidos { get; set; }
    }
}
