using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AIO_R
{
    class WebRequestInfo
    {
        public bool isUseCookie;

        public WebHeaderCollection webHeader;
        public string url;
        public string info;
        public string proxy;
        public string userAgent;
        public string contentType;
        public int timeout;
        public string host;
        public bool autoRedirect;
        public Encoding enCoding;
    }
}
