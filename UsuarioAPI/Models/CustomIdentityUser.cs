using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosApi_.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }
        public string Competencias { get; set; }
        public string InfoAdicionais { get; set; }
    }
}
