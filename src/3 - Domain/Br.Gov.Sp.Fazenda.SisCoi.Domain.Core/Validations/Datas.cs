using System;
using System.Text.RegularExpressions;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Validations
{
    public class Datas
    {
        public static string IsValid(string dtInicial, string dtFinal)
        {
            string mensagem = string.Empty;
            Regex regex = new Regex(@"\d{2}\-\d{2}\-\d{4}");
          
            if (string.IsNullOrEmpty(dtInicial) || string.IsNullOrEmpty(dtFinal))
            {
                mensagem = "Data Inicial e Final são obrigatórias.";
            }
            else if (!regex.Match(dtInicial).Success || !regex.Match(dtFinal).Success)
            {
                mensagem = "Data Inicial ou Final são inválidas.";
            }
            else if (DateTime.Parse(dtInicial.Split("-")[2] + "-" + dtInicial.Split("-")[1] + "-" + dtInicial.Split("-")[0]).Date >
                 DateTime.Parse(dtFinal.Split("-")[2] + "-" + dtFinal.Split("-")[1] + "-" + dtFinal.Split("-")[0]).Date)
            {
                mensagem = "Data Inicial não pode ser maior que Data Final.";
            }
            return mensagem;
        }
    }
}
