using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDScreenCapture.Model
{
    [Serializable]
    public struct Token
    {
        [System.Xml.Serialization.XmlElement("access_token")]
        public string AccessToken;

        [System.Xml.Serialization.XmlElement("access_token_secret")]
        public string AccessTokenSecret;
}
}
