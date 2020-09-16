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
    public class ServerController : ApiController
    {
        [HttpGet]
        ///[Route("GetUserID")]
        public IHttpActionResult Get(string id)
        {
            Server server = new Server();
            server = CassandraDataProvider.GetServerByID(id);
            return Ok(server);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Server server)
        {
            CassandraDataProvider.AddServer(server.name, server.password, server.privateS);

            string sUid = CassandraDataProvider.GetServerUIDByName(server.name);
            CassandraDataProvider.AddServerAdminUID(sUid, server.serverUID);
            CassandraDataProvider.AddUserServerUID(server.serverUID, sUid);
            Server s = CassandraDataProvider.GetServerByID(sUid);
            return Ok(s);
        }

        [HttpPut]
        [Route("PutServerAdmin")]
        public IHttpActionResult PutAdmin(string serverId, string adminId)
        {
            CassandraDataProvider.AddServerAdminUID(serverId, adminId);
            return Ok();
        }

        [HttpPut]
        [Route("PutServerUser")]
        public IHttpActionResult PutUser(string serverId, string userId)
        {
            CassandraDataProvider.AddServerUserUID(serverId, userId);
            return Ok();
        }

        [HttpPut]
        [Route("PutServerPrivate")]
        public IHttpActionResult PutPrivate(string serverId, bool privatan)
        {
            CassandraDataProvider.AddServerPrivate(serverId, privatan);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            CassandraDataProvider.DeleteServerByUID(id);
            return Ok();
        }
    }
}
