namespace webapi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
