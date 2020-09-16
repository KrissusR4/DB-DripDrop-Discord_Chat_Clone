using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DripDropApi.Models
{
    public class Message
    {
        public string messageUID { get; set; }
        public string fromWho { get; set; }
        public string timeSent { get; set; }
        //public DateTime timeRead { get; set; }
        public string text { get; set; }
        public string picture { get; set; }
        public string chatUID { get; set; }
        public Message()
        {

        }
    }
}