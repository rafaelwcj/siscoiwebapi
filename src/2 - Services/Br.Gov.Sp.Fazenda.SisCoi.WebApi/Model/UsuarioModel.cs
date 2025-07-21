namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Model
{
    public class UsuarioModel
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Grupo { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsDtiCoi { get; set; }
        public bool IsSisCoiAdmin { get; set; }

    }
}
