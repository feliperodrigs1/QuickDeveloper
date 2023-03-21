﻿using System;

namespace UsuariosApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Competencias { get; set; }
        public string InfoAdicionais { get; set; }
    }
}
