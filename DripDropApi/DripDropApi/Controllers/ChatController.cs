using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cassandra;
using DripDropApi.Models;

namespace DripDropApi.Controllers
{
    public class ChatController : ApiController
    {
        [HttpGet]
        ///[Route("GetUserID")]
        public IHttpActionResult Get(string id)
        {
            Chat chat = new Chat();
            chat = CassandraDataProvider.GetChatByID(id);
            return Ok(chat);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Chat chat)
        {
            CassandraDataProvider.AddChat(chat.name , chat.serverUID);
            string chatUID = CassandraDataProvider.GetChatUIDByName(chat.name);
            CassandraDataProvider.AddServerChatUID(chat.serverUID, chatUID);
            Chat c = CassandraDataProvider.GetChatByID(chatUID);
            return Ok(c);
        }

        [HttpPut]
        [Route("PutServerId")]
        public IHttpActionResult PutFriend(string chatId, string chaterId)
        {
            CassandraDataProvider.AddChatUserUID(chatId, chaterId);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            CassandraDataProvider.DeleteChatByUID(id);
            return Ok();
        }
    }
}
