using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AIO_R
{
    internal class GlobalInfo
    {
        public static List<string> proxyList= new List<string>();

        public static int taskNumber;

        public static List<BotTask> listTask = new List<BotTask>();
        public void UpdateProxy(Object obj)
        {
            BotProxy? addProxy = obj as BotProxy;
            if(addProxy!=null) proxyList = addProxy.proxy;
        }
    }
}
