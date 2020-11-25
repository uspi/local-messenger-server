using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ServerCmd.MessengerItems;
using System.Threading.Tasks;

namespace ServerCmd
{
    class User
    {
        //поля для синхронизации с базой данных
        public int id;
        public string login;
        public string password;
        public string name;
        List<Message> messages;
        List<Message> channels;
        List<Contact> contacts;
        
        //текущий ip клиента
        public IPEndPoint ipEndPoint;

        public User(IPEndPoint _ipEndPoint, string _login, string _password)
        {
            login = _login;
            password = _password;
            ipEndPoint = _ipEndPoint;
        }
    }
}
