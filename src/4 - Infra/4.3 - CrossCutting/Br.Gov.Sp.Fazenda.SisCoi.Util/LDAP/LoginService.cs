using Br.Gov.Sp.Fazenda.SisCoi.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Br.Gov.Sp.Fazenda.SisCoi.Util.LDAP
{
    public class LoginService
    {
        private static ConfigurationRoot _configurationRoot;

        static LoginService()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables();
            _configurationRoot = (ConfigurationRoot)builder.Build();
        }
        public static bool Login(string senha)
        {
            string _senha = _configurationRoot["AppSettings:Senha"].ToString();
            if (senha == _senha)
                return true;

            return false;
        }

        public static UsuarioEntity Autenticar(UsuarioEntity usuario)
        {
            string domain = _configurationRoot["AppSettings:ADDomain"];
            string adminGroup = _configurationRoot["AppSettings:ADAdminGroup"];
            string userGroup = _configurationRoot["AppSettings:ADUserGroup"];

            usuario.IsAuthenticated = false;
            usuario.IsDtiCoi = false;
            usuario.IsSisCoiAdmin = false;

            LDAPServices ldap = new LDAPServices();
            usuario = ldap.IsAuthenticatedAD(usuario);

            if (!usuario.IsAuthenticated)
                return usuario;

            if (CheckGroupAdmin(usuario, adminGroup))
            {
                usuario.IsDtiCoi = true;
                usuario.IsSisCoiAdmin = true;
                return usuario;
            }

            if (CheckGroupDtiCoi(usuario, userGroup))
                usuario.IsDtiCoi = true;

            return usuario;
        }

        private static bool CheckGroupAdmin(UsuarioEntity usuarioEntity, string adminGroup)
        {
            bool member = false;

            if (usuarioEntity.Groups.IndexOf(adminGroup) != -1)
            {
                member = true;
            }

            return member;
        }

        private static bool CheckGroupDtiCoi(UsuarioEntity usuarioEntity, string userGroup)
        {
            bool member = false;

            if (usuarioEntity.Groups.IndexOf(userGroup) != -1)
            {
                member = true;
            }

            return member;
        }

        private void RunImpersonated(string userID)
        {
            //bool isMember = false;
            //WindowsIdentity identity = new WindowsIdentity(userID);
            //WindowsIdentity.RunImpersonated(identity.AccessToken, () =>
            //{
            //    // This prints the expected output
            //    //var ident = WindowsIdentity.GetCurrent();
            //    //PrincipalContext adCtx = new PrincipalContext(identity.BootstrapContext, Domain);
            //    //UserPrincipal user = UserPrincipal.FindByIdentity(adCtx, userID);
            //    //GroupPrincipal group = GroupPrincipal.FindByIdentity(adCtx, groupName);

            //    //if ((user != null) && (group != null))
            //    //{
            //    //    isMember = group.GetMembers(true).Contains(user);
            //    //}
            //});
        }
    }
}
