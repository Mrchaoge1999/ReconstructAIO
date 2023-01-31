using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIO_R
{
    abstract class Profile
    {
        public delegate void NotifyEventHandler(object dataChange);
        public NotifyEventHandler notify;
    }
}
