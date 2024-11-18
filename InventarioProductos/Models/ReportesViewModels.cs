namespace InventarioProductos.Models
{
    public class ReportesViewModels
    {
        public List<Product> ProductosMasVendidos { get; set; }
        public List<Product> ProductosConMenorStocks { get; set; }
        public List<Product> ProductosMenosVendidos { get; set; }

    }
}
