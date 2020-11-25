using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCmd
{
    public class DBConnection : IDBConnect
    {
        public SqlConnection connect = null;

        #region User

        public void CreatetUser(int idGenerated, string login,
            string password, string name)
        {
            string sql = string.Format("Insert Into Users" +
                   "(idUser, login, password, name)" +
                   " Values(@idUser, @login, @password, @name)");

            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@idUser", idGenerated);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", name);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int idUser)
        {
            string sql = string.Format("Delete from Users where " +
                "idUser=@idUser");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@idUser", idUser);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Пока невозможно удалить", ex);
                    throw error;
                }
            }
        }

        public int GetUserId(string login, string password)
        {
            string sql = string.Format("select idUser from Users " +
                "where login=@login and password=@password");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                object reply = cmd.ExecuteScalar();

                //if not found
                if (reply == null)
                {
                    return -1;
                }
                return (int)reply;
            }
        }

        public string GetUserLogin(int idUser)
        {
            string sql = string.Format("select login from Users " +
                "where idUser=@idUser");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.AddWithValue("@idUser", idUser);
                return (string)cmd.ExecuteScalar();
            }
        }

        public string GetUserPassword(int idUser)
        {
            string sql = string.Format("select password from Users " +
                "where idUser=@idUser");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.AddWithValue("@idUser", idUser);
                return (string)cmd.ExecuteScalar();
            }
        }

        public string GetUserName(int idUser)
        {
            string sql = string.Format("select name from Users " +
                "where idUser=@idUser");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.AddWithValue("@idUser", idUser);
                return (string)cmd.ExecuteScalar();
            }
        }

        public int GetLastUserId()
        {
            string sql = string.Format("SELECT COUNT(*) FROM Users");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        #endregion User

        #region Channel

        public void CreateChannel(int idGenerated, int idOwner, bool itChannel)
        {
            string sql = string.Format("Insert Into Channels" +
                   "(idChannel, idOwner, itChannel)" +
                   " Values(@idChannel, @idOwner, @itChannel)");

            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@idChannel", idGenerated);
                cmd.Parameters.AddWithValue("@idOwner", idOwner);
                cmd.Parameters.AddWithValue("@itChannel", itChannel);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteChannel(int idChannel)
        {
            throw new NotImplementedException();
        }

        public void AddChannelMember(int idChannel, int idMember)
        {
            throw new NotImplementedException();
        }

        public void DeleteChannelMember(int idChannel, int idMember)
        {
            throw new NotImplementedException();
        }

        public List<int> GetChannelMembers(int idChannel)
        {
            throw new NotImplementedException();
        }

        public List<int> GetUserChannels(int idUser)
        {
            throw new NotImplementedException();
        }

        public bool ItChannel(int idChannel)
        {
            throw new NotImplementedException();
        }

        public int GetLastChannelId()
        {
            string sql = string.Format("SELECT max(idChannel) FROM Channels");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                object reply = cmd.ExecuteScalar();
                if (reply == null)
                {
                    return -1;
                }
                return (int)reply;
            }
        }

        #endregion Channel

        #region Connection

        public void OpenConnection(string connectionString =
            @"Data Source= DESKTOP - 8R6N46E\SQLEXPRESS01; Initial Catalog = Messenger; 
            Integrated Security = True; Connect Timeout = 30; Encrypt=False;
            TrustServerCertificate=False;ApplicationIntent=ReadWrite;
            MultiSubnetFailover=False")
        {

            connect = new SqlConnection(@"Data Source=DESKTOP-8R6N46E\SQLEXPRESS01;Initial Catalog=Messenger;Integrated Security=True;Pooling=False");
            connect.Open();
        }

        public void CloseConnection()
        {
            connect.Close();
        }

        #endregion Connection
    }
}
