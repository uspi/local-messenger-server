using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCmd
{
    /// <summary>
    ///there are two entities in the application responsible 
    ///for grouping messages. Dialog and Channel. 
    ///What are the differences. If "Messenger User" Delete Channel —
    ///from DB this channelId to be deleted. 
    ///If "Messenger User" delete Dialog — his Member or Owner id 
    ///to be deleted from this dialog in DB.
    ///but the dialogue will remain.
    ///
    ///these two abstractions(dialog and channel) differ only in this. 
    ///therefore, 
    ///in the database and here, one entity is responsible for this. 
    ///And the difference between them is made by a flag that 
    ///is checked by the method ItChannel(int idChannel)
    /// </summary>

    interface IDBConnect
    {
        //table users
        void CreatetUser(int idGenerated, string login, string password, string name);
        void DeleteUser(int idUser);      
        int GetUserId(string login, string password);
        string GetUserName(int idUser);
        string GetUserPassword(int idUser);
        string GetUserLogin(int idUser);
        int GetLastUserId();

        //table channels
        void CreateChannel(int idGenerated, int idOwner, bool itChannel);
        void DeleteChannel(int idChannel);
        void AddChannelMember(int idChannel, int idMember);
        void DeleteChannelMember(int idChannel, int idMember);
        int GetLastChannelId();
        List<int> GetChannelMembers(int idChannel);
        List<int> GetUserChannels(int idUser);

        //co.channels — dialogs
        bool ItChannel(int idChannel);

        //connection functions
        void OpenConnection(string connectionString);
        void CloseConnection();
    }
}
