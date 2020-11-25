using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCmd.NetworkItems
{
    interface IObserver
    {
        void Listen(int queueSize);
        void Accept();
    }
}
