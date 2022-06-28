using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegramBot.Models
{
    public class AnimeList
    {
        public List<AnimeList> anime { get; set; }

        public string title { get; set; }
        public List<Genre> genres { get; set; }
    }
    public class Genre
    {
        public string name { get; set; }
    }
}
