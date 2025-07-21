using System.Text;
using System.Xml;

namespace Br.Gov.Sp.Fazenda.SisCoi.Utils.Crypto
{
    public class CCrypt
    {
        public string Encode(string str)
        {
            const string key = "nBjetgeAsoaXdudjakTqmOnajYQTBAMq";
            int len = str.Length;
            StringBuilder crypt = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                crypt.Append( (char) ((uint)str[i] ^ (uint) key[ i % key.Length ]) );
            }

            return crypt.ToString();
        }

        public string XmlEncrypt(string str)
        {
            return XmlConvert.EncodeLocalName(Encode(str));
        }

        public string XmlDecrypt(string str)
        {
            return Encode(XmlConvert.DecodeName(str));
        }
    }
}
