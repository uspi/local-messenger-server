using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using ServerCmd.NetworkItems;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCmd
{
    //открываем створы
    class Lobby
    {
        //сокет входящих подключений
        Socket sListener;

        //адресс сервера
        IPEndPoint host;
        public IPEndPoint HostAddress { get => host; private set => host = value; }

        //создание адреса сервера и открытие лобби(начинает слушать входящие подключения)
        public Lobby(int queueSize, string _hostAddress = "localhost", int _port = 11000)
        {
            IPAddress iPAddress;

            //если в _host записано localhost
            if (!IPAddress.TryParse(_hostAddress, out iPAddress))
            {
                //извлекает адресс в формате ipv6 
                host = new IPEndPoint(Dns.GetHostEntry(_hostAddress)
                    .AddressList[0], _port);
            }
            //если в _host записан ip
            else
            {
                host = new IPEndPoint(iPAddress, _port);
            }

            //слушает и сохраняет в стек до (значения"queueSize") одновременно
            Listen(queueSize);
        }

        public void AcceptEveryone(ref List<Connection> activeConnections)
        {
            if (activeConnections == null)
            {
                activeConnections = new List<Connection>() { new Connection(sListener) };
            }
            else
            {
                activeConnections.Add(new Connection(sListener));
            }       
        }

        //создание стека входящего сокета
        private void Listen(int queueSize)
        {
            sListener = new Socket(host.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            //associate sListener with a local hostAdress
            sListener.Bind(host);

            sListener.Listen(queueSize);
        }     
    }
}
