﻿using System.ComponentModel.DataAnnotations;

namespace Usuarios.Data.Requests
{
    public class AtivaContaRequest
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string CodigoDeAtivacao { get; set; }

       
    }
}
