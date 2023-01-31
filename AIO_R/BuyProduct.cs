using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AIO_R.NetInterface;

namespace AIO_R
{
    abstract class BuyProduct : PurchaseProduct
    {
        public BotTask botTasks { get; set; }
        public Request request { get; set; }
        public BuyProduct(BotTask botTasks)
        {
            this.botTasks = botTasks;
            request = new Request();
            if (botTasks.token.Equals(string.Empty) || botTasks.token.Contains("error") || botTasks.token.Contains("false")) return;
        }
        public abstract void Initial();
        public abstract void Login();
        public abstract void FindPage();
        public abstract void AddToCart();
        public abstract void SubmitShipping();
        public abstract void SubmitBilling();
        public abstract void Checkout();
        WebRequestInfo webRequestInfo { get; set; }
    }
}
