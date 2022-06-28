using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegramBot.Models
{
    public class Top
    {
        public List<anime> top { get; set; }
    }
    public class anime
    {
        public string title { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }
    }
}
