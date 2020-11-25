using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCmd.NetworkItems
{
    interface ICommunication
    {
        string Receive();
        void Send(string reply);
        void Close();
    }
}
