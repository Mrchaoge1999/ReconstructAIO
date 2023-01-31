using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AIO_R
{
    internal interface NetInterface
    {
        string Get(WebRequestInfo webInfo);
        void RegexCookie(string cookieString, string host);
        string Post(WebRequestInfo webInfo);
        WebProxy Proxy(string proxy);
        public bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
        public CookieCollection siteCookie { get;set;}
        public string responseUrl { get; set; }
    }
}
