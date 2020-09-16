using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DripDropApi.Models
{
    public class User
    {
        public string userUID { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public List<string> serverUIDsList { get; set; }
        public List<string> friendUIDsList { get; set; }
        public User()
        {
            serverUIDsList = new List<string>();
            friendUIDsList = new List<string>();
        }
    }
}