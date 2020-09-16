using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DripDropApi.Models
{
    public class Chat
    {
        public string chatUID { get; set; }
        public string name { get; set; }
        public List<string> userUIDsList { get; set; }
        public string creationTime { get; set; }
        public string serverUID { get; set; }
        public Chat()
        {
            userUIDsList = new List<string>();
        }
    }
}