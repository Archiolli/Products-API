namespace Inventory.Models
{
    public class Transacao
    {
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }
    }
}
public enum TipoTransacao
{
    Entrada = 0,
    Saida = 1
}