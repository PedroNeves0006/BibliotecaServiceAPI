﻿using BibliotecaServiceAPI.Enums;

namespace BibliotecaServiceAPI.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Email { get; set; }
    }
}
