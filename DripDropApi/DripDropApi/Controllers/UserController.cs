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
    public class UserController : ApiController
    {
        [HttpGet]
        ///[Route("GetUserID")]
        public IHttpActionResult Get(string id)
        {
            User user = new User();
            user = CassandraDataProvider.GetUserByID(id);
            return Ok(user);
        }

        [Route("GetByLogin")]
        public IHttpActionResult GetByName(string name, string pass)
        {
            User user = new User();
            user = CassandraDataProvider.GetUserByUsernameAndPassword(name , pass);
            return Ok(user);
        }

        [Route("api/User/{username}/{password}")]
        public IHttpActionResult Get(string username, string password)
        {
            User user = new User();
            user = CassandraDataProvider.GetUserByUsernameAndPassword(username, password);
            return Ok(user);
        }
        [Route("SearchByName")]
        public IHttpActionResult GetNames(string name)
        {
            List<User> users = CassandraDataProvider.GetUsersByName(name);
            return Ok(users);
        }
        [Route("GetByNamenn")]
        public IHttpActionResult GetByNamenn(string namenn)
        {
            string uid;
            string trimmed = namenn.Substring(1, namenn.Length - 2);
            uid = CassandraDataProvider.GetUserUIDByUsername(namenn);
            return Ok(uid);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] User newUser)
        {
            CassandraDataProvider.AddUser(newUser.username, newUser.nickname, newUser.password);
            return Ok();
        }

        [HttpPut]
        [Route("PutFriend")]
        public IHttpActionResult PutFriend(string myId , string friendId)
        {
            CassandraDataProvider.AddUserFriendUID( myId , friendId);
            return Ok();
        }

        [HttpPut]
        [Route("PutServer")]
        public IHttpActionResult PutServer(string myId, string serverId)
        {
            if(serverId != "0")
            CassandraDataProvider.AddUserServerUID(myId, serverId);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            CassandraDataProvider.DeleteUserByUID(id);
            return Ok();
        }
    }
}
