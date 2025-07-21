using System;
using System.Security.Cryptography.X509Certificates;

namespace SisCOI_crt.Utils
{
    public class StoreUtil
    {
        public static string StoreLocationToString(StoreLocation sl)
        {
            switch (sl)
            {
                case StoreLocation.CurrentUser:
                    return "CurrentUser";
                case StoreLocation.LocalMachine:
                    return "LocalMachine";
                default:
                    return string.Empty;
            }
        }

        public static string StoreNameToString(StoreName sn)
        {
            switch (sn)
            {
                case StoreName.AddressBook:
                    return "AddressBook";
                case StoreName.AuthRoot:
                    return "AuthRoot";
                case StoreName.CertificateAuthority:
                    return "CA";
                //case StoreName.ClientAuthIssuer: //Ainda não existe
                //    return "ClientAuthIssuer";
                case StoreName.Disallowed:
                    return "Disallowed";
                case StoreName.My:
                    return "My";
                case StoreName.Root:
                    return "Root";
                case StoreName.TrustedPeople:
                    return "TrustedPeople";
                case StoreName.TrustedPublisher:
                    return "TrustedPublisher";
                default:
                    return string.Empty;
            }
        }

        public static StoreLocation StringToStoreLocation(string storeLocation)
        {
            switch (storeLocation)
            {
                case "CurrentUser":
                    return (StoreLocation.CurrentUser);
                case "LocalMachine":
                    return (StoreLocation.LocalMachine);
                default:
                    throw new Exception(string.Format("storeLocation '{0}' is not valid.", storeLocation));
            }
        }

        public static StoreName StringToStoreName(string storeName)
        {
            switch (storeName)
            {
                case "AddressBook":
                    return (StoreName.AddressBook);
                case "AuthRoot":
                    return (StoreName.AuthRoot);
                case "CA":
                    return (StoreName.CertificateAuthority);
                case "Disallowed":
                    return (StoreName.Disallowed);
                case "My":
                    return (StoreName.My);
                case "Root":
                    return (StoreName.Root);
                case "TrustedPeople":
                    return (StoreName.TrustedPeople);
                case "TrustedPublisher":
                    return (StoreName.TrustedPublisher);
                default:
                    throw new Exception(string.Format("storeName '{0}' is not valid.", storeName));
            }
        }
    }
}
