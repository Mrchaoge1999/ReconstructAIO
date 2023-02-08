using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIO_R.ABC_Buy
{
    internal class AbcPurchase : BuyProduct
    {
        private  NetInterface Request { get; set; }
        public AbcPurchase(BotTask botTasks,NetInterface Request): base(botTasks)
        {
            this.request = request;
           // request.Post();
           //buy sequence
        }
        public override void Initial()
        {
            Console.WriteLine("test");
            Thread.Sleep(1000);
           // GlobalInfo.changedListTask.Enqueue(botTasks);//result need to write
        }
        public override void AddToCart()
        {
            throw new NotImplementedException();
        }

        public override void BuyProduct()
        {
            throw new NotImplementedException();
        }

        public override void Checkout()
        {
            throw new NotImplementedException();
        }

        public override void FindPage()
        {
            throw new NotImplementedException();
        }

        public override void Login()
        {
            throw new NotImplementedException();
        }

        public override void RaffleProduct()
        {
            throw new NotImplementedException();
        }

        public override void SubmitBilling()
        {
            throw new NotImplementedException();
        }

        public override void SubmitShipping()
        {
            throw new NotImplementedException();
        }
    }
}
