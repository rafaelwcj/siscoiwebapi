using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities
{
    public class UsuarioEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Departamento { get; set; }
        public string Groups { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsDtiCoi { get; set; }
        public bool IsSisCoiAdmin { get; set; }
        public string Token { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberLogin { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}
