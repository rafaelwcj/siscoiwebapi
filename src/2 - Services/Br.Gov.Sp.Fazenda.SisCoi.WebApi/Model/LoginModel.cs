namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.Model
{
    public class LoginModel
    {
        public required string Login { get; set; }
        public required string Senha { get; set; }
        public required string Grupo { get; set; }
    }
}
