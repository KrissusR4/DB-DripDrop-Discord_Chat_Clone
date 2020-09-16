using Cassandra;
using DripDropApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DripDropApi
{
    public class CassandraDataProvider
    {

        #region User

        public static List<User> GetUsers()
        {
            ISession session = SessionManager.GetSession();
            List<User> users = new List<User>();

            if (session == null)
                return null;

            var usersData = session.Execute("select * from User;");

            foreach (var userData in usersData)
            {
                User user = new User();
                user.userUID = userData["useruid"] != null ? userData["useruid"].ToString() : string.Empty;
                user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
                user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
                user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
                if (userData["serveruidslist"] != null)
                    foreach (var item in (String[])userData["serveruidslist"])
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.serverUIDsList.Add(string.Empty);
                if (userData["frienduidslist"] != null)
                    foreach (var item in (System.String[])userData["frienduidslist"])
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.friendUIDsList.Add(string.Empty);
                users.Add(user);
            }

            //session.Cluster.Shutdown();

            return users;
        }
        public static List<User> GetUsersByName(string name)
        {
            ISession session = SessionManager.GetSession();
            List<User> users = new List<User>();
            if (session == null)
                return null;

            RowSet usersData = session.Execute("select * from User where username='" + name + "' allow filtering;");

            if (usersData != null) {

                foreach (Row userData in usersData)
                {
                    User user = new User();
                    user.userUID = userData["useruid"] != null ? userData["useruid"].ToString() : string.Empty;
                    user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
                    user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
                    user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
                    if ((System.String[])userData["serveruidslist"] != null)
                        foreach (var item in (System.String[])userData["serveruidslist"])
                        {
                            user.serverUIDsList.Add(item.ToString());
                        }
                    if ((System.String[])userData["frienduidslist"] != null)
                        foreach (var item in (System.String[])userData["frienduidslist"])
                        {
                            user.serverUIDsList.Add(item.ToString());
                        }
                    users.Add(user);
                }
            }

            return users;
    }
        public static string GetUserUIDByUsername(string username)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row userData = session.Execute("select * from User where username='" + username + "' allow filtering;").FirstOrDefault();

            UID = userData["useruid"] != null ? userData["useruid"].ToString() : string.Empty;

            //session.Cluster.Shutdown();

            return UID;
        }

        public static User GetUserByID(string userUID)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return null;

            Row userData = session.Execute("select * from User where userUID=" + new Guid(userUID) + ";").FirstOrDefault();

            if (userData != null)
            {
                user.userUID = userData["useruid"] != null ? userData["useruid"].ToString() : string.Empty;
                user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
                user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
                user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
                if ((System.String[])userData["serveruidslist"] != null)
                    foreach (var item in (System.String[])userData["serveruidslist"])
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                if ((System.String[])userData["frienduidslist"] != null)
                    foreach (var item in (System.String[])userData["frienduidslist"])
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
            }

            //session.Cluster.Shutdown();

            return user;
        }

        public static User GetUserByUsernameAndPassword(string username, string password)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return null;

            Row userData = session.Execute("select * from User where username='" + username + "' and password='" + password + "' allow filtering").FirstOrDefault();

            if (userData != null)
            {
                user.userUID = userData["useruid"] != null ? userData["useruid"].ToString() : string.Empty;
                user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
                user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
                user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
                if ((System.String[])userData["serveruidslist"] != null)
                    foreach (var item in (System.String[])userData["serveruidslist"])
                    {
                       user.serverUIDsList.Add(item.ToString());
                    }
                if ((System.String[])userData["frienduidslist"] != null)
                    foreach (var item in (System.String[])userData["frienduidslist"])
                    {
                      user.friendUIDsList.Add(item.ToString());
                    }
               
            }

            //session.Cluster.Shutdown();

            return user;
        }

        public static void AddUser(string username, string nickname, string password)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet userData = session.Execute("insert into User (userUID, username, nickname, password, serverUIDsList, friendUIDsList) values (uuid(), '" + username + "', '" + nickname + "', '" + password + "', [], []);");

            //session.Cluster.Shutdown();
        }

        public static void AddUserServerUID(string userUID, string serverUID)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return;

            RowSet userData = session.Execute("update User set serverUIDsList = serverUIDsList + ['" + new Guid(serverUID) + "'] where userUID=" + new Guid(userUID) + ";");

            //session.Cluster.Shutdown();
        }

        public static void AddUserFriendUID(string userUID, string friendUID)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return;

            RowSet userData = session.Execute("update User set friendUIDsList = friendUIDsList + ['" + new Guid(friendUID).ToString() + "'] where userUID=" + new Guid(userUID) + ";");

            //session.Cluster.Shutdown();
        }

        public static void DeleteUserByUsername(string username)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet userData = session.Execute("delete from User where username='" + username + "';");

            //session.Cluster.Shutdown();
        }

        public static void DeleteUserByUID(string userUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet userData = session.Execute("delete from User where userUID=" + new Guid(userUID) + "'");

            //session.Cluster.Shutdown();
        }

        #endregion

        #region Server

        public static List<Server> GetServers()
        {
            ISession session = SessionManager.GetSession();
            List<Server> servers = new List<Server>();

            if (session == null)
                return null;

            var serversData = session.Execute("select * from Server;");

            foreach (var serverData in serversData)
            {
                Server server = new Server();
                server.serverUID = serverData["serveruid"] != null ? serverData["serveruid"].ToString() : string.Empty;
                server.name = serverData["name"] != null ? serverData["name"].ToString() : string.Empty;
                server.password = serverData["password"] != null ? serverData["password"].ToString() : string.Empty;
                if (serverData["useruidslist"] != null)
                    foreach (var item in (System.String[])serverData["useruidslist"])
                    {
                        server.userUIDsList.Add(item.ToString());
                    }
                else
                    server.userUIDsList.Add(string.Empty);
                if (serverData["adminuidslist"] != null)
                    foreach (var item in (System.String[])serverData["adminuidslist"])
                    {
                        server.adminUIDsList.Add(item.ToString());
                    }
                else
                    server.adminUIDsList.Add(string.Empty);
                if (serverData["chatuidslist"] != null)
                    foreach (var item in (System.String[])serverData["chatuidslist"])
                    {
                        server.chatUIDsList.Add(item.ToString());
                    }
                else
                    server.chatUIDsList.Add(string.Empty);
                server.privateS = serverData["privates"] != null ? (Boolean)serverData["privates"] : false;
                server.creationTime = serverData["creationtime"] != null ? (DateTime)serverData["creationtime"] : DateTime.Now;
                servers.Add(server);
            }

            //session.Cluster.Shutdown();

            return servers;
        }

        public static string GetServerUIDByName(string name)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row serverData = session.Execute("select * from Server where name ='" + name + "' allow filtering;").FirstOrDefault();

            UID = serverData["serveruid"] != null ? serverData["serveruid"].ToString() : string.Empty;

            //session.Cluster.Shutdown();

            return UID;
        }

        public static Server GetServerByID(string serverUID)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return null;

            Row serverData = session.Execute("select * from Server where serverUID=" + new Guid(serverUID) + " allow filtering;").FirstOrDefault();

            if (serverData != null)
            {
                server.serverUID = serverData["serveruid"] != null ? serverData["serveruid"].ToString() : string.Empty;
                server.name = serverData["name"] != null ? serverData["name"].ToString() : string.Empty;
                server.password = serverData["password"] != null ? serverData["password"].ToString() : string.Empty;
                if (serverData["useruidslist"] != null)
                    foreach (var item in (System.String[])serverData["useruidslist"])
                    {
                        server.userUIDsList.Add(item.ToString());
                    }
                else
                    server.userUIDsList.Add(string.Empty);
                if (serverData["adminuidslist"] != null)
                    foreach (var item in (System.String[])serverData["adminuidslist"])
                    {
                        server.adminUIDsList.Add(item.ToString());
                    }
                else
                    server.adminUIDsList.Add(string.Empty);
                if (serverData["chatuidslist"] != null)
                    foreach (var item in (System.String[])serverData["chatuidslist"])
                    {
                        server.chatUIDsList.Add(item.ToString());
                    }
                else
                    server.chatUIDsList.Add(string.Empty);
                server.privateS = serverData["privates"] != null ? (Boolean)serverData["privates"] : false;
                //server.creationTime = serverData["creationtime"] != null ? serverData["creationtime"] : DateTime.Now;
            }

            //session.Cluster.Shutdown();

            return server;
        }

        public static void AddServer(string name, string password, bool privateS)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return ;

            RowSet serverData = session.Execute("insert into Server (serverUID, name, password, userUIDsList, adminUIDsList, privateS, creationTime) values (uuid(), '" + name + "', '" + password + "', [], [], " + privateS + ", now());");

            //session.Cluster.Shutdown();
        }

        public static void AddServerUserUID(string serverUID, string userUID)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return;

            session.Execute("update Server set userUIDsList = userUIDsList + ['" + new Guid(userUID) + "'] where serverUID=" + new Guid(serverUID) + ";");

            //session.Cluster.Shutdown();
        }

        public static void AddServerAdminUID(string serverUID, string adminUID)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return;

            RowSet serverData = session.Execute("update Server set adminUIDsList = adminUIDsList + ['" + new Guid(adminUID).ToString() + "'] where serverUID=" + new Guid(serverUID) + ";");

            //session.Cluster.Shutdown();
        }

        public static void AddServerChatUID(string serverUID, string chatUID)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return;

            RowSet serverData = session.Execute("update Server set chatUIDsList = chatUIDsList + ['" + new Guid(chatUID).ToString() + "'] where serverUID =" + new Guid(serverUID) + ";");

            //session.Cluster.Shutdown();
        }

        public static void AddServerPrivate(string serverUID, bool privateS)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return;

            RowSet serverData = session.Execute("update Server set privateS='" + privateS + "' where serverUID=" + new Guid(serverUID) + ";");

            //session.Cluster.Shutdown();
        }

        public static void DeleteServerByName(string name)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet serverData = session.Execute("delete from Server where name='" + name + "';");

            //session.Cluster.Shutdown();
        }

        public static void DeleteServerByUID(string serverUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet serverData = session.Execute("delete from Server where serverUID=" + new Guid(serverUID) + ";");

            //session.Cluster.Shutdown();
        }

        #endregion

        #region Chat

        public static List<Chat> GetChats()
        {
            ISession session = SessionManager.GetSession();
            List<Chat> chats = new List<Chat>();

            if (session == null)
                return null;

            var chatsData = session.Execute("select * from Chat;");

            foreach (var chatData in chatsData)
            {
                Chat chat = new Chat();
                chat.chatUID = chatData["chatuid"] != null ? chatData["chatuid"].ToString() : string.Empty;
                chat.name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                if (chatData["useruidslist"] != null)
                    foreach (var item in (System.String[])chatData["useruidslist"])
                    {
                        chat.userUIDsList.Add(item.ToString());
                    }
                else
                    chat.userUIDsList.Add(string.Empty);
               // chat.creationTime = chatData["creationtime"] != null ? (DateTime)chatData["creationtime"] : DateTime.Now;
                chat.serverUID = chatData["serveruid"] != null ? chatData["serveruid"].ToString() : string.Empty;
                chats.Add(chat);
            }

            //session.Cluster.Shutdown();

            return chats;
        }

        public static List<Chat> GetChatsByServerUID(string serverUID)
        {
            ISession session = SessionManager.GetSession();
            List<Chat> chats = new List<Chat>();

            if (session == null)
                return null;

            var chatsData = session.Execute("select * from Chat where serverUID=" + new Guid(serverUID) + " allow filtering;");

            foreach (var chatData in chatsData)
            {
                Chat chat = new Chat();
                chat.chatUID = chatData["chatuid"] != null ? chatData["chatuid"].ToString() : string.Empty;
                chat.name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                if (chatData["useruidslist"] != null)
                    foreach (var item in (System.String[])chatData["useruidslist"])
                    {
                        chat.userUIDsList.Add(item.ToString());
                    }
                else
                    chat.userUIDsList.Add(string.Empty);
                chat.creationTime = chatData["creationtime"] != null ? chatData["creationtime"].ToString() : null;
                chat.serverUID = chatData["serveruid"] != null ? chatData["serveruid"].ToString() : string.Empty;
                chats.Add(chat);
            }

            //session.Cluster.Shutdown();

            return chats;
        }

        public static string GetChatUIDByName(string name)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row chatData = session.Execute("select * from Chat where name='" + name + "' allow filtering;").FirstOrDefault();

            UID = chatData["chatuid"] != null ? chatData["chatuid"].ToString() : string.Empty;

            //session.Cluster.Shutdown();

            return UID;
        }

        public static Chat GetChatByID(string chatUID)
        {
            ISession session = SessionManager.GetSession();
            Chat chat = new Chat();

            if (session == null)
                return null;

            Row chatData = session.Execute("select * from Chat where chatUID=" + new Guid(chatUID) + " allow filtering;").FirstOrDefault();

            if (chatData != null)
            {
                chat.chatUID = chatData["chatuid"] != null ? chatData["chatuid"].ToString() : string.Empty;
                chat.name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                if (chatData["useruidslist"] != null)
                    foreach (var item in (System.String[])chatData["useruidslist"])
                    {
                        chat.userUIDsList.Add(item.ToString());
                    }
                else
                    chat.userUIDsList.Add(string.Empty);
                chat.creationTime = chatData["creationtime"] != null ? chatData["creationtime"].ToString() : null;
                chat.serverUID = chatData["serveruid"] != null ? chatData["serveruid"].ToString() : string.Empty;
            }

            //session.Cluster.Shutdown();

            return chat;
        }

        public static void AddChat(string name, string serverUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("insert into Chat (chatUID, name, userUIDsList, creationTime, serverUID) values (uuid(), '" + name + "', [], dateof(now()), " + new Guid(serverUID) + ");");

            //session.Cluster.Shutdown();
        }

        public static void AddChatUserUID(string chatUID, string userUID)
        {
            ISession session = SessionManager.GetSession();
            Chat chat = new Chat();

            if (session == null)
                return;

            RowSet chatData = session.Execute("update Chat set userUIDsList = userUIDsList + ['" + userUID + "'] where chatUID=" + new Guid(chatUID) + " ;");

            //session.Cluster.Shutdown();
        }

        public static void DeleteChatByName(string name)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("delete from Chat where name='" + name + "';");

            //session.Cluster.Shutdown();
        }

        public static void DeleteChatByUID(string chatUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("delete from Chat where chatUID=" + new Guid(chatUID) + " ;");

            //session.Cluster.Shutdown();
        }

        #endregion

        #region Message

        public static List<Message> GetMessagesByDateTime(string dateTime, string chatUID)
        {
            ISession session = SessionManager.GetSession();
            List<Message> messages = new List<Message>();

            if (session == null)
                return null;

            var messagesData = session.Execute("select * from Message where timesent <= '" + dateTime + "' and chatuid = '" + new Guid(chatUID) + "' limit 40 allow filtering;");

            foreach (var messageData in messagesData)
            {
                Message message = new Message();
                message.messageUID = messageData["messageuid"] != null ? messageData["messageuid"].ToString() : string.Empty;
                message.fromWho = messageData["fromwho"] != null ? messageData["fromwho"].ToString() : string.Empty;
                message.timeSent = messageData["timesent"] != null ? messageData["timesent"].ToString() :null;
                //message.timeRead = messageData["timeread"] != null ? (DateTime)messageData["timeread"] : DateTime.Now;
                message.text = messageData["text"] != null ? messageData["text"].ToString() : string.Empty;
                message.picture = messageData["picture"] != null ? messageData["picture"].ToString() : string.Empty;
                message.chatUID = messageData["chatuid"] != null ? messageData["chatuid"].ToString() : string.Empty;
                messages.Add(message);
            }

            //session.Cluster.Shutdown();

            return messages;
        }

        public static List<Message> GetMessages()
        {
            ISession session = SessionManager.GetSession();
            List<Message> messages = new List<Message>();

            if (session == null)
                return null;

            var messagesData = session.Execute("select * from Message;");

            foreach (var messageData in messagesData)
            {
                Message message = new Message();
                message.messageUID = messageData["messageuid"] != null ? messageData["messageuid"].ToString() : string.Empty;
                message.fromWho = messageData["fromwho"] != null ? messageData["fromwho"].ToString() : string.Empty;
                message.timeSent = messageData["timesent"] != null ? messageData["timesent"].ToString() : null;
                //message.timeRead = messageData["timeread"] != null ? (DateTime)messageData["timeread"] : DateTime.Now;
                message.text = messageData["text"] != null ? messageData["text"].ToString() : string.Empty;
                message.picture = messageData["picture"] != null ? messageData["picture"].ToString() : string.Empty;
                message.chatUID = messageData["chatuid"] != null ? messageData["chatuid"].ToString() : string.Empty;
                messages.Add(message);
            }

            //session.Cluster.Shutdown();

            return messages;
        }

        public static List<Message> GetMessagesByChatUID(string chatUID)
        {
            ISession session = SessionManager.GetSession();
            List<Message> messages = new List<Message>();

            if (session == null)
                return null;

            var messagesData = session.Execute("select * from Message where chatUID=" + new Guid(chatUID) + " allow filtering;");

            foreach (var messageData in messagesData)
            {
                Message message = new Message();
                message.messageUID = messageData["messageuid"] != null ? messageData["messageuid"].ToString() : string.Empty;
                message.fromWho = messageData["fromwho"] != null ? messageData["fromwho"].ToString() : string.Empty;
                message.timeSent = messageData["timesent"] != null ? messageData["timesent"].ToString() : null;
                //message.timeRead = messageData["timeread"] != null ? (DateTime)messageData["timeread"] : DateTime.Now;
                message.text = messageData["text"] != null ? messageData["text"].ToString() : string.Empty;
                message.picture = messageData["picture"] != null ? messageData["picture"].ToString() : string.Empty;
                message.chatUID = messageData["chatuid"] != null ? messageData["chatuid"].ToString() : string.Empty;
                messages.Add(message);
            }

            //session.Cluster.Shutdown();

            return messages;
        }

        public static string GetMessageUIDByName(string fromWho, string chatUID)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row messagesData = session.Execute("select * from Message where fromWho='" + fromWho + "' and chatUID=" + new Guid(chatUID) + " allow filtering;").FirstOrDefault();

            UID = messagesData["messageuid"] != null ? messagesData["messageuid"].ToString() : string.Empty;

            //session.Cluster.Shutdown();

            return UID;
        }

        public static Message GetMessageByID(string messageUID)
        {
            ISession session = SessionManager.GetSession();
            Message message = new Message();

            if (session == null)
                return null;

            Row messageData = session.Execute("select * from Message where messageUID=" + new Guid(messageUID) + " allow filtering;").FirstOrDefault();

            if (messageData != null)
            {
                message.messageUID = messageData["messageuid"] != null ? messageData["messageuid"].ToString() : string.Empty;
                message.fromWho = messageData["fromwho"] != null ? messageData["fromwho"].ToString() : string.Empty;
                message.timeSent = messageData["timesent"] != null ? messageData["timesent"].ToString() : null;
                //message.timeRead = messageData["timeread"] != null ? (DateTime)messageData["timeread"] : DateTime.Now;
                message.text = messageData["text"] != null ? messageData["text"].ToString() : string.Empty;
                message.picture = messageData["picture"] != null ? messageData["picture"].ToString() : string.Empty;
                message.chatUID = messageData["chatuid"] != null ? messageData["chatuid"].ToString() : string.Empty;
            }

            //session.Cluster.Shutdown();

            return message;
        }

        public static void AddMessage(string fromWho, string text, string picture, string chatUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("insert into Message (messageUID, fromWho, timeSent, text, picture, chatUID) values (uuid(), '" + fromWho + "', dateof(now()), '" + text + "', '" + picture + "', '" + new Guid(chatUID).ToString() + "');");

            //session.Cluster.Shutdown();
        }

        public static void DeleteMessageByUID(string messageUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("delete from Message where messageUID=" + new Guid(messageUID) + " ;");

            //session.Cluster.Shutdown();
        }

        #endregion
    }
}