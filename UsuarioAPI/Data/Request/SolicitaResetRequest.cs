using System.ComponentModel.DataAnnotations;

namespace UsuariosApi_.Data.Request
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
