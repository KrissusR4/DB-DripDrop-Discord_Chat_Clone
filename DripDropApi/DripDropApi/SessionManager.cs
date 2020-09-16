using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DripDropApi
{
    public class SessionManager
    {
        public static ISession session;

        public static ISession GetSession()
        {
            if (session == null)
            {
                Cluster cluster = Cluster.Builder().WithPort(9042).AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("dripdrop");
            }
            return session;
        }
    }
}