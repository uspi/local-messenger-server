using System;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;
using ServerCmd.NetworkItems;

namespace ServerCmd.NetworkItems
{
    //даем рабочим станки
    class Connection : ICommunication
    {
        //сокет обмена
        Socket s;

        public Socket Handler { get => s; private set => s = value; }

        //передача от публичного сокета информации к внутреннему
        public Connection(Socket inputSocket)
        {
            s = inputSocket.Accept();
        }  

        //отправка сообщения
        //Преобразует сообщение в байты и посылает в сокет обмена
        public void Send(string reply)
        {
            byte[] msg = Encoding.UTF8.GetBytes(reply);
            s.Send(msg);
        }

        //получение сообщения
        public string Receive()
        {
            return ByteReceive(1024, 0);
        }
        
        //Создает пустой байтовый массив заданного размера quantityByte
        //и принимает в него сообщение от сокета обмена
        private string ByteReceive(int quantityByte, int indexFirstByte)
        {
            byte[] bytes = new byte[quantityByte];
            int bytesRec = s.Receive(bytes);
            return Encoding.UTF8.GetString(bytes, indexFirstByte, bytesRec);
        }

        //закрывает сокет обмена
        public void Close()
        {
            s.Shutdown(SocketShutdown.Both);
            s.Close();
        } 
    }
}
