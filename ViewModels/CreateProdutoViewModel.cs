using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels
{
    public class CreateProdutoViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        public string? Desc { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatório!")]
        public int Quantidade { get; set; } 

        [Required(ErrorMessage = "Valor é obrigatório!")]
        public float Valor { get; set; } 


    }
}

// public int Id { get; set; }
// public string? Nome { get; set; }
// public string? Desc { get; set; }
// public int Quantidade { get; set; }
// public float Valor { get; set; }