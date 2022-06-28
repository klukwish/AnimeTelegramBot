using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegramBot.Clients;
using telegramBot.Models;

namespace telegramBot.ApiCalls
{
    public class ApiCall
    {
        public static async Task<Anime> GetAnimeByTitle(string title)
        {
            Client client = new Client();
            Anime a = new Anime();
            a = client.AnimeByTitle(title).Result;

            if (a == null)
            {
                return null;
            }
            return a;
        }

        public static async Task<String> SearchWiki(string language, string name)
        {
            Client client = new Client();
            string url = client.GetWikiUrl(name, language).Result;

            if (url == null)
            {
                return null;
            }
            return url;
        }

        public static async Task<List<AnimeList>> AnimeByGenre(int genre_id)
        {
            Client client = new Client();
            return client.AnimeByGenre(genre_id).Result;
        }

        public static async Task<AnimeList> RandomAnime()
        {
            Client client = new Client();
            return client.RandomAnime().Result;
        }

        public static async Task<Top> Trends(string choice)
        {
            Client client = new Client();
            return client.Trends(choice).Result;
        }

        public static async Task<bool> AddToUserList(string user_id, string main_title)
        {
            Client client = new Client();
            bool result = client.PostDataToDB(user_id, main_title).Result;
            if(result == false)
            {
                return false;
            }
            return true;
        }

        public static async Task<List<DataForDB>> GetMyList()
        {
            Client client = new Client();
            var result = await client.GetMyList();
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public static async Task<bool> DeleteData(string user_id, string main_title)
        {
            Client client = new Client();
            var result = await client.DeleteDataFromDB(user_id, main_title);
            return result;
        }

        public static async Task<Anime> GetRemainder(string user_id)
        {
            Client client = new Client();
            var result = await client.Remainder(user_id);
            return result;
        }
    }
}
