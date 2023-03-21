using System.ComponentModel.DataAnnotations;

namespace UsuariosApi_.Data.Request
{
    public class AtivaContaRequest
    {
        [Required]
        public string CodigoDeAtivacao { get; set; }
        [Required]
        public int UsuarioId { get; set; }
    }
}
