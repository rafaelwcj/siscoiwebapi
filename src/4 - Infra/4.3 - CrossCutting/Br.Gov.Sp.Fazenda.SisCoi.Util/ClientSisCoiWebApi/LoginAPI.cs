using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.IO;
using System.Text.Json;

namespace Br.Gov.Sp.Fazenda.SisCoi.Util.ClientSisCoiWebApi
{
    public class LoginAPI
    {
        public static string Conectar(string username, string password)
        {
            var token = string.Empty;

            try
            {
                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .AddEnvironmentVariables();
                var config = builder.Build();

                string urlBase = config["ApplicationSettings:UrlBase"].ToString();
                var loginAPI = RestService.For<ILoginAPI>(urlBase);
                var credencial = new Credencial
                {
                    Username = username,
                    Password = password
                };
                var jsonlogin = JsonSerializer.Serialize(credencial);
                var resultLogin = loginAPI.Post(jsonlogin).Result;
                var dataSerialize = JsonSerializer.Serialize(resultLogin.GetProperty("data"));
                var dataDeserialize = JsonSerializer.Deserialize<string[]>(dataSerialize);
                token = dataDeserialize[1];
            }
            catch (Exception)
            {
                throw;
            }
            return token;
        }
    }
}
