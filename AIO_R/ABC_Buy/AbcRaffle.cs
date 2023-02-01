using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;
using static AIO_R.NetInterface;
using static System.Net.Mime.MediaTypeNames;

namespace AIO_R.ABC_Buy
{
    internal class AbcRaffle : RaffleProduct
    {
        readonly string secret = "secretkey";
        string deviceNumber { get; set; }
        public AbcRaffle(BotTask botTasks) : base(botTasks)
        {
        }
        public override void Initial()
        {
            if (checkRaffle)
            {
                CheckRaffle();
            }
            else { }
          // GlobalInfo.changedListTask.Enqueue(botTasks);//result need to write
        }
        private void GetSpecificId(string info)
        {

        }
        public override void Login()
        {
            deviceNumber = MD5(Guid.NewGuid().ToString()).ToLower();
            GetSpecificId("\"device_number\":\"" + deviceNumber + "\"");
        }
        public override void BuyProduct()
        {
            throw new NotImplementedException();
        }

        public override void EnterRaffle()
        {
            throw new NotImplementedException();
        }

        public string MD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        public override void CheckRaffle()
        {
            throw new NotImplementedException();
        }

        public override void RaffleProduct()
        {
            throw new NotImplementedException();
        }
    }
}
