using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerCmd.NetworkItems;

namespace ServerCmd
{
    //содержит списки подключений, юзеров, подключенных баз
    //содержит цикл сервера для обхода и обслуживания всех клиентов
    class Server
    {
        static List<User> users;

        //подключенные соединениея
        static List<Connection> activeConnections;

        //авторизированные пользователи
        static List<User> activeUsers;

        #region Если отправка сообщений синхронна
        //стек входящих сообщений от пользователей
        List<MessengerItems.Message> inputMess;

        //стек исходящих сообщений для пользователей
        List<MessengerItems.Message> outputMess;
        #endregion

        //создает экземпляр, не создает подключение из конструктора, 
        //смотри метод OpenConnection
        static DBConnection db = new DBConnection();

        static void Main()
        {
            //установка окна
            Console.SetWindowSize(90, 30);
            Console.ForegroundColor = ConsoleColor.Red;

            db.OpenConnection();
            Console.WriteLine(db.GetLastUserId() + " зарегистрированных пользователей");

            //Создать канал
            int lastChannelId = db.GetLastChannelId();
            db.CreateChannel(2, 2, true);

            
            Console.WriteLine(db.GetLastChannelId() + "зарегистрированных канала");
            
            ///ПРОВЕРКА ОТДЕЛЬНЫХ ФУНКЦИЙ, РАЗДЕЛЕНЫ ПУСТОЙ ЛИНИЕЙ
            //Узнать id по login и password
            //int userId = db.GetUserId(db.GetUserLogin(1), db.GetUserPassword(1));
            //Console.WriteLine(userId);

            //Логика как узнать имя пользователя по id
            //string userName = db.GetUserName(1);
            //Console.WriteLine(userName);

            //Логика добавление пользователя
            //int lastDBId = db.GetLastUserId();
            //db.CreatetUser(++lastDBId, "vasya23@gmail.com", "0000", "Васька");

            //Логика удаления пользователя по id
            //db.DeleteUser(1);

            Console.WriteLine(db.GetLastUserId() + " зарегистрированных пользователей");


            db.CloseConnection();//test
            Console.Read();

            //создание сети для подключения
            Lobby lobby = new Lobby(10);


            //цикл сервера


            //Подключение нового пользователя

            lobby.AcceptEveryone(ref activeConnections);

            //получение логина пароля endpoint
            User user = Auth(activeConnections[0]);  
           

            //проверка юзера в базе данных
            db.OpenConnection();
            user.id = db.GetUserId(user.login, user.password);
            if (user.id != -1)
            {
                //загрузка и отправка смс, диалогов из БД

                //отправка уведомления об успешной авторизации
                activeConnections[0].Send("confirmedUser");

                //добавление пользователя в список активных юзеров
                AddActiveUser(user);
            }
            else
            {
                //если пользователь не найден в базе
                activeConnections[0].Send("notConfirmed");
            }
            db.CloseConnection();


            Console.Read();
            //создание сети, два сокета(отправка, посылка)
            //получение логинов и паролей
            //подключение к базе и проверка логинов и паролей
            //подключение к базе и скачивание данных пользователя
            //ответ клиенту что он может войти
            //отправка данных пользователя(channels, contacts, messages)


            //Работа с подключенными 

            //проверка активности сеанса
            //ожидание отправки сообщения
            //сохранение сообщений в таблицу с пометкой "отправить"
            //проверка наличия актуального списка каналов
            //отправка списка каналов
            //проверка наличия актуального списка контактов
            //отправка новых контактов
            //проверка наличия актуального списка сообщений
            //отправка целевых сообщений


            //Завершение работы с пользователем
            //если сеанс при проверке стал неактивен

            //закрытие сети, два сокета(отправка, посылка)
            //удаление из списка юзеров(на сервере)
            //удаление входящих и выходящих сообщений этого юзера(на сервере)
        }
        static User Auth(Connection notAuthConn)
        {
            //получение логина и отправка уведомления о получении
            string login = notAuthConn.Receive();
            notAuthConn.Send("receivedLogin");

            //получение пароля и отправка уведомления о получении
            string password = notAuthConn.Receive();
            notAuthConn.Send("receivedPassword");

            return new User((IPEndPoint)notAuthConn.Handler.RemoteEndPoint, 
                login, password);                  
        }
        static void AddActiveUser(User user)
        {
            if (activeUsers == null)
            {
                activeUsers = new List<User>() { user };
            }
            else { activeUsers.Add(user); }
        }
    }
}
