using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DripDropApi.Models
{
    public class Server
    {
        public string serverUID { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public List<string> userUIDsList { get; set; }
        public List<string> adminUIDsList { get; set; }
        public List<string> chatUIDsList { get; set; }
        public Boolean privateS { get; set; }
        public DateTime creationTime { get; set; }
        public Server()
        {
            userUIDsList = new List<string>();
            adminUIDsList = new List<string>();
            chatUIDsList = new List<string>();
        }
    }
}