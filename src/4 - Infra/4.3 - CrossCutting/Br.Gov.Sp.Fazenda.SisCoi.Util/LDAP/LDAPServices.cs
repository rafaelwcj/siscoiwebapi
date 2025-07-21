using Br.Gov.Sp.Fazenda.SisCoi.Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace Br.Gov.Sp.Fazenda.SisCoi.Util.LDAP
{
    public class LDAPServices
    {
        public String _path;
        public String _filterAttribute;

        public UsuarioEntity IsAuthenticatedAD(UsuarioEntity usuarioEntity)
        {
            SearchResult result = null;

            foreach (var AD in DominiosAD())
            {
                _path = AD;
                String domainAndUsername = usuarioEntity.Username;
                DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, usuarioEntity.Password);
                StringBuilder groupNames = new StringBuilder();

                Object obj = entry.NativeObject;

                /*------------------------------------------
                    * PROCURA PELOS USUÁRIO NO AD
                    * ---------------------------------------*/
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + usuarioEntity.Username + ")";
                search.PropertiesToLoad.Add("displayName");
                search.PropertiesToLoad.Add("name");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("department");
                search.PropertiesToLoad.Add("cn");
                result = search.FindOne();

                if (null != result)
                    break;
            }

            if (null == result)
            {
                usuarioEntity.IsAuthenticated = false;
                return usuarioEntity;
            }

            //Update the new path to the user in the directory.
            _path = result.Path;
            _filterAttribute = (String)result.Properties["cn"][0];
            usuarioEntity.DisplayName = (String)result.Properties["displayName"][0];
            usuarioEntity.Email = (String)result.Properties["mail"][0];
            usuarioEntity.Username = (String)result.Properties["cn"][0];
            usuarioEntity.Departamento = (String)result.Properties["department"][0];
            usuarioEntity.Groups = GetGroups();
            usuarioEntity.IsAuthenticated = true;
            return usuarioEntity;
        }

        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                String dn;
                int equalsIndex, commaIndex;
				
                for(int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if(-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append(";");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }

            return groupNames.ToString();
        }

        public ImportacaoEntity consultarAD(string usr, string pwd, List<string> pathsLdap)
        {
            String l = "sAMAccountName,whenCreated,operatingSystem,operatingSystemVersion,operatingSystemServicePack,description";

            //DirectoryEntry dominio = new DirectoryEntry("LDAP://intra/OU=Servidores,dc=intra,dc=fazenda,dc=sp,DC=gov,dc=br", usr, pwd);
            DirectoryEntry dominio;
            DirectorySearcher buscador;
            SearchResultCollection colecao_resultados;
            ImportacaoEntity resultado = new ImportacaoEntity(); 

            foreach (string path in pathsLdap)
            {
                try
                {
                    dominio = new DirectoryEntry(path, usr, pwd);

                    buscador = new DirectorySearcher(dominio);
                    buscador.PageSize = 1001;
                    buscador.SearchScope = SearchScope.Subtree;
                    buscador.Filter = "(objectClass=computer)";
                    colecao_resultados = buscador.FindAll();                    

                    foreach (SearchResult ResEnt1 in colecao_resultados)
                    {
                        ADEntity ad = new ADEntity();

                        foreach (string propKey in ResEnt1.Properties.PropertyNames)
                        {
                            ResultPropertyValueCollection valcol = ResEnt1.Properties[propKey];

                            foreach (string li in l.Split(','))
                            {
                                if (propKey.ToString() == li.ToLower())
                                {
                                    foreach (object prop in valcol)
                                    {
                                        if (propKey.ToString().Equals("sAMAccountName", StringComparison.CurrentCultureIgnoreCase))
                                            ad.sAMAccountName = prop.ToString();

                                        if (propKey.ToString().Equals("whenCreated", StringComparison.CurrentCultureIgnoreCase))
                                            ad.whenCreated = prop.ToString();

                                        if (propKey.ToString().Equals("operatingSystem", StringComparison.CurrentCultureIgnoreCase))
                                            ad.operatingSystem = prop.ToString();

                                        if (propKey.ToString().Equals("operatingSystemVersion", StringComparison.CurrentCultureIgnoreCase))
                                            ad.operatingSystemVersion = prop.ToString();

                                        if (propKey.ToString().Equals("operatingSystemServicePack", StringComparison.CurrentCultureIgnoreCase))
                                            ad.operatingSystemServicePack = prop.ToString();

                                        if (propKey.ToString().Equals("description", StringComparison.CurrentCultureIgnoreCase))
                                            ad.description = prop.ToString();
                                    }
                                }
                            }
                        }

                        resultado.AD.Add(ad);
                    }
                }
                catch (Exception)
                {
                    // throw new Exception(ex.Message);
                    // NOP
                }
            }

            return resultado;
        }

        // Workstations:
        // OU=Recursos, CN=Computers, -SRV_

        // Servidores: OU=Servidores, CN=Computers, +SRV_

        private List<string> DominiosAD()
        {
            List<string> paths = new List<string>();
            paths.Add("LDAP://intra/dc=intra,dc=fazenda,dc=sp,DC=gov,dc=br");
            paths.Add("LDAP://intra/OU=Servidores,dc=intra,dc=fazenda,dc=sp,DC=gov,dc=br");
            paths.Add("LDAP://intra/CN=Computers,DC=intra,DC=fazenda,DC=sp,DC=gov,DC=br");
            paths.Add("LDAP://intra/OU=Domain Controllers,DC=intra,DC=fazenda,DC=sp,DC=gov,DC=br");
            paths.Add("LDAP://intra/OU=Metaframe40,DC=intra,DC=fazenda,DC=sp,DC=gov,DC=br");

            paths.Add("LDAP://inter/CN=Computers,DC=inter,DC=fazenda,DC=sp,DC=gov,DC=br");
            paths.Add("LDAP://inter/OU=Domain Controllers,DC=inter,DC=fazenda,DC=sp,DC=gov,DC=br");
            paths.Add("LDAP://inter/OU=Servidores,DC=inter,DC=fazenda,DC=sp,DC=gov,DC=br");

            paths.Add("LDAP://homsfz/CN=Computers,DC=homsfz,DC=fazenda,DC=local");
            paths.Add("LDAP://homsfz/OU=Domain Controllers,DC=homsfz,DC=fazenda,DC=local");
            paths.Add("LDAP://homsfz/OU=DTI,DC=homsfz,DC=fazenda,DC=local");
            paths.Add("LDAP://homsfz/OU=PKI,DC=homsfz,DC=fazenda,DC=local");
            paths.Add("LDAP://homsfz/OU=Servidores,DC=homsfz,DC=fazenda,DC=local");

            return paths;
        }

        public ImportacaoEntity consultarAD(string usr, string pwd)
        {
            var paths = DominiosAD();

            return consultarAD(usr, pwd, paths);
        }
    }
}
